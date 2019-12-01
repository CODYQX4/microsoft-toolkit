namespace KMSEmulator.KMS.V6
{
    class KMSV6Request
    {
        public uint BodyLength1 { get; set; }
        public uint BodyLength2 { get; set; }
        public uint Version { get; set; }
        public byte[] Salt { get; set; }
        public byte[] EncryptedRequest { get; set; }
    }
}
