using System;
using System.Collections.Generic;
using System.IO;
using Activation;
using Backup;
using Common;
using License;
using Microsoft.Win32.TaskScheduler;
using ProductDetection;

namespace AutoRearmInstaller
{
    /// <summary>
    /// Group of Methods for installing and uninstalling AutoRearm
    /// </summary>
    public static class AutoRearmInstaller
    {
        /// <summary>
        /// Directory where AutoRearm is installed
        /// </summary>
        public static string AutoRearmInstallPath;

        #region AutoRearm and IORRT Names Constants
        public const string AutoRearmFileName = "AutoRearm.exe";
        public const string AutoRearmProcessName = "AutoRearm";
        public const string AutoRearmLogFileName = "AutoRearm.log";
        public const string IORRTProcessName = "IORRT";
        #endregion

        /// <summary>
        /// Check if AutoRearm is installed by checking if the Scheduled Task is installed
        /// </summary>
        /// <returns>True if the Scheduled Task exists, False if the Scheduled Task does not exist</returns>
        public static bool IsAutoRearmInstalled()
        {
            return Tasks.IsTaskInstalled(AutoRearmProcessName);
        }

        /// <summary>
        /// Check if IORRT is installed by checking if the Scheduled Task is installed
        /// </summary>
        /// <returns>True if the Scheduled Task exists, False if the Scheduled Task does not exist</returns>
        public static bool IsIORRTInstalled()
        {
            return Tasks.IsTaskInstalled(IORRTProcessName);
        }

        /// <summary>
        /// Create the AutoRearm Scheduled Task
        /// </summary>
        public static void MakeAutoRearmNormalTask()
        {
            DailyTrigger dt = new DailyTrigger();
            BootTrigger bt = new BootTrigger();
            LogonTrigger lt = new LogonTrigger();
            List<Trigger> triggerList = new List<Trigger> { dt, bt, lt };

            Tasks.CreateTask(AutoRearmInstallPath, AutoRearmFileName, AutoRearmProcessName, null, triggerList);
        }

        /// <summary>
        /// Delete the AutoRearm Scheduled Task
        /// </summary>
        public static void DeleteAutoRearmNormalTask()
        {
            Tasks.DeleteTask(AutoRearmProcessName);
        }

        /// <summary>
        /// Get the Install Path of AutoRearm by reading Scheduled Task
        /// </summary>
        /// <returns>Directory where AutoRearm is installed</returns>
        public static string GetAutoRearmPath()
        {
            if (IsAutoRearmInstalled())
            {
                try
                {
                    // Read AutoRearm install path
                    string path = Path.GetDirectoryName(Tasks.GetTaskProgramPath(AutoRearmProcessName));
                    if (Directory.Exists(path))
                    {
                        return path;
                    }
                }
                catch (Exception)
                {
                }
            }
            throw new DirectoryNotFoundException("AutoRearm install path not found!");
        }

