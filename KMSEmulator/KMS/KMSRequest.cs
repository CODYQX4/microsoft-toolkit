using System;

namespace KMSEmulator.KMS
{
    class KMSRequest
    {
        // KMS Protocol Version
        public uint Version;

        // Whether or not the KMS Client is a Virtual Machine
        public uint IsClientVM;

        // Microsoft Licensing Status Code
        public uint LicenseStatus;

        // The time in Minutes of the KMS Client's Licensing Grace Period
        public uint GraceTime;

        // Microsoft Product Application ID
        public Guid ApplicationId;

        // Microsoft Product SKU ID
        public Guid SkuId;

        // Microsoft Identifier for KMS Client Request Counting and Activation
        public Guid KmsCountedId;

        // Unique Identifier to Distinguish KMS Clients
        public Guid ClientMachineId;

        // Number of KMS Clients that must have requested KMS Server Activation
        public uint RequiredClientCount;

        // Time of KMS Client Request
        public ulong RequestTime;

        // Previous Client Machine ID
        public byte[] PreviousClientMachineId; //[16]

        // KMS Client Network DNS Name
        public byte[] MachineName; //[64]

        public uint MajorVersion
        {
            get
            {
                return Version >> 16;
            }
        }

        public uint MinorVersion
        {
            get
            {
                return Version & 0x0000FFFF;
            }
        }
    }
}
