namespace Keys
{
    partial class BatchPIDXCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchPIDXCheck));
            this.lstKeys = new System.Windows.Forms.ListBox();
            this.grpKeySource = new System.Windows.Forms.GroupBox();
            this.rdoFile = new System.Windows.Forms.RadioButton();
            this.rdoClipboard = new System.Windows.Forms.RadioButton();
            this.btnAddKeysToList = new System.Windows.Forms.Button();
            this.btnKeysCheck = new System.Windows.Forms.Button();
            this.grpKeySource.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstKeys
            // 
            this.lstKeys.FormattingEnabled = true;
            this.lstKeys.ItemHeight = 15;
            this.lstKeys.Location = new System.Drawing.Point(12, 12);
            this.lstKeys.Name = "lstKeys";
            this.lstKeys.Size = new System.Drawing.Size(306, 244);
            this.lstKeys.TabIndex = 0;
            // 
            // grpKeySource
            // 
            this.grpKeySource.Controls.Add(this.rdoFile);
            this.grpKeySource.Controls.Add(this.rdoClipboard);
            this.grpKeySource.Location = new System.Drawing.Point(13, 263);
            this.grpKeySource.Name = "grpKeySource";
            this.grpKeySource.Size = new System.Drawing.Size(305, 75);
            this.grpKeySource.TabIndex = 1;
            this.grpKeySource.TabStop = false;
            this.grpKeySource.Text = "Key Source";
            // 
            // rdoFile
            // 
            this.rdoFile.AutoSize = true;
            this.rdoFile.Location = new System.Drawing.Point(7, 48);
            this.rdoFile.Name = "rdoFile";
            this.rdoFile.Size = new System.Drawing.Size(43, 19);
            this.rdoFile.TabIndex = 1;
            this.rdoFile.TabStop = true;
            this.rdoFile.Text = "File";
            this.rdoFile.UseVisualStyleBackColor = true;
            // 
            // rdoClipboard
            // 
            this.rdoClipboard.AutoSize = true;
            this.rdoClipboard.Checked = true;
            this.rdoClipboard.Location = new System.Drawing.Point(7, 23);
            this.rdoClipboard.Name = "rdoClipboard";
            this.rdoClipboard.Size = new System.Drawing.Size(77, 19);
            this.rdoClipboard.TabIndex = 0;
            this.rdoClipboard.TabStop = true;
            this.rdoClipboard.Text = "Clipboard";
            this.rdoClipboard.UseVisualStyleBackColor = true;
            // 
            // btnAddKeysToList
            // 
            this.btnAddKeysToList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddKeysToList.ForeColor = System.Drawing.Color.Green;
            this.btnAddKeysToList.Location = new System.Drawing.Point(12, 344);
            this.btnAddKeysToList.Name = "btnAddKeysToList";
            this.btnAddKeysToList.Size = new System.Drawing.Size(139, 23);
            this.btnAddKeysToList.TabIndex = 6;
            this.btnAddKeysToList.Text = "Add Keys to List";
            this.btnAddKeysToList.UseVisualStyleBackColor = true;
            this.btnAddKeysToList.Click += new System.EventHandler(this.BtnAddKeysToListClick);
            // 
            // btnKeysCheck
            // 
            this.btnKeysCheck.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeysCheck.ForeColor = System.Drawing.Color.Green;
            this.btnKeysCheck.Location = new System.Drawing.Point(179, 344);
            this.btnKeysCheck.Name = "btnKeysCheck";
            this.btnKeysCheck.Size = new System.Drawing.Size(139, 23);
            this.btnKeysCheck.TabIndex = 7;
            this.btnKeysCheck.Text = "Check All Keys";
            this.btnKeysCheck.UseVisualStyleBackColor = true;
            this.btnKeysCheck.Click += new System.EventHandler(this.BtnKeysCheckClick);
            // 
            // BatchPIDXCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 377);
            this.Controls.Add(this.btnKeysCheck);
            this.Controls.Add(this.btnAddKeysToList);
            this.Controls.Add(this.grpKeySource);
            this.Controls.Add(this.lstKeys);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BatchPIDXCheck";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Batch Product Key Checker";
            this.grpKeySource.ResumeLayout(false);
            this.grpKeySource.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstKeys;
        private System.Windows.Forms.GroupBox grpKeySource;
        private System.Windows.Forms.RadioButton rdoFile;
        private System.Windows.Forms.RadioButton rdoClipboard;
        private System.Windows.Forms.Button btnAddKeysToList;
        private System.Windows.Forms.Button btnKeysCheck;


    }
}