using System;
using System.IO;
using KMSEmulator.AES;
using KMSEmulator.Logging;

namespace KMSEmulator.KMS.V4
{
	class KMSV4Handler : IMessageHandler
	{
		private ILogger Logger { get; set; }
		private IKMSServer Server { get; set; }
		public KMSV4Handler(IKMSServer server, ILogger logger)
		{
			Logger = logger;
			Server = server;
		}

		public byte[] HandleRequest(byte[] request)
		{
			KMSV4Request v4Request = CreateKMSV4Request(request);

			byte[] response = Server.ExecuteKMSServerLogic(v4Request.Request, Logger);

			byte[] hash = GetHashV4(response);

			KMSV4Response kmsv4Response = new KMSV4Response { Response = response, Hash = hash };

			byte[] responseBytes = CreateKMSV4ResponseBytes(kmsv4Response);
			return responseBytes;
		}

		private static KMSV4Request CreateKMSV4Request(byte[] kmsRequestData)
		{
			KMSV4Request kmsRequest = new KMSV4Request();
			using (MemoryStream stream = new MemoryStream(kmsRequestData))
			{
				using (BinaryReader binaryReader = new BinaryReader(stream))
				{
					kmsRequest.BodyLength1 = binaryReader.ReadUInt32();
					kmsRequest.BodyLength2 = binaryReader.ReadUInt32();
					kmsRequest.Request = binaryReader.ReadBytes(kmsRequestData.Length - 8 - 16);
					kmsRequest.Hash = binaryReader.ReadBytes(16);
				}
			}
			return kmsRequest;
		}

		private static byte[] CreateKMSV4ResponseBytes(KMSV4Response responsev4)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(stream))
				{
					binaryWriter.Write(responsev4.BodyLength);
					binaryWriter.Write(responsev4.Unknown);
					binaryWriter.Write(responsev4.BodyLength2);
					binaryWriter.Write(responsev4.Response);
					binaryWriter.Write(responsev4.Hash);
					binaryWriter.Write(responsev4.Padding);
					binaryWriter.Flush();
					stream.Position = 0;
					return stream.ToArray();
				}
			}
		}

		// Get Hash
		byte[] GetHashV4(byte[] message)
		{
			uint messageSize = (uint)message.Length;
			byte[] lastBlock = new byte[16];
			byte[] hash = new byte[16];

			// MessageSize / Blocksize
			uint j = messageSize >> 4;

			// Remaining bytes
			uint k = messageSize & 0xf;

			// Hash
			for (uint i = 0; i < j; i++)
			{
				XorBuffer(message, i << 4, hash);
				Hash(hash);
			}

			// Bit Padding
			Array.Copy(message, j << 4, lastBlock, 0, k);
			lastBlock[k] = 0x80;

			XorBuffer(lastBlock, 0, hash);
			Hash(hash);

			return hash;
		}

		void Hash(byte[] hash) => Array.Copy(AesCrypt.AesEncryptBlock(AesCrypt.V4RoundKeys, hash, 0), hash, 16);

		// Xor Buffer
		void XorBuffer(byte[] source, uint offset, byte[] destination)
		{
			for (uint i = 0; i < 16; i++)
			{
				destination[i] ^= source[i + offset];
			}
		}
	}
}
