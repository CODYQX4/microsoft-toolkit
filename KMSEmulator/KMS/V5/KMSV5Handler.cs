using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using KMSEmulator.Logging;

namespace KMSEmulator.KMS.V5
{
	class KMSV5Handler : IMessageHandler
	{
		private ILogger Logger { get; set; }
		private IKMSServer Server { get; set; }
		public KMSV5Handler(IKMSServer server, ILogger logger)
		{
			Logger = logger;
			Server = server;
		}

		public byte[] HandleRequest(byte[] kmsRequestData)
		{
			KMSV5Request kmsv5Request = CreateKMSV5Request(kmsRequestData);
			byte[] decrypted = DecryptV5(kmsv5Request);
			byte[] decryptedSalt = decrypted.Take(16).ToArray();
			byte[] decryptedRequest = decrypted.Skip(16).ToArray();
			byte[] responseBytes = Server.ExecuteKMSServerLogic(decryptedRequest, Logger);

			byte[] randomSalt = Guid.NewGuid().ToByteArray();
			byte[] randomSaltHash = GetSHA265Hash(randomSalt);
			byte[] randomStuff = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				randomStuff[i] = (byte)(decryptedSalt[i] ^ kmsv5Request.Salt[i] ^ randomSalt[i]);
			}
			byte[] responsedata = responseBytes.Concat(randomStuff).Concat(randomSaltHash).ToArray();
			byte[] encryptedResponseData = EncryptV5(responsedata, kmsv5Request.Salt);

			KMSV5Response kmsResponse = new KMSV5Response { Version = kmsv5Request.Version, Salt = kmsv5Request.Salt, Encrypted = encryptedResponseData };

			byte[] encryptedResponse = CreateKMSV5ResponseBytes(kmsResponse);
			return encryptedResponse;
		}

		private static byte[] CreateKMSV5ResponseBytes(KMSV5Response responsev5)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(stream))
				{
					binaryWriter.Write(responsev5.BodyLength);
					binaryWriter.Write(responsev5.Unknown);
					binaryWriter.Write(responsev5.BodyLength2);
					binaryWriter.Write(responsev5.Version);
					binaryWriter.Write(responsev5.Salt);
					binaryWriter.Write(responsev5.Encrypted);
					binaryWriter.Write(responsev5.Padding);
					binaryWriter.Flush();
					stream.Position = 0;
					return stream.ToArray();
				}
			}
		}

		private static byte[] GetSHA265Hash(byte[] randomSalt)
		{
			SHA256Managed hasher = new SHA256Managed();
			byte[] hash = hasher.ComputeHash(randomSalt);
			return hash;
		}

		private static readonly byte[] Key = { 0xCD, 0x7E, 0x79, 0x6F, 0x2A, 0xB2, 0x5D, 0xCB, 0x55, 0xFF, 0xC8, 0xEF, 0x83, 0x64, 0xC4, 0x70 };

		private static byte[] DecryptV5(KMSV5Request kmsRequest)
		{
			byte[] iv = kmsRequest.Salt;
			byte[] encrypted =
				kmsRequest.Salt.Concat(kmsRequest.EncryptedRequest)
				.Concat(kmsRequest.Pad).ToArray();

			RijndaelManaged rijndaelManaged = new RijndaelManaged { Key = Key, IV = iv };

			byte[] decrypted;
			using (MemoryStream ms = new MemoryStream())
			{
				using (CryptoStream cs = new CryptoStream(ms, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Write))
				{
					cs.Write(encrypted, 0, encrypted.Length);
				}
				decrypted = ms.ToArray();
			}
			return decrypted;
		}

		private static byte[] EncryptV5(byte[] data, byte[] salt)
		{
			RijndaelManaged rijndaelManaged = new RijndaelManaged { Key = Key, IV = salt };

			byte[] encrypted;
			using (MemoryStream ms = new MemoryStream())
			{
				using (CryptoStream cs = new CryptoStream(ms, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
				{
					cs.Write(data, 0, data.Length);
				}
				encrypted = ms.ToArray();
			}
			return encrypted;
		}

		private static KMSV5Request CreateKMSV5Request(byte[] kmsRequestData)
		{
			KMSV5Request kmsRequest = new KMSV5Request();
			using (MemoryStream stream = new MemoryStream(kmsRequestData))
			{
				using (BinaryReader binaryReader = new BinaryReader(stream))
				{
					kmsRequest.BodyLength1 = binaryReader.ReadUInt32();
					kmsRequest.BodyLength2 = binaryReader.ReadUInt32();
					kmsRequest.Version = binaryReader.ReadUInt32();
					kmsRequest.Salt = binaryReader.ReadBytes(16);
					kmsRequest.EncryptedRequest = binaryReader.ReadBytes(kmsRequestData.Length - 8 - 4 - 16 - 4);
					kmsRequest.Pad = binaryReader.ReadBytes(4);
				}
			}
			return kmsRequest;
		}
	}
}
