namespace Keys
{
    partial class AskForKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AskForKey));
            this.btnEnterKey = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PartialProductKeyLabel = new System.Windows.Forms.Label();
            this.txtEnterKey1 = new System.Windows.Forms.TextBox();
            this.txtEnterKey2 = new System.Windows.Forms.TextBox();
            this.txtEnterKey4 = new System.Windows.Forms.TextBox();
            this.txtEnterKey3 = new System.Windows.Forms.TextBox();
            this.txtEnterKey5 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnEnterKey
            // 
            this.btnEnterKey.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnterKey.Location = new System.Drawing.Point(104, 63);
            this.btnEnterKey.Name = "btnEnterKey";
            this.btnEnterKey.Size = new System.Drawing.Size(87, 25);
            this.btnEnterKey.TabIndex = 23;
            this.btnEnterKey.Text = "Enter";
            this.btnEnterKey.UseVisualStyleBackColor = true;
            this.btnEnterKey.Click += new System.EventHandler(this.BtnEnterKeyClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 15);
            this.label1.TabIndex = 24;
            this.label1.Text = "Enter the key that ends in:";
            // 
            // PartialProductKeyLabel
            // 
            this.PartialProductKeyLabel.AutoSize = true;
            this.PartialProductKeyLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartialProductKeyLabel.Location = new System.Drawing.Point(191, 13);
            this.PartialProductKeyLabel.Name = "PartialProductKeyLabel";
            this.PartialProductKeyLabel.Size = new System.Drawing.Size(0, 15);
            this.PartialProductKeyLabel.TabIndex = 25;
            // 
            // txtEnterKey1
            // 
            this.txtEnterKey1.Location = new System.Drawing.Point(12, 35);
            this.txtEnterKey1.MaxLength = 5;
            this.txtEnterKey1.Name = "txtEnterKey1";
            this.txtEnterKey1.Size = new System.Drawing.Size(50, 23);
            this.txtEnterKey1.TabIndex = 26;
            this.txtEnterKey1.TextChanged += new System.EventHandler(this.TxtEnterKey1TextChanged);
            // 
            // txtEnterKey2
            // 
            this.txtEnterKey2.Location = new System.Drawing.Point(67, 35);
            this.txtEnterKey2.MaxLength = 5;
            this.txtEnterKey2.Name = "txtEnterKey2";
            this.txtEnterKey2.Size = new System.Drawing.Size(50, 23);
            this.txtEnterKey2.TabIndex = 27;
            this.txtEnterKey2.TextChanged += new System.EventHandler(this.TxtEnterKey2TextChanged);
            // 
            // txtEnterKey4
            // 
            this.txtEnterKey4.Location = new System.Drawing.Point(177, 35);
            this.txtEnterKey4.MaxLength = 5;
            this.txtEnterKey4.Name = "txtEnterKey4";
            this.txtEnterKey4.Size = new System.Drawing.Size(50, 23);
            this.txtEnterKey4.TabIndex = 29;
            this.txtEnterKey4.TextChanged += new System.EventHandler(this.TxtEnterKey4TextChanged);
            // 
            // txtEnterKey3
            // 
            this.txtEnterKey3.Location = new System.Drawing.Point(122, 35);
            this.txtEnterKey3.MaxLength = 5;
            this.txtEnterKey3.Name = "txtEnterKey3";
            this.txtEnterKey3.Size = new System.Drawing.Size(50, 23);
            this.txtEnterKey3.TabIndex = 28;
            this.txtEnterKey3.TextChanged += new System.EventHandler(this.TxtEnterKey3TextChanged);
            // 
            // txtEnterKey5
            // 
            this.txtEnterKey5.Location = new System.Drawing.Point(232, 35);
            this.txtEnterKey5.MaxLength = 5;
            this.txtEnterKey5.Name = "txtEnterKey5";
            this.txtEnterKey5.Size = new System.Drawing.Size(50, 23);
            this.txtEnterKey5.TabIndex = 30;
            this.txtEnterKey5.TextChanged += new System.EventHandler(this.TxtEnterKey5TextChanged);
            // 
            // AskForKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 91);
            this.Controls.Add(this.txtEnterKey5);
            this.Controls.Add(this.txtEnterKey4);
            this.Controls.Add(this.txtEnterKey3);
            this.Controls.Add(this.txtEnterKey2);
            this.Controls.Add(this.txtEnterKey1);
            this.Controls.Add(this.PartialProductKeyLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEnterKey);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AskForKey";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEnterKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label PartialProductKeyLabel;
        private System.Windows.Forms.TextBox txtEnterKey1;
        private System.Windows.Forms.TextBox txtEnterKey2;
        private System.Windows.Forms.TextBox txtEnterKey4;
        private System.Windows.Forms.TextBox txtEnterKey3;
        private System.Windows.Forms.TextBox txtEnterKey5;
    }
}