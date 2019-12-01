using System;
using KMSEmulator.KMS.V4;
using KMSEmulator.KMS.V5;
using KMSEmulator.KMS.V6;
using KMSEmulator.Logging;

namespace KMSEmulator.KMS
{
    public class KMSRequestHandler : IMessageHandler
    {
        private readonly IKMSServerSettings _serverSettings;
        private readonly ILogger _logger;
        public KMSRequestHandler(IKMSServerSettings settings, ILogger logger)
        {
            _serverSettings = settings;
            _logger = logger;
        }

        public byte[] HandleRequest(byte[] request)
        {
            const int unknownDataSize = 8;
            byte version1 = request[unknownDataSize + 2];
            byte version2 = request[unknownDataSize + 0];

            IMessageHandler messagehandler;

            try
            {
                messagehandler = GetKMSMessageHandlerForVersion(version1, version2);
            }
            catch (Exception)
            {
                return new byte[0];
            }
            
            return messagehandler.HandleRequest(request);
        }

        private IMessageHandler GetKMSMessageHandlerForVersion(byte version1, byte version2)
        {
            IMessageHandler messagehandler;

            if (version1 == 4 && version2 == 0)
            {
                _logger.LogMessage("KMS Activation Request (KMS V4.0) Received:");
                messagehandler = new KMSV4Handler(new KMSServer(_serverSettings), _logger);
            }
            else if (version1 == 5 && version2 == 0)
            {
                _logger.LogMessage("KMS Activation Request (KMS V5.0) Received:");
                messagehandler = new KMSV5Handler(new KMSServer(_serverSettings), _logger);
            }
            else if (version1 == 6 && version2 == 0)
            {
                _logger.LogMessage("KMS Activation Request (KMS V6.0) Received:");
                messagehandler = new KMSV6Handler(new KMSServer(_serverSettings), _logger);
            }
            else
            {
                throw new ApplicationException("KMS Activation Request (KMS V" + version1 + "." + version2 + ") Unsupported.");
            }
            return messagehandler;
        }
    }
}
