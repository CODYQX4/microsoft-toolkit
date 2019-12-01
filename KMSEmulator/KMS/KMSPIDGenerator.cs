using System;
using System.Collections.Generic;
using System.Globalization;

namespace KMSEmulator.KMS
{
    class KMSPIDGenerator
    {
        private static readonly Random Random = new Random();

        // Application IDs
        private readonly Guid _appIDWindows                     = new Guid("55C92734-D682-4D71-983E-D6EC3F16059F");
        private readonly Guid _appIDOffice2010                  = new Guid("59A52881-A989-479D-AF46-F275C6370663");
        private readonly Guid _appIDOffice2013                  = new Guid("0FF1CE15-A989-479D-AF46-F275C6370663");
        private readonly Guid _appIDOffice2016                  = new Guid("0FF1CE15-A989-479D-AF46-F275C6370663");
        private readonly Guid _appIDOffice2019                  = new Guid("0FF1CE15-A989-479D-AF46-F275C6370663");

        // KMS Counted IDs
        private readonly Guid _kmsCountedIDOffice2010           = new Guid("e85af946-2e25-47b7-83e1-bebcebeac611"); // Office 2010
        private readonly Guid _kmsCountedIDOffice2013           = new Guid("e6a6f1bf-9d40-40c3-aa9f-c77ba21578c0"); // Office 2013
        private readonly Guid _kmsCountedIDOffice2016           = new Guid("85b5f61b-320b-4be3-814a-b76b2bfafc82"); // Office 2016
        private readonly Guid _kmsCountedIDOffice2019           = new Guid("617d9eb1-ef36-4f82-86e0-a65ae07b96c6"); // Office 2019
        private readonly Guid _kmsCountedIDWindowsVista         = new Guid("212a64dc-43b1-4d3d-a30c-2fc69d2095c6"); // Windows Vista
        private readonly Guid _kmsCountedIDWindows7             = new Guid("7fde5219-fbfa-484a-82c9-34d1ad53e856"); // Windows 7
        private readonly Guid _kmsCountedIDWindows8Retail       = new Guid("bbb97b3b-8ca4-4a28-9717-89fabd42c4ac"); // Windows 8 (Retail)
        private readonly Guid _kmsCountedIDWindows8Volume       = new Guid("3c40b358-5948-45af-923b-53d21fcc7e79"); // Windows 8 (Volume)
        private readonly Guid _kmsCountedIDWindows81Retail      = new Guid("6d646890-3606-461a-86ab-598bb84ace82"); // Windows 8.1 (Retail)
        private readonly Guid _kmsCountedIDWindows81Volume      = new Guid("cb8fc780-2c05-495a-9710-85afffc904d7"); // Windows 8.1 (Volume)
        private readonly Guid _kmsCountedIDWindows10Retail      = new Guid("e1c51358-fe3e-4203-a4a2-3b6b20c9734e"); // Windows 10 (Retail)
        private readonly Guid _kmsCountedIDWindows10Volume      = new Guid("58e2134f-8e11-4d17-9cb2-91069c151148"); // Windows 10 (Volume)
        private readonly Guid _kmsCountedIDWindows10Unknown     = new Guid("d27cd636-1962-44e9-8b4f-27b6c23efb85"); // Windows 10 (Unknown)
        private readonly Guid _kmsCountedIDWindows10ChinaGov    = new Guid("7ba0bf23-d0f5-4072-91d9-d55af5a481b6"); // Windows 10 China Government
        private readonly Guid _kmsCountedIDWindows10LTSB2015    = new Guid("58e2134f-8e11-4d17-9cb2-91069c151148"); // Windows 10 LTSB 2015
        private readonly Guid _kmsCountedIDWindows10LTSB2016    = new Guid("969fe3c0-a3ec-491a-9f25-423605deb365"); // Windows 10 LTSB 2016
        private readonly Guid _kmsCountedIDWindows10LTSC2019    = new Guid("11b15659-e603-4cf1-9c1f-f0ec01b81888"); // Windows 10 LTSC 2019
        private readonly Guid _kmsCountedIDWindowsServer2008A   = new Guid("33e156e4-b76f-4a52-9f91-f641dd95ac48"); // Windows Server 2008 A (Web and HPC)
        private readonly Guid _kmsCountedIDWindowsServer2008B   = new Guid("8fe53387-3087-4447-8985-f75132215ac9"); // Windows Server 2008 B (Standard and Enterprise)
        private readonly Guid _kmsCountedIDWindowsServer2008C   = new Guid("8a21fdf3-cbc5-44eb-83f3-fe284e6680a7"); // Windows Server 2008 C (Datacenter)
        private readonly Guid _kmsCountedIDWindowsServer2008R2A = new Guid("0fc6ccaf-ff0e-4fae-9d08-4370785bf7ed"); // Windows Server 2008 R2 A (Web and HPC)
        private readonly Guid _kmsCountedIDWindowsServer2008R2B = new Guid("ca87f5b6-cd46-40c0-b06d-8ecd57a4373f"); // Windows Server 2008 R2 B (Standard and Enterprise)
        private readonly Guid _kmsCountedIDWindowsServer2008R2C = new Guid("b2ca2689-a9a8-42d7-938d-cf8e9f201958"); // Windows Server 2008 R2 C (Datacenter)
        private readonly Guid _kmsCountedIDWindowsServer2012    = new Guid("8665cb71-468c-4aa3-a337-cb9bc9d5eaac"); // Windows Server 2012
        private readonly Guid _kmsCountedIDWindowsServer2012R2  = new Guid("8456efd3-0c04-4089-8740-5b7238535a65"); // Windows Server 2012 R2
        private readonly Guid _kmsCountedIDWindowsServer2016    = new Guid("6e9fc069-257d-4bc4-b4a7-750514d32743"); // Windows Server 2016
        private readonly Guid _kmsCountedIDWindowsServer2019    = new Guid("8449b1fb-f0ea-497a-99ab-66ca96e9a0f5"); // Windows Server 2019

