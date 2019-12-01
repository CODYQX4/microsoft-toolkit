using System;
using System.Drawing;
using System.Windows.Forms;

namespace Keys
{
    /// <summary>
    /// Form for obtaining a full Product Keys from a Partial Product Key
    /// </summary>
    public partial class AskForKey : Form
    {
        /// <summary>
        /// Accessible Input Product Key
        /// </summary>
        public static string KeyToSave { get; private set; }

        public AskForKey(string partialProductKey)
        {
            InitializeComponent();

            // Set Partial Key Lable
            PartialProductKeyLabel.Text = partialProductKey;

            // Reset Key On Form Creation
            KeyToSave = string.Empty;
        }

        private void BtnEnterKeyClick(object sender, EventArgs e)
        {
            // Uppercase and Add Dashes
            string input = txtEnterKey1.Text.ToUpper() + "-" + txtEnterKey2.Text.ToUpper() + "-" + txtEnterKey3.Text.ToUpper() + "-" + txtEnterKey4.Text.ToUpper() + "-" + txtEnterKey5.Text.ToUpper();

            // Check if Key is Valid
            if (KeyBase.IsValidKey(input) == false)
            {
                // Set error colors.
                txtEnterKey1.BackColor = Color.Red;
                txtEnterKey1.ForeColor = Color.White;
                txtEnterKey2.BackColor = Color.Red;
                txtEnterKey2.ForeColor = Color.White;
                txtEnterKey3.BackColor = Color.Red;
                txtEnterKey3.ForeColor = Color.White;
                txtEnterKey4.BackColor = Color.Red;
                txtEnterKey4.ForeColor = Color.White;
                txtEnterKey5.BackColor = Color.Red;
                txtEnterKey5.ForeColor = Color.White;

                MessageBox.Show("The key has an invalid format" + Environment.NewLine + "The correct format is: XXXXX-XXXXX-XXXXX-XXXXX-XXXXX");
                return;
            }
            if (input.Substring(input.Length - 5, 5) != PartialProductKeyLabel.Text)
            {
                // Set error colors.
                txtEnterKey1.BackColor = Color.Red;
                txtEnterKey1.ForeColor = Color.White;
                txtEnterKey2.BackColor = Color.Red;
                txtEnterKey2.ForeColor = Color.White;
                txtEnterKey3.BackColor = Color.Red;
                txtEnterKey3.ForeColor = Color.White;
                txtEnterKey4.BackColor = Color.Red;
                txtEnterKey4.ForeColor = Color.White;
                txtEnterKey5.BackColor = Color.Red;
                txtEnterKey5.ForeColor = Color.White;

                MessageBox.Show("This key is not the right key as it does not match the last 5 digits!");
            }
            else
            {
                // Assign
                KeyToSave = input;

                // Close Form
                Close();
            }
        }

        private void TxtEnterKey1TextChanged(object sender, EventArgs e)
        {
            if (txtEnterKey1.BackColor == Color.Red)
            {
                txtEnterKey1.BackColor = Color.White;
                txtEnterKey1.ForeColor = Color.Black;
            }
            if (txtEnterKey1.TextLength == 5)
            {
                txtEnterKey2.Focus();
            }
        }

        private void TxtEnterKey2TextChanged(object sender, EventArgs e)
        {
            if (txtEnterKey2.BackColor == Color.Red)
            {
                txtEnterKey2.BackColor = Color.White;
                txtEnterKey2.ForeColor = Color.Black;
            }
            if (txtEnterKey2.TextLength == 5)
            {
                txtEnterKey3.Focus();
            }
        }

        private void TxtEnterKey3TextChanged(object sender, EventArgs e)
        {
            if (txtEnterKey3.BackColor == Color.Red)
            {
                txtEnterKey3.BackColor = Color.White;
                txtEnterKey3.ForeColor = Color.Black;
            }
            if (txtEnterKey3.TextLength == 5)
            {
                txtEnterKey4.Focus();
            }
        }

        private void TxtEnterKey4TextChanged(object sender, EventArgs e)
        {
            if (txtEnterKey4.BackColor == Color.Red)
            {
                txtEnterKey4.BackColor = Color.White;
                txtEnterKey4.ForeColor = Color.Black;
            }
            if (txtEnterKey4.TextLength == 5)
            {
                txtEnterKey5.Focus();
            }
        }

        private void TxtEnterKey5TextChanged(object sender, EventArgs e)
        {
            if (txtEnterKey5.BackColor != Color.Red)
            {
                return;
            }
            txtEnterKey5.BackColor = Color.White;
            txtEnterKey5.ForeColor = Color.Black;
        }
    }
}