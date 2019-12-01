using System;
using System.IO;
using KMSEmulator.Logging;

namespace KMSEmulator.KMS
{
    class KMSServer : IKMSServer
    {
        private readonly IKMSServerSettings _serverSettings;
        public KMSServer(IKMSServerSettings settings)
        {
            _serverSettings = settings;
        }

        public byte[] ExecuteKMSServerLogic(byte[] kmsRequestBytes, ILogger logger)
        {
            KMSRequest kmsRequest = CreateKmsRequest(kmsRequestBytes);
   
            KMSResponse response = CreateKMSResponse(kmsRequest, _serverSettings, logger);
            byte[] responseBytes = CreateKMSResponseBytes(response);
            return responseBytes;
        }

        private static byte[] CreateKMSResponseBytes(KMSResponse response)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                {
                    binaryWriter.Write(response.Version);
                    byte[] kmsPidArray = System.Text.Encoding.Unicode.GetBytes(response.KMSPID + "\0");
                    binaryWriter.Write(kmsPidArray.Length);
                    binaryWriter.Write(kmsPidArray);
                    binaryWriter.Write(response.ClientMachineId.ToByteArray());
                    binaryWriter.Write(response.RequestTime);
                    binaryWriter.Write(response.CurrentClientCount);
                    binaryWriter.Write(response.VLActivationInterval);
                    binaryWriter.Write(response.VLRenewalInterval);
                    binaryWriter.Flush();
                    stream.Position = 0;
                    return stream.ToArray();
                }
            }
        }

        private KMSResponse CreateKMSResponse(KMSRequest kmsRequest, IKMSServerSettings serverSettings, ILogger logger)
        {
            KMSResponse response = new KMSResponse {Version = kmsRequest.Version};
            string kmsPID;
            if (serverSettings.GenerateRandomKMSPID)
            {
                KMSPIDGenerator generator = new KMSPIDGenerator();
                kmsPID = generator.CreateKMSPID(kmsRequest);
                logger.LogMessage("KMS PID: " + kmsPID);
                logger.LogMessage("Application ID: " + kmsRequest.ApplicationId);
                logger.LogMessage("Client Machine ID: " + kmsRequest.ClientMachineId);
                logger.LogMessage("KMS Counted ID: " + kmsRequest.KmsCountedId);
                logger.LogMessage("SKUID ID: " + kmsRequest.SkuId);      
                logger.LogMessage("KMS Activation Response (KMS V" + kmsRequest.MajorVersion  + "." + kmsRequest.MinorVersion + ") sent." + Environment.NewLine);
            }
            else
            {
                kmsPID = serverSettings.DefaultKMSPID;
            }
            response.KMSPID = kmsPID;
            response.ClientMachineId = kmsRequest.ClientMachineId;
            response.RequestTime = kmsRequest.RequestTime;
            response.CurrentClientCount = serverSettings.CurrentClientCount;
            response.VLActivationInterval = serverSettings.VLActivationInterval;
            response.VLRenewalInterval = serverSettings.VLRenewalInterval;
            return response;
        }

        private static KMSRequest CreateKmsRequest(byte[] decrypted)
        {
            using (MemoryStream stream = new MemoryStream(decrypted))
            {
                using (BinaryReader binaryReader = new BinaryReader(stream))
                {
                    KMSRequest kmsRequest = new KMSRequest {Version = binaryReader.ReadUInt32(), IsClientVM = binaryReader.ReadUInt32(), LicenseStatus = binaryReader.ReadUInt32(), GraceTime = binaryReader.ReadUInt32(), ApplicationId = new Guid(binaryReader.ReadBytes(16)), SkuId = new Guid(binaryReader.ReadBytes(16)), KmsCountedId = new Guid(binaryReader.ReadBytes(16)), ClientMachineId = new Guid(binaryReader.ReadBytes(16)), RequiredClientCount = binaryReader.ReadUInt32(), RequestTime = binaryReader.ReadUInt64(), PreviousClientMachineId = binaryReader.ReadBytes(16), MachineName = binaryReader.ReadBytes(64)};
                    return kmsRequest;
                }
            }
        }
    }
}
