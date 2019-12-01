namespace MicrosoftToolkit
{
    partial class AboutWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutWindow));
            this.lblMainDeveloper = new System.Windows.Forms.Label();
            this.txtCredits = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lblMainDeveloper
            // 
            this.lblMainDeveloper.AutoSize = true;
            this.lblMainDeveloper.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainDeveloper.ForeColor = System.Drawing.Color.Navy;
            this.lblMainDeveloper.Location = new System.Drawing.Point(47, 15);
            this.lblMainDeveloper.Name = "lblMainDeveloper";
            this.lblMainDeveloper.Size = new System.Drawing.Size(366, 17);
            this.lblMainDeveloper.TabIndex = 4;
            this.lblMainDeveloper.Text = "Microsoft Toolkit developed by CODYQX4 at MDL Forums";
            // 
            // txtCredits
            // 
            this.txtCredits.BackColor = System.Drawing.SystemColors.Control;
            this.txtCredits.Location = new System.Drawing.Point(12, 35);
            this.txtCredits.Name = "txtCredits";
            this.txtCredits.Size = new System.Drawing.Size(457, 195);
            this.txtCredits.TabIndex = 13;
            this.txtCredits.Text = resources.GetString("txtCredits.Text");
            // 
            // AboutWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 242);
            this.Controls.Add(this.txtCredits);
            this.Controls.Add(this.lblMainDeveloper);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Microsoft Toolkit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMainDeveloper;
        private System.Windows.Forms.RichTextBox txtCredits;
    }
}