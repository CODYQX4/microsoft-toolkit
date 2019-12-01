namespace KMSEmulator.KMS.V4
{
    class KMSV4Request
    {
        public uint BodyLength1 { get; set; }
        public uint BodyLength2 { get; set; }

        public byte[] Request { get; set; }
        public byte[] Hash { get; set; }
    }
}
