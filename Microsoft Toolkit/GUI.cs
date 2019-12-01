using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Activation;
using Backup;
using Common;
using Keys;
using License;
using MicrosoftToolkit.Properties;
using OfficeInstaller;
using ProductDetection;

namespace MicrosoftToolkit
{
    public partial class GUI : Form
    {
        #region Office/Windows Selection
        private const string MicrosoftTK = "Microsoft Toolkit ";
        private const string Office = "Office";
        private const string OfficeTK = "Office Toolkit ";
        private const string Windows = "Windows";
        private const string WindowsTK = "Windows Toolkit ";
        private static string _toolkitChoice = string.Empty;
        #endregion
        #region Unsupported Errors
        private const string WindowsUnsupported = "Supported Windows installation was not detected in the system.";
        private const string OfficeUnsupported = "Supported Office installation was not detected in the system.";
        #endregion

        /// <summary>
        /// Whether or not the Form has been fully Resized after Selecting Office or Windows Toolkit
        /// </summary>
        private bool _resized;

        public GUI()
        {
            // TODO: Disable Cross Thread Checks
            CheckForIllegalCrossThreadCalls = false;

            // TODO: Network Drive Support. Rearm, Office Uninstaller, and Backup Will Fail!
            if (Char.IsLetter(AppDomain.CurrentDomain.BaseDirectory[0]) == false || new DriveInfo(AppDomain.CurrentDomain.BaseDirectory[0].ToString(CultureInfo.InvariantCulture)).DriveType == DriveType.Network)
            {
                if (!Debugger.IsAttached)
                {
                    MessageBox.Show(this, "This program cannot be run from a Network Drive!", "Network Drive Unsupported");
                    Environment.Exit(0);
                }                
            }

            // Initialize UI
            InitializeComponent();
            
            // Show Selector Tab
            tabFunctions.TabPages.Clear();
            TabHeight = tabFunctions.Height;
            tabFunctions.Hide();

            // Resize UI
            if (_resized == false)
            {
                Size = new Size(Width, Height - TabHeight);
                consoleBox.Top -= TabHeight;
                btnCredits.Top -= TabHeight;
                btnReadme.Top -= TabHeight;
                btnSettings.Top -= TabHeight;
                progressBar.Top -= TabHeight;
                OfficeButton.Top -= TabHeight;
                WindowsButton.Top -= TabHeight;
            }

            SetText("Press the Office Button to load Office Toolkit, or the Windows Button to load Windows Toolkit.");
        }
        #region MAIN UI METHODS   
        /// <summary>
        /// Assign to TextBox in a Thread-Safe Way
        /// </summary>
        /// <param name="text">Text to Assign</param>
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (txtConsole.InvokeRequired)
            {
                SetTextDelegate d = SetText;
                Invoke(d, new object[] {text});
            }
            else
            {
                txtConsole.Text = text;
            }
        }

        /// <summary>
        /// Write to TextBox in a Thread-Safe Way
        /// </summary>
        /// <param name="text">Text to Write</param>
        private void Write(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (txtConsole.InvokeRequired)
            {
                WriteDelegate d = Write;
                Invoke(d, new object[] {text});
            }
            else
            {
                txtConsole.AppendText(text);
            }
        }

        /// <summary>
        /// Write to TextBox in a Thread-Safe Way
        /// </summary>
        /// <param name="text">Text to Write</param>
        private void WriteLine(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (txtConsole.InvokeRequired)
            {
                WriteLineDelegate d = WriteLine;
                Invoke(d, new object[] { text });
            }
            else
            {
                txtConsole.AppendText(text);
                txtConsole.AppendText(Environment.NewLine);
            }
        }

        #region Nested type: SetTextDelegate
        /// <summary>
        /// This delegate enables asynchronous calls for setting the text property on a TextBox control.
        /// </summary>
        /// <param name="text">Text to Assign to TextBox</param>
        private delegate void SetTextDelegate(string text);
        #endregion
        #region Nested type: WriteDelegate
        /// <summary>
        /// This delegate enables asynchronous calls for appending the text property on a TextBox control.
        /// </summary>
        /// <param name="text">Text to Assign to TextBox</param>
        private delegate void WriteDelegate(string text);
        #endregion
        #region Nested type: WriteLineDelegate
        /// <summary>
        /// This delegate enables asynchronous calls for appending the text property on a TextBox control.
        /// </summary>
        /// <param name="text">Text to Assign to TextBox</param>
        private delegate void WriteLineDelegate(string text);
        #endregion

        /// <summary>
        /// Start the Program Progress Bar
        /// </summary>
        /// <param name="numerical">True if using an incremental Progress Bar, False if using a Marquee Progress Bar.</param>
        private void StartProgressBar(bool numerical = false)
        {
            // Show the progress bar active
            if (numerical == false)
            {
                progressBar.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Value = 0;
            }

            // Disables the tab control, so the user can't perform any other action while there's something running
            tabFunctions.Enabled = false;

            // Disable Toolkit Switch Buttons
            if (_toolkitChoice == Office)
            {
                WindowsButton.Enabled = false;
            }
            else if (_toolkitChoice == Windows)
            {
                OfficeButton.Enabled = false;
            }

            // Disable Credits, Readme, and Settings Buttons
            btnCredits.Enabled = false;
            btnReadme.Enabled = false;
            btnSettings.Enabled = false;

            // Reset the console text
            SetText(string.Empty);
        }

        /// <summary>
        /// Stop the Progress Bar
        /// </summary>
        private void StopProgressBar()
        {
            // Stop and reset the progress bar
            progressBar.Style = ProgressBarStyle.Blocks;
            progressBar.Value = 0;

            // Enables the tab control
            tabFunctions.Enabled = true;

            // Enable Toolkit Switch Buttons
            if (_toolkitChoice == Office)
            {
                WindowsButton.Enabled = true;
            }
            else if (_toolkitChoice == Windows)
            {
                OfficeButton.Enabled = true;
            }

            // Enable Credits, Readme, and Settings Buttons
            btnCredits.Enabled = true;
            btnReadme.Enabled = true;
            btnSettings.Enabled = true;
        }

        private void OfficeButtonClick(object sender, EventArgs e)
        {
            // Change Title, Icon, Toolkit Choice and UI
            _toolkitChoice = "Office";
            Icon = Resources.Office;
            SetText(string.Empty);
            WindowsButton.Enabled = true;
            OfficeButton.Enabled = false;

            // Resize UI
            if (_resized == false)
            {
                Size = new Size(Width, Height + TabHeight);
                consoleBox.Top += TabHeight;
                btnCredits.Top += TabHeight;
                btnReadme.Top += TabHeight;
                btnSettings.Top += TabHeight;
                progressBar.Top += TabHeight;
                OfficeButton.Top += TabHeight;
                WindowsButton.Top += TabHeight;
                _resized = true;
            }

            // Show Function Tabs
            tabFunctions.Show();

            // Change Title
            if (Text.Contains(MicrosoftTK))
            {
                Text = OfficeTK + Text.Replace(MicrosoftTK, string.Empty).Trim();
            }
            else if (Text.Contains(WindowsTK))
            {
                Text = OfficeTK + Text.Replace(WindowsTK, string.Empty).Trim();
            }

            // Change Tabs Based on Selection
            tabFunctions.TabPages.Clear();
            tabFunctions.TabPages.Add(tabMainOffice);
            tabFunctions.TabPages.Add(tabActivationTools);
            tabFunctions.TabPages.Add(tabKey);
            tabFunctions.TabPages.Add(tabBackups);
            tabFunctions.TabPages.Add(tabCustomizeSetup);

            // Change Activation Tools Based on Selection
            cboActivationToolBox.Items.Clear();
            cboActivationToolBox.Items.Add(AutoKMSInstaller.AutoKMSInstaller.AutoKMSProcessName);
            cboActivationToolBox.Items.Add(AutoKMSInstaller.AutoKMSInstaller.AutoKMSCustomTaskInstallerName);
            cboActivationToolBox.Items.Add(AutoRearmInstaller.AutoRearmInstaller.AutoRearmProcessName);
            cboActivationToolBox.Items.Add(KMSServerServiceInstaller.KMSServerServiceInstaller.KMSServerServiceProcessName);

            // Change Product Key Lists Based on Selection
            cboKeyProduct.Items.Clear();
            cboKeyProduct.Items.Add(OfficeVersion.Office2010);
            cboKeyProduct.Items.Add(OfficeVersion.Office2013);
            cboKeyProduct.Items.Add(OfficeVersion.Office2016);
            cboKeyProduct.Items.Add(OfficeVersion.Office2019);

            // Select the initial values for the "Product Keys" combos
            cboKeyProduct.SelectedIndex = 0;

            // Select the first values for the Activation Tool Selector
            cboActivationToolBox.SelectedIndex = 0;

            // Select Default Values for Channel Switcher
            comboInstalledLPs.Items.Clear();
            comboInstalledLPs.Items.Add("No Detected Languages");
            comboInstalledLPs.SelectedIndex = 0;
            comboInstallerChannel.Items.Clear();
            comboInstallerChannel.Items.Add("No Detected Channels");
            comboInstallerChannel.SelectedIndex = 0;

            // Perform Startup Checks
            StartupChecks();
        }

