using System;
using System.Windows.Forms;

namespace MicrosoftToolkit
{
    public partial class ReadmeWindow : Form
    {
        public ReadmeWindow()
        {
            InitializeComponent();
        }

        // TODO: Remove
        private void BtnCopyReadmeClick(object sender, EventArgs e)
        {
            if (tabReadme.SelectedTab.Controls.Count > 0)
            {
                Clipboard.SetDataObject(tabReadme.SelectedTab.Controls[0].Text, true);
            }
        }
    }
}