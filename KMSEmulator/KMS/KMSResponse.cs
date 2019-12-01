using System;

namespace KMSEmulator.KMS
{
    class KMSResponse
    {
        public uint Version { get; set; }
        public uint KMSPIDLength
        {
            get
            {
                return (uint)(KMSPID.Length + 1) * 2;
            }
        }
        public string KMSPID { get; set; }
        public Guid ClientMachineId { get; set; }
        public ulong RequestTime { get; set; }
        public uint CurrentClientCount { get; set; }
        public uint VLActivationInterval { get; set; }
        public uint VLRenewalInterval { get; set; }
    }
}
