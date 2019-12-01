namespace KMSEmulator.KMS.V5
{
    class KMSV5Request
    {
        public uint BodyLength1 { get; set; }
        public uint BodyLength2 { get; set; }
        public uint Version { get; set; }
        public byte[] Salt { get; set; }
        public byte[] EncryptedRequest { get; set; }
        public byte[] Pad { get; set; }
    }
}
