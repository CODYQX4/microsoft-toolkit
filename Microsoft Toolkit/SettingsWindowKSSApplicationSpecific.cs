using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MicrosoftToolkit
{
    public partial class SettingsWindowKSSApplicationSpecific : Form
    {
        /// <summary>
        /// Form Constructor
        /// </summary>
        public SettingsWindowKSSApplicationSpecific()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create Needed Registry Keys for Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsWindowKSSApplicationSpecificLoad(object sender, EventArgs e)
        {
            // Create Registry Key
            using (RegistryKey registry = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\KMSServerService", true))
            {
                if (registry != null)
                {
                    registry.CreateSubKey("Parameters");
                    using (RegistryKey registry2 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\KMSServerService\\Parameters", true))
                    {
                        if (registry2 != null)
                        {
                            registry2.CreateSubKey("KMSPID");
                            registry2.CreateSubKey("KMSHWID");
                        }
                    }
                }
            }

            // Load First Index
            cboApplicationType.SelectedIndex = 0;
        }

        /// <summary>
        /// Populate or Clear Application ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboApplicationTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear Errors
            txtKMSPID.BackColor = Color.White;
            txtKMSPID.ForeColor = Color.Black;
            txtKMSPID.Text = string.Empty;
            txtKMSHWID.BackColor = Color.White;
            txtKMSHWID.ForeColor = Color.Black;
            txtKMSHWID.Text = string.Empty;

            if (cboApplicationType.Text == "Microsoft Windows")
            {
                txtApplicationID.Enabled = false;
                txtApplicationID.Text = "55C92734-D682-4D71-983E-D6EC3F16059F";
            }
            else if (cboApplicationType.Text == "Microsoft Office 2010")
            {
                txtApplicationID.Enabled = false;
                txtApplicationID.Text = "59A52881-A989-479D-AF46-F275C6370663";
            }
            else if (cboApplicationType.Text == "Microsoft Office 2013-2016")
            {
                txtApplicationID.Enabled = false;
                txtApplicationID.Text = "0FF1CE15-A989-479D-Af46-F275C6370663";
            }
        }

        /// <summary>
        /// Attempt to get KMSPID and KMSHWID if the Application ID field is a valid GUID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtApplicationIDTextChanged(object sender, EventArgs e)
        {
            // Clear Errors
            txtApplicationID.BackColor = Color.White;
            txtApplicationID.ForeColor = Color.Black;

            // Get KMSPID if it exists
            if (Regex.IsMatch(txtApplicationID.Text, @"^[a-fA-F\d]{8}-([a-fA-F\d]{4}-){3}[a-fA-F\d]{12}$"))
            {
                using (RegistryKey registry = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\KMSServerService\\Parameters\\KMSPID", true))
                {
                    if (registry != null && registry.GetValue(txtApplicationID.Text) != null)
                    {
                        txtKMSPID.Text = registry.GetValue(txtApplicationID.Text).ToString();
                    }
                }
            }
            else
            {
                txtKMSPID.Text = string.Empty;
            }

            // Get KMSHWID if it exists
            if (Regex.IsMatch(txtApplicationID.Text, @"^[a-fA-F\d]{8}-([a-fA-F\d]{4}-){3}[a-fA-F\d]{12}$"))
            {
                using (RegistryKey registry = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\KMSServerService\\Parameters\\KMSHWID", true))
                {
                    if (registry != null && registry.GetValue(txtApplicationID.Text) != null)
                    {
                        txtKMSHWID.Text = registry.GetValue(txtApplicationID.Text).ToString();
                    }
                }
            }
            else
            {
                txtKMSHWID.Text = string.Empty;
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

        /// <summary>
        /// Save KMSPID in the Registry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveKMSPIDClick(object sender, EventArgs e)
        {
            // Check Application ID
            if (!Regex.IsMatch(txtApplicationID.Text, @"^[a-fA-F\d]{8}-([a-fA-F\d]{4}-){3}[a-fA-F\d]{12}$"))
            {
                // Set error colors.
                txtApplicationID.BackColor = Color.Red;
                txtApplicationID.ForeColor = Color.White;

                MessageBox.Show("The Application ID has an invalid format" + Environment.NewLine + "The correct format is: XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX");

                return;
            }

            // Check KMSPID
            if (!Keys.KeyBase.IsValidPKEYID(txtKMSPID.Text))
            {
                // Set error colors.
                txtKMSPID.BackColor = Color.Red;
                txtKMSPID.ForeColor = Color.White;

                MessageBox.Show("The KMS PID has an invalid format" + Environment.NewLine + "The correct format is: XXXXX-XXXXX-XXX-XXXXXX-XX-LCID-OSBUILD.XXXX-XXXXXXX");

                return;
            }

            // Write KMSPID in the Registry
            using (RegistryKey registry = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\KMSServerService\\Parameters\\KMSPID", true))
            {
                if (registry != null)
                {
                    registry.SetValue(txtApplicationID.Text, txtKMSPID.Text);
                    if (registry.GetValue(txtApplicationID.Text).ToString() == txtKMSPID.Text)
                    {
                        // Success
                        txtKMSPID.BackColor = Color.Green;
                        txtKMSPID.ForeColor = Color.White;
                    }
                    else
                    {
                        // Error
                        txtKMSPID.BackColor = Color.Red;
                        txtKMSPID.ForeColor = Color.White;

                        MessageBox.Show("Failed to set KMS PID in the registry!");
                    }
                }
            }
        }

        /// <summary>
        /// Save KMSHWID in the Registry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveKMSHWIDClick(object sender, EventArgs e)
        {
            // Check Application ID
            if (!Regex.IsMatch(txtApplicationID.Text, @"^[a-fA-F\d]{8}-([a-fA-F\d]{4}-){3}[a-fA-F\d]{12}$"))
            {
                // Set error colors.
                txtApplicationID.BackColor = Color.Red;
                txtApplicationID.ForeColor = Color.White;

                MessageBox.Show("The Application ID has an invalid format" + Environment.NewLine + "The correct format is: XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX");

                return;
            }

            // Check KMSHWID
            if (!Regex.IsMatch(txtKMSHWID.Text, "[a-fA-F0-9]{16}"))
            {
                // Set error colors.
                txtKMSHWID.BackColor = Color.Red;
                txtKMSHWID.ForeColor = Color.White;

                MessageBox.Show("The KMS Hardware ID has an invalid format" + Environment.NewLine + "The correct format is: XXXXXXXXXXXXXXXX (0-9, A-F, a-f)");

                return;
            }

            // Write KMSHWID in the Registry
            using (RegistryKey registry = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\KMSServerService\\Parameters\\KMSHWID", true))
            {
                if (registry != null)
                {
                    registry.SetValue(txtApplicationID.Text, txtKMSHWID.Text);
                    if (registry.GetValue(txtApplicationID.Text).ToString() == txtKMSHWID.Text)
                    {
                        // Success
                        txtKMSHWID.BackColor = Color.Green;
                        txtKMSHWID.ForeColor = Color.White;
                    }
                    else
                    {
                        // Error
                        txtKMSHWID.BackColor = Color.Red;
                        txtKMSHWID.ForeColor = Color.White;

                        MessageBox.Show("Failed to set KMS HWID in the registry!");
                    }
                }
            }
        }
    }
}