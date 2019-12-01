using System;
using System.IO;
using Common;
using Microsoft.Win32;
using ProductDetection;

namespace KMSServerServiceInstaller
{
    /// <summary>
    /// Group of Methods for installing and uninstalling KMS Server Service
    /// </summary>
    public static class KMSServerServiceInstaller
    {
        /// <summary>
        /// Directory where KMS Server Service is installed
        /// </summary>
        private static string _kmsServerServiceInstallPath;

        #region KMS Server Service Names Constants
        public const string KMSServerServiceFileName = "KMS Server Service.exe";
        public const string KMSServerServiceProcessName = "KMS Server Service";
        public const string KMSServerServiceName = "KMSServerService";
        #endregion

        /// <summary>
        /// Check if KMS Server Service is installed by checking if the Windows Service is registered
        /// </summary>
        /// <returns>True if the Windows Service exists, False if the Windows Service does not exist</returns>
        public static bool IsKMSServerServiceInstalled()
        {
            return Services.IsServiceInstalled(KMSServerServiceName);
        }

        /// <summary>
        /// Create the KMS Server Service Registration
        /// </summary>
        private static void MakeKMSServerServiceRegistration()
        {
            // Install KMS Server Service
            Services.InstallService(KMSServerServiceProcessName, KMSServerServiceName, "Windows Service that emulates a Key Management Service (KMS) Server", _kmsServerServiceInstallPath + Path.DirectorySeparatorChar + KMSServerServiceFileName, 2);

            // Set Default Settings
            using (RegistryKey registry = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\KMSServerService\\Parameters"))
            {
                if (registry != null)
                {
                    // Set KMS PID
                    registry.SetValue("KMSPID", "RandomKMSPID", RegistryValueKind.String);

                    // Set KMS HWID
                    registry.SetValue("KMSHWID", "364F463A8863D35F", RegistryValueKind.String);

                    // Set KMS Port
                    registry.SetValue("KMSPort", "1688", RegistryValueKind.String);

                    // Set KMS Activation Interval
                    registry.SetValue("VLActivationInterval", 120, RegistryValueKind.DWord);

                    // Set KMS Renewal Interval
                    registry.SetValue("VLRenewalInterval", 10080, RegistryValueKind.DWord);

                    // Set Free TCP/IP Port
                    registry.SetValue("KillProcessOnPort", 1, RegistryValueKind.DWord);
                }
            }
        }

        /// <summary>
        /// Delete the KMS Server Service Registration
        /// </summary>
        private static void DeleteKMSServerServiceRegistration()
        {
            Services.UninstallService(KMSServerServiceName);
        }

        /// <summary>
        /// Get the Install Path of KMS Server Service by reading Windows Service Registration
        /// </summary>
        /// <returns>Directory where KMS Server Service is installed</returns>
        public static string GetKMSServerServicePath()
        {
            if (IsKMSServerServiceInstalled())
            {
                return Path.GetDirectoryName(Services.GetServiceProgramPath(KMSServerServiceName));
            }
            throw new DirectoryNotFoundException("KMS Server Service install path not found!");
        }

        /// <summary>
        /// Install KMS Server Service and Setup Windows Service Registration
        /// </summary>
        /// <param name="installPath">Path to write Program File</param>
        /// <param name="kmsServerService">Byte Array representation of KMS Server Service Program File </param>
        /// <returns>Output of Installation Process</returns>
        public static string InstallKMSServerService(string installPath, byte[] kmsServerService)
        {
            using (StringWriter output = new StringWriter())
            {
                // Create Program File
                try
                {
                    try
                    {
                        _kmsServerServiceInstallPath = installPath;

                        // Create Windows Defender Exclusion
                        if (OSVersion.GetWindowsNumber() >= 10.0)
                        {
                            CommonUtilities.ExecuteCommand("powershell.exe Add-MpPreference -ExclusionPath " + CommonUtilities.EscapePath(_kmsServerServiceInstallPath) + " -Force", true);
                        }

                        // Create KMS Server Service
                        CommonUtilities.FileCreate(KMSServerServiceFileName, kmsServerService, _kmsServerServiceInstallPath);
                        output.WriteLine("Created " + _kmsServerServiceInstallPath + Path.DirectorySeparatorChar + KMSServerServiceFileName);
                    }
                    catch (Exception)
                    {
                        _kmsServerServiceInstallPath = Environment.GetEnvironmentVariable("windir") + Path.DirectorySeparatorChar + KMSServerServiceName;
                        output.WriteLine("Could not access KMS Server Service path set in Settings, using default installation path.");
                        CommonUtilities.FileCreate(KMSServerServiceFileName, kmsServerService, _kmsServerServiceInstallPath);
                        output.WriteLine("Created " + _kmsServerServiceInstallPath + Path.DirectorySeparatorChar + KMSServerServiceFileName);
                    }
                }
                catch (Exception)
                {
                    // Remove Windows Defender Exclusion
                    if (OSVersion.GetWindowsNumber() >= 10.0)
                    {
                        CommonUtilities.ExecuteCommand("powershell.exe Remove-MpPreference -ExclusionPath " + CommonUtilities.EscapePath(_kmsServerServiceInstallPath) + " -Force", true);
                    }

                    output.WriteLine("Failed to create " + _kmsServerServiceInstallPath + Path.DirectorySeparatorChar + KMSServerServiceFileName);
                    output.Write("KMS Server Service failed to install!");
                    return output.ToString();
                }

                // Create Windows Service Registration
                try
                {
                    MakeKMSServerServiceRegistration();
                    output.WriteLine("Created KMS Server Service Registration");
                }
                catch (Exception ex)
                {
                    output.WriteLine("Failed to create KMS Server Service Registration");
                    output.WriteLine(ex.Message);
                    output.WriteLine("Cleaning up as KMS Server Service is partially installed");

                    // Delete Program File
                    try
                    {
                        CommonUtilities.FileDelete(_kmsServerServiceInstallPath + Path.DirectorySeparatorChar + KMSServerServiceFileName);
                        output.WriteLine("Deleted " + _kmsServerServiceInstallPath + Path.DirectorySeparatorChar + KMSServerServiceFileName);
                    }
                    catch (Exception)
                    {
                        output.WriteLine("Failed to delete " + _kmsServerServiceInstallPath + Path.DirectorySeparatorChar + KMSServerServiceFileName);
                    }

                    // Remove Windows Defender Exclusion
                    if (OSVersion.GetWindowsNumber() >= 10.0)
                    {
                        CommonUtilities.ExecuteCommand("powershell.exe Remove-MpPreference -ExclusionPath " + CommonUtilities.EscapePath(_kmsServerServiceInstallPath) + " -Force", true);
                    }

                    output.Write("KMS Server Service failed to install!");
                    return output.ToString();
                }

                output.Write("KMS Server Service was succesfully installed.");
                Services.StartService(KMSServerServiceName);
                return output.ToString();
            }
        }

        /// <summary>
        /// Uninstall KMS Server Service and Remove Windows Service Registration
        /// </summary>
        /// <returns>Output of Uninstallation Process</returns>
        public static string UninstallKMSServerService()
        {
            using (StringWriter output = new StringWriter())
            {
                // Get Program File Path
                try
                {
                    _kmsServerServiceInstallPath = GetKMSServerServicePath();
                }
                catch (Exception)
                {
                    output.WriteLine("Failed to access KMS Server Service installation path!");
                    return output.ToString();
                }

                // Delete Program File
                try
                {
                    Services.StopService(KMSServerServiceName, KMSServerServiceProcessName);
                    CommonUtilities.FileDelete(_kmsServerServiceInstallPath + Path.DirectorySeparatorChar + KMSServerServiceFileName);
                    output.WriteLine("Deleted " + _kmsServerServiceInstallPath + Path.DirectorySeparatorChar + KMSServerServiceFileName);
                }
                catch (Exception)
                {
                    output.WriteLine("Failed to delete " + _kmsServerServiceInstallPath + Path.DirectorySeparatorChar + KMSServerServiceFileName);
                }

                // Delete Windows Service Registration
                try
                {
                    DeleteKMSServerServiceRegistration();
                    output.WriteLine("Deleted KMS Server Service Registration");
                }
                catch (Exception)
                {
                    output.WriteLine("Failed to delete KMS Server Service Registration");
                }

                // Remove Windows Defender Exclusion
                if (OSVersion.GetWindowsNumber() >= 10.0)
                {
                    CommonUtilities.ExecuteCommand("powershell.exe Remove-MpPreference -ExclusionPath " + CommonUtilities.EscapePath(_kmsServerServiceInstallPath) + " -Force", true);
                }

                if (output.ToString().Contains("Failed"))
                {
                    output.Write("KMS Server Service failed to uninstall!");
                }
                else
                {
                    // Delete Install Folder If Empty
                    CommonUtilities.DeleteEmptyFolders(_kmsServerServiceInstallPath + Path.DirectorySeparatorChar);

                    output.Write("KMS Server Service was succesfully uninstalled.");
                }
                return output.ToString();
            }
        }
    }
}