        /// <summary>
        /// Install AutoRearm and Setup Scheduled Task
        /// </summary>
        /// <param name="installPath">Path to write Program File</param>
        /// <param name="autoRearm">Byte Array representation of AutoRearm Program File</param>
        /// <returns>Output of Installation Process</returns>
        public static string InstallAutoRearm(string installPath, byte[] autoRearm)
        {
            using (StringWriter output = new StringWriter())
            {
                LicenseList licenses = new LicenseListOffice();

                // Show Activation Errors if No Licenses or Keys Exist
                if (licenses.GetListUnlicensed().Count == 0 && licenses.GetListLicensed().Count == 0)
                {
                    output.WriteLine(LicenseErrorCode.ErrBroken);
                    return output.ToString();
                }
                if (licenses.GetListUnlicensed().Count > 0 && licenses.GetListLicensed().Count == 0)
                {
                    output.WriteLine(LicenseErrorCode.ErrKeyless);
                    return output.ToString();
                }

                // Get Rearm Count  
                try
                {
                    int rearmCount = RearmOffice.GetRearmCount();
                    if (rearmCount < 1)
                    {
                        output.Write("You must have at least 1 available rearm to use this tool." + Environment.NewLine + "You have " + rearmCount + " rearms.");
                        return output.ToString();
                    }
                    output.WriteLine("You have " + rearmCount + " rearms.");
                }
                catch (Exception)
                {
                    output.Write("Failed to check rearm count. AutoRearm cannot be installed");
                    return output.ToString();
                }

                // Create Program File
                try
                {
                    try
                    {
                        AutoRearmInstallPath = installPath;

                        // Create Windows Defender Exclusion
                        if (OSVersion.GetWindowsNumber() >= 10.0)
                        {
                            CommonUtilities.ExecuteCommand("powershell.exe Add-MpPreference -ExclusionPath " + CommonUtilities.EscapePath(AutoRearmInstallPath) + " -Force", true);
                        }

                        // Create AutoRearm
                        CommonUtilities.FileCreate(AutoRearmFileName, autoRearm, AutoRearmInstallPath);
                        output.WriteLine("Created " + AutoRearmInstallPath + Path.DirectorySeparatorChar + AutoRearmFileName);
                    }
                    catch (Exception)
                    {
                        AutoRearmInstallPath = Environment.GetEnvironmentVariable("windir") + Path.DirectorySeparatorChar + AutoRearmProcessName;
                        output.WriteLine("Could not access AutoRearm path set in Settings, using default installation path.");
                        CommonUtilities.FileCreate(AutoRearmFileName, autoRearm, AutoRearmInstallPath);
                        output.WriteLine("Created " + AutoRearmInstallPath + Path.DirectorySeparatorChar + AutoRearmFileName);
                    }
                }
                catch (Exception)
                {
                    // Remove Windows Defender Exclusion
                    if (OSVersion.GetWindowsNumber() >= 10.0)
                    {
                        CommonUtilities.ExecuteCommand("powershell.exe Remove-MpPreference -ExclusionPath " + CommonUtilities.EscapePath(AutoRearmInstallPath) + " -Force", true);
                    }

                    output.WriteLine("Failed to create " + AutoRearmInstallPath + Path.DirectorySeparatorChar + AutoRearmFileName);
                    output.Write("AutoRearm failed to install!");
                    return output.ToString();
                }

                // Create Scheduled Tasks
                try
                {
                    MakeAutoRearmNormalTask();
                    output.WriteLine("Created AutoRearm Scheduled Tasks");
                }
                catch (Exception ex)
                {
                    output.WriteLine("Failed to create AutoRearm Scheduled Tasks");
                    output.WriteLine(ex.Message);
                    output.WriteLine("Cleaning up as AutoRearm is partially installed");

                    // Delete Program File
                    try
                    {
                        CommonUtilities.FileDelete(AutoRearmInstallPath + Path.DirectorySeparatorChar + AutoRearmFileName);
                        output.WriteLine("Deleted " + AutoRearmInstallPath + Path.DirectorySeparatorChar + AutoRearmFileName);
                    }
                    catch (Exception)
                    {
                        output.WriteLine("Failed to delete " + AutoRearmInstallPath + Path.DirectorySeparatorChar + AutoRearmFileName);
                    }

                    // Remove Windows Defender Exclusion
                    if (OSVersion.GetWindowsNumber() >= 10.0)
                    {
                        CommonUtilities.ExecuteCommand("powershell.exe Remove-MpPreference -ExclusionPath " + CommonUtilities.EscapePath(AutoRearmInstallPath) + " -Force", true);
                    }

                    output.Write("AutoRearm failed to install!");
                    return output.ToString();
                }

                // Make License Backup
                try
                {
                    LicenseBackup.Backup("AutoRearmBackup", AutoRearmInstallPath + "\\Backups\\" + OfficeVersion.GetOfficeName() + Path.DirectorySeparatorChar, licenses);
                    output.WriteLine("Created License Backup Of Current Configuration");
                }
                catch (Exception)
                {
                    output.WriteLine("Failed to create License Backup Of Current Configuration");
                    output.WriteLine("Cleaning up as AutoRearm is partially installed");

                    // Delete Program File
                    try
                    {
                        CommonUtilities.FileDelete(AutoRearmInstallPath + Path.DirectorySeparatorChar + AutoRearmFileName);
                        output.WriteLine("Deleted " + AutoRearmInstallPath + Path.DirectorySeparatorChar + AutoRearmFileName);
                    }
                    catch (Exception)
                    {
                        output.WriteLine("Failed to delete " + AutoRearmInstallPath + Path.DirectorySeparatorChar + AutoRearmFileName);
                    }

                    // Delete Scheduled Tasks
                    try
                    {
                        DeleteAutoRearmNormalTask();
                        output.WriteLine("Deleted AutoRearm Scheduled Tasks");
                    }
                    catch (Exception)
                    {
                        output.WriteLine("Failed to delete AutoRearm Scheduled Tasks");
                    }

                    // Remove Windows Defender Exclusion
                    if (OSVersion.GetWindowsNumber() >= 10.0)
                    {
                        CommonUtilities.ExecuteCommand("powershell.exe Remove-MpPreference -ExclusionPath " + CommonUtilities.EscapePath(AutoRearmInstallPath) + " -Force", true);
                    }

                    output.Write("AutoRearm failed to install!");
                    return output.ToString();
                }

                output.Write("AutoRearm was succesfully installed.");
                return output.ToString();
            }
        }