        private void WindowsButtonClick(object sender, EventArgs e)
        {
            // Change Title, Icon, Toolkit Choice and UI
            _toolkitChoice = "Windows";
            Icon = Resources.Windows;
            SetText(string.Empty);
            WindowsButton.Enabled = false;
            OfficeButton.Enabled = true;

            // Resize UI
            if (_resized == false)
            {
                Size = new Size(Width, Height + TabHeight);
                consoleBox.Top += TabHeight;
                btnCredits.Top += TabHeight;
                btnReadme.Top += TabHeight;
                btnSettings.Top += TabHeight;
                progressBar.Top += TabHeight;
                OfficeButton.Top += TabHeight;
                WindowsButton.Top += TabHeight;
                _resized = true;
            }

            // Show Function Tabs
            tabFunctions.Show();

            // Change Title
            if (Text.Contains(MicrosoftTK))
            {
                Text = WindowsTK + Text.Replace(MicrosoftTK, string.Empty).Trim();
            }
            else if (Text.Contains(OfficeTK))
            {
                Text = WindowsTK + Text.Replace(OfficeTK, string.Empty).Trim();
            }

            // Change Tabs Based on Selection
            tabFunctions.TabPages.Clear();
            tabFunctions.TabPages.Add(tabMainWindows);
            tabFunctions.TabPages.Add(tabActivationTools);
            tabFunctions.TabPages.Add(tabKey);
            tabFunctions.TabPages.Add(tabBackups);

            // Change Activation Tools Based on Selection
            cboActivationToolBox.Items.Clear();
            cboActivationToolBox.Items.Add(AutoKMSInstaller.AutoKMSInstaller.AutoKMSProcessName);
            cboActivationToolBox.Items.Add(AutoKMSInstaller.AutoKMSInstaller.AutoKMSCustomTaskInstallerName);
            cboActivationToolBox.Items.Add(KMSServerServiceInstaller.KMSServerServiceInstaller.KMSServerServiceProcessName);

            // Change Product Key Lists Based on Selection
            cboKeyProduct.Items.Clear();
            cboKeyProduct.Items.Add(OSVersion.WinVista);
            cboKeyProduct.Items.Add(OSVersion.Win7);
            cboKeyProduct.Items.Add(OSVersion.Win7Embedded);
            cboKeyProduct.Items.Add(OSVersion.Win8);
            cboKeyProduct.Items.Add(OSVersion.Win8Embedded);
            cboKeyProduct.Items.Add(OSVersion.Win81);
            cboKeyProduct.Items.Add(OSVersion.Win81Embedded);
            cboKeyProduct.Items.Add(OSVersion.Win10);
            //cboKeyProduct.Items.Add(OSVersion.Win10Embedded);
            cboKeyProduct.Items.Add(OSVersion.WinServer2008);
            cboKeyProduct.Items.Add(OSVersion.WinServer2008R2);
            cboKeyProduct.Items.Add(OSVersion.WinServer2012);
            cboKeyProduct.Items.Add(OSVersion.WinServer2012R2);
            cboKeyProduct.Items.Add(OSVersion.WinServer2016);
            cboKeyProduct.Items.Add(OSVersion.WinServer2019);

            // Select the initial values for the "Product Keys" combos
            cboKeyProduct.SelectedIndex = 0;

            // Select the first values for the Activation Tool Selector
            cboActivationToolBox.SelectedIndex = 0;

            // Perform Startup Checks
            StartupChecks();
        }

        private void BtnCreditsClick(object sender, EventArgs e)
        {
            // Create new window
            using (AboutWindow aboutWindow = new AboutWindow())
            {
                aboutWindow.ShowDialog(this);
            }
        }

        private void BtnReadmeClick(object sender, EventArgs e)
        {
            // Create new window
            using (ReadmeWindow readmeWindow = new ReadmeWindow())
            {
                readmeWindow.ShowDialog(this);
            }
        }

        private void BtnSettingsClick(object sender, EventArgs e)
        {
            // Create new window
            using (SettingsWindow settingsWindow = new SettingsWindow())
            {
                settingsWindow.ShowDialog(this);
            }
        }

        private void BtnOfficeUninstallerClick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Are you sure you want to uninstall Microsoft Office?", "Microsoft Office Uninstaller", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Start the progress bar
                StartProgressBar();

