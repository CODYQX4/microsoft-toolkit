namespace KMSEmulator.KMS.V4
{
    class KMSV4Response : KMSResponseBase
    {
        public byte[] Response { get; set; }
        public byte[] Hash { get; set; }

        public override uint BodyLength
        {
            get { 
                return (uint)(Response.Length + Hash.Length); 
            }
        }
    }
}
