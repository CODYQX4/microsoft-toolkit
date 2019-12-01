using System;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Common;
using Configuration;
using Keys;
using Microsoft.Win32;
using MicrosoftToolkit.Properties;

namespace MicrosoftToolkit
{
    public partial class SettingsWindow : Form
    {
        /// <summary>
        /// This variable is used to control if the user clicked the "Save" button, because the FormClosing event tells the user "the
        /// changes will not be saved", but if the user clicked the "Save" button the user is not prompted with that question.
        /// </summary>
        private bool _clickOnSaveButton;

        public SettingsWindow()
        {
            InitializeComponent();

            // ClickOnSaveButton is set to false
            _clickOnSaveButton = false;

            // Loads the settings stored in the Settings Object
            LoadStoredSettings();
        }

        /// <summary>
        /// Load Default Settings into Form and Save
        /// </summary>
        private void LoadDefaultSettings()
        {
            // Reset Default Settings
            Settings.Default.Reset();

            // Update Settings Manager and Save
            SettingsManager.Save(Settings.Default);
            
            // Load Stored Settings
            LoadStoredSettings();
        }

        /// <summary>
        /// Load Settings
        /// </summary>
        private void LoadStoredSettings()
        {
            // KMS Server
            KMSSettingstxtKMSServerOffice.Text = Settings.Default.KMSServerOffice;
            KMSSettingstxtKMSServerWindows.Text = Settings.Default.KMSServerWindows;

            // KMS Port
            KMSSettingstxtKMSPortOffice.Value = Settings.Default.KMSPortOffice;
            KMSSettingstxtKMSPortWindows.Value = Settings.Default.KMSPortWindows;

            // KMS PID
            KMSSettingstxtKMSPIDOffice.Text = Settings.Default.KMSPIDOffice;
            KMSSettingstxtKMSPIDWindows.Text = Settings.Default.KMSPIDWindows;

            // KMS Hardware ID
            KMSSettingstxtKMSHWID.Text = Settings.Default.KMSHardwareID;

            // KMS Connection
            KMSSettingscheckRemoveKMSConnection.Checked = Settings.Default.RemoveKMSConnection;
            KMSSettingscheckKillProcessOnPort.Checked = Settings.Default.KillProcessOnPort;

            // KMS Emulator
            KMSSettingscheckUseKMSEmulator.Checked = Settings.Default.UseKMSEmulator;

            // License Display
            LicenseDisplaycheckShowCMID.Checked = Settings.Default.ShowCMID;
            LicenseDisplaycheckShowUnlicensed.Checked = Settings.Default.ShowUnlicensed;

            // LocalHost Bypass
            LocalHostBypassIPAddress.Text = Settings.Default.LocalHostBypassIPAddress;
            LocalHostBypassIPSubnet.Text = Settings.Default.LocalHostBypassIPSubnet;
            LocalHostBypassUseDLLInjection.Checked = Settings.Default.LocalHostBypassUseDLLInjection;
            LocalHostBypassUseTAPAdapter.Checked = Settings.Default.LocalHostBypassUseTAPAdapter;
            LocalHostBypassUseWinDivertClient.Checked = Settings.Default.LocalHostBypassUseWinDivertClient;

            // Paths
            PathstxtAutoKMS.Text = Settings.Default.AutoKMSPath;
            PathstxtAutoRearm.Text = Settings.Default.AutoRearmPath;
            PathstxtKMSServerService.Text = Settings.Default.KMSServerServicePath;
            PathstxtLicenseBackups.Text = Settings.Default.LicenseBackupsPath;

            // Handle KMS Server Service
            if (!KMSServerServiceInstaller.KMSServerServiceInstaller.IsKMSServerServiceInstalled())
            {
                KSScheckKillProcessOnPort.Checked = true;
                KSStxtKMSPID.Text = "RandomKMSPID";
                KSStxtKMSHWID.Text = "364F463A8863D35F";
                KSStxtKMSPort.Value = 1688;
                KSStxtActivationInterval.Value = 120;
                KSStxtRenewalInterval.Value = 10080;
                KSScheckKillProcessOnPort.Enabled = false;
                KSStxtKMSPID.Enabled = false;
                KSStxtKMSHWID.Enabled = false;
                KSStxtKMSPort.Enabled = false;
                KSStxtActivationInterval.Enabled = false;
                KSStxtRenewalInterval.Enabled = false;
                btnKSSMoreSettings.Enabled = false;
            }
            else
            {
                // Get KMS Server Service Parameters
                using (RegistryKey registry = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\KMSServerService\\Parameters", true))
                {
                    if (registry != null)
                    {
                        // Get KMS PID
                        string kmsPid = Convert.ToString(registry.GetValue("KMSPID"));
                        if (kmsPid == "RandomKMSPID" || KeyBase.IsValidPKEYID(kmsPid))
                        {
                            KSStxtKMSPID.Text = kmsPid;
                        }

                        // Get KMS HWID
                        string kmsHwid = Convert.ToString(registry.GetValue("KMSHWID"));
                        if (Regex.IsMatch(kmsHwid, "[a-fA-F0-9]{16}"))
                        {
                            KSStxtKMSHWID.Text = kmsHwid;
                        }

                        // Get KMS Port
                        int kmsPort = Convert.ToInt32(registry.GetValue("KMSPort"));
                        if (kmsPort >= 1 && kmsPort <= 65535)
                        {
                            KSStxtKMSPort.Value = kmsPort;
                        }

                        // Get KMS Activation Interval
                        int kmsActivationInterval = Convert.ToInt32(registry.GetValue("VLActivationInterval"));
                        if (kmsActivationInterval >= 15 && kmsActivationInterval <= 43200)
                        {
                            KSStxtActivationInterval.Value = kmsActivationInterval;
                        }

                        // Get KMS Renewal Interval
                        int kmsRenewalInterval = Convert.ToInt32(registry.GetValue("VLRenewalInterval"));
                        if (kmsRenewalInterval >= 15 && kmsActivationInterval <= 43200)
                        {
                            KSStxtRenewalInterval.Value = kmsRenewalInterval;
                        }

                        // Get Free TCP/IP Port
                        if (registry.GetValue("KillProcessOnPort") != null)
                        {
                            KSScheckKillProcessOnPort.Checked = Convert.ToBoolean(registry.GetValue("KillProcessOnPort"));
                        }
                        else
                        {
                            KSScheckKillProcessOnPort.Checked = true;
                        }
                    }
                }
            }            
        }

        /// <summary>
        /// Save Settings
        /// </summary>
        private void SaveStoredSettings()
        {
            // KMS Server
            Settings.Default.KMSServerOffice = KMSSettingstxtKMSServerOffice.Text;
            Settings.Default.KMSServerWindows = KMSSettingstxtKMSServerWindows.Text;

            // KMS Port
            Settings.Default.KMSPortOffice = Convert.ToInt32(KMSSettingstxtKMSPortOffice.Value);
            Settings.Default.KMSPortWindows = Convert.ToInt32(KMSSettingstxtKMSPortWindows.Value);

            // KMS PID
            Settings.Default.KMSPIDOffice = KMSSettingstxtKMSPIDOffice.Text;
            Settings.Default.KMSPIDWindows = KMSSettingstxtKMSPIDWindows.Text;

            // KMS Hardware ID
            Settings.Default.KMSHardwareID = KMSSettingstxtKMSHWID.Text;

            // KMS Connection
            Settings.Default.RemoveKMSConnection = KMSSettingscheckRemoveKMSConnection.Checked;
            Settings.Default.KillProcessOnPort = KMSSettingscheckKillProcessOnPort.Checked;

            // KMS Emulator
            Settings.Default.UseKMSEmulator = KMSSettingscheckUseKMSEmulator.Checked;

            // License Display
            Settings.Default.ShowCMID = LicenseDisplaycheckShowCMID.Checked;
            Settings.Default.ShowUnlicensed = LicenseDisplaycheckShowUnlicensed.Checked;

            // LocalHost Bypass
            Settings.Default.LocalHostBypassIPAddress = LocalHostBypassIPAddress.Text;
            Settings.Default.LocalHostBypassIPSubnet = LocalHostBypassIPSubnet.Text;
            Settings.Default.LocalHostBypassUseDLLInjection = LocalHostBypassUseDLLInjection.Checked;
            Settings.Default.LocalHostBypassUseTAPAdapter = LocalHostBypassUseTAPAdapter.Checked;
            Settings.Default.LocalHostBypassUseWinDivertClient = LocalHostBypassUseWinDivertClient.Checked;

            // Paths
            Settings.Default.AutoKMSPath = PathstxtAutoKMS.Text;
            Settings.Default.AutoRearmPath = PathstxtAutoRearm.Text;
            Settings.Default.KMSServerServicePath = PathstxtKMSServerService.Text;
            Settings.Default.LicenseBackupsPath = PathstxtLicenseBackups.Text;

            // Update Settings Manager and Save
            SettingsManager.Save(Settings.Default);

            // Handle KMS Server Service
            if (KMSServerServiceInstaller.KMSServerServiceInstaller.IsKMSServerServiceInstalled())
            {
                using (RegistryKey registry = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\KMSServerService\\Parameters"))
                {
                    if (registry != null)
                    {
                        // Set KMS PID
                        registry.SetValue("KMSPID", KSStxtKMSPID.Text, RegistryValueKind.String);

                        // Set KMS HWID
                        registry.SetValue("KMSHWID", KSStxtKMSHWID.Text, RegistryValueKind.String);

                        // Set KMS Port
                        registry.SetValue("KMSPort", KSStxtKMSPort.Value, RegistryValueKind.String);

                        // Set KMS Activation Interval
                        registry.SetValue("VLActivationInterval", KSStxtActivationInterval.Value, RegistryValueKind.DWord);

                        // Set KMS Renewal Interval
                        registry.SetValue("VLRenewalInterval", KSStxtRenewalInterval.Value, RegistryValueKind.DWord);

                        // Set Free TCP/IP Port
                        if (KSScheckKillProcessOnPort.Checked)
                        {
                            registry.SetValue("KillProcessOnPort", 1, RegistryValueKind.DWord);
                        }
                        else
                        {
                            registry.SetValue("KillProcessOnPort", 0, RegistryValueKind.DWord);
                        }
                    }
                }
            }
        }
        
        private void BtnLoadDefaultsClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("All the settings will be set to their default value and will be saved.\nDo you want to continue?", "Attention", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LoadDefaultSettings();
            }
        }

        private void BtnSaveSettingsClick(object sender, EventArgs e)
        {
            // Lock Form While Saving Settings
            btnSaveSettings.Enabled = false;
            btnLoadDefaults.Enabled = false;
            tabSettings.Enabled = false;

            // Check Set PID
            if (!KeyBase.IsValidPKEYID(KMSSettingstxtKMSPIDOffice.Text) && KMSSettingstxtKMSPIDOffice.Text != "DefaultKMSPID" && KMSSettingstxtKMSPIDOffice.Text != "RandomKMSPID" && KMSSettingstxtKMSPIDOffice.Text != "ReuseKMSPID")
            {
                tabSettings.SelectedTab = tabKMSOptions;

                // Set error colors.
                KMSSettingstxtKMSPIDOffice.BackColor = Color.Red;
                KMSSettingstxtKMSPIDOffice.ForeColor = Color.White;

                MessageBox.Show("The KMS PID has an invalid format" + Environment.NewLine + "The correct format is: XXXXX-XXXXX-XXX-XXXXXX-XX-LCID-OSBUILD.XXXX-XXXXXXX");

                btnSaveSettings.Enabled = true;
                btnLoadDefaults.Enabled = true;
                tabSettings.Enabled = true;
                return;
            }
            if (!KeyBase.IsValidPKEYID(KMSSettingstxtKMSPIDWindows.Text) && KMSSettingstxtKMSPIDWindows.Text != "DefaultKMSPID" && KMSSettingstxtKMSPIDWindows.Text != "RandomKMSPID" && KMSSettingstxtKMSPIDWindows.Text != "ReuseKMSPID")
            {
                tabSettings.SelectedTab = tabKMSOptions;

                // Set error colors.
                KMSSettingstxtKMSPIDWindows.BackColor = Color.Red;
                KMSSettingstxtKMSPIDWindows.ForeColor = Color.White;

                MessageBox.Show("The KMS PID has an invalid format" + Environment.NewLine + "The correct format is: XXXXX-XXXXX-XXX-XXXXXX-XX-LCID-OSBUILD.XXXX-XXXXXXX");

                btnSaveSettings.Enabled = true;
                btnLoadDefaults.Enabled = true;
                tabSettings.Enabled = true;
                return;
            }
            if (!KeyBase.IsValidPKEYID(KSStxtKMSPID.Text) && KSStxtKMSPID.Text != "RandomKMSPID")
            {
                tabSettings.SelectedTab = tabKMSServerService;

                // Set error colors.
                KSStxtKMSPID.BackColor = Color.Red;
                KSStxtKMSPID.ForeColor = Color.White;

                MessageBox.Show("The KMS PID has an invalid format" + Environment.NewLine + "The correct format is: XXXXX-XXXXX-XXX-XXXXXX-XX-LCID-OSBUILD.XXXX-XXXXXXX");

                btnSaveSettings.Enabled = true;
                btnLoadDefaults.Enabled = true;
                tabSettings.Enabled = true;
                return;
            }

            // Check Set HWID
            if (!Regex.IsMatch(KMSSettingstxtKMSHWID.Text, "[a-fA-F0-9]{16}"))
            {
                tabSettings.SelectedTab = tabKMSOptions;

                // Set error colors.
                KMSSettingstxtKMSHWID.BackColor = Color.Red;
                KMSSettingstxtKMSHWID.ForeColor = Color.White;

                MessageBox.Show("The KMS Hardware ID has an invalid format" + Environment.NewLine + "The correct format is: XXXXXXXXXXXXXXXX (0-9, A-F, a-f)");

                btnSaveSettings.Enabled = true;
                btnLoadDefaults.Enabled = true;
                tabSettings.Enabled = true;
                return;
            }
            if (!Regex.IsMatch(KSStxtKMSHWID.Text, "[a-fA-F0-9]{16}"))
            {
                tabSettings.SelectedTab = tabKMSServerService;

                // Set error colors.
                KSStxtKMSHWID.BackColor = Color.Red;
                KSStxtKMSHWID.ForeColor = Color.White;

                MessageBox.Show("The KMS Hardware ID has an invalid format" + Environment.NewLine + "The correct format is: XXXXXXXXXXXXXXXX (0-9, A-F, a-f)");

                btnSaveSettings.Enabled = true;
                btnLoadDefaults.Enabled = true;
                tabSettings.Enabled = true;
                return;
            }

            // Check LocalHost Bypass IP Configuration
            bool iperror = false;
            bool ipaddresserror = false;
            string iperrormessage = string.Empty;
            IPAddress ipAddress, ipSubnet;
            if (!Regex.IsMatch(LocalHostBypassIPAddress.Text, @"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$") || !IPAddress.TryParse(LocalHostBypassIPAddress.Text, out ipAddress))
            {
                iperror = true;
                ipaddresserror = true;
                iperrormessage = "The LocalHost Bypass IP Address is not a valid IPV4 Address!";
            }
            else if (!Regex.IsMatch(LocalHostBypassIPSubnet.Text, @"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$") || !IPAddress.TryParse(LocalHostBypassIPSubnet.Text, out ipSubnet))
            {
                iperror = true;
                iperrormessage = "The LocalHost Bypass Subnet Mask is not a valid IPV4 Address!";
            }
            else if (!CommonUtilities.IsValidSubnet(ipSubnet))
            {
                iperror = true;
                iperrormessage = "The LocalHost Bypass Subnet Mask is not a valid Subnet Mask!";
            }
            else if (ipAddress.Equals(CommonUtilities.GetNetworkAddress(ipAddress, ipSubnet)))
            {
                iperror = true;
                ipaddresserror = true;
                iperrormessage = "The LocalHost Bypass IP Address cannot be the network address!";
            }
            else if (ipAddress.Equals(CommonUtilities.GetBroadcastAddress(ipAddress, ipSubnet)))
            {
                iperror = true;
                ipaddresserror = true;
                iperrormessage = "The LocalHost Bypass IP Address cannot be the broadcast address!";
            }
            else if (Regex.IsMatch(LocalHostBypassIPAddress.Text, @"^(169\.254\.([0,1]?[0-9]{1,2}|2[0-4][0-9]|25[0-5])\.([0,1]?[0-9]{1,2}|2[0-4][0-9]|25[0-5]))$"))
            {
                iperror = true;
                ipaddresserror = true;
                iperrormessage = "The LocalHost Bypass IP Address cannot be an APIPA address!";
            }
            if (iperror)
            {
                tabSettings.SelectedTab = tabLocalHostBypass;

                // Set error colors.
                if (ipaddresserror)
                {
                    LocalHostBypassIPAddress.BackColor = Color.Red;
                    LocalHostBypassIPAddress.ForeColor = Color.White;
                }
                else
                {
                    LocalHostBypassIPSubnet.BackColor = Color.Red;
                    LocalHostBypassIPSubnet.ForeColor = Color.White;
                }

                MessageBox.Show(iperrormessage);

                btnSaveSettings.Enabled = true;
                btnLoadDefaults.Enabled = true;
                tabSettings.Enabled = true;
                return;
            }


            // Save Settings
            SaveStoredSettings();

            // Restart KMS Server Service
            if (KMSServerServiceInstaller.KMSServerServiceInstaller.IsKMSServerServiceInstalled())
            {
                Services.StopService(KMSServerServiceInstaller.KMSServerServiceInstaller.KMSServerServiceName, KMSServerServiceInstaller.KMSServerServiceInstaller.KMSServerServiceProcessName);
                Services.StartService(KMSServerServiceInstaller.KMSServerServiceInstaller.KMSServerServiceName);
            }

            // Close Form
            _clickOnSaveButton = true;
            Close();
        }

        private void BtnBrowseAutoKMSClick(object sender, EventArgs e)
        {
            using (FolderBrowserDialog ddlg = new FolderBrowserDialog {Description = "What folder to install AutoKMS to?"})
            {
                if (ddlg.ShowDialog() == DialogResult.OK)
                {
                    PathstxtAutoKMS.Text = ddlg.SelectedPath;
                }
            }
        }

        private void BtnBrowseAutoRearmClick(object sender, EventArgs e)
        {
            using (FolderBrowserDialog ddlg = new FolderBrowserDialog {Description = "What folder to install AutoRearm to?"})
            {
                if (ddlg.ShowDialog() == DialogResult.OK)
                {
                    PathstxtAutoRearm.Text = ddlg.SelectedPath;
                }
            }
        }

        private void BtnBrowseKMSServerServiceClick(object sender, EventArgs e)
        {
            using (FolderBrowserDialog ddlg = new FolderBrowserDialog {Description = "What folder to install KMS Server Service to?"})
            {
                if (ddlg.ShowDialog() == DialogResult.OK)
                {
                    PathstxtKMSServerService.Text = ddlg.SelectedPath;
                }
            }
        }

        private void BtnBrowseLicenseBackupsClick(object sender, EventArgs e)
        {
            using (FolderBrowserDialog ddlg = new FolderBrowserDialog { Description = "What folder to save License Backups to?" })
            {
                if (ddlg.ShowDialog() == DialogResult.OK)
                {
                    PathstxtLicenseBackups.Text = ddlg.SelectedPath;
                }
            }
        }

        private void ErrorTextChanged(object sender, EventArgs e)
        {
            TextBox text = (TextBox)sender;

            // Change color of KMSPID entry textbox
            if (text.BackColor != Color.Red)
            {
                return;
            }
            text.BackColor = Color.White;
            text.ForeColor = Color.Black;
        }

        private void SettingsWindowFormClosing(object sender, FormClosingEventArgs e)
        {
            //If the user selects "No", enter inside the "if statement" 
            if (!_clickOnSaveButton && MessageBox.Show("If you made a change it will not be saved.\nAre you sure you want to close this window?", "Attention", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                // Don't Close Form
                e.Cancel = true;
            }
        }

        private void BtnKSSMoreSettingsClick(object sender, EventArgs e)
        {
            // Create new window
            using (SettingsWindowKSSApplicationSpecific kssMoreWindow = new SettingsWindowKSSApplicationSpecific())
            {
                kssMoreWindow.ShowDialog(this);
            }
        }
    }
}
