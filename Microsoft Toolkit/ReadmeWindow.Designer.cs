namespace MicrosoftToolkit
{
    partial class ReadmeWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReadmeWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.btnCopyReadme = new System.Windows.Forms.Button();
            this.tabSupport = new System.Windows.Forms.TabPage();
            this.txtSupport = new System.Windows.Forms.TextBox();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.txtSettings = new System.Windows.Forms.TextBox();
            this.tabChangelog = new System.Windows.Forms.TabPage();
            this.txtChangelog = new System.Windows.Forms.TextBox();
            this.tabFunctions = new System.Windows.Forms.TabPage();
            this.txtFunctions = new System.Windows.Forms.TextBox();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.txtMain = new System.Windows.Forms.TextBox();
            this.tabReadme = new System.Windows.Forms.TabControl();
            this.tabSupport.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.tabChangelog.SuspendLayout();
            this.tabFunctions.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabReadme.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Microsoft Toolkit 2.7";
            // 
            // btnCopyReadme
            // 
            this.btnCopyReadme.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyReadme.ForeColor = System.Drawing.Color.Navy;
            this.btnCopyReadme.Location = new System.Drawing.Point(329, 13);
            this.btnCopyReadme.Name = "btnCopyReadme";
            this.btnCopyReadme.Size = new System.Drawing.Size(137, 23);
            this.btnCopyReadme.TabIndex = 4;
            this.btnCopyReadme.Text = "Copy To Clipboard";
            this.btnCopyReadme.UseVisualStyleBackColor = true;
            this.btnCopyReadme.Click += new System.EventHandler(this.BtnCopyReadmeClick);
            // 
            // tabSupport
            // 
            this.tabSupport.Controls.Add(this.txtSupport);
            this.tabSupport.Location = new System.Drawing.Point(4, 24);
            this.tabSupport.Name = "tabSupport";
            this.tabSupport.Padding = new System.Windows.Forms.Padding(3);
            this.tabSupport.Size = new System.Drawing.Size(445, 383);
            this.tabSupport.TabIndex = 1;
            this.tabSupport.Text = "FAQ/Support";
            this.tabSupport.UseVisualStyleBackColor = true;
            // 
            // txtSupport
            // 
            this.txtSupport.BackColor = System.Drawing.Color.White;
            this.txtSupport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSupport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtSupport.Location = new System.Drawing.Point(3, 3);
            this.txtSupport.Multiline = true;
            this.txtSupport.Name = "txtSupport";
            this.txtSupport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSupport.ShortcutsEnabled = false;
            this.txtSupport.Size = new System.Drawing.Size(436, 374);
            this.txtSupport.TabIndex = 6;
            this.txtSupport.TabStop = false;
            this.txtSupport.Text = resources.GetString("txtSupport.Text");
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.txtSettings);
            this.tabSettings.Location = new System.Drawing.Point(4, 24);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(445, 383);
            this.tabSettings.TabIndex = 4;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // txtSettings
            // 
            this.txtSettings.BackColor = System.Drawing.Color.White;
            this.txtSettings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSettings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtSettings.Location = new System.Drawing.Point(3, 3);
            this.txtSettings.Multiline = true;
            this.txtSettings.Name = "txtSettings";
            this.txtSettings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSettings.ShortcutsEnabled = false;
            this.txtSettings.Size = new System.Drawing.Size(436, 374);
            this.txtSettings.TabIndex = 7;
            this.txtSettings.TabStop = false;
            this.txtSettings.Text = resources.GetString("txtSettings.Text");
            // 
            // tabChangelog
            // 
            this.tabChangelog.Controls.Add(this.txtChangelog);
            this.tabChangelog.Location = new System.Drawing.Point(4, 24);
            this.tabChangelog.Name = "tabChangelog";
            this.tabChangelog.Padding = new System.Windows.Forms.Padding(3);
            this.tabChangelog.Size = new System.Drawing.Size(445, 383);
            this.tabChangelog.TabIndex = 0;
            this.tabChangelog.Text = "Changelog";
            this.tabChangelog.UseVisualStyleBackColor = true;
            // 
            // txtChangelog
            // 
            this.txtChangelog.BackColor = System.Drawing.Color.White;
            this.txtChangelog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChangelog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtChangelog.Location = new System.Drawing.Point(3, 3);
            this.txtChangelog.Multiline = true;
            this.txtChangelog.Name = "txtChangelog";
            this.txtChangelog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChangelog.ShortcutsEnabled = false;
            this.txtChangelog.Size = new System.Drawing.Size(439, 377);
            this.txtChangelog.TabIndex = 4;
            this.txtChangelog.TabStop = false;
            this.txtChangelog.Text = resources.GetString("txtChangelog.Text");
            // 
            // tabFunctions
            // 
            this.tabFunctions.Controls.Add(this.txtFunctions);
            this.tabFunctions.Location = new System.Drawing.Point(4, 24);
            this.tabFunctions.Name = "tabFunctions";
            this.tabFunctions.Size = new System.Drawing.Size(445, 383);
            this.tabFunctions.TabIndex = 2;
            this.tabFunctions.Text = "Toolkit Functions";
            this.tabFunctions.UseVisualStyleBackColor = true;
            // 
            // txtFunctions
            // 
            this.txtFunctions.BackColor = System.Drawing.Color.White;
            this.txtFunctions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFunctions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtFunctions.Location = new System.Drawing.Point(3, 3);
            this.txtFunctions.Multiline = true;
            this.txtFunctions.Name = "txtFunctions";
            this.txtFunctions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFunctions.ShortcutsEnabled = false;
            this.txtFunctions.Size = new System.Drawing.Size(439, 377);
            this.txtFunctions.TabIndex = 6;
            this.txtFunctions.TabStop = false;
            this.txtFunctions.Text = resources.GetString("txtFunctions.Text");
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.txtMain);
            this.tabMain.Location = new System.Drawing.Point(4, 24);
            this.tabMain.Name = "tabMain";
            this.tabMain.Size = new System.Drawing.Size(445, 383);
            this.tabMain.TabIndex = 3;
            this.tabMain.Text = "Main";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // txtMain
            // 
            this.txtMain.BackColor = System.Drawing.Color.White;
            this.txtMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtMain.Location = new System.Drawing.Point(3, 3);
            this.txtMain.Multiline = true;
            this.txtMain.Name = "txtMain";
            this.txtMain.ShortcutsEnabled = false;
            this.txtMain.Size = new System.Drawing.Size(439, 377);
            this.txtMain.TabIndex = 7;
            this.txtMain.TabStop = false;
            this.txtMain.Text = resources.GetString("txtMain.Text");
            // 
            // tabReadme
            // 
            this.tabReadme.Controls.Add(this.tabMain);
            this.tabReadme.Controls.Add(this.tabFunctions);
            this.tabReadme.Controls.Add(this.tabChangelog);
            this.tabReadme.Controls.Add(this.tabSettings);
            this.tabReadme.Controls.Add(this.tabSupport);
            this.tabReadme.Location = new System.Drawing.Point(13, 40);
            this.tabReadme.Name = "tabReadme";
            this.tabReadme.SelectedIndex = 0;
            this.tabReadme.Size = new System.Drawing.Size(453, 411);
            this.tabReadme.TabIndex = 5;
            // 
            // ReadmeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(477, 463);
            this.Controls.Add(this.tabReadme);
            this.Controls.Add(this.btnCopyReadme);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReadmeWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Readme";
            this.tabSupport.ResumeLayout(false);
            this.tabSupport.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            this.tabChangelog.ResumeLayout(false);
            this.tabChangelog.PerformLayout();
            this.tabFunctions.ResumeLayout(false);
            this.tabFunctions.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabMain.PerformLayout();
            this.tabReadme.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCopyReadme;
        private System.Windows.Forms.TabPage tabSupport;
        private System.Windows.Forms.TextBox txtSupport;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.TextBox txtSettings;
        private System.Windows.Forms.TabPage tabChangelog;
        private System.Windows.Forms.TextBox txtChangelog;
        private System.Windows.Forms.TabPage tabFunctions;
        private System.Windows.Forms.TextBox txtFunctions;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TextBox txtMain;
        private System.Windows.Forms.TabControl tabReadme;
    }
}