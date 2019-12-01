using KMSEmulator.Logging;

namespace KMSEmulator.KMS
{
    interface IKMSServer
    {
        byte[] ExecuteKMSServerLogic(byte[] kmsRequestBytes, ILogger logger);
    }
}