        // Host OS Build and Type
        struct HostOS
        {
            public int Type;
            public int OSBuild;
            public int MinimumDay;
            public int MinimumYear;
        };

        private const int HostServer2008 = 0;
        private const int HostServer2008R2 = 1;
        private const int HostServer2012 = 2;
        private const int HostServer2012R2 = 3;
        private const int HostServer2016 = 4;
        private const int HostServer2019 = 5;

        // Group ID and Product Key ID
        struct PkeyConfig
        {
            public uint GroupID;
            public uint PIDRangeMin;
            public uint PIDRangeMax;
        };

        private const int PkeyConfigWindows10ChinaGov = 0;
        private const int PkeyConfigWindowsServer2008 = 1;
        private const int PkeyConfigWindowsServer2008R2 = 2;
        private const int PkeyConfigWindowsServer2012 = 3;
        private const int PkeyConfigWindowsServer2012R2 = 4;
        private const int PkeyConfigWindowsServer2016 = 5;
        private const int PkeyConfigWindowsServer2019 = 6;
        private const int PkeyConfigOffice14 = 7;
        private const int PkeyConfigOffice15 = 8;
        private const int PkeyConfigOffice16 = 9;
        private const int PkeyConfigOffice19 = 10;

        // Create Random KMS PID
        public string CreateKMSPID(KMSRequest request)
        {
            // KMS Host OS Type
            List<HostOS> hostOSList = new List<HostOS>
            {
                new HostOS { Type = 55041, OSBuild = 6002, MinimumDay = 146, MinimumYear = 2009},  // Windows Server 2008 SP2:    05/26/2009 SP2 GA
                new HostOS { Type = 55041, OSBuild = 7601, MinimumDay =  53, MinimumYear = 2011},  // Windows Server 2008 R2 SP1: 02/22/2011 SP1 GA
                new HostOS { Type = 5426, OSBuild =  9200, MinimumDay = 248, MinimumYear = 2012},  // Windows Server 2012 RTM:    09/04/2012 RTM GA
                new HostOS { Type = 6401, OSBuild =  9600, MinimumDay = 291, MinimumYear = 2013},  // Windows Server 2012 R2 RTM: 10/18/2013 RTM GA
                new HostOS { Type = 3612, OSBuild = 14393, MinimumDay = 286, MinimumYear = 2016},  // Windows Server 2016 RTM:    10/12/2016 RTM GA
                new HostOS { Type = 3612, OSBuild = 17763, MinimumDay = 275, MinimumYear = 2018}   // Windows Server 2019 RTM:    10/02/2018 RTM GA
            };
            
            // Product Specific KeyConfig
            List<PkeyConfig> pkeyConfigList = new List<PkeyConfig>
            {
                new PkeyConfig { GroupID = 3858, PIDRangeMin = 0, PIDRangeMax = 14999999 },         // Windows 10 China Government
                new PkeyConfig { GroupID = 152, PIDRangeMin = 381000000, PIDRangeMax = 392999999 }, // Windows Server 2008 C
                new PkeyConfig { GroupID = 168, PIDRangeMin = 305000000, PIDRangeMax = 312119999 }, // Windows Server 2008 R2 C
                new PkeyConfig { GroupID = 206, PIDRangeMin = 152000000, PIDRangeMax = 191999999 }, // Windows Server 2012
                new PkeyConfig { GroupID = 206, PIDRangeMin = 271000000, PIDRangeMax = 310999999 }, // Windows Server 2012 R2
                new PkeyConfig { GroupID = 206, PIDRangeMin = 491000000, PIDRangeMax = 530999999 }, // Windows Server 2016
                new PkeyConfig { GroupID = 206, PIDRangeMin = 551000000, PIDRangeMax = 570999999 }, // Windows Server 2019
                new PkeyConfig { GroupID =  96, PIDRangeMin = 199000000, PIDRangeMax = 217999999 }, // Office 2010
                new PkeyConfig { GroupID = 206, PIDRangeMin = 234000000, PIDRangeMax = 255999999 }, // Office 2013
                new PkeyConfig { GroupID = 206, PIDRangeMin = 437000000, PIDRangeMax = 458999999 }, // Office 2016
                new PkeyConfig { GroupID = 206, PIDRangeMin = 666000000, PIDRangeMax = 685999999 }  // Office 2019
            };

            // Product Specific Detection
            int osTypeIndex = HostServer2019;
            int keyConfigIndex = PkeyConfigWindowsServer2019;
            if (request.ApplicationId == _appIDOffice2010 || request.ApplicationId == _appIDOffice2013 || request.ApplicationId == _appIDOffice2016 || request.ApplicationId == _appIDOffice2019)
            {
                if (request.KmsCountedId == _kmsCountedIDOffice2010)
                {
                    keyConfigIndex = PkeyConfigOffice14;
                    osTypeIndex = Random.Next(HostServer2012, HostServer2019 + 1);
                }
                else if (request.KmsCountedId == _kmsCountedIDOffice2013)
                {
                    keyConfigIndex = PkeyConfigOffice15;
                    osTypeIndex = Random.Next(HostServer2012, HostServer2019 + 1);
                }
                else if (request.KmsCountedId == _kmsCountedIDOffice2016)
                {
                    keyConfigIndex = PkeyConfigOffice16;
                    osTypeIndex = Random.Next(HostServer2012, HostServer2019 + 1);
                }
                else if (request.KmsCountedId == _kmsCountedIDOffice2019)
                {
                    keyConfigIndex = PkeyConfigOffice19;
                    osTypeIndex = Random.Next(HostServer2012, HostServer2019 + 1);
                }
            }
            else if (request.ApplicationId == _appIDWindows)
            {
                if (request.KmsCountedId == _kmsCountedIDWindowsVista || request.KmsCountedId == _kmsCountedIDWindowsServer2008A || request.KmsCountedId == _kmsCountedIDWindowsServer2008B || request.KmsCountedId == _kmsCountedIDWindowsServer2008C)
                {
                    keyConfigIndex = Random.Next(PkeyConfigWindowsServer2012, PkeyConfigWindowsServer2019 + 1);
                    osTypeIndex = Random.Next(HostServer2012, HostServer2019 + 1);
                }
                else if (request.KmsCountedId == _kmsCountedIDWindows7 || request.KmsCountedId == _kmsCountedIDWindowsServer2008R2A || request.KmsCountedId == _kmsCountedIDWindowsServer2008R2B || request.KmsCountedId == _kmsCountedIDWindowsServer2008R2C)
                {
                    keyConfigIndex = Random.Next(PkeyConfigWindowsServer2012, PkeyConfigWindowsServer2019 + 1);
                    osTypeIndex = Random.Next(HostServer2012, HostServer2019 + 1);
                }
                else if (request.KmsCountedId == _kmsCountedIDWindows8Retail || request.KmsCountedId == _kmsCountedIDWindows8Volume || request.KmsCountedId == _kmsCountedIDWindowsServer2012)
                {
                    keyConfigIndex = Random.Next(PkeyConfigWindowsServer2012, PkeyConfigWindowsServer2019 + 1);
                    osTypeIndex = Random.Next(HostServer2012, HostServer2019 + 1);
                }
                else if (request.KmsCountedId == _kmsCountedIDWindows81Retail || request.KmsCountedId == _kmsCountedIDWindows81Volume || request.KmsCountedId == _kmsCountedIDWindowsServer2012R2)
                {
                    keyConfigIndex = Random.Next(PkeyConfigWindowsServer2012R2, PkeyConfigWindowsServer2019 + 1);
                    osTypeIndex = Random.Next(HostServer2012R2, HostServer2019 + 1);
                }
                else if (request.KmsCountedId == _kmsCountedIDWindows10Retail || request.KmsCountedId == _kmsCountedIDWindows10Volume || request.KmsCountedId == _kmsCountedIDWindows10LTSB2015 || request.KmsCountedId == _kmsCountedIDWindows10LTSB2016 || request.KmsCountedId == _kmsCountedIDWindowsServer2016)
                {
                    keyConfigIndex = Random.Next(PkeyConfigWindowsServer2016, PkeyConfigWindowsServer2019 + 1);
                    osTypeIndex = Random.Next(HostServer2016, HostServer2019 + 1);
                }
                else if (request.KmsCountedId == _kmsCountedIDWindows10LTSC2019 || request.KmsCountedId == _kmsCountedIDWindowsServer2019)
                {
                    keyConfigIndex = PkeyConfigWindowsServer2019;
                    osTypeIndex = HostServer2019;
                }
                else if (request.KmsCountedId == _kmsCountedIDWindows10ChinaGov)
                {
                    keyConfigIndex = PkeyConfigWindows10ChinaGov;
                    osTypeIndex = HostServer2019;
                }
            }

            // Generate Part 1 & 7: Host Type and KMS Server OS Build
            HostOS osType = hostOSList[osTypeIndex];

            // Generate Part 2: Group ID and Product Key ID Range
            PkeyConfig keyConfig = pkeyConfigList[keyConfigIndex];

            // Generate Part 3 and Part 4: Product Key ID
            long productKeyID = Random.Next(0, int.MaxValue) % (keyConfig.PIDRangeMax - keyConfig.PIDRangeMin) + keyConfig.PIDRangeMin;

            // Generate Part 5: License Channel (00=Retail, 01=Retail, 02=OEM, 03=Volume(GVLK,MAK)) - always 03
            const int licenseChannel = 3;

            // Generate Part 6: Language - use system default language
            int languageCode = CultureInfo.InstalledUICulture.LCID; // GetSystemDefaultLCID();

            /* Generate Part 8: KMS Host Activation Date */
            int minimumDay, minimumYear;

            // Get Minimum Possible Date
            minimumDay = osType.MinimumDay;
            minimumYear = osType.MinimumYear;

            // Get Current Time Information
            int currentYear = DateTime.Now.Year;           // Year (current year minus 1900).
            int currentDay = DateTime.Now.DayOfYear;       // Day of Year (0-365; January 1 = 0)

            // Use 2018 for Current Year if time is screwed up.
            if (minimumYear > currentYear)
            {
                currentYear = 2018;
            }

            // Generate Year (Minimum Year to Current Year)
            int year = Random.Next() % (currentYear - (minimumYear - 1)) + minimumYear;

            // Generate Day
            int randomDay = Random.Next() % 365 + 1;

            // Check for future days and limits the random up to current day.
            if ((randomDay > currentDay) && (year == currentYear))
            {
                randomDay = Random.Next() % currentDay + 1;
            }
            // Check for product minimum build year and day and adjust the random accordingly.
            else if ((randomDay < minimumDay) && (year == minimumYear))
            {
                randomDay = minimumDay + (Random.Next() % (365 - minimumDay) + 1);
            }

            // Apply Setting
            string result = string.Format("{0:D5}-{1:D5}-{2:D3}-{3:D6}-{4:D2}-{5}-{6:D4}.0000-{7:D3}{8:D4}",
                osType.Type, keyConfig.GroupID, productKeyID / 1000000, productKeyID % 1000000, licenseChannel,
                languageCode, osType.OSBuild, randomDay, year);

            return result;
        }
    }
}
