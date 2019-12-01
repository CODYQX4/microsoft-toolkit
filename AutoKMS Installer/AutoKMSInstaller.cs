using System;
using System.Collections.Generic;
using System.IO;
using Common;
using Microsoft.Win32.TaskScheduler;
using ProductDetection;

namespace AutoKMSInstaller
{
    /// <summary>
    /// Group of Methods for installing and uninstalling AutoKMS
    /// </summary>
    public static class AutoKMSInstaller
    {
        /// <summary>
        /// Directory where AutoKMS is installed
        /// </summary>
        public static string AutoKMSInstallPath;

        #region AutoKMS Names Constants
        public const string AutoKMSFileName = "AutoKMS.exe";
        public const string AutoKMSProcessName = "AutoKMS";
        public const string AutoKMSLogFileName = "AutoKMS.log";
        public const string AutoKMSCustomTaskName = "AutoKMSCustom";
        public const string AutoKMSCustomTaskInstallerName = "AutoKMS Custom Task";
        #endregion

        /// <summary>
        /// Check if AutoKMS is installed by checking if the Scheduled Task is installed
        /// </summary>
        /// <returns>True if the Scheduled Task exists, False if the Scheduled Task does not exist</returns>
        public static bool IsAutoKMSInstalled()
        {
            if (IsAutoKMSCustomTaskInstalled())
            {
                return true;
            }
            return Tasks.IsTaskInstalled(AutoKMSProcessName);
        }

        /// <summary>
        /// Check if AutoKMS Custom Scheduled Task is installed
        /// </summary>
        /// <returns>True if the Scheduled Task exists, False if the Scheduled Task does not exist</returns>
        public static bool IsAutoKMSCustomTaskInstalled()
        {
            return Tasks.IsTaskInstalled(AutoKMSCustomTaskName);
        }

        /// <summary>
        /// Create the AutoKMS Scheduled Task
        /// </summary>
        public static void MakeAutoKMSNormalTask()
        {
            DailyTrigger dt = new DailyTrigger();
            BootTrigger bt = new BootTrigger();
            LogonTrigger lt = new LogonTrigger();
            List<Trigger> triggerList = new List<Trigger> {dt, bt, lt};

            Tasks.CreateTask(AutoKMSInstallPath, AutoKMSFileName, AutoKMSProcessName, null, triggerList);
        }

        /// <summary>
        /// Create the AutoKMS Custom Scheduled Task
        /// </summary>
        public static void MakeAutoKMSCustomTask()
        {
            Tasks.CreateTask(AutoKMSInstallPath, AutoKMSFileName, AutoKMSCustomTaskName);
        }

        /// <summary>
        /// Delete the AutoKMS Scheduled Task
        /// </summary>
        public static void DeleteAutoKMSNormalTask()
        {
            Tasks.DeleteTask(AutoKMSProcessName);
        }

        /// <summary>
        /// Delete the AutoKMS Custom Scheduled Task
        /// </summary>
        public static void DeleteAutoKMSCustomTask()
        {
            Tasks.DeleteTask(AutoKMSCustomTaskName);
        }

