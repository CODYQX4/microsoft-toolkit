using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Activation;
using AutoKMS.Properties;
using Common;
using License;
using ProductDetection;

namespace AutoKMS
{
    internal class AutoKMS
    {
        /// <summary>
        /// Stream to Log File
        /// </summary>
        private StreamWriter _log;

        /// <summary>
        /// Launch AutoKMS
        /// </summary>
        public void RunAutoKMS()
        {
            // Start Logging   
            string logPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + AutoKMSInstaller.AutoKMSInstaller.AutoKMSLogFileName;

            // Delete Log if Greater than 10MB
            FileInfo logFile = new FileInfo(logPath);
            if (logFile.Exists && logFile.Length >= 10485760)
            {
                CommonUtilities.FileDelete(logPath);
            }

            // Access Log
            using (_log = new StreamWriter(logPath, true))
            {
                WriteLine("----------------------------------------");

                // Get the Program File Version
                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetEntryAssembly().Location);
                WriteLine("AutoKMS " + myFileVersionInfo.FileVersion + " Ran At " + DateTime.Now + ".");

                // Check for Recent BSOD
                if (BSODTest() && Settings.Default.LocalHostBypassUseDLLInjection)
                {
                    // Log Early Termination
                    WriteLine("A recent unexpected shutdown or BSOD occured.");
                    WriteLine("AutoKMS will not continue until the next Scheduled run.");
                    WriteLine("----------------------------------------");

                    // Remake AutoKMS Tasks
                    if (AutoKMSInstaller.AutoKMSInstaller.IsAutoKMSCustomTaskInstalled() == false)
                    {
                        AutoKMSInstaller.AutoKMSInstaller.AutoKMSInstallPath = Environment.CurrentDirectory;
                        AutoKMSInstaller.AutoKMSInstaller.MakeAutoKMSNormalTask();
                    } 
                    return;
                }

                // EZ-Activator Check
                bool ezActivator = false;
                String[] arguments = Environment.GetCommandLineArgs();
                foreach (string argument in arguments)
                {
                    if (String.Compare(argument, "/EZActivator", StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        ezActivator = true;
                        break;
                    }
                }

                // Show Splash Screen
                Thread splashScreenThread = null;
                if (ezActivator)
                {
                    splashScreenThread = new Thread(ShowSplashScreen);
                    splashScreenThread.Start();
                }

                // Activate Microsoft Office
                if (OfficeVersion.IsOfficeSupported())
                {
                    // Only Activate KMS Licenses
                    LicenseList licenses = new LicenseListOffice();
                    if (!ezActivator)
                    {
                        licenses.List = licenses.GetListLicensed();
                        licenses.List = licenses.GetListKMS();
                    }
                    else
                    {
                        licenses.List = licenses.GetListKMS();
                    }

                    if (licenses.List.Count > 0)
                    {
                        // Handle KMS Server Service
                        if (Regex.IsMatch(Settings.Default.KMSServerOffice, @"^(127(\.\d+){1,3}|[0:]+1|localhost)$") || String.Compare(Settings.Default.KMSServerOffice, Environment.MachineName, StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            if (KMSServerServiceInstaller.KMSServerServiceInstaller.IsKMSServerServiceInstalled())
                            {
                                Services.StartService(KMSServerServiceInstaller.KMSServerServiceInstaller.KMSServerServiceName);
                                Settings.Default.UseKMSEmulator = false;
                                WriteLine("Using KMS Server Service.");
                            }
                            else if (Settings.Default.UseKMSEmulator)
                            {
                                WriteLine("Using built-in KMS Server Emulator.");
                            }
                            else
                            {
                                WriteLine("WARNING: Attempting LocalHost KMS Activation without KMS Server Service or built-in KMS Server Emulator!");
                            }
                        }
                        else
                        {
                            WriteLine("Using KMS Server over the Network.");
                        }

                        if (!ezActivator)
                        {
                            WriteLine("Attempting To Activate Microsoft Office");
                            Write(ActivationState.AttemptActivationOffice(licenses, true, Settings.Default.KMSServerOffice, Settings.Default.KMSPortOffice, Settings.Default.KMSPIDOffice, Settings.Default.KMSHardwareID, Settings.Default.UseKMSEmulator, Settings.Default.RemoveKMSConnection, Settings.Default.KillProcessOnPort, Settings.Default.LocalHostBypassUseDLLInjection, Settings.Default.LocalHostBypassUseTAPAdapter, Settings.Default.LocalHostBypassUseWinDivertClient, Settings.Default.LocalHostBypassIPAddress, Settings.Default.LocalHostBypassIPSubnet));
                            WriteLine(string.Empty);
                        }
                        else
                        {
                            Write(EZActivator.RunEZActivator(licenses, true, Settings.Default.KMSServerOffice, Settings.Default.KMSPortOffice, Settings.Default.KMSPIDOffice, Settings.Default.KMSHardwareID, Settings.Default.UseKMSEmulator, Settings.Default.RemoveKMSConnection, Settings.Default.KillProcessOnPort, Settings.Default.LocalHostBypassUseDLLInjection, Settings.Default.LocalHostBypassUseTAPAdapter, Settings.Default.LocalHostBypassUseWinDivertClient, Settings.Default.LocalHostBypassIPAddress, Settings.Default.LocalHostBypassIPSubnet));
                            WriteLine(string.Empty);
                        }
                    }
                }
                // Activate Microsoft Windows
                if (OSVersion.IsWindowsSupported())
                {
                    // Only Activate KMS Licenses
                    LicenseList licenses = new LicenseListWindows();
                    if (!ezActivator)
                    {
                        licenses.List = licenses.GetListLicensed();
                        licenses.List = licenses.GetListKMS();
                    }
                    else
                    {
                        licenses.List = licenses.GetListKMS();
                    }

                    if (licenses.List.Count > 0)
                    {
                        // Handle KMS Server Service
                        if (Regex.IsMatch(Settings.Default.KMSServerWindows, @"^(127(\.\d+){1,3}|[0:]+1|localhost)$") || String.Compare(Settings.Default.KMSServerWindows, Environment.MachineName, StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            if (KMSServerServiceInstaller.KMSServerServiceInstaller.IsKMSServerServiceInstalled())
                            {
                                Services.StartService(KMSServerServiceInstaller.KMSServerServiceInstaller.KMSServerServiceName);
                                Settings.Default.UseKMSEmulator = false;
                                WriteLine("Using KMS Server Service.");
                            }
                            else if (Settings.Default.UseKMSEmulator)
                            {
                                WriteLine("Using built-in KMS Server Emulator.");
                            }
                            else
                            {
                                WriteLine("WARNING: Attempting LocalHost KMS Activation without KMS Server Service or built-in KMS Server Emulator!");
                            }
                        }
                        else
                        {
                            WriteLine("Using KMS Server over the Network.");
                        }

                        if (!ezActivator)
                        {
                            WriteLine("Attempting To Activate Microsoft Windows");
                            Write(ActivationState.AttemptActivationWindows(licenses, true, Settings.Default.KMSServerWindows, Settings.Default.KMSPortWindows, Settings.Default.KMSPIDWindows, Settings.Default.KMSHardwareID, Settings.Default.UseKMSEmulator, Settings.Default.RemoveKMSConnection, Settings.Default.KillProcessOnPort, Settings.Default.LocalHostBypassUseDLLInjection, Settings.Default.LocalHostBypassUseTAPAdapter, Settings.Default.LocalHostBypassUseWinDivertClient, Settings.Default.LocalHostBypassIPAddress, Settings.Default.LocalHostBypassIPSubnet));
                            WriteLine(string.Empty);
                        }
                        else
                        {
                            Write(EZActivator.RunEZActivator(licenses, true, Settings.Default.KMSServerWindows, Settings.Default.KMSPortWindows, Settings.Default.KMSPIDWindows, Settings.Default.KMSHardwareID, Settings.Default.UseKMSEmulator, Settings.Default.RemoveKMSConnection, Settings.Default.KillProcessOnPort, Settings.Default.LocalHostBypassUseDLLInjection, Settings.Default.LocalHostBypassUseTAPAdapter, Settings.Default.LocalHostBypassUseWinDivertClient, Settings.Default.LocalHostBypassIPAddress, Settings.Default.LocalHostBypassIPSubnet));
                            WriteLine(string.Empty);
                        }
                    }
                }

                // Hide Splash Screen
                if (ezActivator)
                {
                    splashScreenThread.Abort();
                }
            }

            // Remake AutoKMS Tasks
            if (AutoKMSInstaller.AutoKMSInstaller.IsAutoKMSCustomTaskInstalled() == false)
            {
                AutoKMSInstaller.AutoKMSInstaller.AutoKMSInstallPath = Environment.CurrentDirectory;
                AutoKMSInstaller.AutoKMSInstaller.MakeAutoKMSNormalTask();
            }            
        }

        /// <summary>
        /// Check Event Log for Unexpected Shutdowns. Skip running AutoKMS once if a BSOD occurred to prevent infinite boot loops.
        /// </summary>
        /// <returns>True if the last logged BSOD occurred close to the run of AutoKMS, False if AutoKMS is guessed to not have caused the BSOD or no BSOD is found.</returns>
        private bool BSODTest()
        {
            DateTime lastRun = new DateTime();
            DateTime lastBSOD = new DateTime();

            // Get Last Task Run Time
            if (AutoKMSInstaller.AutoKMSInstaller.IsAutoKMSInstalled())
            {
                if (AutoKMSInstaller.AutoKMSInstaller.IsAutoKMSCustomTaskInstalled() == false)
                {
                    lastRun = Tasks.GetTaskLastRunTime(AutoKMSInstaller.AutoKMSInstaller.AutoKMSProcessName);
                }
                else
                {
                    lastRun = Tasks.GetTaskLastRunTime(AutoKMSInstaller.AutoKMSInstaller.AutoKMSCustomTaskName);
                }
            }
            if (lastRun == DateTime.MinValue)
            {
                return false;
            }

            // Get System Events Last BSOD Time
            if (Services.IsServiceDisabled("EventLog"))
            {
                Services.EnableService("EventLog", 2);
                Services.StartService("EventLog");
            }
            using (EventLog myLog = new EventLog("System"))
            {
                foreach (EventLogEntry entry in myLog.Entries)
                {
                    // Check for Unexpected Shutdown
                    int eventID = (int)(entry.InstanceId & 0x3fff);
                    if (eventID == 6008)
                    {
                        // Get Time Written
                        DateTime eventTime = entry.TimeWritten;
                        if (eventTime > lastBSOD)
                        {
                            lastBSOD = eventTime;
                        }
                    }
                }
            }
            if (lastBSOD == DateTime.MinValue)
            {
                return false;
            }

            // Check if BSOD Occured within 30 minutes of Starting AutoKMS Task
            TimeSpan time = lastBSOD.Subtract(lastRun);
            if (Math.Abs(time.TotalMinutes) < 30)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Show a warning Splash Scrren when run via Command Line to prevent selling of pre-activated Microsoft Software Disks with silent installs of AutoKMS.
        /// </summary>
        private void ShowSplashScreen()
        {
            using (SplashScreen splashScreen = new SplashScreen())
            {
                splashScreen.ShowDialog();
            }
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