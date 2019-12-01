using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Common;
using Keys;
using License;
using ProductDetection;

namespace Backup
{
    /// <summary>
    /// Class with Properties and Methods Common to Microsoft Office and Windows Backup
    /// </summary>
    public static class BackupBase
    {
        /// <summary>
        /// Handle to the Active GUI Window
        /// </summary>
        public static IWin32Window Window;
    }

    /// <summary>
    /// Group of Methods for Saving and Restoring Activation Data
    /// </summary>
    public static class LicenseBackup
    {
        /// <summary>
        /// Save Tokens and other Data to Save and later Restore Activation and License State.
        /// </summary>
        /// <param name="backupName">Name of the Backup Folder.</param>
        /// <param name="backupPath">Path where the Backup will be saved.</param>
        /// <param name="licenses">List of Licenses for the Product to Backup.</param>
        /// <returns>Output of Backup Process</returns>
        public static string Backup(string backupName, string backupPath, LicenseList licenses)
        {
            // On Windows 8, Microsoft Office 2013 uses Windows Licensing Services
            bool mergedOfficeWindows = OfficeVersion.IsOfficeSPP();
            if (mergedOfficeWindows && licenses is LicenseListOffice)
            {
                throw new Exception("Cannot Backup Microsoft Office 2013 or Later on Windows 8 or Later!");
            }

            // Get Tokens Paths
            string tokensSourcePath;
            if (mergedOfficeWindows || licenses is LicenseListWindows)
            {
                tokensSourcePath = GetTokensPath(OSVersion.GetWindowsName());
            }
            else
            {
                tokensSourcePath = GetTokensPath(OfficeVersion.GetOfficeName());
            }
            string tokensDestinationPath = backupPath + backupName + "\\Files\\Tokens";
            string cacheSourcePath = tokensSourcePath + "\\Cache";
            string cacheDestinationPath = tokensDestinationPath + "\\Cache";

            using (StringWriter output = new StringWriter())
            {
                try
                {
                    // Prevent Invalid Backup Name
                    if (String.IsNullOrWhiteSpace(backupName))
                    {
                        output.WriteLine("License Backup Failed!");
                        output.WriteLine("You did not enter a backup name.");
                        return output.ToString();
                    }
                    if (Directory.Exists(backupPath + backupName))
                    {
                        output.WriteLine("License Backup Failed!");
                        output.WriteLine("This Backup Name is already in use. Delete the old one if you want to reuse the name.");
                        return output.ToString();
                    }

                    // Show Activation Errors if No Licenses or Keys Exist
                    if (licenses.GetListUnlicensed().Count == 0 && licenses.GetListLicensed().Count == 0)
                    {
                        output.WriteLine("License Backup Failed!");
                        output.WriteLine(LicenseErrorCode.ErrBroken);
                        return output.ToString();
                    }
                    if (licenses.GetListUnlicensed().Count > 0 && licenses.GetListLicensed().Count == 0)
                    {
                        output.WriteLine("License Backup Failed!");
                        output.WriteLine(LicenseErrorCode.ErrKeyless);
                        return output.ToString();
                    }

                    // Stop Licensing Service
                    if (mergedOfficeWindows == false && licenses is LicenseListOffice)
                    {
                        Services.StopOSPPSVC();
                    }
                    else
                    {
                        Services.StopSPPSVC();
                    }

                    // Create Folders
                    Directory.CreateDirectory(backupPath);
                    Directory.CreateDirectory(backupPath + backupName);
                    Directory.CreateDirectory(backupPath + backupName + "\\Files");
                    Directory.CreateDirectory(backupPath + backupName + "\\Files\\Tokens");
                    Directory.CreateDirectory(backupPath + backupName + "\\Files\\Tokens\\Cache");
                    Directory.CreateDirectory(backupPath + backupName + "\\Registry");

                    // Save Registry
                    if (mergedOfficeWindows == false && licenses is LicenseListOffice)
                    {
                        // Save Trusted Store
                        CommonUtilities.ExecuteCommand("REG SAVE HKLM\\SOFTWARE\\Microsoft\\OfficeSoftwareProtectionPlatform " + CommonUtilities.EscapePath(backupPath + backupName + "\\Registry\\" + "OfficeSPPInfo.hiv"), true);
                        
                        // Save Office Registration
                        //BackupOfficeRegistration(OfficeVersion.GetOfficeNumber(), backupPath, backupName);
                    }
                    else
                    {
                        // Save Trusted Store
                        CommonUtilities.ExecuteCommand("REG SAVE HKLM\\SYSTEM\\WPA " + CommonUtilities.EscapePath(backupPath + backupName + "\\Registry\\" + "WPAInfo.hiv"), true);

                        // Save Office Registration
                        if (mergedOfficeWindows)
                        {
                            //BackupOfficeRegistration(OfficeVersion.GetOfficeNumber(), backupPath, backupName);
                        }
                    }

                    // Save Tokens
                    File.Copy(tokensSourcePath + "\\tokens.dat", tokensDestinationPath + "\\tokens.dat", true);
                    if (File.Exists(cacheSourcePath + "\\Cache.dat"))
                    {
                        File.Copy(cacheSourcePath + "\\Cache.dat", cacheDestinationPath + "\\Cache.dat", true);
                    }
                    if (File.Exists(tokensSourcePath + "\\data.dat"))
                    {
                        File.Copy(tokensSourcePath + "\\data.dat", tokensDestinationPath + "\\data.dat", true);
                    }

                    // Start Licensing Service
                    if (mergedOfficeWindows == false && licenses is LicenseListOffice)
                    {
                        Services.StartOSPPSVC();
                    }
                    else
                    {
                        Services.StartSPPSVC();
                    }

                    // Save Product Keys
                    try
                    {
                        using (StreamWriter outfile = new StreamWriter(backupPath + backupName + "\\Keys.ini"))
                        {
                            List<string> keys = GetKeys(licenses);
                            foreach (string key in keys)
                            {
                                outfile.WriteLine(key);
                            }
                            if (mergedOfficeWindows)
                            {
                                keys = GetKeys(new LicenseListOffice());
                                foreach (string key in keys)
                                {
                                    outfile.WriteLine(key);
                                }
                            }
                        }
                    }
                    catch (ApplicationException ex)
                    {
                        output.WriteLine("License Backup Failed!");
                        output.WriteLine(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    if (output.ToString().Contains("Failed") == false)
                    {
                        output.WriteLine("License Backup Failed!");
                    }
                    output.WriteLine(ex.Message);
                }
                finally
                {
                    if (output.ToString().Contains("Failed"))
                    {
                        CommonUtilities.FileDelete(backupPath + backupName + "\\Keys.ini");
                        CommonUtilities.FolderDelete(backupPath + backupName);
                    }
                }
                if (output.ToString().Contains("Failed"))
                {
                    return output.ToString();
                }
                output.WriteLine("Backup Complete.");
                return output.ToString();
            }
        }

        /// <summary>
        /// Restore Tokens and other Data to Restore Activation and License State.
        /// </summary>
        /// <param name="backupName">Name of the Backup Folder.</param>
        /// <param name="backupPath">Path where the Backup will be loaded.</param>
        /// <param name="licenses">List of Licenses for the Product to Backup.</param>
        /// <returns>Output of Restore Process</returns>
        public static string Restore(string backupName, string backupPath, LicenseList licenses)
        {
            // On Windows 8, Microsoft Office 2013 uses Windows Licensing Services
            bool mergedOfficeWindows = OfficeVersion.IsOfficeSPP();
            if (mergedOfficeWindows && licenses is LicenseListOffice)
            {
                throw new Exception("Cannot Restore Microsoft Office 2013 or Later on Windows 8 or Later!");
            }

            // Get Tokens Paths
            string tokensSourcePath = backupPath + backupName + "\\Files\\Tokens";
            string tokensDestinationPath;
            if (mergedOfficeWindows || licenses is LicenseListWindows)
            {
                tokensDestinationPath = GetTokensPath(OSVersion.GetWindowsName());
            }
            else
            {
                tokensDestinationPath = GetTokensPath(OfficeVersion.GetOfficeName());
            }
            string cacheDestinationPath = tokensDestinationPath + "\\Cache";

            using (StringWriter output = new StringWriter())
            {
                try
                {
                    // Prevent Invalid Backup Name
                    if (String.IsNullOrWhiteSpace(backupName))
                    {
                        output.WriteLine("License Restore Failed!");
                        output.WriteLine("You did not enter a backup name.");
                        return output.ToString();
                    }
                    if (Directory.Exists(backupPath + backupName) == false)
                    {
                        output.WriteLine("License Restore Failed!");
                        output.WriteLine("Your Backup " + backupName + " Was Not Found.");
                        return output.ToString();
                    }
                    if (File.Exists(backupPath + backupName + "\\Keys.ini") == false)
                    {
                        output.WriteLine("License Restore Failed!");
                        output.WriteLine("Cannot use Restore with Key Reinstall Without Saved Keys.ini!");
                        return output.ToString();
                    }

                    // Show Activation Errors if No Licenses or Keys Exist
                    if (licenses.GetListUnlicensed().Count == 0 && licenses.GetListLicensed().Count == 0)
                    {
                        output.WriteLine("License Restore Failed!");
                        output.WriteLine(LicenseErrorCode.ErrBroken);
                        return output.ToString();
                    }
                    if (licenses.GetListUnlicensed().Count > 0 && licenses.GetListLicensed().Count == 0)
                    {
                        output.WriteLine("License Restore Failed!");
                        output.WriteLine(LicenseErrorCode.ErrKeyless);
                        return output.ToString();
                    }

                    // Stop Licensing Service
                    if (mergedOfficeWindows == false && licenses is LicenseListOffice)
                    {
                        Services.StopOSPPSVC();
                    }
                    else
                    {
                        Services.StopSPPSVC();
                    }

                    // Create Failsafe Backup
                    output.WriteLine("Creating Failsafe Backup.");
                    output.WriteLine("Backing up Current Tokens.");
                    File.Copy(tokensDestinationPath + "\\tokens.dat", tokensDestinationPath + "\\tokens.dat.backup");
                    if (File.Exists(cacheDestinationPath + "\\cache.dat"))
                    {
                        File.Copy(cacheDestinationPath + "\\cache.dat", cacheDestinationPath + "\\cache.dat.backup");
                    }
                    if (File.Exists(tokensDestinationPath + "\\data.dat"))
                    {
                        File.Copy(tokensDestinationPath + "\\data.dat", tokensDestinationPath + "\\data.dat.backup");
                    }

                    // Save All Current Product Keys
                    output.WriteLine("Backing up Current Product Keys.");
                    List<string> currentProductKeys = new List<string>();
                    if (mergedOfficeWindows == false && licenses is LicenseListOffice)
                    {
                        currentProductKeys.AddRange(GetKeys(licenses));
                    }
                    else
                    {
                        currentProductKeys.AddRange(GetKeys(licenses));
                        if (mergedOfficeWindows)
                        {
                            currentProductKeys.AddRange(GetKeys(new LicenseListOffice()));
                        }
                    }

                    // Stop Licensing Service
                    if (mergedOfficeWindows == false && licenses is LicenseListOffice)
                    {
                        Services.StopOSPPSVC();
                    }
                    else
                    {
                        Services.StopSPPSVC();
                    }

                    // Replace Tokens (Upgrade Support)
                    CommonUtilities.FileDelete(tokensDestinationPath + "\\tokens.dat");
                    File.Copy(tokensSourcePath + "\\tokens.dat", tokensDestinationPath + "\\tokens.dat");
                    if (File.Exists(tokensDestinationPath + "\\data.dat"))
                    {
                        CommonUtilities.FileDelete(tokensDestinationPath + "\\data.dat");
                        File.Copy(tokensSourcePath + "\\data.dat", tokensDestinationPath + "\\data.dat");
                    }
					
					// Start Licensing Service
                    if (mergedOfficeWindows == false && licenses is LicenseListOffice)
                    {
                        Services.StartOSPPSVC();
                    }
                    else
                    {
                        Services.StartSPPSVC();
                    }

                    // Reinstall Product Keys
                    output.WriteLine("Installing Product Keys from Backup.");
                    using (StreamReader keyFile = new StreamReader(backupPath + backupName + "\\Keys.ini"))
                    {
                        string key;
                        while ((key = keyFile.ReadLine()) != null)
                        {
                            try
                            {
                                output.WriteLine("Installing: " + key);
                                // Install Product Keys
                                if (mergedOfficeWindows == false && licenses is LicenseListOffice)
                                {
                                    KeyInstaller.InstallKeyOffice(key);
                                }
                                else
                                {
                                    KeyInstaller.InstallKeyWindows(key);
                                }
                                output.WriteLine("<Product key installation successful>");
                            }
                            catch (Exception ex)
                            {
                                output.WriteLine(ex.Message);
                                output.WriteLine("Failed to Install Product Keys from Backup.");
                                output.WriteLine("Restoring Previous Tokens.");

                                // Stop Licensing Service
                                if (mergedOfficeWindows == false && licenses is LicenseListOffice)
                                {
                                    Services.StopOSPPSVC();
                                }
                                else
                                {
                                    Services.StopSPPSVC();
                                }

                                // Replace Tokens
                                CommonUtilities.FileDelete(tokensDestinationPath + "\\tokens.dat");
                                File.Copy(tokensDestinationPath + "\\tokens.dat.backup", tokensDestinationPath + "\\tokens.dat");
                                if (File.Exists(cacheDestinationPath + "\\cache.dat.backup"))
                                {
                                    CommonUtilities.FileDelete(cacheDestinationPath + "\\cache.dat");
                                    File.Copy(cacheDestinationPath + "\\cache.dat.backup", cacheDestinationPath + "\\cache.dat");
                                }
                                if (File.Exists(tokensDestinationPath + "\\data.dat.backup"))
                                {
                                    CommonUtilities.FileDelete(tokensDestinationPath + "\\data.dat");
                                    File.Copy(tokensDestinationPath + "\\data.dat.backup", tokensDestinationPath + "\\data.dat");
                                }

                                // Throw Exception to End
                                throw new Exception("Failed to Install Product Keys from the License Backup!");
                            }
                        }
                    }

                    // Stop Licensing Service
                    if (mergedOfficeWindows == false && licenses is LicenseListOffice)
                    {
                        Services.StopOSPPSVC();
                    }
                    else
                    {
                        Services.StopSPPSVC();
                    }

                    // Replace Tokens Again (Phone Activation Support)
                    CommonUtilities.FileDelete(tokensDestinationPath + "\\tokens.dat");
                    File.Copy(tokensSourcePath + "\\tokens.dat", tokensDestinationPath + "\\tokens.dat");
                    if (File.Exists(tokensDestinationPath + "\\data.dat"))
                    {
                        CommonUtilities.FileDelete(tokensDestinationPath + "\\data.dat");
                        File.Copy(tokensSourcePath + "\\data.dat", tokensDestinationPath + "\\data.dat");
                    }

                    // Start Licensing Service
                    if (mergedOfficeWindows == false && licenses is LicenseListOffice)
                    {
                        Services.StartOSPPSVC();
                    }
                    else
                    {
                        Services.StartSPPSVC();
                    }

                    // Check If Successful Restore
                    licenses.Refresh();
                    if (IsRestoreSuccessful(licenses) == false)
                    {
                        output.WriteLine("Detected that the Restore Failed! Attempting to Reverse Changes.");

                        // Reinstall Product Keys
                        foreach (string currentProductKey in currentProductKeys)
                        {
                            try
                            {
                                output.WriteLine("Installing: " + currentProductKey);
                                // Install Product Keys
                                if (mergedOfficeWindows == false && licenses is LicenseListOffice)
                                {
                                    KeyInstaller.InstallKeyOffice(currentProductKey);
                                }
                                else
                                {
                                    KeyInstaller.InstallKeyWindows(currentProductKey);
                                }
                                output.WriteLine("<Product key installation successful>");
                            }
                            catch (Exception ex)
                            {
                                output.WriteLine(ex.Message);
                            }
                        }

                        // Stop Licensing Service
                        if (mergedOfficeWindows == false && licenses is LicenseListOffice)
                        {
                            Services.StopOSPPSVC();
                        }
                        else
                        {
                            Services.StopSPPSVC();
                        }

                        // Replace Tokens
                        CommonUtilities.FileDelete(tokensDestinationPath + "\\tokens.dat");
                        File.Copy(tokensDestinationPath + "\\tokens.dat.backup", tokensDestinationPath + "\\tokens.dat");
                        if (File.Exists(cacheDestinationPath + "\\cache.dat.backup"))
                        {
                            CommonUtilities.FileDelete(cacheDestinationPath + "\\cache.dat");
                            File.Copy(cacheDestinationPath + "\\cache.dat.backup", cacheDestinationPath + "\\cache.dat");
                        }
                        if (File.Exists(tokensDestinationPath + "\\data.dat.backup"))
                        {
                            CommonUtilities.FileDelete(tokensDestinationPath + "\\data.dat");
                            File.Copy(tokensDestinationPath + "\\data.dat.backup", tokensDestinationPath + "\\data.dat");
                        }

                        // Start Licensing Service
                        if (mergedOfficeWindows == false && licenses is LicenseListOffice)
                        {
                            Services.StartOSPPSVC();
                        }
                        else
                        {
                            Services.StartSPPSVC();
                        }

                        // Check If Failsafe Backup Restored Properly
                        licenses.Refresh();
                        if (IsRestoreSuccessful(licenses) == false)
                        {
                            output.WriteLine("Data Before Trying Restore Also Failed To Restore!" + Environment.NewLine + "Your License Data May Have Been Corrupted/Marked As Tampered!");
                        }
                        else
                        {
                            output.WriteLine("Data Before Trying Restore Has Been Restored!" + Environment.NewLine + "Your System Is The Same As It Was Before Trying To Restore!");
                        }
                    }
                    else
                    {
                        // Restore Registration Key
                        if (mergedOfficeWindows || licenses is LicenseListOffice)
                        {
                            //RestoreOfficeRegistration(OfficeVersion.GetOfficeNumber(), backupPath, backupName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    output.WriteLine("License Restore Failed!");
                    output.Write(ex.Message);
                }
                finally
                {
                    // Delete Failsafe Backup
                    CommonUtilities.FileDelete(tokensDestinationPath + "\\tokens.dat.backup");
                    if (File.Exists(cacheDestinationPath + "\\cache.dat.backup"))
                    {
                        CommonUtilities.FileDelete(cacheDestinationPath + "\\cache.dat.backup");
                    }
                    if (File.Exists(tokensDestinationPath + "\\data.dat.backup"))
                    {
                        CommonUtilities.FileDelete(tokensDestinationPath + "\\data.dat.backup");
                    }
                }
                if (output.ToString().Contains("Failed") == false)
                {
                    output.WriteLine("Restore Complete. Check Activation Status to see if it worked.");
                }
                
                return output.ToString();
            }
        }

        /// <summary>
        /// Restore Tokens and other Data to Restore Activation and License State.
        /// </summary>
        /// <param name="backupName">Name of the Backup Folder.</param>
        /// <param name="backupPath">Path where the Backup will be saved.</param>
        /// <returns>Output of Restore Process</returns>
        public static string RestoreOfficeTrustedStore(string backupName, string backupPath)
        {
            // On Windows 8, Microsoft Office 2013 uses Windows Licensing Services
            bool mergedOfficeWindows = OfficeVersion.IsOfficeSPP();
            if (mergedOfficeWindows)
            {
                throw new Exception("Cannot Restore Microsoft Office 2013 or Later on Windows 8 or Later!");
            }

            // Get Tokens Paths
            string tokensSourcePath = backupPath + backupName + "\\Files\\Tokens";
            string tokensDestinationPath = GetTokensPath(OfficeVersion.GetOfficeName());
            string cacheSourcePath = tokensSourcePath + "\\Cache";
            string cacheDestinationPath = tokensDestinationPath + "\\Cache";

            using (StringWriter output = new StringWriter())
            {
                try
                {
                    // Prevent Invalid Backup Name
                    if (String.IsNullOrWhiteSpace(backupName))
                    {
                        output.WriteLine("License Restore Failed!");
                        output.WriteLine("You did not enter a backup name.");
                        return output.ToString();
                    }
                    if (Directory.Exists(backupPath + backupName) == false)
                    {
                        output.WriteLine("License Restore Failed!");
                        output.WriteLine("Your Backup " + backupName + " Was Not Found.");
                        return output.ToString();
                    }

                    // Show Activation Errors if No Licenses or Keys Exist
                    LicenseList licenses = new LicenseListOffice();
                    if (licenses.GetListUnlicensed().Count == 0 && licenses.GetListLicensed().Count == 0)
                    {
                        output.WriteLine("License Restore Failed!");
                        output.WriteLine(LicenseErrorCode.ErrBroken);
                        return output.ToString();
                    }
                    if (licenses.GetListUnlicensed().Count > 0 && licenses.GetListLicensed().Count == 0)
                    {
                        output.WriteLine("License Restore Failed!");
                        output.WriteLine(LicenseErrorCode.ErrKeyless);
                        return output.ToString();
                    }

                    // Stop Licensing Service
                    Services.StopOSPPSVC();

                    // Create Failsafe Backup
                    File.Copy(tokensDestinationPath + "\\tokens.dat", tokensDestinationPath + "\\tokens.dat.backup");
                    File.Copy(cacheDestinationPath + "\\cache.dat", cacheDestinationPath + "\\cache.dat.backup");
                    CommonUtilities.ExecuteCommand("REG SAVE HKLM\\SOFTWARE\\Microsoft\\OfficeSoftwareProtectionPlatform " + CommonUtilities.EscapePath(tokensDestinationPath + "\\OfficeSPPInfo.hiv"), true);

                    // Replace Tokens and Cache
                    CommonUtilities.FileDelete(tokensDestinationPath + "\\tokens.dat");
                    File.Copy(tokensSourcePath + "\\tokens.dat", tokensDestinationPath + "\\tokens.dat");
                    CommonUtilities.FileDelete(cacheDestinationPath + "\\cache.dat");
                    File.Copy(cacheSourcePath + "\\cache.dat", cacheDestinationPath + "\\cache.dat");

                    // Replace Trusted Store
                    CommonUtilities.ExecuteCommand("REG DELETE HKLM\\SOFTWARE\\Microsoft\\OfficeSoftwareProtectionPlatform /f", true);
                    CommonUtilities.ExecuteCommand("REG ADD HKLM\\SOFTWARE\\Microsoft\\OfficeSoftwareProtectionPlatform /f", true);
                    CommonUtilities.ExecuteCommand("REG RESTORE HKLM\\SOFTWARE\\Microsoft\\OfficeSoftwareProtectionPlatform " + CommonUtilities.EscapePath(backupPath + backupName + "\\Registry\\" + "OfficeSPPInfo.hiv"), true);

                    // Start Licensing Service
                    Services.StartOSPPSVC();

                    // Check If Successful Restore
                    licenses.Refresh();
                    if (licenses.GetListLicensed().Count == 0 && licenses.GetListUnlicensed().Count == 0)
                    {
                        // Try To Restore Temporary Backup
                        output.WriteLine("License Restore Failed! Attempting to Reverse Changes.");

                        // Stop Licensing Service
                        Services.StopOSPPSVC();

                        // Replace Trusted Store
                        CommonUtilities.ExecuteCommand("REG DELETE HKLM\\SOFTWARE\\Microsoft\\OfficeSoftwareProtectionPlatform /f", true);
                        CommonUtilities.ExecuteCommand("REG ADD HKLM\\SOFTWARE\\Microsoft\\OfficeSoftwareProtectionPlatform /f", true);
                        CommonUtilities.ExecuteCommand("REG RESTORE HKLM\\SOFTWARE\\Microsoft\\OfficeSoftwareProtectionPlatform " + CommonUtilities.EscapePath(tokensDestinationPath + "\\OfficeSPPInfo.hiv"), true);

                        // Replace Tokens             
                        CommonUtilities.FileDelete(tokensDestinationPath + "\\tokens.dat");
                        File.Copy(tokensDestinationPath + "\\tokens.dat.backup", tokensDestinationPath + "\\tokens.dat");
                        // Replace Cache
                        CommonUtilities.FileDelete(cacheDestinationPath + "\\cache.dat");
                        File.Copy(cacheDestinationPath + "\\cache.dat.backup", cacheDestinationPath + "\\cache.dat");

                        // Start Licensing Service
                        Services.StartOSPPSVC();

                        // Check If Temporary Backup Restored Properly
                        licenses.Refresh();
                        if (licenses.GetListLicensed().Count == 0 && licenses.GetListUnlicensed().Count == 0)
                        {
                            output.WriteLine("Data Before Trying Restore Also Failed To Restore!" + Environment.NewLine + "Your License Data May Have Been Corrupted/Marked As Tampered!");
                        }
                        else
                        {
                            if (licenses.GetListLicensed().Count == 0 && licenses.GetListUnlicensed().Count > 0)
                            {
                                output.WriteLine("Data Before Trying Restore Has Been Restored!" + Environment.NewLine + "Your may need to reinstall your product keys to completely reverse the changes of the failed restore!");
                            }
                            else
                            {
                                output.WriteLine("Data Before Trying Restore Has Been Restored!" + Environment.NewLine + "Your System Is The Same As It Was Before Trying To Restore!");
                            }
                        }
                    }
                    else
                    {
                        // Restore Registration Key
                        //RestoreOfficeRegistration(OfficeVersion.GetOfficeNumber(), backupPath, backupName);
                    }
                }
                catch (Exception ex)
                {
                    if (output.ToString().Contains("Failed") == false)
                    {
                        output.WriteLine("License Restore Failed!");
                    }
                    output.WriteLine(ex.Message);
                }
                finally
                {
                    // Delete Failsafe Backup
                    CommonUtilities.FileDelete(tokensDestinationPath + "\\tokens.dat.backup");
                    CommonUtilities.FileDelete(cacheDestinationPath + "\\cache.dat.backup");
                    CommonUtilities.FileDelete(tokensDestinationPath + "\\OfficeSPPInfo.hiv");
                }
                if (output.ToString().Contains("Failed") == false)
                {
                    output.WriteLine("Restore Complete. Check Activation Status to see if it worked.");
                }
                return output.ToString();
            }
        }

        /// <summary>
        /// Check if License State indicates a Valid License Restore
        /// </summary>
        /// <param name="licenses">List of All Licenses</param>
        /// <returns>True if License Restore is Successful, False if Licensing is Broken or Out of Tolerance</returns>
        private static bool IsRestoreSuccessful(LicenseList licenses)
        {
            // Return False if No Licenses or Keys Exist
            if (licenses.GetListUnlicensed().Count == 0 && licenses.GetListLicensed().Count == 0)
            {
                return false;
            }
            if (licenses.GetListUnlicensed().Count > 0 && licenses.GetListLicensed().Count == 0)
            {
                return false;
            }

            // If a License has one of these error codes, we consider the Restore to have failed.
            List<string> failedErrorCodes = new List<string>{"0xC004F00F", "0x4004F00D"};
            foreach (LicenseInstance license in licenses.GetListFull())
            {
                // Check Failed Error Codes List
                if (failedErrorCodes.Contains(license.LicenseCode))
                {
                    return false;
                }
                // Check if Out of Tolerance
                if (license.LicenseStatus == "Out-of-Tolerance Grace Period")
                {
                    return false;
                }
                // Check if Marked as Tampered
                if (license.LicenseCode == "0xC004FE00")
                {
                    // We will only allow this restore if Microsoft Office 2013 or later on Windows 8 or later is Activated.
                    // In this case, the user restored Microsoft Office 2013 successfully on a different OS SKU.
                    if (OfficeVersion.IsOfficeSPP())
                    {
                        LicenseList officeLicenses = new LicenseListOffice();
                        foreach (LicenseInstance officeLicense in officeLicenses.GetListFull())
                        {
                            if (officeLicense.LicenseStatus == "Activated")
                            {
                                DialogResult result = MessageBox.Show(BackupBase.Window, "You have successfully restored Microsoft Office activation, but on a different OS Edition. This has caused your OS to lose Activation.\n\nIf you activated Windows via KMS, you can simply reactivate. Otherwise, if your Windows was activated with a limited key, you may not want to lose your Windows activation.\n\nIf you want to allow keeping Microsoft Office activated and losing the Windows activation, press OK. Otherwise, press Cancel and your system will be reverted to the way it was before you pressed Restore.", "Allow Partially Successful Restore?", MessageBoxButtons.OKCancel);
                                if (result == DialogResult.OK)
                                {
                                    return true;
                                }
                                return false;
                            }
                        }
                    }
                    return false;
                }
            }
            return true;
        }

        /*
        /// <summary>
        /// Save Microsoft Office Registration Key
        /// </summary>
        /// <param name="officeNumber">Microsoft Office Platform version number</param>
        /// <param name="backupPath">Path where the Backup will be saved.</param>
        /// <param name="backupName">Name of the Backup Folder.</param>
        private static void BackupOfficeRegistration(int officeNumber, string backupPath, string backupName)
        {       
            string registration32 = CommonUtilities.EscapePath(backupPath + backupName + "\\Registry\\" + "Registration32.reg");
            string registration64 = CommonUtilities.EscapePath(backupPath + backupName + "\\Registry\\" + "Registration64.reg");
            string registrationWOW = CommonUtilities.EscapePath(backupPath + backupName + "\\Registry\\" + "RegistrationWOW.reg");
            string architecture = Architecture.GetOfficeArch();
            if (architecture != Architecture.WOW)
            {
                if (!OfficeVersion.IsOfficeVirtual())
                {
                    CommonUtilities.ExecuteCommand("REG EXPORT HKLM\\SOFTWARE\\Microsoft\\Office\\" + officeNumber + ".0\\Registration " + registration32, true);
                    CommonUtilities.ExecuteCommand("REG EXPORT HKLM\\SOFTWARE\\Microsoft\\Office\\" + officeNumber + ".0\\Registration " + registration64, true);
                    CommonUtilities.ExecuteCommand("REG EXPORT HKLM\\SOFTWARE\\Microsoft\\Office\\" + officeNumber + ".0\\Registration " + registrationWOW, true);
                }
                else
                {
                    CommonUtilities.ExecuteCommand("REG EXPORT HKLM\\SOFTWARE\\Microsoft\\Office\\ClickToRun\\REGISTRY\\MACHINE\\Software\\Microsoft\\Office\\" + officeNumber + ".0\\Registration " + registration32, true);
                    CommonUtilities.ExecuteCommand("REG EXPORT HKLM\\SOFTWARE\\Microsoft\\Office\\ClickToRun\\REGISTRY\\MACHINE\\Software\\Microsoft\\Office\\" + officeNumber + ".0\\Registration " + registration64, true);
                    CommonUtilities.ExecuteCommand("REG EXPORT HKLM\\SOFTWARE\\Microsoft\\Office\\ClickToRun\\REGISTRY\\MACHINE\\Software\\Microsoft\\Office\\" + officeNumber + ".0\\Registration " + registrationWOW, true);
                }        
            }
            else
            {
                if (!OfficeVersion.IsOfficeVirtual())
                {
                    CommonUtilities.ExecuteCommand("REG EXPORT HKLM\\SOFTWARE\\Wow6432Node\\Microsoft\\Office\\" + officeNumber + ".0\\Registration " + registration32, true);
                    CommonUtilities.ExecuteCommand("REG EXPORT HKLM\\SOFTWARE\\Wow6432Node\\Microsoft\\Office\\" + officeNumber + ".0\\Registration " + registration64, true);
                    CommonUtilities.ExecuteCommand("REG EXPORT HKLM\\SOFTWARE\\Wow6432Node\\Microsoft\\Office\\" + officeNumber + ".0\\Registration " + registrationWOW, true);
                }
                else
                {
                    CommonUtilities.ExecuteCommand("REG EXPORT HKLM\\SOFTWARE\\Microsoft\\Office\\ClickToRun\\REGISTRY\\MACHINE\\Software\\Wow6432Node\\Microsoft\\Office\\" + officeNumber + ".0\\Registration " + registration32, true);
                    CommonUtilities.ExecuteCommand("REG EXPORT HKLM\\SOFTWARE\\Microsoft\\Office\\ClickToRun\\REGISTRY\\MACHINE\\Software\\Wow6432Node\\Microsoft\\Office\\" + officeNumber + ".0\\Registration " + registration64, true);
                    CommonUtilities.ExecuteCommand("REG EXPORT HKLM\\SOFTWARE\\Microsoft\\Office\\ClickToRun\\REGISTRY\\MACHINE\\Software\\Wow6432Node\\Microsoft\\Office\\" + officeNumber + ".0\\Registration " + registrationWOW, true);
                }
            }

            // Convert Registration Key.
            registration32 = backupPath + backupName + "\\Registry\\" + "Registration32.reg";
            registration64 = backupPath + backupName + "\\Registry\\" + "Registration64.reg";
            registrationWOW = backupPath + backupName + "\\Registry\\" + "RegistrationWOW.reg";

            // Convert Click To Run to Traditional Format
            if (OfficeVersion.IsOfficeVirtual())
            {
                CommonUtilities.ReplaceTextInFile(registration32, @"Microsoft\\Office\\ClickToRun\\REGISTRY\\MACHINE\\Software\\", "");
                CommonUtilities.ReplaceTextInFile(registration64, @"Microsoft\\Office\\ClickToRun\\REGISTRY\\MACHINE\\Software\\", "");
                CommonUtilities.ReplaceTextInFile(registrationWOW, @"Microsoft\\Office\\ClickToRun\\REGISTRY\\MACHINE\\Software\\", "");
            }

            if (architecture == Architecture.X86)
            {
                // Convert x86 to x64.
                CommonUtilities.ReplaceTextInFile(registration64, "-0000-0000-", "-0000-1000-");
                // Convert x86 to WOW.
                CommonUtilities.ReplaceTextInFile(registrationWOW, @"SOFTWARE\\Microsoft", "SOFTWARE\\Wow6432Node\\Microsoft");
            }
            else if (architecture == Architecture.WOW)
            {
                // Convert WOW to x86.
                CommonUtilities.ReplaceTextInFile(registration32, @"SOFTWARE\\Wow6432Node", "SOFTWARE");
                // Convert WOW to x64.
                CommonUtilities.ReplaceTextInFile(registration64, @"SOFTWARE\\Wow6432Node", "SOFTWARE");
                CommonUtilities.ReplaceTextInFile(registration64, "-0000-0000-", "-0000-1000-");
            }
            else
            {
                // Convert x64 to x86.
                CommonUtilities.ReplaceTextInFile(registration32, "-0000-1000-", "-0000-0000-");
                // Convert x64 to WOW.
                CommonUtilities.ReplaceTextInFile(registrationWOW, "-0000-1000-", "-0000-0000-");
                CommonUtilities.ReplaceTextInFile(registrationWOW, @"SOFTWARE\\Microsoft", "SOFTWARE\\Wow6432Node\\Microsoft");
            }
        }

        /// <summary>
        /// Restore Microsoft Office Registration Key
        /// </summary>
        /// <param name="officeNumber">Microsoft Office Platform version number</param>
        /// <param name="backupPath">Path where the Backup will be saved.</param>
        /// <param name="backupName">Name of the Backup Folder.</param>
        private static void RestoreOfficeRegistration(int officeNumber, string backupPath, string backupName)
        {
            // Skip for Click To Run
            if (OfficeVersion.IsOfficeVirtual())
            {
                return;
            }

            // Restore Registration Key
            string registration32 = CommonUtilities.EscapePath(backupPath + backupName + "\\Registry\\" + "Registration32.reg");
            string registration64 = CommonUtilities.EscapePath(backupPath + backupName + "\\Registry\\" + "Registration64.reg");
            string registrationWOW = CommonUtilities.EscapePath(backupPath + backupName + "\\Registry\\" + "RegistrationWOW.reg");
            string architecture = Architecture.GetOfficeArch();

            if (architecture == Architecture.X86)
            {
                CommonUtilities.ExecuteCommand("REG DELETE HKLM\\SOFTWARE\\Microsoft\\Office\\" + officeNumber + ".0\\Registration /f", true);
                CommonUtilities.ExecuteCommand("regedit /s " + registration32, true);
            }
            else if (architecture == Architecture.WOW)
            {
                CommonUtilities.ExecuteCommand("REG DELETE HKLM\\SOFTWARE\\Wow6432Node\\Microsoft\\Office\\" + officeNumber + ".0\\Registration /f", true);
                CommonUtilities.ExecuteCommand("regedit /s " + registrationWOW, true);
            }
            else
            {
                CommonUtilities.ExecuteCommand("REG DELETE HKLM\\SOFTWARE\\Microsoft\\Office\\" + officeNumber + ".0\\Registration /f", true);
                CommonUtilities.ExecuteCommand("regedit /s " + registration64, true);
            }
        }
        */

        /// <summary>
        /// Get a List of all currently installed Product Keys
        /// </summary>
        /// <param name="licenses">List of all Licenses to obtain Product Keys for</param>
        /// <returns>String List of all Installed Product Keys</returns>
        private static List<string> GetKeys(LicenseList licenses)
        {
            // List of Full Keys
            List<string> fullKeyList = new List<string>();
            // List of Partial Keys
            List<string> partialKeyList = new List<string>();
            // List of Keys in Registry
            List<string> registryKeys = new List<string>();
			// List of Keys in MSDM
            List<string> msdmKeys = new List<string>();
            // List of All KMS Keys        
            List<string> kmsKeys = new List<string>();
            if (licenses is LicenseListOffice)
            {
				// Get Keys from Registry
                registryKeys.AddRange(KeyFinder.GetKeysFromRegistryOffice());
				
				// Get Keys from KeyLists
                kmsKeys.AddRange(KeyBase.GetApplicableKeysList(OfficeVersion.GetOfficeName()).GetKeys());
            }
            else
            {
				// Get Keys from Registry
                registryKeys.AddRange(KeyFinder.GetKeysFromRegistryWindows());
                registryKeys.AddRange(KeyFinder.GetDefaultKeysFromRegistryWindows());
				
				// Get Keys from KeyLists
                kmsKeys.AddRange(KeyBase.GetApplicableKeysList(OSVersion.GetWindowsName()).GetKeys());
				
				// Get Keys from MSDM
				msdmKeys.AddRange(KeyFinder.GetKeysFromMSDMWindows());
            }

            // Get All Partial Product Keys
            foreach (LicenseInstance license in licenses.GetListLicensed())
            {
                partialKeyList.Add(license.PartialProductKey);
            }

            // Get Full Keys for each Partial Key
            foreach (string partialKey in partialKeyList)
            {
                bool gotKey = false;
                // Eliminate KMS Keys
                foreach (string kmsKey in kmsKeys)
                {
                    if (kmsKey.Substring(kmsKey.Length - 5, 5) == partialKey)
                    {
                        fullKeyList.Add(kmsKey);
                        gotKey = true;
                        break;
                    }
                }
				
                // Eliminate Keys in Registry
                if (gotKey == false)
                {
                    foreach (string registryKey in registryKeys)
                    {
                        if (registryKey.Substring(registryKey.Length - 5, 5) == partialKey)
                        {
                            fullKeyList.Add(registryKey);
                            gotKey = true;
                            break;
                        }
                    }
                }
				
				// Eliminate Keys in MSDM
                if (gotKey == false)
                {
                    foreach (string msdmKey in msdmKeys)
                    {
                        if (msdmKey.Substring(msdmKey.Length - 5, 5) == partialKey)
                        {
                            fullKeyList.Add(msdmKey);
                            gotKey = true;
                            break;
                        }
                    }
                }
				
                // Ask For Key
                if (gotKey == false)
                {
                    // Show Key Prompt Which Sets KeyToSave
                    using (AskForKey askForKey = new AskForKey(partialKey))
                    {
                        // TODO Fix Threading
                        askForKey.ShowDialog(BackupBase.Window);
                    }

                    if (String.IsNullOrWhiteSpace(AskForKey.KeyToSave))
                    {
                        throw new ApplicationException("Failed to Obtain Full Product Key from User Input!");
                    }

                    fullKeyList.Add(AskForKey.KeyToSave);
                }
            }
            return fullKeyList;
        }

        /// <summary>
        /// Get Path to Tokens Files
        /// </summary>
        /// <param name="productType">Microsoft Windows or Office Product Edition</param>
        /// <returns>Path to Tokens Files</returns>
        private static string GetTokensPath(string productType)
        {
            string tokensPath;
            switch (productType)
            {
                case OSVersion.WinServer2008:
                case OSVersion.WinVista:
                    tokensPath = Environment.GetEnvironmentVariable("windir") + @"\ServiceProfiles\NetworkService\AppData\Roaming\Microsoft\SoftwareLicensing";
                    break;
                case OSVersion.WinServer2008R2:
                case OSVersion.Win7:
                case OSVersion.Win7Embedded:
                    tokensPath = Environment.GetEnvironmentVariable("windir") + @"\ServiceProfiles\NetworkService\AppData\Roaming\Microsoft\SoftwareProtectionPlatform";
                    break;
                case OSVersion.WinServer2012:
                case OSVersion.Win8:
                case OSVersion.Win8Embedded:
                    if (Architecture.GetOSArch() == Architecture.X64 && !Environment.Is64BitProcess)
                    {
                        tokensPath = Environment.GetEnvironmentVariable("windir") + @"\Sysnative\spp\store";
                    }
                    else
                    {
                        tokensPath = Environment.GetEnvironmentVariable("windir") + @"\System32\spp\store";
                    }
                    break;
                case OSVersion.WinServer2012R2:
                case OSVersion.Win81:
                case OSVersion.Win81Embedded:
                case OSVersion.WinServer2016:
                case OSVersion.Win10:
                case OSVersion.Win10Embedded:
                case OSVersion.WinServer2019:
                    if (Architecture.GetOSArch() == Architecture.X64 && !Environment.Is64BitProcess)
                    {
                        tokensPath = Environment.GetEnvironmentVariable("windir") + @"\Sysnative\spp\store\2.0";
                    }
                    else
                    {
                        tokensPath = Environment.GetEnvironmentVariable("windir") + @"\System32\spp\store\2.0";
                    }
                    break;
                case OfficeVersion.Office2010:
                case OfficeVersion.Office2013:
                case OfficeVersion.Office2016:
                case OfficeVersion.Office2019:
                    tokensPath = Environment.GetEnvironmentVariable("ALLUSERSPROFILE") + "\\Microsoft\\OfficeSoftwareProtectionPlatform";
                    break;
                default:
                    throw new Exception("Could not Find Tokens Path!");
            }
            // Create Directory if it has been deleted
            if (Directory.Exists(tokensPath) == false)
            {
                Directory.CreateDirectory(tokensPath);
            }
            return tokensPath;
        }
    }
}