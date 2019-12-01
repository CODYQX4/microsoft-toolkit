using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Keys
{
    /// <summary>
    /// Form to Obtain multiple Product Keys for PIDX Checking
    /// </summary>
    public partial class BatchPIDXCheck : Form
    {
        /// <summary>
        /// Accessible List of Keys in the ListBox
        /// </summary>
        public static List<string> MatchedKeys { get; private set; }

        public BatchPIDXCheck()
        {
            InitializeComponent();

            // Empty List On Form Creation
            MatchedKeys = new List<string>();
        }

        private void BtnAddKeysToListClick(object sender, EventArgs e)
        {
            // String to Store Clipboard or File Text
            string source = string.Empty;

            // Read from Clipboard
            if (rdoClipboard.Checked)
            {
                source = Clipboard.GetText();
            }
            // Read from File
            else if (rdoFile.Checked)
            {
                // Create new Open File Dialog
                using (OpenFileDialog fdlg = new OpenFileDialog {Title = "Browse to Key List File"})
                {
                    if (fdlg.ShowDialog() == DialogResult.OK)
                    {
                        source = File.ReadAllText(fdlg.FileName);
                    }
                }
            }
            // Regex Match Source String Variable for Product Keys
            foreach (Match keyMatch in KeyBase.KeyRegex.Matches(source))
            {
                // Don't Add Duplicate Keys
                if (lstKeys.Items.Contains(keyMatch.ToString()) == false)
                {
                    lstKeys.Items.Add(keyMatch.ToString());
                }
            }
        }

        private void BtnKeysCheckClick(object sender, EventArgs e)
        {
            // Add all Keys to Public List and Close Form
            foreach (string key in lstKeys.Items)
            {
                MatchedKeys.Add(key);
            }

            // Close Form
            Close();
        }
    }
}