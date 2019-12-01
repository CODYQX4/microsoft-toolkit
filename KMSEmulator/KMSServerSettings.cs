using System;
using System.Text.RegularExpressions;
using KMSEmulator.KMS;

namespace KMSEmulator
{
    public class KMSServerSettings : IKMSServerSettings
    {
        /// <summary>
        /// Initialize Default Settings
        /// </summary>
        public KMSServerSettings()
        {
            CurrentClientCount = 25;
            DefaultKMSPID = "55041-00168-305-246209-03-1033-7600.0000-0522010";
            DefaultKMSHWID = "364F463A8863D35F";
            GenerateRandomKMSPID = true;
            Port = 1688;
            VLActivationInterval = 120;
            VLRenewalInterval = 43200;
            KillProcessOnPort = false;
        }

        /// <summary>
        /// KMS Current Client Count Reported by KMS Server
        /// </summary>
        private uint _mCurrentClientCount;
        public uint CurrentClientCount
        {
            get
            {
                return _mCurrentClientCount;
            }
            set
            {
                // Validate KMS Current Client Count Range
                if (value > 1000)
                {
                    throw new ArgumentOutOfRangeException("value", "KMS Current Client Count must be between 0 and 1000.");
                }
                _mCurrentClientCount = value;
            }
        }

        /// <summary>
        /// KMS PID Reported by KMS Server unless Randomized
        /// </summary>
        private string _mDefaultKMSPID;
        public string DefaultKMSPID
        {
            get
            {
                return _mDefaultKMSPID;
            }
            set
            {
                // Validate KMS PID Format
                if (!Regex.IsMatch(value, "^([0-9]{5})-([0-9]{5})-([0-9]{3})-([0-9]{6})-([0-9]{2})-([0-9]{4,5})-([0-9]{4,5}).([0-9]{4})-([0-9]{7})$", RegexOptions.Multiline))
                {
                    throw new ArgumentException("Provided value is not a valid KMS PID.", "value");
                }
                _mDefaultKMSPID = value;
            }
        }

        /// <summary>
        /// KMS HWID Reported by KMS Server
        /// </summary>
        private string _mDefaultKMSHWID;
        public string DefaultKMSHWID
        {
            get
            {
                return _mDefaultKMSHWID;
            }
            set
            {
                // Validate KMS HWID Format
                if (!Regex.IsMatch(value, "[a-fA-F0-9]{16}", RegexOptions.Multiline))
                {
                    throw new ArgumentException("Provided value is not a valid KMS HWID.", "value");
                }
                _mDefaultKMSHWID = value;
            }
        }
     
        /// <summary>
        /// Generate Randomized KMS PID based on KMS Client instead of sending the Default KMS PID
        /// </summary>
        public bool GenerateRandomKMSPID { get; set; }

        /// <summary>
        /// TCP/IP Port that the KMS Server will Listen On.
        /// </summary>
        private int _mPort;
        public int Port
        {
            get
            {
                return _mPort;
            }
            set
            {
                // Validate KMS Server TCP/IP Port Range
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "KMS Server TCP/IP Port cannot be less than 0.");
                }
                if (value > 65535)
                {
                    throw new ArgumentOutOfRangeException("value", "KMS Server TCP/IP Port cannot be greater than 65535.");
                }
                _mPort = value;
            }
        }

        /// <summary>
        /// KMS Activation Request Interval in minutes for Unactivated KMS Clients
        /// </summary>
        private uint _mVLActivationInterval;
        public uint VLActivationInterval
        {
            get
            {
                return _mVLActivationInterval;
            }
            set
            {
                if (value < 15)
                {
                    throw new ArgumentOutOfRangeException("value", "KMS Activation Interval cannot be less than 15 minutes.");
                }
                if (value > 43200)
                {
                    throw new ArgumentOutOfRangeException("value", "KMS Activation Interval cannot be greater than 43200 minutes.");
                }
                _mVLActivationInterval = value;
            }
        }

        /// <summary>
        /// KMS Renewal Request Interval in minutes for Activated KMS Clients
        /// </summary>
        private uint _mVLRenewalInterval;
        public uint VLRenewalInterval
        {
            get
            {
                return _mVLRenewalInterval;
            }
            set
            {
                if (value < 15)
                {
                    throw new ArgumentOutOfRangeException("value", "KMS Renewal Interval cannot be less than 15 minutes.");
                }
                if (value > 43200)
                {
                    throw new ArgumentOutOfRangeException("value", "KMS Renewal Interval cannot be greater than 43200 minutes.");
                }
                _mVLRenewalInterval = value;
            }
        }

        /// <summary>
        /// Force Open TCP/IP Port by Killing Process
        /// </summary>
        public bool KillProcessOnPort { get; set; }
    }
}
