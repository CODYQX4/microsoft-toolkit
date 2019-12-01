namespace KMSEmulator.KMS.V6
{
    class KMSV6Response : KMSResponseBase
    {
        public uint Version { get; set; }
        public byte[] Salt { get; set; }
        public byte[] Encrypted { get; set; }

        public override uint BodyLength
        {
            get 
            {
                return (uint)(4 + Salt.Length + Encrypted.Length);
            }
        }
    }
}
