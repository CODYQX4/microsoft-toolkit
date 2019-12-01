using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using KMSEmulator.AES;
using KMSEmulator.Logging;

namespace KMSEmulator.KMS.V6
{
  class KMSV6Handler : IMessageHandler
  {
    private ILogger Logger { get; set; }
    private IKMSServer Server { get; set; }
    public KMSV6Handler(IKMSServer server, ILogger logger)
    {
      Logger = logger;
      Server = server;
    }

    public byte[] HandleRequest(byte[] kmsRequestData)
    {
      KMSV6Request kmsv6Request = CreateKMSV6Request(kmsRequestData);
      byte[] decrypted = DecryptV6(kmsv6Request);
      byte[] decryptedSalt = decrypted.Take(16).ToArray();
      byte[] decryptedRequest = decrypted.Skip(16).ToArray();
      byte[] responseBytes = Server.ExecuteKMSServerLogic(decryptedRequest, Logger);

      byte[] xorRequestSalt = new byte[16];
      for (int i = 0; i < 16; i++)
      {
        xorRequestSalt[i] = (byte)(decryptedSalt[i] ^ kmsv6Request.Salt[i]);
      }
      byte[] randomSalt = Guid.NewGuid().ToByteArray();
      byte[] randomSaltHash = GetSHA256Hash(randomSalt);
      for (int i = 0; i < 16; i++)
      {
        randomSalt[i] ^= xorRequestSalt[i];
      }

      // Get KMS Hardware ID
      byte[] hardwareID;
      string hexString = KMSEmulator.KMSServer.Settings.DefaultKMSHWID;
      if ((hexString.Length) % 2 != 0)
      {
        hardwareID = new byte[] { 0x36, 0x4F, 0x46, 0x3A, 0x88, 0x63, 0xD3, 0x5F };
      }
      else
      {
        byte[] hexAsBytes = new byte[hexString.Length / 2];
        for (int index = 0; index < hexAsBytes.Length; index++)
        {
          string byteValue = hexString.Substring(index * 2, 2);
          hexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        }
        hardwareID = hexAsBytes;
      }

      hardwareID = hardwareID.Reverse().ToArray();

      byte[] responseSalt = Guid.NewGuid().ToByteArray();
      byte[] decryptedResponseSalt = AesCrypt.DecryptAesCbc(responseSalt, AesCrypt.V6RoundKeys, responseSalt);

      byte[] responsedata = responseBytes.Concat(randomSalt).Concat(randomSaltHash).Concat(hardwareID).Concat(xorRequestSalt).ToArray();

      byte[] hmacKey = GetHmacKey(BitConverter.ToUInt64(decryptedRequest, 84));
      byte[] xorResponseSalt = new byte[16];
      for (int i = 0; i < 16; i++)
      {
        xorResponseSalt[i] = (byte)(responseSalt[i] ^ decryptedResponseSalt[i]);
      }
      byte[] encryptedResponseData = EncryptV6(responsedata.Concat(GetHmacSha256(hmacKey, xorResponseSalt.Concat(responsedata).ToArray()).Skip(16)).ToArray(), responseSalt);

      KMSV6Response kmsResponse = new KMSV6Response { Version = kmsv6Request.Version, Salt = responseSalt, Encrypted = encryptedResponseData };

      byte[] encryptedResponse = CreateKMSV6ResponseBytes(kmsResponse);
      return encryptedResponse;
    }

    private static byte[] CreateKMSV6ResponseBytes(KMSV6Response responsev6)
    {
      using (MemoryStream stream = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter(stream))
        {
          binaryWriter.Write(responsev6.BodyLength);
          binaryWriter.Write(responsev6.Unknown);
          binaryWriter.Write(responsev6.BodyLength2);
          binaryWriter.Write(responsev6.Version);
          binaryWriter.Write(responsev6.Salt);
          binaryWriter.Write(responsev6.Encrypted);
          binaryWriter.Write(responsev6.Padding);
          binaryWriter.Flush();
          stream.Position = 0;
          return stream.ToArray();
        }
      }
    }

    private static byte[] GetSHA256Hash(byte[] randomSalt)
    {
      SHA256Managed hasher = new SHA256Managed();
      return hasher.ComputeHash(randomSalt);
    }

    private static byte[] DecryptV6(KMSV6Request kmsRequest)
    {
      byte[] iv = kmsRequest.Salt;
      byte[] encrypted = kmsRequest.Salt.Concat(kmsRequest.EncryptedRequest).ToArray();

      return AesCrypt.DecryptAesCbc(encrypted, AesCrypt.V6RoundKeys, iv);
    }

    private static byte[] EncryptV6(byte[] data, byte[] iv)
    {
      return AesCrypt.EncryptAesCbc(data, AesCrypt.V6RoundKeys, iv);
    }

    private static KMSV6Request CreateKMSV6Request(byte[] kmsRequestData)
    {
      KMSV6Request kmsRequest = new KMSV6Request();
      using (MemoryStream stream = new MemoryStream(kmsRequestData))
      {
        using (BinaryReader binaryReader = new BinaryReader(stream))
        {
          kmsRequest.BodyLength1 = binaryReader.ReadUInt32();
          kmsRequest.BodyLength2 = binaryReader.ReadUInt32();
          kmsRequest.Version = binaryReader.ReadUInt32();
          kmsRequest.Salt = binaryReader.ReadBytes(16);
          kmsRequest.EncryptedRequest = binaryReader.ReadBytes(kmsRequestData.Length - 8 - 4 - 16);
        }
      }
      return kmsRequest;
    }


    private static byte[] GetHmacKey(ulong timeStamp)
    {
      ulong seed = (timeStamp / 0x00000022816889bd) * 0x000000208cbab5ed + 0x3156cd5ac628477a;

      SHA256Managed hasher = new SHA256Managed();
      return hasher.ComputeHash(BitConverter.GetBytes(seed)).Skip(16).ToArray();
    }

    private static byte[] GetHmacSha256(byte[] key, byte[] data)
    {
      HMACSHA256 hmac = new HMACSHA256(key);
      return hmac.ComputeHash(data);
    }
  }
}
