using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Activation;
using Backup;
using Common;
using License;
using ProductDetection;

namespace AutoRearm
{
    internal class AutoRearm
    {
        /// <summary>
        /// Stream to Log File
        /// </summary>
        private StreamWriter _log;

        /// <summary>
        /// Launch AutoRearm
        /// </summary>
        public void RunAutoRearm()
        {
            // Start Logging   
            string logPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + AutoRearmInstaller.AutoRearmInstaller.AutoRearmLogFileName;
      
            // Delete Log if Greater than 10MB
            FileInfo logFile = new FileInfo(logPath);
            if (logFile.Exists && logFile.Length >= 10485760)
            {
                CommonUtilities.FileDelete(logPath);
            }

            // Access Log
            using (_log = new StreamWriter(logPath, true))
            {
                // Get Lowest Grace Period
                if (OfficeVersion.IsOfficeSupported())
                {
                    // Rearm if we only have 5 days left
                    LicenseList licenses = new LicenseListOffice();
                    if (RearmBase.LowestGraceDays(licenses) < 6)
                    {
                        // Get the Program File Version
                        FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetEntryAssembly().Location);
                        WriteLine("------------------------------------" + Environment.NewLine + "AutoRearm " + myFileVersionInfo.FileVersion + " Ran At " + DateTime.Now + ".");

                        // Rearm if Possible, Restore Backup if Rearm Fails
                        WriteLine("Rearming Office because grace period is almost expired.");
                        if (RearmOffice.Rearm().Contains("failed"))
                        {
                            WriteLine("Failed to Rearm. Restoring License Backup.");

                            string restoreoutput = LicenseBackup.RestoreOfficeTrustedStore("AutoRearmBackup", Environment.CurrentDirectory + "\\Backups\\" + OfficeVersion.GetOfficeName() + Path.DirectorySeparatorChar);
                            if (restoreoutput.Contains("Failed") == false)
                            {
                                WriteLine("Successfully Restored License Backup. Rearming Office.");

                                // Wait and Cycle OSPPSVC
                                Services.StopOSPPSVC();
                                Thread.Sleep(2000);
                                Services.StartOSPPSVC();
                                Thread.Sleep(2000);
                                if (RearmOffice.Rearm().Contains("failed"))
                                {
                                    WriteLine("Failed to Rearm.");
                                }
                                else
                                {
                                    WriteLine("Successfully Rearmed.");
                                }
                            }
                        }
                        else
                        {
                            WriteLine("Successfully Rearmed.");
                        }
                    }
                }
                else
                {
                    WriteLine("Office Is Not Installed!");
                }
            }
            // Remake AutoRearm Tasks
            AutoRearmInstaller.AutoRearmInstaller.AutoRearmInstallPath = Environment.CurrentDirectory;
            //AutoRearmInstaller.AutoRearmInstaller.DeleteAutoRearmNormalTask();
            AutoRearmInstaller.AutoRearmInstaller.MakeAutoRearmNormalTask();
        }

        /// <summary>
        /// Write to the Log and to the Console
        /// </summary>
        /// <param name="text">String to Output</param>
        private void Write(string text)
        {
            _log.Write(text);
            Console.Write(text);
        }

        /// <summary>
        /// Write to the Log and to the Console and Append a New Line
        /// </summary>
        /// <param name="text">String to Output</param>
        private void WriteLine(string text)
        {
            Write(text + Environment.NewLine);
        }
    }
}