        /// <summary>
        /// Get the Install Path of AutoKMS by reading Scheduled Task
        /// </summary>
        /// <returns>Directory where AutoKMS is installed</returns>
        public static string GetAutoKMSPath()
        {
            if (IsAutoKMSInstalled())
            {
                try
                {
                    // Read AutoKMS install path
                    string path = Path.GetDirectoryName(Tasks.GetTaskProgramPath(AutoKMSProcessName));
                    if (Directory.Exists(path))
                    {
                        return path;
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    // Read AutoKMSCustom install path
                    string path = Path.GetDirectoryName(Tasks.GetTaskProgramPath(AutoKMSCustomTaskName));
                    if (Directory.Exists(path))
                    {
                        return path;
                    }
                }
                catch (Exception)
                {
                }
            }
            throw new DirectoryNotFoundException("AutoKMS install path not found!");
        }

        /// <summary>
        /// Install AutoKMS and Setup Scheduled Task
        /// </summary>
        /// <param name="installPath">Path to write Program File</param>
        /// <param name="autoKMS">Byte Array representation of AutoKMS Program File </param>
        /// <returns>Output of Installation Process</returns>
        public static string InstallAutoKMS(string installPath, byte[] autoKMS)
        {
            using (StringWriter output = new StringWriter())
            {
                // Create Program File
                try
                {
                    try
                    {
                        AutoKMSInstallPath = installPath;

                        // Create Windows Defender Exclusion
                        if (OSVersion.GetWindowsNumber() >= 10.0)
                        {
                            CommonUtilities.ExecuteCommand("powershell.exe Add-MpPreference -ExclusionPath " + CommonUtilities.EscapePath(AutoKMSInstallPath) + " -Force", true);
                        }

                        // Create AutoKMS
                        CommonUtilities.FileCreate(AutoKMSFileName, autoKMS, AutoKMSInstallPath);
                        output.WriteLine("Created " + AutoKMSInstallPath + Path.DirectorySeparatorChar + AutoKMSFileName);
                    }
                    catch (Exception)
                    {
                        AutoKMSInstallPath = Environment.GetEnvironmentVariable("windir") + Path.DirectorySeparatorChar + AutoKMSProcessName;
                        output.WriteLine("Could not access AutoKMS path set in Settings, using default installation path.");
                        CommonUtilities.FileCreate(AutoKMSFileName, autoKMS, AutoKMSInstallPath);
                        output.WriteLine("Created " + AutoKMSInstallPath + Path.DirectorySeparatorChar + AutoKMSFileName);
                    }
                }
                catch (Exception)
                {
                    // Remove Windows Defender Exclusion
                    if (OSVersion.GetWindowsNumber() >= 10.0)
                    {
                        CommonUtilities.ExecuteCommand("powershell.exe Remove-MpPreference -ExclusionPath " + CommonUtilities.EscapePath(AutoKMSInstallPath) + " -Force", true);
                    }

                    output.WriteLine("Failed to create " + AutoKMSInstallPath + Path.DirectorySeparatorChar + AutoKMSFileName);
                    output.Write("AutoKMS failed to install!");
                    return output.ToString();
                }

                // Create Scheduled Tasks
                try
                {
                    MakeAutoKMSNormalTask();
                    output.WriteLine("Created AutoKMS Scheduled Task");
                }
                catch (Exception ex)
                {
                    output.WriteLine("Failed to create AutoKMS Scheduled Task");
                    output.WriteLine(ex.Message);
                    output.WriteLine("Cleaning up as AutoKMS is partially installed");

                    // Delete Program File
                    try
                    {
                        CommonUtilities.FileDelete(AutoKMSInstallPath + Path.DirectorySeparatorChar + AutoKMSFileName);
                        output.WriteLine("Deleted " + AutoKMSInstallPath + Path.DirectorySeparatorChar + AutoKMSFileName);
                    }
                    catch (Exception)
                    {
                        output.WriteLine("Failed to delete " + AutoKMSInstallPath + Path.DirectorySeparatorChar + AutoKMSFileName);
                    }

                    // Remove Windows Defender Exclusion
                    if (OSVersion.GetWindowsNumber() >= 10.0)
                    {
                        CommonUtilities.ExecuteCommand("powershell.exe Remove-MpPreference -ExclusionPath " + CommonUtilities.EscapePath(AutoKMSInstallPath) + " -Force", true);
                    }

                    output.Write("AutoKMS failed to install!");
                    return output.ToString();
                }

                output.Write("AutoKMS was succesfully installed.");
                return output.ToString();
            }
        }

        /// <summary>
        /// Replace AutoKMS Scheduled Task with Customizable Scheduled Task
        /// </summary>
        /// <returns>Output of Installation Process</returns>
        public static string InstallAutoKMSCustomTask()
        {
            using (StringWriter output = new StringWriter())
            {
                try
                {
                    // Set AutoKMS Path
                    AutoKMSInstallPath = GetAutoKMSPath();

                    DeleteAutoKMSNormalTask();
                    MakeAutoKMSCustomTask();

                    output.WriteLine("Created AutoKMS Custom Scheduled Task and deleted default task");
                    output.WriteLine("Open Task Scheduler and customize the triggers to your needs");
                }
                catch (Exception)
                {
                    output.WriteLine("Failed to create AutoKMS Custom Scheduled Task");
                }
                return output.ToString();
            }
        }

        /// <summary>
        /// Uninstall AutoKMS and Remove Scheduled Task
        /// </summary>
        /// <returns>Output of Uninstallation Process</returns>
        public static string UninstallAutoKMS()
        {
            using (StringWriter output = new StringWriter())
            {
                // Get Program File Path
                try
                {
                    AutoKMSInstallPath = GetAutoKMSPath();
                }
                catch (Exception)
                {
                    output.WriteLine("Failed to access AutoKMS installation path!");
                    return output.ToString();
                }

                // Delete Program File
                try
                {
                    CommonUtilities.FileDelete(AutoKMSInstallPath + Path.DirectorySeparatorChar + AutoKMSFileName);
                    output.WriteLine("Deleted " + AutoKMSInstallPath + Path.DirectorySeparatorChar + AutoKMSFileName);
                }
                catch (Exception)
                {
                    output.WriteLine("Failed to delete " + AutoKMSInstallPath + Path.DirectorySeparatorChar + AutoKMSFileName);
                }

                // Delete Program Log
                try
                {
                    CommonUtilities.FileDelete(AutoKMSInstallPath + Path.DirectorySeparatorChar + AutoKMSLogFileName);
                    output.WriteLine("Deleted " + AutoKMSInstallPath + Path.DirectorySeparatorChar + AutoKMSLogFileName);
                }
                catch (Exception)
                {
                    output.WriteLine("Failed to delete " + AutoKMSInstallPath + Path.DirectorySeparatorChar + AutoKMSLogFileName);
                }

                // Delete Scheduled Tasks
                try
                {
                    if (IsAutoKMSCustomTaskInstalled())
                    {
                        DeleteAutoKMSCustomTask();
                    }
                    else
                    {
                        DeleteAutoKMSNormalTask();
                    }
                    output.WriteLine("Deleted AutoKMS Scheduled Task");
                }
                catch (Exception)
                {
                    output.WriteLine("Failed to delete AutoKMS Scheduled Task");
                }

                // Remove Windows Defender Exclusion
                if (OSVersion.GetWindowsNumber() >= 10.0)
                {
                    CommonUtilities.ExecuteCommand("powershell.exe Remove-MpPreference -ExclusionPath " + CommonUtilities.EscapePath(AutoKMSInstallPath) + " -Force", true);
                }

                if (output.ToString().Contains("Failed"))
                {
                    output.Write("AutoKMS failed to uninstall!");
                }
                else
                {
                    // Delete Install Folder If Empty
                    CommonUtilities.DeleteEmptyFolders(AutoKMSInstallPath);

                    output.Write("AutoKMS was succesfully uninstalled.");
                }
                return output.ToString();
            }
        }

        /// <summary>
        /// Replace AutoKMS Custom Scheduled Task with Default Scheduled Task
        /// </summary>
        /// <returns>Output of Uninstallation Process</returns>
        public static string UninstallAutoKMSCustomTask()
        {
            using (StringWriter output = new StringWriter())
            {
                try
                {
                    // Set AutoKMS Path
                    AutoKMSInstallPath = GetAutoKMSPath();

                    DeleteAutoKMSCustomTask();
                    MakeAutoKMSNormalTask();
                    output.WriteLine("Deleted AutoKMS Custom Scheduled Task and created default task");
                }
                catch (Exception)
                {
                    output.WriteLine("Failed to delete AutoKMS Custom Scheduled Task");
                }
                return output.ToString();
            }
        }
    }
}