                workerOfficeUninstaller.RunWorkerAsync(cboOfficeUninstaller.Text);
            }
        }

        private void WorkerOfficeUninstallerDoWork(object sender, DoWorkEventArgs e)
        {
            // The argument passed to the method was the product 
            string product = (string)e.Argument;

            e.Result = OfficeUninstaller.OfficeUninstaller.Uninstaller(product);
        }

        private void WorkerOfficeUninstallerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Redo Startup Checks
            StartupChecks();

            // Output the Result
            SetText(e.Result.ToString());

            // Stops the progress bar
            StopProgressBar();
        }
        #endregion
        #region Startup Checks
        private void StartupChecks()
        {
            // Check Activation Tool Status
            ActivationToolInstallCheck();

            if (_toolkitChoice == Windows)
            {
                // Is Windows Supported?
                if (OSVersion.IsWindowsSupported())
                {
                    lblWindowsSupported.Text = "Yes";

                    // Enable Buttons
                    btnAttemptActivation.Enabled = true;
                    btnEZActivator.Enabled = true;
                    btnCheckActivation.Enabled = true;
                    btnRearm.Enabled = true;
                    btnPhoneActivation.Enabled = true;
                    btnGetRearmCount.Enabled = true;
                    btnInstallKey.Enabled = true;
                    btnUninstallKey.Enabled = true;
                    btnBackup.Enabled = true;
                    btnRestore.Enabled = true;
                    btnBackupReinstallKeys.Enabled = true;
                    btnBackupCheckKeyList.Enabled = true;
                    BtnKeysSystemCheck.Enabled = true;
                    BtnKeysSystemShow.Enabled = true;

                    // Disable Windows Vista Rearm Count
                    if (OSVersion.GetWindowsNumber() <= 6.0)
                    {
                        btnGetRearmCount.Enabled = false;
                    }

                    // Default Product Key Picker
                    string windowsName = OSVersion.GetWindowsName();
                    for (int i = 0; i < cboKeyProduct.Items.Count; i++)
                    {
                        if (cboKeyProduct.Items[i].ToString() == windowsName)
                        {
                            cboKeyProduct.SelectedIndex = i;
                            break;
                        }
                    }

                    // Load Backup List
                    try
                    {
                        textBackupName.Items.Clear();
                        textBackupName.Text = string.Empty;
                        List<string> backups = new List<string>();
                        backups.AddRange(Directory.GetDirectories(Settings.Default.LicenseBackupsPath + Path.DirectorySeparatorChar + OSVersion.GetWindowsName() + Path.DirectorySeparatorChar));
                        foreach (string backup in backups)
                        {
                            textBackupName.Items.Add(backup.Substring((Settings.Default.LicenseBackupsPath + Path.DirectorySeparatorChar + OSVersion.GetWindowsName() + Path.DirectorySeparatorChar).Length));
                            textBackupName.Sorted = true;
                        }
                    }
                    catch (IOException)
                    {
                        // Ignore, there are simply no backups
                    }
                }
                else
                {
                    SetText(WindowsUnsupported);
                    lblWindowsSupported.Text = "No";

                    // Disable Buttons
                    btnAttemptActivation.Enabled = false;
                    btnEZActivator.Enabled = false;
                    btnCheckActivation.Enabled = false;
                    btnRearm.Enabled = false;
                    btnPhoneActivation.Enabled = false;
                    btnGetRearmCount.Enabled = false;
                    btnInstallKey.Enabled = false;
                    btnUninstallKey.Enabled = false;
                    btnBackup.Enabled = false;
                    btnRestore.Enabled = false;
                    btnBackupReinstallKeys.Enabled = false;
                    btnBackupCheckKeyList.Enabled = false;
                    BtnKeysSystemCheck.Enabled = false;
                    BtnKeysSystemShow.Enabled = true;
                }

                // What Is OS Architecture?
                lblWindowsArchitecture.Text = Architecture.GetOSArch();

                // What Is Windows Product?
                lblWindowsProduct.Text = OSVersion.GetWindowsName();

                // What Is Windows Number?
                lblWindowsNumber.Text = OSVersion.GetWindowsNumber() + " Build " + OSVersion.GetWindowsBuildNumber();

            }
            else if (_toolkitChoice == Office)
            {
                // Is Supported Microsoft Office Installed?
                if (OfficeVersion.IsOfficeSupported())
                {
                    lblOfficeSupported.Text = "Yes";

                    // Enable Buttons
                    btnAttemptActivation.Enabled = true;
                    btnEZActivator.Enabled = true;
                    btnCheckActivation.Enabled = true;
                    btnRearm.Enabled = true;
                    btnPhoneActivation.Enabled = true;
                    btnGetRearmCount.Enabled = true;
                    btnInstallKey.Enabled = true;
                    btnUninstallKey.Enabled = true;
                    btnBackup.Enabled = true;
                    btnRestore.Enabled = true;
                    btnBackupReinstallKeys.Enabled = true;
                    btnBackupCheckKeyList.Enabled = true;
                    BtnKeysSystemCheck.Enabled = true;
                    BtnKeysSystemShow.Enabled = true;

                    // Default Product Key Picker
                    string officeName = OfficeVersion.GetOfficeName();
                    for (int i = 0; i < cboKeyProduct.Items.Count; i++)
                    {
                        if (cboKeyProduct.Items[i].ToString() == officeName)
                        {
                            cboKeyProduct.SelectedIndex = i;
                            break;
                        }
                    }

                    // Show Only Installed Office for Uninstall
                    cboOfficeUninstaller.Items.Clear();
                    cboOfficeUninstaller.Items.Add(officeName);
                    cboOfficeUninstaller.SelectedIndex = cboOfficeUninstaller.Items.Count - 1;

                    // Load Backup List
                    try
                    {
                        // Disable Microsoft Office Backups if using Windows Licensing Services
                        if (OfficeVersion.IsOfficeSPP())
                        {
                            tabFunctions.TabPages.Remove(tabBackups);
                        }
                        else
                        {
                            textBackupName.Items.Clear();
                            textBackupName.Text = string.Empty;
                            List<string> backups = new List<string>();
                            backups.AddRange(Directory.GetDirectories(Settings.Default.LicenseBackupsPath + Path.DirectorySeparatorChar + OfficeVersion.GetOfficeName() + Path.DirectorySeparatorChar));
                            foreach (string backup in backups)
                            {
                                textBackupName.Items.Add(backup.Substring((Settings.Default.LicenseBackupsPath + Path.DirectorySeparatorChar + OfficeVersion.GetOfficeName() + Path.DirectorySeparatorChar).Length));
                                textBackupName.Sorted = true;
                            }
                        }
                    }
                    catch (IOException)
                    {
                        // Ignore, there are simply no backups
                    }
                }
                else
                {
                    SetText(OfficeUnsupported);
                    lblOfficeSupported.Text = "No";

                    // Disable Buttons
                    btnAttemptActivation.Enabled = false;
                    btnEZActivator.Enabled = false;
                    btnCheckActivation.Enabled = false;
                    btnRearm.Enabled = false;
                    btnPhoneActivation.Enabled = false;
                    btnGetRearmCount.Enabled = false;
                    btnInstallKey.Enabled = false;
                    btnUninstallKey.Enabled = false;
                    btnBackup.Enabled = false;
                    btnRestore.Enabled = false;
                    btnBackupReinstallKeys.Enabled = false;
                    btnBackupCheckKeyList.Enabled = false;
                    BtnKeysSystemCheck.Enabled = false;
                    BtnKeysSystemShow.Enabled = false;

                    // Show All Office for Uninstall
                    cboOfficeUninstaller.Items.Clear();
                    cboOfficeUninstaller.Items.Add(OfficeVersion.Office2003);
                    cboOfficeUninstaller.Items.Add(OfficeVersion.Office2007);
                    cboOfficeUninstaller.Items.Add(OfficeVersion.Office2010);
                    cboOfficeUninstaller.Items.Add(OfficeVersion.Office2013);
                    cboOfficeUninstaller.Items.Add(OfficeVersion.Office2016);
                    cboOfficeUninstaller.Items.Add(OfficeVersion.Office2019);
                    cboOfficeUninstaller.SelectedIndex = cboOfficeUninstaller.Items.Count - 1;
                }

                // What Is Office Architecture?
                try
                {
                    lblOfficeArchitecture.Text = Architecture.GetOfficeArch();
                }
                catch (ApplicationException)
                {
                    lblOfficeArchitecture.Text = "N/A"; 
                }

                // What Is Office Product?
                try
                {
                    lblOfficeProduct.Text = OfficeVersion.GetOfficeName();
                }
                catch (ApplicationException)
                {
                    lblOfficeProduct.Text = "N/A";
                }

                // What Is Office Number?
                try
                {
                    lblOfficeNumber.Text = OfficeVersion.GetOfficeNumber().ToString(CultureInfo.InvariantCulture);
                }
                catch (ApplicationException)
                {
                    lblOfficeNumber.Text = "N/A";
                }

                // What Is Office Type?
                try
                {
                    try
                    {
                        if (OfficeVersion.GetInstallationPath() != null)
                        {
                            if (OfficeVersion.IsOfficeVirtual())
                            {
                                lblOfficeInstallType.Text = "Click To Run";
                            }
                            else
                            {
                                lblOfficeInstallType.Text = "Traditional";
                            }
                        }
                    }
                    catch (Exception)
                    {
                        lblOfficeInstallType.Text = "N/A";
                    }  
                }
                catch (ApplicationException)
                {
                    lblOfficeInstallType.Text = "N/A";
                }
            }
        }

        #endregion
        #region Activation
        #region Activation Tools
        private void BtnEZActivatorClick(object sender, EventArgs e)
        {
            // Start the progress bar
            StartProgressBar();

            workerEZActivator.RunWorkerAsync();
        }

        private void WorkerEZActivatorDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Stop KMS Server Service
                if (KMSServerServiceInstaller.KMSServerServiceInstaller.IsKMSServerServiceInstalled())
                {
                    Services.StopService(KMSServerServiceInstaller.KMSServerServiceInstaller.KMSServerServiceName, KMSServerServiceInstaller.KMSServerServiceInstaller.KMSServerServiceProcessName);
                }

                if (_toolkitChoice == Office)
                {
                    e.Result = EZActivator.RunEZActivator(new LicenseListOffice(), true, Settings.Default.KMSServerOffice, Settings.Default.KMSPortOffice, Settings.Default.KMSPIDOffice, Settings.Default.KMSHardwareID, Settings.Default.UseKMSEmulator, Settings.Default.RemoveKMSConnection, Settings.Default.KillProcessOnPort,
                        Settings.Default.LocalHostBypassUseDLLInjection, Settings.Default.LocalHostBypassUseTAPAdapter, Settings.Default.LocalHostBypassUseWinDivertClient, Settings.Default.LocalHostBypassIPAddress, Settings.Default.LocalHostBypassIPSubnet);
                }
                else if (_toolkitChoice == Windows)
                {
                    e.Result = EZActivator.RunEZActivator(new LicenseListWindows(), true, Settings.Default.KMSServerWindows, Settings.Default.KMSPortWindows, Settings.Default.KMSPIDWindows, Settings.Default.KMSHardwareID, Settings.Default.UseKMSEmulator, Settings.Default.RemoveKMSConnection, Settings.Default.KillProcessOnPort,
                        Settings.Default.LocalHostBypassUseDLLInjection, Settings.Default.LocalHostBypassUseTAPAdapter, Settings.Default.LocalHostBypassUseWinDivertClient, Settings.Default.LocalHostBypassIPAddress, Settings.Default.LocalHostBypassIPSubnet);
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
            finally
            {
                // Start KMS Server Service
                if (KMSServerServiceInstaller.KMSServerServiceInstaller.IsKMSServerServiceInstalled())
                {
                    Services.StartService(KMSServerServiceInstaller.KMSServerServiceInstaller.KMSServerServiceName);
                }
            }
        }

        private void WorkerEZActivatorRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Output the Result
            SetText(e.Result.ToString());

            // Install AutoKMS
            if (e.Result.ToString() != LicenseErrorCode.ErrNoKMS && !e.Result.ToString().Contains("<Product activation failed>") && !e.Result.ToString().Contains("Windows is already permanently activated.") && !AutoKMSInstaller.AutoKMSInstaller.IsAutoKMSInstalled() && !KMSServerServiceInstaller.KMSServerServiceInstaller.IsKMSServerServiceInstalled())
            {
                WriteLine(string.Empty);
                WriteLine("Installing AutoKMS.");
                WriteLine(AutoKMSInstaller.AutoKMSInstaller.InstallAutoKMS(Environment.ExpandEnvironmentVariables(Settings.Default.AutoKMSPath), Resources.AutoKMS));
                Write("----------------------------------------");
            }

            // Check Install Status
            ActivationToolInstallCheck();

            // Stops the progress bar
            StopProgressBar();
        }

        private void ActivationToolBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            // Check Install Status to decide if Install/Uninstall will be allowed
            ActivationToolInstallCheck();
        }

        private void ActivationToolInstallCheck()
        {
            btnActivationToolInstall.Enabled = false;
            btnActivationToolUninstall.Enabled = false;
            btnActivationToolInstall.Enabled = false;
            btnActivationToolUninstall.Enabled = false;

            if (cboActivationToolBox.Text == AutoKMSInstaller.AutoKMSInstaller.AutoKMSProcessName)
            {
                //if (KMSServerServiceInstaller.KMSServerServiceInstaller.IsKMSServerServiceInstalled())
                //{
                //    btnActivationToolInstall.Enabled = false;
                //    btnActivationToolUninstall.Enabled = false;
                //}
                //else 
                if (AutoKMSInstaller.AutoKMSInstaller.IsAutoKMSInstalled())
                {
                    btnActivationToolInstall.Enabled = false;
                    btnActivationToolUninstall.Enabled = true;
                }
                else
                {
                    if (_toolkitChoice == Windows && OSVersion.IsWindowsSupported())
                    {
                        btnActivationToolInstall.Enabled = true;
                        btnActivationToolUninstall.Enabled = false;
                    }
                    else if (_toolkitChoice == Office && OfficeVersion.IsOfficeSupported())
                    {
                        btnActivationToolInstall.Enabled = true;
                        btnActivationToolUninstall.Enabled = false;
                    }
                    else
                    {
                        btnActivationToolInstall.Enabled = false;
                        btnActivationToolUninstall.Enabled = false;
                    }
                }
            }
            else if (cboActivationToolBox.Text == AutoKMSInstaller.AutoKMSInstaller.AutoKMSCustomTaskInstallerName)
            {
                if (AutoKMSInstaller.AutoKMSInstaller.IsAutoKMSInstalled())
                {
                    if (AutoKMSInstaller.AutoKMSInstaller.IsAutoKMSCustomTaskInstalled())
                    {
                        btnActivationToolInstall.Enabled = false;
                        btnActivationToolUninstall.Enabled = true;
                    }
                    else
                    {
                        btnActivationToolInstall.Enabled = true;
                        btnActivationToolUninstall.Enabled = false;
                    }
                }
                else
                {
                    btnActivationToolInstall.Enabled = false;
                    btnActivationToolUninstall.Enabled = false;
                }
            }
            else if (cboActivationToolBox.Text == AutoRearmInstaller.AutoRearmInstaller.AutoRearmProcessName)
            {
                if (AutoRearmInstaller.AutoRearmInstaller.IsIORRTInstalled())
                {
                    btnActivationToolInstall.Enabled = false;
                    btnActivationToolUninstall.Enabled = false;
                }
                else if (AutoRearmInstaller.AutoRearmInstaller.IsAutoRearmInstalled())
                {
                    btnActivationToolInstall.Enabled = false;
                    btnActivationToolUninstall.Enabled = true;
                }
                else
                {
                    if (OfficeVersion.IsOfficeSupported())
                    {
                        // Disable Microsoft Office AutoRearm if using Windows Licensing Services
                        if (OfficeVersion.IsOfficeSPP())
                        {
                            btnActivationToolInstall.Enabled = false;
                        }
                        else
                        {
                            btnActivationToolInstall.Enabled = true;
                        }
                    }
                    btnActivationToolUninstall.Enabled = false;
                }
            }
            else if (cboActivationToolBox.Text == KMSServerServiceInstaller.KMSServerServiceInstaller.KMSServerServiceProcessName)
            {
                //if (AutoKMSInstaller.AutoKMSInstaller.IsAutoKMSInstalled())
                //{
                //    btnActivationToolInstall.Enabled = false;
                //    btnActivationToolUninstall.Enabled = false;
                //}
                //else 
                if (KMSServerServiceInstaller.KMSServerServiceInstaller.IsKMSServerServiceInstalled())
                {
                    btnActivationToolInstall.Enabled = false;
                    btnActivationToolUninstall.Enabled = true;
                }
                else
                {
                    btnActivationToolInstall.Enabled = true;
                    btnActivationToolUninstall.Enabled = false;
                }
            }
        }

        private void BtnActivationToolInstallClick(object sender, EventArgs e)
        {
            // Start the progress bar
            StartProgressBar();

            if (cboActivationToolBox.Text == AutoKMSInstaller.AutoKMSInstaller.AutoKMSProcessName)
            {
                workerAutoKMSInstall.RunWorkerAsync();
            }
            else if (cboActivationToolBox.Text == AutoKMSInstaller.AutoKMSInstaller.AutoKMSCustomTaskInstallerName)
            {
                workerAutoKMSCustomTaskInstall.RunWorkerAsync();
            }
            else if (cboActivationToolBox.Text == AutoRearmInstaller.AutoRearmInstaller.AutoRearmProcessName)
            {
                workerAutoRearmInstall.RunWorkerAsync();
            }
            else if (cboActivationToolBox.Text == KMSServerServiceInstaller.KMSServerServiceInstaller.KMSServerServiceProcessName)
            {
                workerKMSServerServiceInstall.RunWorkerAsync();
            }
        }

        private void WorkerAutoKMSInstallDoWork(object sender, DoWorkEventArgs e)
        {
            // Output via the Worker Thread to Textbox
            SetText(AutoKMSInstaller.AutoKMSInstaller.InstallAutoKMS(Environment.ExpandEnvironmentVariables(Settings.Default.AutoKMSPath), Resources.AutoKMS));
        }

        private void WorkerAutoKMSInstallRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Check Install Status
            ActivationToolInstallCheck();

            // Stops the progress bar
            StopProgressBar();
        }

        private void WorkerAutoKMSCustomTaskInstallDoWork(object sender, DoWorkEventArgs e)
        {
            // Output via the Worker Thread to Textbox
            SetText(AutoKMSInstaller.AutoKMSInstaller.InstallAutoKMSCustomTask());
        }

        private void WorkerAutoKMSCustomTaskInstallRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Check Install Status
            ActivationToolInstallCheck();

            // Stops the progress bar
            StopProgressBar();
        }

        private void WorkerAutoRearmInstallDoWork(object sender, DoWorkEventArgs e)
        {
            // Output via the Worker Thread to Textbox
            SetText(AutoRearmInstaller.AutoRearmInstaller.InstallAutoRearm(Environment.ExpandEnvironmentVariables(Settings.Default.AutoRearmPath), Resources.AutoRearm));
        }

        private void WorkerAutoRearmInstallRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Check Install Status
            ActivationToolInstallCheck();

            // Stops the progress bar
            StopProgressBar();
        }

        private void WorkerKMSServerServiceInstallDoWork(object sender, DoWorkEventArgs e)
        {
            // Output via the Worker Thread to Textbox
            SetText(KMSServerServiceInstaller.KMSServerServiceInstaller.InstallKMSServerService(Environment.ExpandEnvironmentVariables(Settings.Default.KMSServerServicePath), Resources.KMSServerService));
        }

        private void WorkerKMSServerServiceInstallRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Check Install Status
            ActivationToolInstallCheck();

            // Stops the progress bar
            StopProgressBar();
        }

        private void BtnActivationToolUninstallClick(object sender, EventArgs e)
        {
            // Start the progress bar
            StartProgressBar();

            if (cboActivationToolBox.Text == AutoKMSInstaller.AutoKMSInstaller.AutoKMSProcessName)
            {
                workerAutoKMSUninstall.RunWorkerAsync();
            }
            else if (cboActivationToolBox.Text == AutoKMSInstaller.AutoKMSInstaller.AutoKMSCustomTaskInstallerName)
            {
                workerAutoKMSCustomTaskUninstall.RunWorkerAsync();
            }
            else if (cboActivationToolBox.Text == AutoRearmInstaller.AutoRearmInstaller.AutoRearmProcessName)
            {
                workerAutoRearmUninstall.RunWorkerAsync();
            }
            else if (cboActivationToolBox.Text == KMSServerServiceInstaller.KMSServerServiceInstaller.KMSServerServiceProcessName)
            {
                workerKMSServerServiceUninstall.RunWorkerAsync();
            }
        }

        private void WorkerAutoKMSUninstallDoWork(object sender, DoWorkEventArgs e)
        {
            // Output via the Worker Thread to Textbox
            SetText(AutoKMSInstaller.AutoKMSInstaller.UninstallAutoKMS());
        }

        private void WorkerAutoKMSUninstallRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Check Install Status
            ActivationToolInstallCheck();

            // Stops the progress bar
            StopProgressBar();
        }

        private void WorkerAutoKMSCustomTaskUninstallDoWork(object sender, DoWorkEventArgs e)
        {
            // Output via the Worker Thread to Textbox
            SetText(AutoKMSInstaller.AutoKMSInstaller.UninstallAutoKMSCustomTask());
        }

        private void WorkerAutoKMSCustomTaskUninstallRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Check Install Status
            ActivationToolInstallCheck();

            // Stops the progress bar
            StopProgressBar();
        }

        private void WorkerAutoRearmUninstallDoWork(object sender, DoWorkEventArgs e)
        {
            // Check if Connected to Internet
            if (CommonUtilities.IsConnectedToInternet)
            {
                SetText("In order to properly Restore your License Backup from when you installed AutoRearm, please disconnect from the Internet and try again.");
                return;
            }

            // Output via the Worker Thread to Textbox
            SetText(AutoRearmInstaller.AutoRearmInstaller.UninstallAutoRearm());
        }

        private void WorkerAutoRearmUninstallRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Check Install Status
            ActivationToolInstallCheck();

            // Stops the progress bar
            StopProgressBar();
        }

        private void WorkerKMSServerServiceUninstallDoWork(object sender, DoWorkEventArgs e)
        {
            // Output via the Worker Thread to Textbox
            SetText(KMSServerServiceInstaller.KMSServerServiceInstaller.UninstallKMSServerService());
        }

        private void WorkerKMSServerServiceUninstallRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Check Install Status
            ActivationToolInstallCheck();

            // Stops the progress bar
            StopProgressBar();
        }
        #endregion
        private void BtnAttemptActivationClick(object sender, EventArgs e)
        {
            // Start the progress bar
            StartProgressBar();

            workerAttemptActivation.RunWorkerAsync();
        }

        private void WorkerAttemptActivationDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Stop KMS Server Service
                if (KMSServerServiceInstaller.KMSServerServiceInstaller.IsKMSServerServiceInstalled())
                {
                    Services.StopService(KMSServerServiceInstaller.KMSServerServiceInstaller.KMSServerServiceName, KMSServerServiceInstaller.KMSServerServiceInstaller.KMSServerServiceProcessName);
                }

                if (_toolkitChoice == Office)
                {
                    e.Result = ActivationState.AttemptActivationOffice(new LicenseListOffice(), false, Settings.Default.KMSServerOffice, Settings.Default.KMSPortOffice, Settings.Default.KMSPIDOffice, Settings.Default.KMSHardwareID, Settings.Default.UseKMSEmulator, Settings.Default.RemoveKMSConnection, Settings.Default.KillProcessOnPort, 
                        Settings.Default.LocalHostBypassUseDLLInjection, Settings.Default.LocalHostBypassUseTAPAdapter, Settings.Default.LocalHostBypassUseWinDivertClient, Settings.Default.LocalHostBypassIPAddress, Settings.Default.LocalHostBypassIPSubnet);
                }
                else if (_toolkitChoice == Windows)
                {
                    e.Result = ActivationState.AttemptActivationWindows(new LicenseListWindows(), false, Settings.Default.KMSServerWindows, Settings.Default.KMSPortWindows, Settings.Default.KMSPIDWindows, Settings.Default.KMSHardwareID, Settings.Default.UseKMSEmulator, Settings.Default.RemoveKMSConnection, Settings.Default.KillProcessOnPort,
                        Settings.Default.LocalHostBypassUseDLLInjection, Settings.Default.LocalHostBypassUseTAPAdapter, Settings.Default.LocalHostBypassUseWinDivertClient, Settings.Default.LocalHostBypassIPAddress, Settings.Default.LocalHostBypassIPSubnet);
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.ToString();
            }
            finally
            {
                // Start KMS Server Service
                if (KMSServerServiceInstaller.KMSServerServiceInstaller.IsKMSServerServiceInstalled())
                {
                    Services.StartService(KMSServerServiceInstaller.KMSServerServiceInstaller.KMSServerServiceName);
                }
            }
        }

        private void WorkerAttemptActivationRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Output the Result
            SetText(e.Result.ToString());

            // Stops the progress bar
            StopProgressBar();
        }

        private void BtnPhoneActivationClick(object sender, EventArgs e)
        {
            // Start the progress bar
            StartProgressBar();

            workerPhoneActivate.RunWorkerAsync();
        }

        private void WorkerPhoneActivationDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (_toolkitChoice == Office)
                {
                    e.Result = Phone.PhoneActivationQueryOffice(new LicenseListOffice(), this);
                }
                else if (_toolkitChoice == Windows)
                {
                    e.Result = Phone.PhoneActivationQueryWindows(new LicenseListWindows(), this);
                }
            }
            catch (Exception ex)
            {
               e.Result = ex.Message;
            }
        }

        private void WorkerPhoneActivationRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Output the Result
            SetText(e.Result.ToString());

            // Stops the progress bar
            StopProgressBar();
        }

        private void BtnCheckActivationClick(object sender, EventArgs e)
        {
            // Start the progress bar
            StartProgressBar();

            workerCheckActivation.RunWorkerAsync();
        }

        private void WorkerCheckActivationDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (_toolkitChoice == Office)
                {
                    e.Result = ActivationState.CheckActivation(new LicenseListOffice(), Settings.Default.ShowCMID, Settings.Default.ShowUnlicensed);
                }
                else
                {
                    e.Result = ActivationState.CheckActivation(new LicenseListWindows(), Settings.Default.ShowCMID, Settings.Default.ShowUnlicensed);
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void WorkerCheckActivationRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Output the Result
            SetText(e.Result.ToString());

            // Stops the progress bar
            StopProgressBar();
        }

        private void BtnRearmClick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Are you sure you want to rearm? You will lose activation and your licensing will be reset!", "Rearm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Start the progress bar
                StartProgressBar();

                workerRearm.RunWorkerAsync();
            }
        }

        private void WorkerRearmDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (_toolkitChoice == Office)
                {
                    e.Result = RearmOffice.Rearm();
                }
                else if (_toolkitChoice == Windows)
                {
                    e.Result = RearmWindows.Rearm();
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void WorkerRearmRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Output the Result
            SetText(e.Result.ToString());

            // Stops the progress bar
            StopProgressBar();
        }

        private void BtnGetRearmCountClick(object sender, EventArgs e)
        {
            // Start the progress bar
            StartProgressBar();

            workerRearmCount.RunWorkerAsync();
        }

        private void WorkerGetRearmCountDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (_toolkitChoice == Office)
                {
                    e.Result = "You have " + Convert.ToString(RearmOffice.GetRearmCount()) + " Available Rearms";
                }
                else if (_toolkitChoice == Windows)
                {
                    e.Result = "You have " + Convert.ToString(RearmWindows.GetRearmCount()) + " Available Rearms";
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void WorkerGetRearmCountRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Output the Result
            SetText(e.Result.ToString());

            // Stops the progress bar
            StopProgressBar();
        }
        #endregion
        #region Product Keys
        private void CboKeyProductSelectedIndexChanged(object sender, EventArgs e)
        {
            // Get KeyList
            cboKeyEdition.Items.Clear();
            KeyList keyList = KeyBase.GetKeyListFromSelection(cboKeyProduct.Text);

            // Add Products to Combobox
            foreach (string productName in keyList.GetProducts())
            {
                cboKeyEdition.Items.Add(productName);
                cboKeyEdition.Sorted = true;
            }
            cboKeyEdition.SelectedIndex = 0;
        }

        private void TxtCustomKeyTextChanged(object sender, EventArgs e)
        {
            // Change color of key entry textbox
            if (txtCustomKey.BackColor != Color.Red)
            {
                return;
            }
            txtCustomKey.BackColor = Color.White;
            txtCustomKey.ForeColor = Color.Black;
        }

        private void ChkCustomKeyCheckedChanged(object sender, EventArgs e)
        {
            // Change UI when Custom Key is checked or unchecked
            if (((CheckBox)sender).Checked)
            {
                txtCustomKey.Enabled = true;
                cboKeyEdition.Enabled = false;
            }
            else
            {
                txtCustomKey.Enabled = false;
                cboKeyEdition.Enabled = true;
            }
        }

        private void BtnInstallKeyClick(object sender, EventArgs e)
        {
            // Check the format of the key.
            if (chkEnterCustomKey.Checked)
            {
                // First, the key is made Uppercase.
                txtCustomKey.Text = txtCustomKey.Text.ToUpper();

                string customKey = txtCustomKey.Text;
                if (KeyBase.IsValidKey(customKey) == false)
                {
                    // Set error colors.
                    txtCustomKey.BackColor = Color.Red;
                    txtCustomKey.ForeColor = Color.White;

                    // Reset the console text
                    SetText(string.Empty);

                    WriteLine("The key has an invalid format");
                    Write("The correct format is: XXXXX-XXXXX-XXXXX-XXXXX-XXXXX");
                    return;
                }
            }

            string keyToInstall;
            if (chkEnterCustomKey.Checked)
            {
                keyToInstall = txtCustomKey.Text;
            }
            else
            {
                KeyList keyList = KeyBase.GetKeyListFromSelection(cboKeyProduct.SelectedItem.ToString());
                keyToInstall = keyList.GetProductKey(cboKeyEdition.SelectedItem.ToString());
            }

            // Start the progress bar.
            StartProgressBar();

            workerInstallKey.RunWorkerAsync(keyToInstall);
        }

        private void WorkerInstallKeyDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // The argument passed to the method was the key.
                string key = e.Argument.ToString();
                if (_toolkitChoice == Office)
                {
                    KeyInstaller.InstallKeyOffice(key);
                    e.Result = "<Product key installation successful>";
                }
                else
                {
                    KeyInstaller.InstallKeyWindows(key);
                    e.Result = "<Product key installation successful>";
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void WorkerInstallKeyRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Output the Result
            SetText(e.Result.ToString());

            // Stops the progress bar.
            StopProgressBar();
        }

        private void BtnUninstallKeyClick(object sender, EventArgs e)
        {
            // Check the format of the key.
            if (chkEnterCustomKey.Checked)
            {
                // First, the key is made Uppercase.
                txtCustomKey.Text = txtCustomKey.Text.ToUpper();

                string customKey = txtCustomKey.Text;

                // The key must be a 25 character key (29 characters including hyphens), or the last 5 digits.
                if (customKey.Length != 5 && customKey.Length != 29)
                {
                    // Set error colors.
                    txtCustomKey.BackColor = Color.Red;
                    txtCustomKey.ForeColor = Color.White;

                    // Set Console Text
                    SetText("The key must be the full key or the last 5 digits");
                    return;
                }
            }

            string keyToDelete;
            if (chkEnterCustomKey.Checked)
            {
                keyToDelete = txtCustomKey.Text;
            }
            else
            {
                KeyList keyList = KeyBase.GetKeyListFromSelection(cboKeyProduct.SelectedItem.ToString());
                keyToDelete = keyList.GetProductKey(cboKeyEdition.SelectedItem.ToString());
            }
            // Start the progress bar.
            StartProgressBar();

            workerUninstallKey.RunWorkerAsync(keyToDelete);
        }

        private void WorkerUninstallKeyDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // The argument passed to the method was the key.
                string key = e.Argument.ToString();
                if (_toolkitChoice == Office)
                {
                    KeyInstaller.UnInstallKeyByKeyOffice(key);
                    e.Result = "<Product key uninstall successful>";
                }
                else
                {
                    KeyInstaller.UnInstallKeyByKeyWindows(key);
                    e.Result = "<Product key uninstall successful>";
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void WorkerUninstallKeyRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Output the Result
            SetText(e.Result.ToString());

            // Stops the progress bar.
            StopProgressBar();
        }

        private void BtnCheckKeyClick(object sender, EventArgs e)
        {
            // Check the format of the key.
            if (chkEnterCustomKey.Checked)
            {
                // First, the key is made Uppercase.
                txtCustomKey.Text = txtCustomKey.Text.ToUpper();

                string customKey = txtCustomKey.Text;
                if (KeyBase.IsValidKey(customKey) == false)
                {
                    // Set error colors.
                    txtCustomKey.BackColor = Color.Red;
                    txtCustomKey.ForeColor = Color.White;

                    // Set the Console Text
                    SetText("The key has an invalid format" + Environment.NewLine + "The correct format is: XXXXX-XXXXX-XXXXX-XXXXX-XXXXX");
                    return;
                }
            }

            string keyToCheck;
            if (chkEnterCustomKey.Checked)
            {
                keyToCheck = txtCustomKey.Text;
            }
            else
            {
                KeyList keyList = KeyBase.GetKeyListFromSelection(cboKeyProduct.SelectedItem.ToString());
                keyToCheck = keyList.GetProductKey(cboKeyEdition.SelectedItem.ToString());
            }
            // Start the progress bar.
            StartProgressBar();

            workerCheckKey.RunWorkerAsync(new List<string> {keyToCheck, cboKeyProduct.Text});
        }

        private void WorkerCheckKeyDoWork(object sender, DoWorkEventArgs e)
        {
            // The arguments passed to the method were the key and product 
            List<string> parameters = (List<string>)e.Argument;

            try
            {
                if (_toolkitChoice == Office)
                {
                    e.Result = KeyCheck.CheckKey(parameters[0], KeyCheck.GetPkeyConfigOffice(parameters[1]));
                }
                else if (_toolkitChoice == Windows)
                {
                    e.Result = KeyCheck.CheckKey(parameters[0], KeyCheck.GetPkeyConfigWindows(parameters[1]));
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void WorkerCheckKeyRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Output the Result
            SetText(e.Result.ToString());

            // Stops the progress bar.
            StopProgressBar();
        }

        private void BtnKeysSystemCheckClick(object sender, EventArgs e)
        {
            // Start the progress bar.
            StartProgressBar();

            workerKeysSystemCheck.RunWorkerAsync();
        }

        private void WorkerKeysSystemCheckDoWork(object sender, DoWorkEventArgs e)
        {
            // Check Keys
            List<string> keys = new List<string>();
            if (_toolkitChoice == Office)
            {
                keys.AddRange(KeyFinder.GetKeysFromRegistryOffice());
                foreach (string key in keys)
                {
                    // Output via the Worker Thread to Textbox
                    WriteLine("---------------------------------------");
                    WriteLine(KeyCheck.CheckKey(key, KeyCheck.GetPkeyConfigOffice()));
                }
            }
            else if (_toolkitChoice == Windows)
            {
                keys.AddRange(KeyFinder.GetKeysFromRegistryWindows());
				keys.AddRange(KeyFinder.GetDefaultKeysFromRegistryWindows());
				keys.AddRange(KeyFinder.GetKeysFromMSDMWindows());
                foreach (string key in keys)
                {
                    // Output via the Worker Thread to Textbox
                    WriteLine("---------------------------------------");
                    WriteLine(KeyCheck.CheckKey(key, KeyCheck.GetPkeyConfigWindows()));
                }
            }
            if (keys.Count > 0)
            {
                // Output via the Worker Thread to Textbox
                Write("---------------------------------------");
            }
            else
            {
                // Output via the Worker Thread to Textbox
                Write("<Product key not found>");
            }
        }

        private void WorkerKeysSystemCheckRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Stops the progress bar.
            StopProgressBar();
        }

        private void BtnKeysSystemShowClick(object sender, EventArgs e)
        {
            // Start the progress bar.
            StartProgressBar();

            workerKeysSystemShow.RunWorkerAsync();
        }

        private void WorkerKeysSystemShowDoWork(object sender, DoWorkEventArgs e)
        {
            // Check Keys
            List<string> keys = new List<string>();
            if (_toolkitChoice == Office)
            {
                keys.AddRange(KeyFinder.GetKeysFromRegistryOffice());
                foreach (string key in keys)
                {
                    // Output via the Worker Thread to Textbox
                    WriteLine("---------------------------------------");
                    WriteLine(key.Trim());
                }
            }
            else if (_toolkitChoice == Windows)
            {
                keys.AddRange(KeyFinder.GetKeysFromRegistryWindows());
				keys.AddRange(KeyFinder.GetDefaultKeysFromRegistryWindows());
                keys.AddRange(KeyFinder.GetKeysFromMSDMWindows());
                foreach (string key in keys)
                {
                    // Output via the Worker Thread to Textbox
                    WriteLine("---------------------------------------");
                    WriteLine(key.Trim());
                }
            }
            if (keys.Count > 0)
            {
                // Output via the Worker Thread to Textbox
                Write("---------------------------------------");
            }
            else
            {
                // Output via the Worker Thread to Textbox
                Write("<Product key not found>");
            }
        }

        private void WorkerKeysSystemShowRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Stops the progress bar.
            StopProgressBar();
        }

        private void BtnCheckKeyListClick(object sender, EventArgs e)
        {
            // Create new window
            using (BatchPIDXCheck batchPIDXCheck = new BatchPIDXCheck())
            {
                batchPIDXCheck.ShowDialog(this);
            }

            // Start the progress bar.
            StartProgressBar();

            workerCheckKeyList.RunWorkerAsync(cboKeyProduct.Text);
        }

        private void WorkerCheckKeyListDoWork(object sender, DoWorkEventArgs e)
        {
            // The argument passed to the method was the product 
            string product = (string)e.Argument;

            // Check All Keys
            if (BatchPIDXCheck.MatchedKeys.Count > 0)
            {
                foreach (string key in BatchPIDXCheck.MatchedKeys)
                {
                    if (_toolkitChoice == Office)
                    {
                        // Output via the Worker Thread to Textbox
                        WriteLine("---------------------------------------");
                        WriteLine(KeyCheck.CheckKey(key, KeyCheck.GetPkeyConfigOffice(product)));
                    }
                    else if (_toolkitChoice == Windows)
                    {
                        // Output via the Worker Thread to Textbox
                        WriteLine("---------------------------------------");
                        WriteLine(KeyCheck.CheckKey(key, KeyCheck.GetPkeyConfigWindows(product)));
                    }
                }
                // Output via the Worker Thread to Textbox
                Write("---------------------------------------");
            }
            else
            {
                // Output via the Worker Thread to Textbox
                Write("You did not provide any Keys!");
            }
        }

        private void WorkerCheckKeyListRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Stops the progress bar.
            StopProgressBar();
        }
        #endregion
        #region License Backup
        private void BtnBackupClick(object sender, EventArgs e)
        {
            // Start the progress bar.
            StartProgressBar();

            workerBackup.RunWorkerAsync(new List<string> {textBackupName.Text});
        }

        private void WorkerBackupDoWork(object sender, DoWorkEventArgs e)
        {
            // The arguments passed to the method were the Backup Name and whether or not to Save Keys
            List<string> parameters = (List<string>)e.Argument;

            try
            {
                if (_toolkitChoice == Office)
                {
                    BackupBase.Window = this;
                    e.Result = LicenseBackup.Backup(parameters[0], Settings.Default.LicenseBackupsPath + Path.DirectorySeparatorChar + OfficeVersion.GetOfficeName() + Path.DirectorySeparatorChar, new LicenseListOffice());
                }
                else if (_toolkitChoice == Windows)
                {
                    BackupBase.Window = this;
                    e.Result = LicenseBackup.Backup(parameters[0], Settings.Default.LicenseBackupsPath + Path.DirectorySeparatorChar + OSVersion.GetWindowsName() + Path.DirectorySeparatorChar, new LicenseListWindows());
                }
            }
            catch (Exception ex)
            {
                e.Result = "License Backup Failed!" + Environment.NewLine + ex.Message;
            }
        }

        private void WorkerBackupRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Output the Result
            SetText(e.Result.ToString());

            if (e.Result.ToString().Contains("Failed") == false)
            {
                textBackupName.Items.Add(textBackupName.Text);
                textBackupName.Sorted = true;
                textBackupName.SelectedIndex = 0;
            }

            // Stops the progress bar.
            StopProgressBar();
        }

        private void BtnRestoreClick(object sender, EventArgs e)
        {
            // Start the progress bar.
            StartProgressBar();

            workerRestore.RunWorkerAsync(new List<string> {textBackupName.Text});
        }

        private void WorkerRestoreDoWork(object sender, DoWorkEventArgs e)
        {
            // Check if Connected to Internet
            if (CommonUtilities.IsConnectedToInternet)
            {
                e.Result = "In order to properly Restore your License Backup, please disconnect from the Internet and try again.";
                return;
            }

            // The arguments passed to the method were the Backup Name and whether or not to Restore Keys
            List<string> parameters = (List<string>)e.Argument;

            try
            {
                if (_toolkitChoice == Office)
                {
                    e.Result = LicenseBackup.Restore(parameters[0], Settings.Default.LicenseBackupsPath + Path.DirectorySeparatorChar + OfficeVersion.GetOfficeName() + Path.DirectorySeparatorChar, new LicenseListOffice());
                }
                else if (_toolkitChoice == Windows)
                {
                    e.Result = LicenseBackup.Restore(parameters[0], Settings.Default.LicenseBackupsPath + Path.DirectorySeparatorChar + OSVersion.GetWindowsName() + Path.DirectorySeparatorChar, new LicenseListWindows());
                }
            }
            catch (Exception ex)
            {
                e.Result = "License Restore Failed!" + Environment.NewLine + ex.Message;
            }
        }

        private void WorkerRestoreRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Output the Result
            SetText(e.Result.ToString());

            // Stops the progress bar.
            StopProgressBar();
        }

        private void BtnBackupCheckKeyListClick(object sender, EventArgs e)
        {
            // Start the progress bar.
            StartProgressBar();

            workerBackupCheckKeyList.RunWorkerAsync(textBackupName.Text);
        }

        private void WorkerBackupCheckKeyListDoWork(object sender, DoWorkEventArgs e)
        {
            // The argument passed to the method was the Backup Name
            string backupName = (string)e.Argument;

            if (String.IsNullOrWhiteSpace(backupName))
            {
                // Output via the Worker Thread to Textbox
                SetText("You did not enter a backup name.");
                return;
            }

            string keyListPath = string.Empty;
            if (_toolkitChoice == Office)
            {
                keyListPath = Settings.Default.LicenseBackupsPath + Path.DirectorySeparatorChar + OfficeVersion.GetOfficeName() + Path.DirectorySeparatorChar + backupName + "\\Keys.ini";
            }
            else if (_toolkitChoice == Windows)
            {
                keyListPath = Settings.Default.LicenseBackupsPath + Path.DirectorySeparatorChar + OSVersion.GetWindowsName() + Path.DirectorySeparatorChar + backupName + "\\Keys.ini";
            }

            if (File.Exists(keyListPath) == false)
            {
                // Output via the Worker Thread to Textbox
                SetText(keyListPath + " Not Found!");
            }
            else
            {
                // Regex Match Source String Variable for Product Keys
                List<string> lstKeys = new List<string>();
                foreach (Match keyMatch in KeyBase.KeyRegex.Matches(File.ReadAllText(keyListPath)))
                {
                    // Don't Add Duplicate Keys
                    if (lstKeys.Contains(keyMatch.ToString()) == false)
                    {
                        lstKeys.Add(keyMatch.ToString());
                    }
                }
                foreach (string key in lstKeys)
                {
                    // Output via the Worker Thread to Textbox
                    WriteLine("---------------------------------------");
                    if (_toolkitChoice == Office)
                    {
                        // Output via the Worker Thread to Textbox
                        WriteLine(KeyCheck.CheckKey(key, KeyCheck.GetPkeyConfigOffice()));
                    }
                    else if (_toolkitChoice == Windows)
                    {
                        // Save Output
                        string output = KeyCheck.CheckKey(key, KeyCheck.GetPkeyConfigWindows());

                        // Since the Backup Contains Windows and Office Keys, Check if the Failed Key is an Office Key
                        if (OfficeVersion.IsOfficeSupported() && OfficeVersion.IsOfficeSPP() && output.Contains("Invalid"))
                        {
                            output = KeyCheck.CheckKey(key, KeyCheck.GetPkeyConfigOffice());
                        }
                        // Output via the Worker Thread to Textbox
                        WriteLine(output);
                    }
                }

                if (lstKeys.Count > 0)
                {
                    // Output via the Worker Thread to Textbox
                    Write("---------------------------------------");
                }
                else
                {
                    // Output via the Worker Thread to Textbox
                    Write("No Valid Product Keys Found!");
                }
            }
        }

        private void WorkerBackupCheckKeyListRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Stops the progress bar.
            StopProgressBar();
        }

        private void BtnBackupReinstallKeysClick(object sender, EventArgs e)
        {
            // Start the progress bar.
            StartProgressBar();

            workerBackupReinstallKeys.RunWorkerAsync(textBackupName.Text);
        }

        private void WorkerBackupReinstallKeysDoWork(object sender, DoWorkEventArgs e)
        {
            // The arguments passed to the method was the Backup Name
            string backupName = (string)e.Argument;

            if (String.IsNullOrWhiteSpace(backupName))
            {
                // Output via the Worker Thread to Textbox
                SetText("You did not enter a backup name.");
                return;
            }

            string keyListPath = string.Empty;
            if (_toolkitChoice == Office)
            {
                keyListPath = Settings.Default.LicenseBackupsPath + Path.DirectorySeparatorChar + OfficeVersion.GetOfficeName() + Path.DirectorySeparatorChar + backupName + "\\Keys.ini";
            }
            else if (_toolkitChoice == Windows)
            {
                keyListPath = Settings.Default.LicenseBackupsPath + Path.DirectorySeparatorChar + OSVersion.GetWindowsName() + Path.DirectorySeparatorChar + backupName + "\\Keys.ini";
            }

            if (File.Exists(keyListPath) == false)
            {
                // Output via the Worker Thread to Textbox
                SetText(keyListPath + " Not Found!");
            }
            else
            {
                // Regex Match Source String Variable for Product Keys
                List<string> lstKeys = new List<string>();
                foreach (Match keyMatch in KeyBase.KeyRegex.Matches(File.ReadAllText(keyListPath)))
                {
                    // Don't Add Duplicate Keys
                    if (lstKeys.Contains(keyMatch.ToString()) == false)
                    {
                        lstKeys.Add(keyMatch.ToString());
                    }
                }

                foreach (string key in lstKeys)
                {
                    // Output via the Worker Thread to Textbox
                    WriteLine("---------------------------------------");
                    WriteLine("Installing: " + key);
                    try
                    {
                        if (_toolkitChoice == Office)
                        {
                            KeyInstaller.InstallKeyOffice(key);
                        }
                        else if (_toolkitChoice == Windows)
                        {
                            KeyInstaller.InstallKeyWindows(key);
                        }
                        // Output via the Worker Thread to Textbox
                        WriteLine("<Product key installation successful>");
                    }
                    catch (Exception ex)
                    {
                        // Output via the Worker Thread to Textbox
                        WriteLine(ex.Message);
                    }
                }
                if (lstKeys.Count > 0)
                {
                    // Output via the Worker Thread to Textbox
                    Write("---------------------------------------");
                }
                else
                {
                    // Output via the Worker Thread to Textbox
                    WriteLine("No Valid Product Keys Found!");
                }
            }
        }

        private void WorkerBackupReinstallKeysRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Stops the progress bar.
            StopProgressBar();
        }
        #endregion
        #region Customize Setup
        private void CboInstallerChannelSelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLicenseType = comboInstallerChannel.SelectedItem.ToString();

            switch (selectedLicenseType)
            {
                case "Volume":
                {
                    comboInstallerApp.Items.Clear();
                    // These are the available licenses for the Volume editions.
                    foreach (string product in CustomizeSetup.InstallerInformation.VolumeProducts)
                    {
                        comboInstallerApp.Items.Add(product);
                    }
                    comboInstallerApp.Sorted = true;
                    comboInstallerApp.SelectedIndex = 0;

                    break;
                }
                case "Retail":
                {
                    comboInstallerApp.Items.Clear();

                    // These are the available licenses for the Retail editions.
                    foreach (string product in CustomizeSetup.InstallerInformation.RetailProducts)
                    {
                        comboInstallerApp.Items.Add(product);
                    }
                    comboInstallerApp.Sorted = true;
                    comboInstallerApp.SelectedIndex = 0;

                    break;
                }
                default:
                {
                    comboInstallerApp.Items.Clear();
                    comboInstallerApp.Items.Add("No Detected Products");
                    comboInstallerApp.SelectedIndex = 0;
                    break;
                }
            }
        }

        private void UpdateProducts()
        {
            // Make Sure Setup Exists
            if (String.IsNullOrWhiteSpace(CustomizeSetup.SelectedInstallationPath) == false && File.Exists(CustomizeSetup.SelectedInstallationPath + "setup.exe") == false)
            {
                return;
            }
   
            // Handle Dual Architecture Setup
            bool hasX64Folder = Directory.Exists(CustomizeSetup.SelectedInstallationPath + "x64");
            bool hasX86Folder = Directory.Exists(CustomizeSetup.SelectedInstallationPath + "x86");
            if (hasX86Folder || hasX64Folder)
            {
                if (hasX64Folder == false)
                {
                    CustomizeSetup.SelectedInstallationPath += "x86\\";
                }
                else if (hasX86Folder == false)
                {
                    CustomizeSetup.SelectedInstallationPath += "x64\\";
                }
                else
                {
                    if (MessageBox.Show("Installer contains both x86 and x64 Microsoft Office.\nPress Yes for x86 or No for x64.", "Attention", MessageBoxButtons.YesNo).Equals(DialogResult.Yes))
                    {
                        CustomizeSetup.SelectedInstallationPath += "x86\\";
                    }
                    else
                    {
                        CustomizeSetup.SelectedInstallationPath += "x64\\";
                    }
                }
            }

            // Get Setup Information
            try
            {
                CustomizeSetup.InstallerInformation = new InstallerInfo(CustomizeSetup.SelectedInstallationPath);
            }
            catch (ApplicationException ex)
            {
                SetText(ex.Message);
                comboInstallerApp.Items.Clear();
                comboInstallerApp.Items.Add("No Detected Products");
                comboInstallerApp.SelectedIndex = 0;
                comboInstalledLPs.Items.Clear();
                comboInstalledLPs.Items.Add("No Detected Languages");
                comboInstalledLPs.SelectedIndex = 0;
                comboInstallerChannel.Items.Clear();
                comboInstallerChannel.Items.Add("No Detected Channels");
                comboInstallerChannel.SelectedIndex = 0;
                return;
            }
            
            // Clear UI
            comboInstallerApp.Items.Clear();
            comboInstalledLPs.Items.Clear();
            comboInstallerChannel.Items.Clear();

            // Add Languages
            if (CustomizeSetup.InstallerInformation.Languages.Count > 0)
            {
                foreach (string installedLanguage in CustomizeSetup.InstallerInformation.Languages)
                {
                    comboInstalledLPs.Items.Add(installedLanguage);
                    comboInstalledLPs.Sorted = true;
                }
            }
            else
            {
                comboInstalledLPs.Items.Add("No Detected Languages");
            }
            comboInstalledLPs.SelectedIndex = 0;

            // Get Products and License Channels
            if (CustomizeSetup.InstallerInformation.RetailProducts.Count > 0)
            {
                comboInstallerChannel.Items.Add("Retail");
                comboInstallerChannel.SelectedIndex = 0;
            }
            if (CustomizeSetup.InstallerInformation.VolumeProducts.Count > 0)
            {
                comboInstallerChannel.Items.Add("Volume");
                comboInstallerChannel.SelectedIndex = 0;
            }
            if (CustomizeSetup.InstallerInformation.VolumeProducts.Count == 0 && CustomizeSetup.InstallerInformation.RetailProducts.Count == 0)
            {
                comboInstallerChannel.Items.Add("No Detected Channels");
                comboInstallerChannel.SelectedIndex = 0;
            }

            // Disable Patching Unsupported Microsoft Office Warning
            if (!CustomizeSetup.ChangeChannelSupported())
            {
                btnChangeChannel.Enabled = false;
                SetText(string.Empty);
                WriteLine("WARNING: This copy of Microsoft Office is not supported.");
                WriteLine("Channel Switcher has been disabled.");
            }
            else
            {
                btnChangeChannel.Enabled = true;
            }

            // Set Version Label
            lblVersion.Text = "Office " + CustomizeSetup.InstallerInformation.SetupVersionFull + " " + CustomizeSetup.InstallerInformation.InstallerArchitecture;
        }

        private void BtnBrowseToSetupClick(object sender, EventArgs e)
        {
            // Browse to Setup
            using (OpenFileDialog fdlg = new OpenFileDialog {Title = "Browse To Extracted Microsoft Office Installer Setup.exe", Filter = "Setup.exe|Setup.exe", RestoreDirectory = true})
            {
                if (fdlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                // Check if Path is Writable
                string path = Path.GetDirectoryName(fdlg.FileName) + Path.DirectorySeparatorChar;
                if (!CommonUtilities.IsDirectoryWritable(path))
                {
                    SetText(string.Empty);
                    WriteLine("ERROR: Microsoft Office setup cannot be extracted to a non-writable path!");
                    WriteLine("Please copy or extract setup to a writable location.");
                    return;
                }

                // Get Path of Browsed File
                CustomizeSetup.SelectedInstallationPath = path;
            }

            // Start the progress bar.
            StartProgressBar();

            // Update Product UI
            UpdateProducts();

            // Stops the progress bar.
            StopProgressBar();
        }

        private void BtnExtractSetupClick(object sender, EventArgs e)
        {
            // Browse to Setup
            using (OpenFileDialog fdlg = new OpenFileDialog {Title = "Browse To Microsoft Office EXE or ISO", Filter = "Office SFX|*.exe|Office ISO|*.iso"})
            {
                if (fdlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                // Browse to Folder to Extract To
                using (FolderBrowserDialog ddlg = new FolderBrowserDialog {Description = "What folder to extract to?"})
                {
                    if (ddlg.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    // Check if Path is Writable
                    string path = ddlg.SelectedPath;
                    if (!CommonUtilities.IsDirectoryWritable(path))
                    {
                        SetText(string.Empty);
                        WriteLine("ERROR: Microsoft Office setup cannot be extracted to a non-writable path!");
                        WriteLine("Please copy or extract setup to a writable location.");
                        return;
                    }

                    // Start the progress bar.
                    StartProgressBar();

                    workerExtractSetup.RunWorkerAsync(new ArrayList {fdlg, ddlg});
                }
            }
        }

        private void WorkerExtractSetupDoWork(object sender, DoWorkEventArgs e)
        {
            // The arguments passed to the method were the File and Folder Dialogs
            ArrayList aryList = (ArrayList)e.Argument;

            using (OpenFileDialog fdlg = (OpenFileDialog)aryList[0])
            {
                using (FolderBrowserDialog ddlg = (FolderBrowserDialog)aryList[1])
                {
                    e.Result = ddlg.SelectedPath + Path.DirectorySeparatorChar;

                    // File is an Executable
                    if (fdlg.FilterIndex == 1)
                    {
                        try
                        {
                            Process script = new Process {StartInfo = {FileName = CommonUtilities.EscapePath(fdlg.FileName), Arguments = " /extract:" + CommonUtilities.EscapePath(ddlg.SelectedPath) + " /quiet"}};
                            script.Start();
                            script.WaitForExit();

                            // Get Path of Browsed File
                            if (File.Exists(ddlg.SelectedPath + "\\setup.exe"))
                            {
                                CustomizeSetup.SelectedInstallationPath = ddlg.SelectedPath + Path.DirectorySeparatorChar;
                                SetText("Successfully extracted Microsoft Office Setup.");
                            }
                            else
                            {
                                throw new Exception("setup.exe not found at the extraction path.");
                            }
                        }
                        catch (Exception ex)
                        {
                            SetText("Failed to extract Office SFX!" + Environment.NewLine + ex.Message + Environment.NewLine);
                        }
                    }
                    // File is an ISO
                    else if (fdlg.FilterIndex == 2)
                    {
                        try
                        {
                            CustomizeSetup.CopyFilesfromISO(fdlg.FileName, ddlg.SelectedPath + Path.DirectorySeparatorChar);
                            if (File.Exists(ddlg.SelectedPath + "\\setup.exe"))
                            {
                                CustomizeSetup.SelectedInstallationPath = ddlg.SelectedPath + Path.DirectorySeparatorChar;
                                SetText("Successfully extracted Microsoft Office Setup.");
                            }
                            else
                            {
                                throw new Exception("setup.exe not found at the extraction path.");
                            }
                        }
                        catch (Exception ex)
                        {
                            SetText("Failed to extract Office ISO!" + Environment.NewLine + ex.Message + Environment.NewLine);
                        }
                    }
                }
            }
        }

        private void WorkerExtractSetupRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Update Products UI
            UpdateProducts();

            // Stops the progress bar.
            StopProgressBar();
        }

        private void BtnAddUpdateClick(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(CustomizeSetup.SelectedInstallationPath))
            {
                SetText("You have to first browse to Microsoft Office Setup!");
            }
            else
            {
                using (OpenFileDialog fdlg = new OpenFileDialog { Multiselect = true, Title = "Browse To Microsoft Office Update(s)", Filter = "Update File (*.exe;*.msp)|*.exe;*.msp", RestoreDirectory = true })
                {
                    if (fdlg.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    // Start the progress bar.
                    StartProgressBar();

                    workerAddUpdate.RunWorkerAsync(fdlg);
                }
            }
        }

        private void WorkerAddUpdateDoWork(object sender, DoWorkEventArgs e)
        {
            // The argument passed to the method was the File Dialog
            using (OpenFileDialog fdlg = (OpenFileDialog)e.Argument)
            {
                foreach (string file in fdlg.FileNames)
                {
                    try
                    {
                        CustomizeSetup.AddUpdate(file);
                        WriteLine("Successfully integrated " + Path.GetFileName(file));
                    }
                    catch (Exception ex)
                    {
                        WriteLine("Failed to integrate " + Path.GetFileName(file));
                        WriteLine(ex.Message);
                    }
                }
            }
        }

        private void WorkerAddUpdateRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Stops the progress bar.
            StopProgressBar();
        }

        private void BtnAddLPClick(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(CustomizeSetup.SelectedInstallationPath))
            {
                SetText("You have to first browse to Microsoft Office Setup!");
            }
            else
            {
                using (OpenFileDialog fdlg = new OpenFileDialog { Multiselect = true, Title = "Browse To Microsoft Office Language Pack(s)", Filter = "Language Pack File (*.exe;*.iso)|*.exe;*.iso", RestoreDirectory = true })
                {
                    if (fdlg.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    // Start the progress bar.
                    StartProgressBar();

                    workerAddLP.RunWorkerAsync(fdlg);
                }
            }
        }

        private void WorkerAddLPDoWork(object sender, DoWorkEventArgs e)
        {
            // The argument passed to the method was the File Dialog
            using (OpenFileDialog fdlg = (OpenFileDialog)e.Argument)
            {
                foreach (string file in fdlg.FileNames)
                {
                    try
                    {
                        CustomizeSetup.AddLanguagePack(file);
                        WriteLine("Successfully integrated " + Path.GetFileName(file));
                    }
                    catch (Exception ex)
                    {
                        WriteLine("Failed to integrate " + Path.GetFileName(file));
                        WriteLine(ex.Message);
                    }
                }
            }
        }

        private void WorkerAddLPRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Update Products UI
            UpdateProducts();

            // Stops the progress bar.
            StopProgressBar();
        }

        private void BtnRemoveLPClick(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(CustomizeSetup.SelectedInstallationPath))
            {
                SetText("You have to first browse to Microsoft Office Setup!");
            }
            else if (comboInstalledLPs.Text == "No Detected Languages")
            {
                SetText("Error! For Some Reason Languages were not detected in the chosen setup!");
            }
            else
            {
                // Start the progress bar.
                StartProgressBar();

                workerRemoveLP.RunWorkerAsync(comboInstalledLPs.Text);
            }
        }

        private void WorkerRemoveLPDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // The argument passed to the method was the Language Name
                CustomizeSetup.RemoveLanguagePack(e.Argument.ToString());
                e.Result = "Language Removal Complete.";
            }
            catch (Exception ex)
            {
                e.Result = "Language Removal Failed!" + Environment.NewLine + ex.Message;
            }
        }

        private void WorkerRemoveLPRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Output the Result
            SetText(e.Result.ToString());

            // Update Products UI
            UpdateProducts();

            // Stops the progress bar.
            StopProgressBar();
        }

        private void BtnRunOCTClick(object sender, EventArgs e)
        {
            // Start the progress bar.
            StartProgressBar();

            if (String.IsNullOrWhiteSpace(CustomizeSetup.SelectedInstallationPath))
            {
                SetText("You have to first browse to Microsoft Office Setup!");
            }
            else
            {
                CustomizeSetup.RunOCT();
            }

            // Stops the progress bar.
            StopProgressBar();
        }

        private void BtnChangeChannelClick(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(CustomizeSetup.SelectedInstallationPath))
            {
                SetText("You have to first browse to Microsoft Office Setup!");
            }
            else if (comboInstallerChannel.Text == "No Detected Channels")
            {
                SetText("Error! For Some Reason No Convertable Products were found!");
            }
            else
            {
                // Start the progress bar.
                StartProgressBar();

                workerChangeChannel.RunWorkerAsync(new List<string> {comboInstallerChannel.Text, comboInstallerApp.Text});
            }
        }

        private void WorkerChangeChannelDoWork(object sender, DoWorkEventArgs e)
        {
            // The arguments passed to the method were the License Channel and Product Name
            List<string> parameters = (List<string>)e.Argument;

            try
            {
                // Run Channel Changer
                CustomizeSetup.ChangeChannel(parameters[0], parameters[1]);
                e.Result = "Successfully changed Microsoft Office Setup License Channel.";
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }

        }

        private void WorkerChangeChannelRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Output the Result
            SetText(e.Result.ToString());

            // Update Products UI
            UpdateProducts();

            // Stops the progress bar.
            StopProgressBar();
        }

        private void BtnMakeISOClick(object sender, EventArgs e)
        {
            using (OpenFileDialog fdlg = new OpenFileDialog {Title = "Browse To Extracted Microsoft Office Installer Setup.exe", Filter = "Setup.exe|Setup.exe", RestoreDirectory = true})
            {
                if (fdlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                using (SaveFileDialog sdlg = new SaveFileDialog {Title = "Save ISO as?", Filter = "Office ISO|*.iso"})
                {
                    if (sdlg.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    // Start the progress bar.
                    StartProgressBar();

                    workerMakeISO.RunWorkerAsync(new List<string> {fdlg.FileName, sdlg.FileName});
                }
            }
        }

        private void WorkerMakeISODoWork(object sender, DoWorkEventArgs e)
        {
            // The arguments passed to the method were the Open and Save File Dialogs
            List<string> parameters = (List<string>)e.Argument;

            try
            {
                CustomizeSetup.MakeISO(Path.GetDirectoryName(parameters[0]), parameters[1]);
                e.Result = "Successfully made ISO!";
            }
            catch (Exception ex)
            {
                e.Result = "ISO Creation Failed!" + Environment.NewLine + ex.Message;
            }
            
        }

        private void WorkerMakeISORunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Output the Result
            SetText(e.Result.ToString());

            // Stops the progress bar
            StopProgressBar();
        }
        #endregion
    }
}