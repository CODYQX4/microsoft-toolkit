namespace MicrosoftToolkit
{
    partial class SettingsWindowKSSApplicationSpecific
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindowKSSApplicationSpecific));
            this.cboApplicationType = new System.Windows.Forms.ComboBox();
            this.txtApplicationID = new System.Windows.Forms.TextBox();
            this.btnSaveKMSPID = new System.Windows.Forms.Button();
            this.txtKMSPID = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKMSHWID = new System.Windows.Forms.TextBox();
            this.btnSaveKMSHWID = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboApplicationType
            // 
            this.cboApplicationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboApplicationType.FormattingEnabled = true;
            this.cboApplicationType.Items.AddRange(new object[] {
            "Microsoft Windows",
            "Microsoft Office 2010",
            "Microsoft Office 2013-2016"});
            this.cboApplicationType.Location = new System.Drawing.Point(14, 29);
            this.cboApplicationType.Name = "cboApplicationType";
            this.cboApplicationType.Size = new System.Drawing.Size(363, 23);
            this.cboApplicationType.TabIndex = 0;
            this.cboApplicationType.SelectedIndexChanged += new System.EventHandler(this.CboApplicationTypeSelectedIndexChanged);
            // 
            // txtApplicationID
            // 
            this.txtApplicationID.Location = new System.Drawing.Point(14, 75);
            this.txtApplicationID.Name = "txtApplicationID";
            this.txtApplicationID.Size = new System.Drawing.Size(363, 23);
            this.txtApplicationID.TabIndex = 1;
            this.txtApplicationID.TextChanged += new System.EventHandler(this.TxtApplicationIDTextChanged);
            // 
            // btnSaveKMSPID
            // 
            this.btnSaveKMSPID.Location = new System.Drawing.Point(259, 209);
            this.btnSaveKMSPID.Name = "btnSaveKMSPID";
            this.btnSaveKMSPID.Size = new System.Drawing.Size(118, 27);
            this.btnSaveKMSPID.TabIndex = 2;
            this.btnSaveKMSPID.Text = "Set KMS PID";
            this.btnSaveKMSPID.UseVisualStyleBackColor = true;
            this.btnSaveKMSPID.Click += new System.EventHandler(this.BtnSaveKMSPIDClick);
            // 
            // txtKMSPID
            // 
            this.txtKMSPID.Location = new System.Drawing.Point(14, 121);
            this.txtKMSPID.Name = "txtKMSPID";
            this.txtKMSPID.Size = new System.Drawing.Size(363, 23);
            this.txtKMSPID.TabIndex = 4;
            this.txtKMSPID.TextChanged += new System.EventHandler(this.ErrorTextChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(14, 10);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(81, 15);
            this.label31.TabIndex = 25;
            this.label31.Text = "Product Type:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 26;
            this.label1.Text = "Product KMS PID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 15);
            this.label2.TabIndex = 27;
            this.label2.Text = "Product Application ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "Product KMS HWID:";
            // 
            // txtKMSHWID
            // 
            this.txtKMSHWID.Location = new System.Drawing.Point(14, 167);
            this.txtKMSHWID.MaxLength = 16;
            this.txtKMSHWID.Name = "txtKMSHWID";
            this.txtKMSHWID.Size = new System.Drawing.Size(363, 23);
            this.txtKMSHWID.TabIndex = 28;
            this.txtKMSHWID.TextChanged += new System.EventHandler(this.ErrorTextChanged);
            // 
            // btnSaveKMSHWID
            // 
            this.btnSaveKMSHWID.Location = new System.Drawing.Point(135, 209);
            this.btnSaveKMSHWID.Name = "btnSaveKMSHWID";
            this.btnSaveKMSHWID.Size = new System.Drawing.Size(118, 27);
            this.btnSaveKMSHWID.TabIndex = 30;
            this.btnSaveKMSHWID.Text = "Set KMS HWID";
            this.btnSaveKMSHWID.UseVisualStyleBackColor = true;
            this.btnSaveKMSHWID.Click += new System.EventHandler(this.BtnSaveKMSHWIDClick);
            // 
            // SettingsWindowKSSApplicationSpecific
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 248);
            this.Controls.Add(this.btnSaveKMSHWID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtKMSHWID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.txtKMSPID);
            this.Controls.Add(this.btnSaveKMSPID);
            this.Controls.Add(this.txtApplicationID);
            this.Controls.Add(this.cboApplicationType);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindowKSSApplicationSpecific";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "KMS Server Service Product Specific Settings";
            this.Load += new System.EventHandler(this.SettingsWindowKSSApplicationSpecificLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboApplicationType;
        private System.Windows.Forms.TextBox txtApplicationID;
        private System.Windows.Forms.Button btnSaveKMSPID;
        private System.Windows.Forms.TextBox txtKMSPID;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKMSHWID;
        private System.Windows.Forms.Button btnSaveKMSHWID;
    }
}