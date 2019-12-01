namespace KMSEmulator.KMS
{
    public interface IKMSServerSettings
    {
        uint CurrentClientCount { get; set; }
        string DefaultKMSPID { get; set; }
        string DefaultKMSHWID { get; set; }
        bool GenerateRandomKMSPID { get; set; }
        int Port { get; set; }
        uint VLActivationInterval { get; set; }
        uint VLRenewalInterval { get; set; }
        bool KillProcessOnPort { get; set; }
        
    }
}
