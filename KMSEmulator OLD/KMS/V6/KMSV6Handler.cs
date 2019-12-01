using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
            byte[] decryptedResponseSalt = DecryptAESV6(responseSalt, responseSalt);

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

            return DecryptAESV6(encrypted, iv);
        }

        private static byte[] EncryptV6(byte[] data, byte[] iv)
        {
            return EncryptAESV6(data, iv);
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

        // Rijndael S-Box
        private static readonly byte[] SBox = {
	        0x63, 0x7C, 0x77, 0x7B, 0xF2, 0x6B, 0x6F, 0xC5, 0x30, 0x01, 0x67, 0x2B, 0xFE, 0xD7, 0xAB, 0x76,
        	0xCA, 0x82, 0xC9, 0x7D, 0xFA, 0x59, 0x47, 0xF0, 0xAD, 0xD4, 0xA2, 0xAF, 0x9C, 0xA4, 0x72, 0xC0,
        	0xB7, 0xFD, 0x93, 0x26, 0x36, 0x3F, 0xF7, 0xCC, 0x34, 0xA5, 0xE5, 0xF1, 0x71, 0xD8, 0x31, 0x15,
        	0x04, 0xC7, 0x23, 0xC3, 0x18, 0x96, 0x05, 0x9A, 0x07, 0x12, 0x80, 0xE2, 0xEB, 0x27, 0xB2, 0x75,
        	0x09, 0x83, 0x2C, 0x1A, 0x1B, 0x6E, 0x5A, 0xA0, 0x52, 0x3B, 0xD6, 0xB3, 0x29, 0xE3, 0x2F, 0x84,
        	0x53, 0xD1, 0x00, 0xED, 0x20, 0xFC, 0xB1, 0x5B, 0x6A, 0xCB, 0xBE, 0x39, 0x4A, 0x4C, 0x58, 0xCF,
        	0xD0, 0xEF, 0xAA, 0xFB, 0x43, 0x4D, 0x33, 0x85, 0x45, 0xF9, 0x02, 0x7F, 0x50, 0x3C, 0x9F, 0xA8,
        	0x51, 0xA3, 0x40, 0x8F, 0x92, 0x9D, 0x38, 0xF5, 0xBC, 0xB6, 0xDA, 0x21, 0x10, 0xFF, 0xF3, 0xD2,
        	0xCD, 0x0C, 0x13, 0xEC, 0x5F, 0x97, 0x44, 0x17, 0xC4, 0xA7, 0x7E, 0x3D, 0x64, 0x5D, 0x19, 0x73,
        	0x60, 0x81, 0x4F, 0xDC, 0x22, 0x2A, 0x90, 0x88, 0x46, 0xEE, 0xB8, 0x14, 0xDE, 0x5E, 0x0B, 0xDB,
        	0xE0, 0x32, 0x3A, 0x0A, 0x49, 0x06, 0x24, 0x5C, 0xC2, 0xD3, 0xAC, 0x62, 0x91, 0x95, 0xE4, 0x79,
        	0xE7, 0xC8, 0x37, 0x6D, 0x8D, 0xD5, 0x4E, 0xA9, 0x6C, 0x56, 0xF4, 0xEA, 0x65, 0x7A, 0xAE, 0x08,
        	0xBA, 0x78, 0x25, 0x2E, 0x1C, 0xA6, 0xB4, 0xC6, 0xE8, 0xDD, 0x74, 0x1F, 0x4B, 0xBD, 0x8B, 0x8A,
        	0x70, 0x3E, 0xB5, 0x66, 0x48, 0x03, 0xF6, 0x0E, 0x61, 0x35, 0x57, 0xB9, 0x86, 0xC1, 0x1D, 0x9E,
        	0xE1, 0xF8, 0x98, 0x11, 0x69, 0xD9, 0x8E, 0x94, 0x9B, 0x1E, 0x87, 0xE9, 0xCE, 0x55, 0x28, 0xDF,
        	0x8C, 0xA1, 0x89, 0x0D, 0xBF, 0xE6, 0x42, 0x68, 0x41, 0x99, 0x2D, 0x0F, 0xB0, 0x54, 0xBB, 0x16 };

        // Rijndael S-Box inverse
        private static readonly byte[] InvSBox = {
        	0x52, 0x09, 0x6A, 0xD5, 0x30, 0x36, 0xA5, 0x38, 0xBF, 0x40, 0xA3, 0x9E, 0x81, 0xF3, 0xD7, 0xFB,
            0x7C, 0xE3, 0x39, 0x82, 0x9B, 0x2F, 0xFF, 0x87, 0x34, 0x8E, 0x43, 0x44, 0xC4, 0xDE, 0xE9, 0xCB,
        	0x54, 0x7B, 0x94, 0x32, 0xA6, 0xC2, 0x23, 0x3D, 0xEE, 0x4C, 0x95, 0x0B, 0x42, 0xFA, 0xC3, 0x4E,
    	    0x08, 0x2E, 0xA1, 0x66, 0x28, 0xD9, 0x24, 0xB2, 0x76, 0x5B, 0xA2, 0x49, 0x6D, 0x8B, 0xD1, 0x25,
    	    0x72, 0xF8, 0xF6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xD4, 0xA4, 0x5C, 0xCC, 0x5D, 0x65, 0xB6, 0x92,
    	    0x6C, 0x70, 0x48, 0x50, 0xFD, 0xED, 0xB9, 0xDA, 0x5E, 0x15, 0x46, 0x57, 0xA7, 0x8D, 0x9D, 0x84,
    	    0x90, 0xD8, 0xAB, 0x00, 0x8C, 0xBC, 0xD3, 0x0A, 0xF7, 0xE4, 0x58, 0x05, 0xB8, 0xB3, 0x45, 0x06,
    	    0xD0, 0x2C, 0x1E, 0x8F, 0xCA, 0x3F, 0x0F, 0x02, 0xC1, 0xAF, 0xBD, 0x03, 0x01, 0x13, 0x8A, 0x6B,
    	    0x3A, 0x91, 0x11, 0x41, 0x4F, 0x67, 0xDC, 0xEA, 0x97, 0xF2, 0xCF, 0xCE, 0xF0, 0xB4, 0xE6, 0x73,
    	    0x96, 0xAC, 0x74, 0x22, 0xE7, 0xAD, 0x35, 0x85, 0xE2, 0xF9, 0x37, 0xE8, 0x1C, 0x75, 0xDF, 0x6E,
    	    0x47, 0xF1, 0x1A, 0x71, 0x1D, 0x29, 0xC5, 0x89, 0x6F, 0xB7, 0x62, 0x0E, 0xAA, 0x18, 0xBE, 0x1B,
    	    0xFC, 0x56, 0x3E, 0x4B, 0xC6, 0xD2, 0x79, 0x20, 0x9A, 0xDB, 0xC0, 0xFE, 0x78, 0xCD, 0x5A, 0xF4,
    	    0x1F, 0xDD, 0xA8, 0x33, 0x88, 0x07, 0xC7, 0x31, 0xB1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xEC, 0x5F,
    	    0x60, 0x51, 0x7F, 0xA9, 0x19, 0xB5, 0x4A, 0x0D, 0x2D, 0xE5, 0x7A, 0x9F, 0x93, 0xC9, 0x9C, 0xEF,
    	    0xA0, 0xE0, 0x3B, 0x4D, 0xAE, 0x2A, 0xF5, 0xB0, 0xC8, 0xEB, 0xBB, 0x3C, 0x83, 0x53, 0x99, 0x61,
    	    0x17, 0x2B, 0x04, 0x7E, 0xBA, 0x77, 0xD6, 0x26, 0xE1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0C, 0x7D };

        // Rijndael precomputed Key Schedule with V6 mod
        private static readonly byte[,] V6RoundKeys = {
        	{ 0xA9, 0x4A, 0x41, 0x95, 0xE2, 0x01, 0x43, 0x2D, 0x9B, 0xCB, 0x46, 0x04, 0x05, 0xD8, 0x4A, 0x21 }, 
        	{ 0xC9, 0x9C, 0xBC, 0xFE, 0x2B, 0x9D, 0xFF, 0xD3, 0xB0, 0x56, 0xB9, 0xD7, 0xB5, 0x8E, 0xF3, 0xF6 }, 
        	{ 0xD2, 0x91, 0xFE, 0x2B, 0xF9, 0x0C, 0x01, 0xF8, 0x49, 0x5A, 0xB8, 0x2F, 0xFC, 0xD4, 0x4B, 0xD9 }, 
        	{ 0x9E, 0x22, 0xCB, 0x9B, 0x67, 0x2E, 0xCA, 0x63, 0x2E, 0x74, 0x72, 0x4C, 0xD2, 0xA0, 0x39, 0x95 }, 
        	{ 0x05, 0x30, 0xE1, 0x2E, 0x11, 0x1E, 0x2B, 0x4D, 0x3F, 0x6A, 0x59, 0x01, 0xED, 0xCA, 0x60, 0x94 }, 
        	{ 0x12, 0xE0, 0xC3, 0x7B, 0x03, 0xFE, 0xE8, 0x36, 0x3C, 0x94, 0xB1, 0x37, 0xD1, 0x5E, 0xD1, 0xA3 }, 
        	{ 0x63, 0xDE, 0xC9, 0x45, 0x69, 0x20, 0x21, 0x73, 0x55, 0xB4, 0x90, 0x44, 0x84, 0xEA, 0x41, 0xE7 }, 
        	{ 0xAD, 0x5D, 0x5D, 0x1A, 0xC4, 0x7D, 0x7C, 0x69, 0x91, 0xC9, 0xEC, 0x2D, 0x15, 0x23, 0xAD, 0xCA }, 
        	{ 0xEF, 0xC8, 0x29, 0x43, 0xCF, 0xB5, 0x55, 0x2A, 0x5E, 0x7C, 0xB9, 0x07, 0x4B, 0x5F, 0x14, 0xCD }, 
        	{ 0xDF, 0x32, 0x94, 0xF0, 0x10, 0x87, 0xC1, 0xDA, 0x4E, 0xFB, 0x78, 0xDD, 0x05, 0xA4, 0x6C, 0x10 }, 
        	{ 0xA0, 0x62, 0x5E, 0x9B, 0xB0, 0xE5, 0x9F, 0x41, 0xFE, 0x1E, 0xE7, 0x9C, 0xFB, 0xBA, 0x8B, 0x8C } };

        // Galois finite fields multiplications
        private static byte MULx2(byte v)
        {
            return (byte)(v < 0x80 ? v << 1 : ((v << 1) ^ 0x1b));
        }

        private static byte MULx3(byte v)
        {
            return (byte)(MULx2(v) ^ v);
        }

        private static byte MULx4(byte v)
        {
            return MULx2(MULx2(v));
        }

        private static byte MULx8(byte v)
        {
            return MULx2(MULx2(MULx2(v)));
        }

        private static byte MULx9(byte v)
        {
            return (byte)(MULx8(v) ^ v);
        }

        // ReSharper disable once InconsistentNaming
        private static byte MULxB(byte v)
        {
            return (byte)(MULx8(v) ^ MULx2(v) ^ v);
        }

        // ReSharper disable once InconsistentNaming
        private static byte MULxD(byte v)
        {
            return (byte)(MULx8(v) ^ MULx4(v) ^ v);
        }

        // ReSharper disable once InconsistentNaming
        private static byte MULxE(byte v)
        {
            return (byte)(MULx8(v) ^ MULx4(v) ^ MULx2(v));
        }

        // SubBytes and ShiftRows combined
        private static void SubBytesAndShiftRows(byte[] state)
        {
            byte[] temp = new byte[16];

            for (int i = 0; i < 16; i++)
                temp[i] = state[(i + ((i & 0x3) << 2)) & 0xf];

            for (int i = 0; i < 16; i++)
                state[i] = SBox[temp[i]];
        }

        // InvSubBytes and InvShiftRows combined
        private static void InvSubBytesAndShiftRows(byte[] state)
        {
            byte[] temp = new byte[16];

            for (int i = 0; i < 16; i++)
                temp[i] = state[(i - ((i & 0x3) << 2)) & 0xf];

            for (int i = 0; i < 16; i++)
                state[i] = InvSBox[temp[i]];
        }

        // MixColumns
        private static void MixColumns(byte[] state)
        {
            byte[] temp = new byte[16];

            for (int i = 0; i < 16; i++)
                temp[i] = state[i];

            for (int i = 0; i < 16; i += 4)
            {
                state[i] = (byte)(MULx2(temp[i]) ^ MULx3(temp[i + 1]) ^ temp[i + 2] ^ temp[i + 3]);
                state[i + 1] = (byte)(temp[i] ^ MULx2(temp[i + 1]) ^ MULx3(temp[i + 2]) ^ temp[i + 3]);
                state[i + 2] = (byte)(temp[i] ^ temp[i + 1] ^ MULx2(temp[i + 2]) ^ MULx3(temp[i + 3]));
                state[i + 3] = (byte)(MULx3(temp[i]) ^ temp[i + 1] ^ temp[i + 2] ^ MULx2(temp[i + 3]));
            }
        }

        // MixColumns
        private static void InvMixColumns(byte[] state)
        {
            byte[] temp = new byte[16];

            for (int i = 0; i < 16; i++)
                temp[i] = state[i];

            for (int i = 0; i < 16; i += 4)
            {
                state[i] = (byte)(MULxE(temp[i]) ^ MULxB(temp[i + 1]) ^ MULxD(temp[i + 2]) ^ MULx9(temp[i + 3]));
                state[i + 1] = (byte)(MULx9(temp[i]) ^ MULxE(temp[i + 1]) ^ MULxB(temp[i + 2]) ^ MULxD(temp[i + 3]));
                state[i + 2] = (byte)(MULxD(temp[i]) ^ MULx9(temp[i + 1]) ^ MULxE(temp[i + 2]) ^ MULxB(temp[i + 3]));
                state[i + 3] = (byte)(MULxB(temp[i]) ^ MULxD(temp[i + 1]) ^ MULx9(temp[i + 2]) ^ MULxE(temp[i + 3]));
            }
        }

        // RoundKey
        private static void AddRoundKey(byte[] state, int round)
        {
            for (int i = 0; i < 16; i++)
                state[i] ^= V6RoundKeys[round, i];
        }

        private static byte[] AesEncryptBlock(byte[] message, int offset)
        {
            byte[] state = new byte[16];

            for (int i = 0; i < 16; i++)
                state[i] = message[offset + i];

            AddRoundKey(state, 0);

            for (int round = 1; round < 10; round++)
            {
                SubBytesAndShiftRows(state);
                MixColumns(state);
                AddRoundKey(state, round);
            }

            SubBytesAndShiftRows(state);
            AddRoundKey(state, 10);

            return state;
        }

        private static byte[] AesDecryptBlock(byte[] message, int offset)
        {
            byte[] state = new byte[16];

            for (int i = 0; i < 16; i++)
                state[i] = message[offset + i];

            AddRoundKey(state, 10);
            InvSubBytesAndShiftRows(state);

            for (int round = 9; round > 0; round--)
            {
                AddRoundKey(state, round);
                InvMixColumns(state);
                InvSubBytesAndShiftRows(state);
            }

            AddRoundKey(state, 0);

            return state;
        }

        public static byte[] EncryptAESV6(byte[] message, byte[] iv)
        {
            byte padding = (byte)(16 - (message.Length % 16));
            byte[] plainText = message.Concat(Enumerable.Repeat(padding, padding)).ToArray();

            if (iv != null)
            {
                for (int i = 0; i < 16; i++)
                    plainText[i] ^= iv[i];
            }

            byte[] cipherText = AesEncryptBlock(plainText, 0);

            for (int offset = 16; offset < plainText.Length; offset += 16)
            {
                for (int i = 0; i < 16; i++)
                    plainText[offset + i] ^= cipherText[offset - 16 + i];

                cipherText = cipherText.Concat(AesEncryptBlock(plainText, offset)).ToArray();
            }

            return cipherText;
        }

        public static byte[] DecryptAESV6(byte[] message, byte[] iv)
        {
            int cipherTextLen = message.Length;

            byte[] plainText = AesDecryptBlock(message, 0);

            if (iv != null)
            {
                for (int i = 0; i < 16; i++)
                    plainText[i] ^= iv[i];
            }

            for (int offset = 16; offset < cipherTextLen; offset += 16)
            {
                plainText = plainText.Concat(AesDecryptBlock(message, offset)).ToArray();
                for (int i = 0; i < 16; i++)
                    plainText[offset + i] ^= message[offset - 16 + i];
            }

            if (plainText.Length == 16)
                return plainText;

            byte padding = plainText[plainText.Length - 1];

            plainText = plainText.Take(plainText.Length - padding).ToArray();

            return plainText;
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
