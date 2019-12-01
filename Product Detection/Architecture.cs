using System;
using Microsoft.Win32;

namespace ProductDetection
{
    /// <summary>
    /// Group of methods for determining the Processor Architecture of Microsoft Office or Windows.
    /// </summary>
    public static class Architecture
    {
        #region Architecture Constants
        public const string X86 = "x86";
        public const string X64 = "x64";
        public const string WOW = "x86-64";
        #endregion
        /// <summary>
        /// Determine whether the operating system is 32 or 64 bit
        /// </summary>
        /// <returns>String value representation of the OS Architecture</returns>
        public static string GetOSArch()
        {
            // Trivially Check 64 Bit OS
            if (Environment.Is64BitOperatingSystem)
            {
                return X64;
            }
            return X86;
        }

        /// <summary>
        /// Determine whether the installed copy of Microsoft Office is 32 bit, 64 bit, or running under WOW64 emulation
        /// </summary>
        /// <returns>String value representation of the Office Architecture</returns>
        public static string GetOfficeArch()
        {
            // Trivially Check 32 Bit OS
            if (Environment.Is64BitOperatingSystem == false)
            {
                return X86;
            }

            /*
            // Use WMI Detection
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT Name from Win32_Service"))
            {
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj["Name"].ToString() == "ose64")
                    {
                        return X64;
                    }
                    if (queryObj["Name"].ToString() == "ose")
                    {
                        return WOW;
                    }
                }
            }
            throw new ApplicationException("Could not detect Microsoft Office Architcture via WMI!");
            S*/

            // Use Registry Detection
            if (!OfficeVersion.IsOfficeVirtual())
            {
                // Traditional Check
                string installRoot;
                int officeNumber = OfficeVersion.GetOfficeNumber();
                switch (officeNumber)
                {
                    // Office 2003 and Office 2007 Don't have 64 Bit Edition, so they must be WOW
                    case 11:
                    case 12:
                        return WOW;
                    case 14:
                    case 15:
                    case 16:
                        installRoot = "SOFTWARE\\Microsoft\\Office\\" + officeNumber + ".0\\Common\\InstallRoot";
                        break;
                    default:
                        throw new ApplicationException("Unsupported Microsoft Office Edition!");
                }
                // x86-64 Check
                using (RegistryKey officeArchCheck = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(installRoot, false))
                {
                    if (officeArchCheck != null)
                    {
                        string installPath = (string) officeArchCheck.GetValue("Path");
                        // If a path is stored here, Office is running under WOW64
                        if (String.IsNullOrWhiteSpace(installPath) == false)
                        {
                            return WOW;
                        }
                    }
                }
                // x64 Check
                using (RegistryKey officeArchCheck = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(installRoot, false))
                {
                    if (officeArchCheck != null)
                    {
                        string installPath = (string)officeArchCheck.GetValue("Path");
                        // If a path is stored here, Office is running under x64
                        if (String.IsNullOrWhiteSpace(installPath) == false)
                        {
                            return X64;
                        }
                    }
                }
            }
            else
            {
                // Virtual Check
                using (RegistryKey officeArchCheck = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(@"SOFTWARE\Microsoft\Office\ClickToRun\Configuration", false))
                {
                    if (officeArchCheck != null)
                    {
                        string platform = (string)officeArchCheck.GetValue("Platform");
                        if (platform == "x86")
                        {
                            return WOW;
                        }
                        return X64;
                    }
                }
            }
            throw new ApplicationException("Unsupported Microsoft Office Architecture!");
        }
    }
}