        /// <summary>
        /// Uninstall AutoRearm and Remove Scheduled Task
        /// </summary>
        /// <returns>Output of Uninstallation Process</returns>
        public static string UninstallAutoRearm()
        {
            using (StringWriter output = new StringWriter())
            {
                // Get Program File Path
                try
                {
                    AutoRearmInstallPath = GetAutoRearmPath();
                }
                catch (Exception)
                {
                    output.WriteLine("Failed to access AutoRearm installation path!");
                    return output.ToString();
                }

                // Restore AutoRearm Backup
                try
                {
                    string restoreoutput = LicenseBackup.RestoreOfficeTrustedStore("AutoRearmBackup", AutoRearmInstallPath + "Backups\\" + OfficeVersion.GetOfficeName() + Path.DirectorySeparatorChar);

                    if (restoreoutput.Contains("Failed"))
                    {
                        throw new Exception();
                    }
                    output.WriteLine("Restored License Backup from when you installed AutoRearm");
                }
                catch (Exception)
                {
                    output.WriteLine("Failed to Restore License Backup from when you installed AutoRearm");
                }

                // Delete AutoRearm Backups folder
                try
                {
                    CommonUtilities.FolderDelete(AutoRearmInstallPath + "Backups\\" + OfficeVersion.GetOfficeName() + Path.DirectorySeparatorChar);
                    output.WriteLine("Deleted " + AutoRearmInstallPath + "Backups\\" + OfficeVersion.GetOfficeName() + Path.DirectorySeparatorChar);
                }
                catch (Exception)
                {
                    output.WriteLine("Failed to delete AutoRearm Backups directory");
                }

                // Delete Program File
                try
                {
                    CommonUtilities.FileDelete(AutoRearmInstallPath + Path.DirectorySeparatorChar + AutoRearmFileName);
                    output.WriteLine("Deleted " + AutoRearmInstallPath + Path.DirectorySeparatorChar + AutoRearmFileName);
                }
                catch (Exception)
                {
                    output.WriteLine("Failed to delete " + AutoRearmInstallPath + Path.DirectorySeparatorChar + AutoRearmFileName);
                }

                // Delete Program Log
                try
                {
                    CommonUtilities.FileDelete(AutoRearmInstallPath + Path.DirectorySeparatorChar + AutoRearmLogFileName);
                    output.WriteLine("Deleted " + AutoRearmInstallPath + Path.DirectorySeparatorChar + AutoRearmLogFileName);
                }
                catch (Exception)
                {
                    output.WriteLine("Failed to delete " + AutoRearmInstallPath + Path.DirectorySeparatorChar + AutoRearmLogFileName);
                }

                // Delete Scheduled Tasks
                try
                {
                    DeleteAutoRearmNormalTask();
                    output.WriteLine("Deleted AutoRearm Scheduled Tasks");
                }
                catch (Exception)
                {
                    output.WriteLine("Failed to delete AutoRearm Scheduled Tasks");
                }

                // Remove Windows Defender Exclusion
                if (OSVersion.GetWindowsNumber() >= 10.0)
                {
                    CommonUtilities.ExecuteCommand("powershell.exe Remove-MpPreference -ExclusionPath " + CommonUtilities.EscapePath(AutoRearmInstallPath) + " -Force", true);
                }

                if (output.ToString().Contains("Failed"))
                {
                    output.Write("AutoRearm failed to uninstall!");
                }
                else
                {
                    // Delete Install Folder If Empty
                    CommonUtilities.DeleteEmptyFolders(AutoRearmInstallPath + Path.DirectorySeparatorChar);

                    output.Write("AutoRearm was succesfully uninstalled.");
                }
                return output.ToString();
            }
        }
    }
}