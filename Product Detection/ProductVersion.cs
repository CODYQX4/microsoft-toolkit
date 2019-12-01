using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using Microsoft.Win32;

namespace ProductDetection
{
    /// <summary>
    /// Generic class for obtaining the Software Licensing Service version
    /// </summary>
    internal static class SLVersion
    {
        /// <summary>
        /// Query WMI to determine the Software Licensing Service version
        /// </summary>
        /// <param name="wmiInfo">WMI Provider and associated data to get the Software Licensing Service version</param>
        /// <returns>Version from SPPWMI Provider</returns>
        internal static string GetSLVersion(string wmiInfo)
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\CIMV2", wmiInfo))
            {
                foreach (ManagementBaseObject queryObj in searcher.Get())
                {
                    return Convert.ToString(queryObj["Version"]);
                }
            }

            throw new Exception("Failed to get SL Version!");
        }
    }

    /// <summary>
    /// Group of methods for determining Operating System version and Product Name
    /// </summary>
    public static class OSVersion
    {
        #region Product Name Constants
        public const string Win95 = "Windows 95";
        public const string Win95SE = "Windows 95 SE";
        public const string Win98 = "Windows 98";
        public const string WinME = "Windows ME";
        public const string WinNT351 = "Windows NT 3.51";
        public const string WinNT4 = "Windows NT 4.0";
        public const string Win2000 = "Windows 2000";
        public const string WinXP = "Windows XP";

        public const string WinVista = "Windows Vista";
        public const string WinServer2008 = "Windows Server 2008";

        public const string Win7 = "Windows 7";
        public const string Win7Embedded = "Windows 7 Embedded";
        public const string WinServer2008R2 = "Windows Server 2008 R2";

        public const string Win8 = "Windows 8";
        public const string Win8Embedded = "Windows 8 Embedded";
        public const string WinServer2012 = "Windows Server 2012";

        public const string Win81 = "Windows 8.1";
        public const string Win81Embedded = "Windows 8.1 Embedded";
        public const string WinServer2012R2 = "Windows Server 2012 R2";

        public const string Win10 = "Windows 10";
        public const string Win10Embedded = "Windows 10 Embedded";
        public const string WinServer2016 = "Windows Server 2016";
        public const string WinServer2019 = "Windows Server 2019";
        #endregion
        /// <summary>
        /// Determine if the installed version of Windows is a server version
        /// </summary>
        /// <returns>True if Windows is a Server Version, False if Windows is a Client Version</returns>
        public static bool IsWindowsServer()
        {
            return NativeMethods.IsOS(OSType.AnyServer);
        }

        /// <summary>
        /// Call to GetSLVersion() to get the Windows Software Licensing Service Version
        /// </summary>
        /// <returns>Version of Windows Software Licensing Service</returns>
        public static string GetSPPSVCVersion()
        {
            return SLVersion.GetSLVersion("SELECT Version FROM SoftwareLicensingService");
        }

        /// <summary>
        /// Get the Product Name of the installed copy of Windows
        /// </summary>
        /// <returns>Microsoft Windows Product Name</returns>
        public static string GetWindowsName()
        {
            // Get the Kernel32 DLL File Version
            FileVersionInfo osVersionInfo = FileVersionInfo.GetVersionInfo(Environment.GetEnvironmentVariable("windir") + @"\System32\Kernel32.dll");
            
            // Variable to hold our return value
            string operatingSystem = string.Empty;

            switch (osVersionInfo.FileMajorPart)
            {
                case 3:
                    operatingSystem = WinNT351;
                    break;
                case 4:
                    operatingSystem = WinNT4;
                    break;
                case 5:
                    if (osVersionInfo.FileMinorPart == 0)
                    {
                        operatingSystem = Win2000;
                    }
                    else
                    {
                        operatingSystem = WinXP;
                    }
                    break;
                case 6:
                    switch (osVersionInfo.FileMinorPart)
                    {
                        case 0:
                            if (IsWindowsServer() == false)
                            {
                                operatingSystem = WinVista;
                            }
                            else
                            {
                                operatingSystem = WinServer2008;
                            }
                            break;
                        case 1:
                            if (IsWindowsServer() == false)
                            {
                                operatingSystem = Win7;
                            }
                            else
                            {
                                operatingSystem = WinServer2008R2;
                            }
                            break;
                        case 2:
                            if (IsWindowsServer() == false)
                            {
                                operatingSystem = Win8;
                            }
                            else
                            {
                                operatingSystem = WinServer2012;
                            }
                            break;
                        case 3:
                            if (IsWindowsServer() == false)
                            {
                                operatingSystem = Win81;
                            }
                            else
                            {
                                operatingSystem = WinServer2012R2;
                            }
                            break;
                    }
                    break;
                case 10:
                    switch (osVersionInfo.FileMinorPart)
                    {
                        case 0:
                            if (IsWindowsServer() == false)
                            {
                                operatingSystem = Win10;
                            }
                            else
                            {
                                if (osVersionInfo.FileBuildPart >= 17763)
                                {
                                    operatingSystem = WinServer2019;
                                }
                                else
                                {
                                    operatingSystem = WinServer2016;
                                }
                            }
                            break;
                    }
                    break;
                default:
                    operatingSystem = "Unknown";
                    break;
            }

            // Return the information we've gathered.
            return operatingSystem;
        }

        /// <summary>
        /// Get the Build number of the installed copy of Windows
        /// </summary>
        /// <returns>Microsoft Windows Build number as integer</returns>
        public static int GetWindowsBuildNumber()
        {
            // Get the Kernel32 DLL File Version
            FileVersionInfo osVersionInfo = FileVersionInfo.GetVersionInfo(Environment.GetEnvironmentVariable("windir") + @"\System32\Kernel32.dll");

            return osVersionInfo.FileBuildPart;
        }

        /// <summary>
        /// Get the Platform Version of the installed copy of Windows
        /// </summary>
        /// <returns>Microsoft Windows Platform version as double</returns>
        public static double GetWindowsNumber()
        {
            // Get the Kernel32 DLL File Version
            FileVersionInfo osVersionInfo = FileVersionInfo.GetVersionInfo(Environment.GetEnvironmentVariable("windir") + @"\System32\Kernel32.dll");

            return double.Parse(osVersionInfo.FileMajorPart + "." + osVersionInfo.FileMinorPart, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Determine if the installed copy of Windows is supported based on its platform version number
        /// </summary>
        /// <returns>True if supported, False if unsupported</returns>
        public static bool IsWindowsSupported()
        {
            double windowsNumber = GetWindowsNumber();
            if ((windowsNumber >= 6.0 && windowsNumber <= 10.0))
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Group of methods for determining Microsoft Office version
    /// </summary>
    public static class OfficeVersion
    {
        #region Product Name Constants
        public const string Office2003 = "Microsoft Office 2003";
        public const string Office2007 = "Microsoft Office 2007";
        public const string Office2010 = "Microsoft Office 2010";
        public const string Office2013 = "Microsoft Office 2013";
        public const string Office2016 = "Microsoft Office 2016";
        public const string Office2019 = "Microsoft Office 2019";
        #endregion
        /// <summary>
        /// Call to GetSLVersion() to get the Office Software Licensing Service Version
        /// </summary>
        /// <returns>Version of Office Software Licensing Service</returns>
        public static string GetOSPPSVCVersion()
        {
            return SLVersion.GetSLVersion("SELECT Version FROM OfficeSoftwareProtectionService");
        }

        /// <summary>
        /// Get the Product Name of the installed copy of Office
        /// </summary>
        /// <returns>Microsoft Office Product Name</returns>
        public static string GetOfficeName()
        {
            // Use Registry Detection for Traditional Installed Office
            if (!IsOfficeVirtual())
            {
                using (RegistryKey officeKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(@"SOFTWARE\Microsoft\Office", false))
                {
                    using (RegistryKey officeKeyWOW = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\Office", false))
                    {
                        // Check Office 2016
                        if (officeKeyWOW != null && (officeKey != null && IsOfficeInstalledTraditional(officeKey.OpenSubKey(@"16.0\Common\InstallRoot"), officeKeyWOW.OpenSubKey(@"16.0\Common\InstallRoot"))))
                        {
                            return Office2016;
                        }
                        // Check Office 2013
                        if (officeKeyWOW != null && (officeKey != null && IsOfficeInstalledTraditional(officeKey.OpenSubKey(@"15.0\Common\InstallRoot"), officeKeyWOW.OpenSubKey(@"15.0\Common\InstallRoot"))))
                        {
                            return Office2013;
                        }
                        // Check Office 2010
                        if (officeKeyWOW != null && (officeKey != null && IsOfficeInstalledTraditional(officeKey.OpenSubKey(@"14.0\Common\InstallRoot"), officeKeyWOW.OpenSubKey(@"14.0\Common\InstallRoot"))))
                        {
                            return Office2010;
                        }
                        // Check Office 2007
                        if (officeKeyWOW != null && (officeKey != null && IsOfficeInstalledTraditional(officeKey.OpenSubKey(@"12.0\Common\InstallRoot"), officeKeyWOW.OpenSubKey(@"12.0\Common\InstallRoot"))))
                        {
                            return Office2007;
                        }
                        // Check Office 2003
                        if (officeKeyWOW != null && (officeKey != null && IsOfficeInstalledTraditional(officeKey.OpenSubKey(@"11.0\Common\InstallRoot"), officeKeyWOW.OpenSubKey(@"11.0\Common\InstallRoot"))))
                        {
                            return Office2003;
                        }
                    }
                }
            }
            else
            {
                // Use Registry Detection for Virtually Installed Office
                using (RegistryKey officeKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(@"SOFTWARE\Microsoft\Office", false))
                {
                    if (officeKey != null)
                    {
                        using (RegistryKey officeKeyC2R = officeKey.OpenSubKey(@"ClickToRun", false))
                        {
                            if (officeKeyC2R != null)
                            {
                                // Get Package GUID
                                string packageGUID = officeKeyC2R.GetValue("PackageGUID").ToString();

                                // Check Office 2016/2019
                                if (IsOfficeInstalledVirtual(officeKey.OpenSubKey(@"16.0\ClickToRunStore\Packages\{" + packageGUID + "}")))
                                {
                                    string installversion = officeKeyC2R.OpenSubKey(@"Scenario\INSTALL").GetValue("Version").ToString();
                                    if (Convert.ToInt32(installversion.Split('.')[2]) >= 10336)
                                    {
                                        return Office2019;
                                    }
                                    else
                                    {
                                        return Office2016;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            throw new ApplicationException("Unsupported Microsoft Office Edition!");
        }

        /// <summary>
        /// Helper Method for GetOfficeName()
        /// </summary>
        /// <param name="officeInstallCheck">Registry Key to Check</param>
        /// <param name="officeInstallCheckWOW">WOW Redirected Version of officeInstallCheck</param>
        /// <returns>True if Installed, False if Not Installed</returns>
        private static bool IsOfficeInstalledTraditional(RegistryKey officeInstallCheck, RegistryKey officeInstallCheckWOW)
        {
            // Check if the Passed Keys Contain a Path Value that isn't null
            if (officeInstallCheck != null)
            {
                string installPath = (string)officeInstallCheck.GetValue("Path");
                // If a path is stored here, Office is installed
                if (String.IsNullOrWhiteSpace(installPath) == false)
                {
                    return true;
                }
            }
            else if (officeInstallCheckWOW != null)
            {
                string installPath = (string)officeInstallCheckWOW.GetValue("Path");
                // If a path is stored here, Office is installed
                if (String.IsNullOrWhiteSpace(installPath) == false)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Helper Method for GetOfficeName()
        /// </summary>
        /// <param name="officeInstallCheck">Registry Key to Check</param>
        /// <returns>True if Installed, False if Not Installed</returns>
        private static bool IsOfficeInstalledVirtual(RegistryKey officeInstallCheck)
        {
            // Check if the Passed Keys Contain a Path Value that isn't null
            if (officeInstallCheck != null)
            {
                string installPath = (string)officeInstallCheck.GetValue(null);
                // If a path is stored here, Office is installed
                if (String.IsNullOrWhiteSpace(installPath) == false)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get the installation of Microsoft Office from the registry
        /// </summary>
        /// <returns>String Representation of the Path to Microsoft Office Installation</returns>
        public static string GetInstallationPath()
        {      
            int officeNumber = GetOfficeNumber();
            // Traditional Check
            if (!IsOfficeVirtual())
            {
                switch (Architecture.GetOfficeArch())
                {
                    case Architecture.X86:
                    case Architecture.X64:
                        {
                            using (RegistryKey installRoot = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Office\\" + officeNumber + ".0\\Common\\InstallRoot", false))
                            {
                                if (installRoot != null)
                                {
                                    return (string)installRoot.GetValue("Path");
                                }
                            }
                            break;
                        }
                    case Architecture.WOW:
                        {
                            using (RegistryKey installRoot = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\Office\\" + officeNumber + ".0\\Common\\InstallRoot", false))
                            {
                                if (installRoot != null)
                                {
                                    return (string)installRoot.GetValue("Path");
                                }
                            }
                            break;
                        }
                }
            }
            // Virtual Check
            else
            {
                using (RegistryKey installRoot = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Office\\ClickToRun", false))
                {
                    if (installRoot != null)
                    {
                        return (string)installRoot.GetValue("InstallPath") + "\\Office" + officeNumber + Path.DirectorySeparatorChar;
                    }
                }
            }

            throw new Exception("Failed to get Microsoft Office Installation Path!");
        }

        /// <summary>
        /// Get the Platform Version of the installed copy of Office
        /// </summary>
        /// <returns>Microsoft Office Platform version number</returns>
        public static int GetOfficeNumber()
        {
            switch (GetOfficeName())
            {
                case Office2003:
                    return 11;
                case Office2007:
                    return 12;
                case Office2010:
                    return 14;
                case Office2013:
                    return 15;
                case Office2016:
                case Office2019:
                    return 16;
                default:
                    throw new ApplicationException("Unsupported Microsoft Office Edition!");
            }
        }

        /// <summary>
        /// Determine if the installed copy of Office is supported based on its platform version number
        /// </summary>
        /// <returns>True if supported, False if unsupported</returns>
        public static bool IsOfficeSupported()
        {
            try
            {
                int officeNumber = GetOfficeNumber();
                if (officeNumber >= 14 && officeNumber <= 16)
                {
                    return true;
                }
                return false;
            }
            catch (ApplicationException)
            {
                return false;
            }
        }

        /// <summary>
        /// Check if Microsoft Office is using SPPSVC from the OS instead of bundled OSPPSVC.
        /// </summary>
        /// <returns>True if using SPPSVC, False if using OSPPSVC</returns>
        public static bool IsOfficeSPP()
        {
            try
            {
                return (GetOfficeNumber() >= 15 && OSVersion.GetWindowsNumber() >= 6.2);
            }
            catch (ApplicationException)
            {
                return false;
            }
        }

        /// <summary>
        /// Check if Microsoft Office is using Click To Run or is a Traditional install.
        /// </summary>
        /// <returns>True if using Click To Run, False if using Traditional</returns>
        public static bool IsOfficeVirtual()
        {
            using (RegistryKey officeKeyC2R = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Office\\ClickToRun", false))
            {
                if (officeKeyC2R != null)
                {
                    try
                    {
                        // Get Package GUID
                        string packageGUID = officeKeyC2R.GetValue("PackageGUID").ToString();
                        return (!String.IsNullOrWhiteSpace(packageGUID));
                    }
                    catch (NullReferenceException)
                    {
                        return false;
                    }                    
                }
            }
            return false;
        }
    }
}