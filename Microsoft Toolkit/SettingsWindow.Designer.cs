namespace MicrosoftToolkit
{
    partial class SettingsWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            this.tabKMSOptions = new System.Windows.Forms.TabPage();
            this.KMSSettingstxtKMSHWID = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.KMSSettingscheckKillProcessOnPort = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.KMSSettingstxtKMSPIDWindows = new System.Windows.Forms.TextBox();
            this.KMSSettingstxtKMSPIDOffice = new System.Windows.Forms.TextBox();
            this.KMSSettingstxtKMSPortWindows = new System.Windows.Forms.NumericUpDown();
            this.KMSSettingstxtKMSServerWindows = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.KMSSettingstxtKMSPortOffice = new System.Windows.Forms.NumericUpDown();
            this.label31 = new System.Windows.Forms.Label();
            this.KMSSettingscheckRemoveKMSConnection = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.KMSSettingstxtKMSServerOffice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.KMSSettingscheckUseKMSEmulator = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabKMSServerService = new System.Windows.Forms.TabPage();
            this.KSStxtKMSHWID = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnKSSMoreSettings = new System.Windows.Forms.Button();
            this.KSStxtRenewalInterval = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.KSStxtActivationInterval = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.KSScheckKillProcessOnPort = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.KSStxtKMSPID = new System.Windows.Forms.TextBox();
            this.KSStxtKMSPort = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabLicenseDisplay = new System.Windows.Forms.TabPage();
            this.LicenseDisplaycheckShowCMID = new System.Windows.Forms.CheckBox();
            this.label28 = new System.Windows.Forms.Label();
            this.LicenseDisplaycheckShowUnlicensed = new System.Windows.Forms.CheckBox();
            this.label24 = new System.Windows.Forms.Label();
            this.tabLocalHostBypass = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LocalHostBypassUseDLLInjection = new System.Windows.Forms.RadioButton();
            this.LocalHostBypassUseTAPAdapter = new System.Windows.Forms.RadioButton();
            this.LocalHostBypassUseWinDivertClient = new System.Windows.Forms.RadioButton();
            this.LocalHostBypassIPSubnet = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.LocalHostBypassIPAddress = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tabPaths = new System.Windows.Forms.TabPage();
            this.btnBrowseLicenseBackups = new System.Windows.Forms.Button();
            this.PathstxtLicenseBackups = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnBrowseKMSServerService = new System.Windows.Forms.Button();
            this.PathstxtKMSServerService = new System.Windows.Forms.TextBox();
            this.PathstxtAutoRearm = new System.Windows.Forms.TextBox();
            this.PathstxtAutoKMS = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBrowseAutoRearm = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.btnBrowseAutoKMS = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.btnLoadDefaults = new System.Windows.Forms.Button();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.tabKMSOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KMSSettingstxtKMSPortWindows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KMSSettingstxtKMSPortOffice)).BeginInit();
            this.tabSettings.SuspendLayout();
            this.tabKMSServerService.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KSStxtRenewalInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KSStxtActivationInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KSStxtKMSPort)).BeginInit();
            this.tabLicenseDisplay.SuspendLayout();
            this.tabLocalHostBypass.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPaths.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabKMSOptions
            // 
            this.tabKMSOptions.BackColor = System.Drawing.SystemColors.Control;
            this.tabKMSOptions.Controls.Add(this.KMSSettingstxtKMSHWID);
            this.tabKMSOptions.Controls.Add(this.label14);
            this.tabKMSOptions.Controls.Add(this.KMSSettingscheckKillProcessOnPort);
            this.tabKMSOptions.Controls.Add(this.label4);
            this.tabKMSOptions.Controls.Add(this.KMSSettingstxtKMSPIDWindows);
            this.tabKMSOptions.Controls.Add(this.KMSSettingstxtKMSPIDOffice);
            this.tabKMSOptions.Controls.Add(this.KMSSettingstxtKMSPortWindows);
            this.tabKMSOptions.Controls.Add(this.KMSSettingstxtKMSServerWindows);
            this.tabKMSOptions.Controls.Add(this.label1);
            this.tabKMSOptions.Controls.Add(this.label33);
            this.tabKMSOptions.Controls.Add(this.KMSSettingstxtKMSPortOffice);
            this.tabKMSOptions.Controls.Add(this.label31);
            this.tabKMSOptions.Controls.Add(this.KMSSettingscheckRemoveKMSConnection);
            this.tabKMSOptions.Controls.Add(this.label16);
            this.tabKMSOptions.Controls.Add(this.KMSSettingstxtKMSServerOffice);
            this.tabKMSOptions.Controls.Add(this.label2);
            this.tabKMSOptions.Controls.Add(this.KMSSettingscheckUseKMSEmulator);
            this.tabKMSOptions.Controls.Add(this.label3);
            this.tabKMSOptions.Location = new System.Drawing.Point(4, 24);
            this.tabKMSOptions.Name = "tabKMSOptions";
            this.tabKMSOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabKMSOptions.Size = new System.Drawing.Size(469, 284);
            this.tabKMSOptions.TabIndex = 0;
            this.tabKMSOptions.Text = "KMS Options";
            // 
            // KMSSettingstxtKMSHWID
            // 
            this.KMSSettingstxtKMSHWID.Location = new System.Drawing.Point(10, 184);
            this.KMSSettingstxtKMSHWID.MaxLength = 16;
            this.KMSSettingstxtKMSHWID.Name = "KMSSettingstxtKMSHWID";
            this.KMSSettingstxtKMSHWID.Size = new System.Drawing.Size(430, 23);
            this.KMSSettingstxtKMSHWID.TabIndex = 41;
            this.KMSSettingstxtKMSHWID.TextChanged += new System.EventHandler(this.ErrorTextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 168);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(176, 15);
            this.label14.TabIndex = 40;
            this.label14.Text = "KMS Server Hardware ID To Use:";
            // 
            // KMSSettingscheckKillProcessOnPort
            // 
            this.KMSSettingscheckKillProcessOnPort.AutoSize = true;
            this.KMSSettingscheckKillProcessOnPort.Location = new System.Drawing.Point(425, 249);
            this.KMSSettingscheckKillProcessOnPort.Name = "KMSSettingscheckKillProcessOnPort";
            this.KMSSettingscheckKillProcessOnPort.Size = new System.Drawing.Size(15, 14);
            this.KMSSettingscheckKillProcessOnPort.TabIndex = 39;
            this.KMSSettingscheckKillProcessOnPort.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(258, 15);
            this.label4.TabIndex = 38;
            this.label4.Text = "Force Open KMS Port By Terminating Processes";
            // 
            // KMSSettingstxtKMSPIDWindows
            // 
            this.KMSSettingstxtKMSPIDWindows.Location = new System.Drawing.Point(10, 144);
            this.KMSSettingstxtKMSPIDWindows.Name = "KMSSettingstxtKMSPIDWindows";
            this.KMSSettingstxtKMSPIDWindows.Size = new System.Drawing.Size(430, 23);
            this.KMSSettingstxtKMSPIDWindows.TabIndex = 37;
            this.KMSSettingstxtKMSPIDWindows.TextChanged += new System.EventHandler(this.ErrorTextChanged);
            // 
            // KMSSettingstxtKMSPIDOffice
            // 
            this.KMSSettingstxtKMSPIDOffice.Location = new System.Drawing.Point(10, 104);
            this.KMSSettingstxtKMSPIDOffice.Name = "KMSSettingstxtKMSPIDOffice";
            this.KMSSettingstxtKMSPIDOffice.Size = new System.Drawing.Size(430, 23);
            this.KMSSettingstxtKMSPIDOffice.TabIndex = 36;
            this.KMSSettingstxtKMSPIDOffice.TextChanged += new System.EventHandler(this.ErrorTextChanged);
            // 
            // KMSSettingstxtKMSPortWindows
            // 
            this.KMSSettingstxtKMSPortWindows.BackColor = System.Drawing.SystemColors.Window;
            this.KMSSettingstxtKMSPortWindows.Location = new System.Drawing.Point(373, 63);
            this.KMSSettingstxtKMSPortWindows.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.KMSSettingstxtKMSPortWindows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KMSSettingstxtKMSPortWindows.Name = "KMSSettingstxtKMSPortWindows";
            this.KMSSettingstxtKMSPortWindows.Size = new System.Drawing.Size(67, 23);
            this.KMSSettingstxtKMSPortWindows.TabIndex = 35;
            this.KMSSettingstxtKMSPortWindows.Value = new decimal(new int[] {
            1688,
            0,
            0,
            0});
            // 
            // KMSSettingstxtKMSServerWindows
            // 
            this.KMSSettingstxtKMSServerWindows.Location = new System.Drawing.Point(10, 62);
            this.KMSSettingstxtKMSServerWindows.Name = "KMSSettingstxtKMSServerWindows";
            this.KMSSettingstxtKMSServerWindows.Size = new System.Drawing.Size(357, 23);
            this.KMSSettingstxtKMSServerWindows.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 15);
            this.label1.TabIndex = 33;
            this.label1.Text = "KMS Server/Port To Use For Windows:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(10, 128);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(166, 15);
            this.label33.TabIndex = 27;
            this.label33.Text = "KMS PID To Use For Windows:";
            // 
            // KMSSettingstxtKMSPortOffice
            // 
            this.KMSSettingstxtKMSPortOffice.BackColor = System.Drawing.SystemColors.Window;
            this.KMSSettingstxtKMSPortOffice.Location = new System.Drawing.Point(373, 20);
            this.KMSSettingstxtKMSPortOffice.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.KMSSettingstxtKMSPortOffice.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KMSSettingstxtKMSPortOffice.Name = "KMSSettingstxtKMSPortOffice";
            this.KMSSettingstxtKMSPortOffice.Size = new System.Drawing.Size(67, 23);
            this.KMSSettingstxtKMSPortOffice.TabIndex = 26;
            this.KMSSettingstxtKMSPortOffice.Value = new decimal(new int[] {
            1688,
            0,
            0,
            0});
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(10, 88);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(149, 15);
            this.label31.TabIndex = 24;
            this.label31.Text = "KMS PID To Use For Office:";
            // 
            // KMSSettingscheckRemoveKMSConnection
            // 
            this.KMSSettingscheckRemoveKMSConnection.AutoSize = true;
            this.KMSSettingscheckRemoveKMSConnection.Location = new System.Drawing.Point(425, 231);
            this.KMSSettingscheckRemoveKMSConnection.Name = "KMSSettingscheckRemoveKMSConnection";
            this.KMSSettingscheckRemoveKMSConnection.Size = new System.Drawing.Size(15, 14);
            this.KMSSettingscheckRemoveKMSConnection.TabIndex = 17;
            this.KMSSettingscheckRemoveKMSConnection.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 231);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(178, 15);
            this.label16.TabIndex = 16;
            this.label16.Text = "Delete KMS Host/Port After Run:";
            // 
            // KMSSettingstxtKMSServerOffice
            // 
            this.KMSSettingstxtKMSServerOffice.Location = new System.Drawing.Point(10, 20);
            this.KMSSettingstxtKMSServerOffice.Name = "KMSSettingstxtKMSServerOffice";
            this.KMSSettingstxtKMSServerOffice.Size = new System.Drawing.Size(357, 23);
            this.KMSSettingstxtKMSServerOffice.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "KMS Server/Port To Use For Office:";
            // 
            // KMSSettingscheckUseKMSEmulator
            // 
            this.KMSSettingscheckUseKMSEmulator.AutoSize = true;
            this.KMSSettingscheckUseKMSEmulator.Location = new System.Drawing.Point(425, 213);
            this.KMSSettingscheckUseKMSEmulator.Name = "KMSSettingscheckUseKMSEmulator";
            this.KMSSettingscheckUseKMSEmulator.Size = new System.Drawing.Size(15, 14);
            this.KMSSettingscheckUseKMSEmulator.TabIndex = 5;
            this.KMSSettingscheckUseKMSEmulator.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Activate Using KMSEmulator:";
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabKMSOptions);
            this.tabSettings.Controls.Add(this.tabKMSServerService);
            this.tabSettings.Controls.Add(this.tabLicenseDisplay);
            this.tabSettings.Controls.Add(this.tabLocalHostBypass);
            this.tabSettings.Controls.Add(this.tabPaths);
            this.tabSettings.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabSettings.Location = new System.Drawing.Point(4, 4);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(477, 312);
            this.tabSettings.TabIndex = 26;
            // 
            // tabKMSServerService
            // 
            this.tabKMSServerService.BackColor = System.Drawing.SystemColors.Control;
            this.tabKMSServerService.Controls.Add(this.KSStxtKMSHWID);
            this.tabKMSServerService.Controls.Add(this.label15);
            this.tabKMSServerService.Controls.Add(this.btnKSSMoreSettings);
            this.tabKMSServerService.Controls.Add(this.KSStxtRenewalInterval);
            this.tabKMSServerService.Controls.Add(this.label11);
            this.tabKMSServerService.Controls.Add(this.KSStxtActivationInterval);
            this.tabKMSServerService.Controls.Add(this.label10);
            this.tabKMSServerService.Controls.Add(this.KSScheckKillProcessOnPort);
            this.tabKMSServerService.Controls.Add(this.label8);
            this.tabKMSServerService.Controls.Add(this.KSStxtKMSPID);
            this.tabKMSServerService.Controls.Add(this.KSStxtKMSPort);
            this.tabKMSServerService.Controls.Add(this.label5);
            this.tabKMSServerService.Controls.Add(this.label6);
            this.tabKMSServerService.Location = new System.Drawing.Point(4, 24);
            this.tabKMSServerService.Name = "tabKMSServerService";
            this.tabKMSServerService.Padding = new System.Windows.Forms.Padding(3);
            this.tabKMSServerService.Size = new System.Drawing.Size(469, 284);
            this.tabKMSServerService.TabIndex = 4;
            this.tabKMSServerService.Text = "KMS Server Service";
            // 
            // KSStxtKMSHWID
            // 
            this.KSStxtKMSHWID.Location = new System.Drawing.Point(10, 104);
            this.KSStxtKMSHWID.MaxLength = 16;
            this.KSStxtKMSHWID.Name = "KSStxtKMSHWID";
            this.KSStxtKMSHWID.Size = new System.Drawing.Size(430, 23);
            this.KSStxtKMSHWID.TabIndex = 50;
            this.KSStxtKMSHWID.Text = "364F463A8863D35F";
            this.KSStxtKMSHWID.TextChanged += new System.EventHandler(this.ErrorTextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 88);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(262, 15);
            this.label15.TabIndex = 49;
            this.label15.Text = "KMS Server Hardware ID To Use (Global Default):";
            // 
            // btnKSSMoreSettings
            // 
            this.btnKSSMoreSettings.Location = new System.Drawing.Point(10, 239);
            this.btnKSSMoreSettings.Name = "btnKSSMoreSettings";
            this.btnKSSMoreSettings.Size = new System.Drawing.Size(111, 23);
            this.btnKSSMoreSettings.TabIndex = 48;
            this.btnKSSMoreSettings.Text = "More Settings";
            this.btnKSSMoreSettings.UseVisualStyleBackColor = true;
            this.btnKSSMoreSettings.Click += new System.EventHandler(this.BtnKSSMoreSettingsClick);
            // 
            // KSStxtRenewalInterval
            // 
            this.KSStxtRenewalInterval.BackColor = System.Drawing.SystemColors.Window;
            this.KSStxtRenewalInterval.Location = new System.Drawing.Point(10, 186);
            this.KSStxtRenewalInterval.Maximum = new decimal(new int[] {
            43200,
            0,
            0,
            0});
            this.KSStxtRenewalInterval.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.KSStxtRenewalInterval.Name = "KSStxtRenewalInterval";
            this.KSStxtRenewalInterval.Size = new System.Drawing.Size(430, 23);
            this.KSStxtRenewalInterval.TabIndex = 47;
            this.KSStxtRenewalInterval.Value = new decimal(new int[] {
            10080,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 170);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(224, 15);
            this.label11.TabIndex = 46;
            this.label11.Text = "KMS Client Renewal Interval (in minutes):";
            // 
            // KSStxtActivationInterval
            // 
            this.KSStxtActivationInterval.BackColor = System.Drawing.SystemColors.Window;
            this.KSStxtActivationInterval.Location = new System.Drawing.Point(10, 144);
            this.KSStxtActivationInterval.Maximum = new decimal(new int[] {
            43200,
            0,
            0,
            0});
            this.KSStxtActivationInterval.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.KSStxtActivationInterval.Name = "KSStxtActivationInterval";
            this.KSStxtActivationInterval.Size = new System.Drawing.Size(430, 23);
            this.KSStxtActivationInterval.TabIndex = 45;
            this.KSStxtActivationInterval.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 128);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(234, 15);
            this.label10.TabIndex = 44;
            this.label10.Text = "KMS Client Activation Interval (in minutes):";
            // 
            // KSScheckKillProcessOnPort
            // 
            this.KSScheckKillProcessOnPort.AutoSize = true;
            this.KSScheckKillProcessOnPort.Checked = true;
            this.KSScheckKillProcessOnPort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.KSScheckKillProcessOnPort.Location = new System.Drawing.Point(425, 212);
            this.KSScheckKillProcessOnPort.Name = "KSScheckKillProcessOnPort";
            this.KSScheckKillProcessOnPort.Size = new System.Drawing.Size(15, 14);
            this.KSScheckKillProcessOnPort.TabIndex = 43;
            this.KSScheckKillProcessOnPort.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 212);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(258, 15);
            this.label8.TabIndex = 42;
            this.label8.Text = "Force Open KMS Port By Terminating Processes";
            // 
            // KSStxtKMSPID
            // 
            this.KSStxtKMSPID.Location = new System.Drawing.Point(10, 62);
            this.KSStxtKMSPID.Name = "KSStxtKMSPID";
            this.KSStxtKMSPID.Size = new System.Drawing.Size(430, 23);
            this.KSStxtKMSPID.TabIndex = 41;
            this.KSStxtKMSPID.Text = "RandomKMSPID";
            this.KSStxtKMSPID.TextChanged += new System.EventHandler(this.ErrorTextChanged);
            // 
            // KSStxtKMSPort
            // 
            this.KSStxtKMSPort.BackColor = System.Drawing.SystemColors.Window;
            this.KSStxtKMSPort.Location = new System.Drawing.Point(10, 20);
            this.KSStxtKMSPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.KSStxtKMSPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KSStxtKMSPort.Name = "KSStxtKMSPort";
            this.KSStxtKMSPort.Size = new System.Drawing.Size(430, 23);
            this.KSStxtKMSPort.TabIndex = 40;
            this.KSStxtKMSPort.Value = new decimal(new int[] {
            1688,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 15);
            this.label5.TabIndex = 38;
            this.label5.Text = "KMS Port To Use:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 15);
            this.label6.TabIndex = 37;
            this.label6.Text = "KMS PID To Use (Global Default):";
            // 
            // tabLicenseDisplay
            // 
            this.tabLicenseDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.tabLicenseDisplay.Controls.Add(this.LicenseDisplaycheckShowCMID);
            this.tabLicenseDisplay.Controls.Add(this.label28);
            this.tabLicenseDisplay.Controls.Add(this.LicenseDisplaycheckShowUnlicensed);
            this.tabLicenseDisplay.Controls.Add(this.label24);
            this.tabLicenseDisplay.Location = new System.Drawing.Point(4, 24);
            this.tabLicenseDisplay.Name = "tabLicenseDisplay";
            this.tabLicenseDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.tabLicenseDisplay.Size = new System.Drawing.Size(469, 284);
            this.tabLicenseDisplay.TabIndex = 3;
            this.tabLicenseDisplay.Text = "License Display";
            // 
            // LicenseDisplaycheckShowCMID
            // 
            this.LicenseDisplaycheckShowCMID.AutoSize = true;
            this.LicenseDisplaycheckShowCMID.Location = new System.Drawing.Point(425, 20);
            this.LicenseDisplaycheckShowCMID.Name = "LicenseDisplaycheckShowCMID";
            this.LicenseDisplaycheckShowCMID.Size = new System.Drawing.Size(15, 14);
            this.LicenseDisplaycheckShowCMID.TabIndex = 27;
            this.LicenseDisplaycheckShowCMID.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(10, 20);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(69, 15);
            this.label28.TabIndex = 26;
            this.label28.Text = "Show CMID";
            // 
            // LicenseDisplaycheckShowUnlicensed
            // 
            this.LicenseDisplaycheckShowUnlicensed.AutoSize = true;
            this.LicenseDisplaycheckShowUnlicensed.Location = new System.Drawing.Point(425, 38);
            this.LicenseDisplaycheckShowUnlicensed.Name = "LicenseDisplaycheckShowUnlicensed";
            this.LicenseDisplaycheckShowUnlicensed.Size = new System.Drawing.Size(15, 14);
            this.LicenseDisplaycheckShowUnlicensed.TabIndex = 25;
            this.LicenseDisplaycheckShowUnlicensed.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(10, 38);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(97, 15);
            this.label24.TabIndex = 24;
            this.label24.Text = "Show Unlicensed";
            // 
            // tabLocalHostBypass
            // 
            this.tabLocalHostBypass.BackColor = System.Drawing.SystemColors.Control;
            this.tabLocalHostBypass.Controls.Add(this.groupBox1);
            this.tabLocalHostBypass.Controls.Add(this.LocalHostBypassIPSubnet);
            this.tabLocalHostBypass.Controls.Add(this.label12);
            this.tabLocalHostBypass.Controls.Add(this.LocalHostBypassIPAddress);
            this.tabLocalHostBypass.Controls.Add(this.label13);
            this.tabLocalHostBypass.Location = new System.Drawing.Point(4, 24);
            this.tabLocalHostBypass.Name = "tabLocalHostBypass";
            this.tabLocalHostBypass.Padding = new System.Windows.Forms.Padding(3);
            this.tabLocalHostBypass.Size = new System.Drawing.Size(469, 284);
            this.tabLocalHostBypass.TabIndex = 5;
            this.tabLocalHostBypass.Text = "LocalHost Bypass";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LocalHostBypassUseDLLInjection);
            this.groupBox1.Controls.Add(this.LocalHostBypassUseTAPAdapter);
            this.groupBox1.Controls.Add(this.LocalHostBypassUseWinDivertClient);
            this.groupBox1.Location = new System.Drawing.Point(10, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(439, 103);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bypass Method:";
            // 
            // LocalHostBypassUseDLLInjection
            // 
            this.LocalHostBypassUseDLLInjection.AutoSize = true;
            this.LocalHostBypassUseDLLInjection.Location = new System.Drawing.Point(6, 22);
            this.LocalHostBypassUseDLLInjection.Name = "LocalHostBypassUseDLLInjection";
            this.LocalHostBypassUseDLLInjection.Size = new System.Drawing.Size(116, 19);
            this.LocalHostBypassUseDLLInjection.TabIndex = 43;
            this.LocalHostBypassUseDLLInjection.Text = "Use DLL Injection";
            this.LocalHostBypassUseDLLInjection.UseVisualStyleBackColor = true;
            // 
            // LocalHostBypassUseTAPAdapter
            // 
            this.LocalHostBypassUseTAPAdapter.AutoSize = true;
            this.LocalHostBypassUseTAPAdapter.Location = new System.Drawing.Point(6, 47);
            this.LocalHostBypassUseTAPAdapter.Name = "LocalHostBypassUseTAPAdapter";
            this.LocalHostBypassUseTAPAdapter.Size = new System.Drawing.Size(114, 19);
            this.LocalHostBypassUseTAPAdapter.TabIndex = 41;
            this.LocalHostBypassUseTAPAdapter.Text = "Use TAP Adapter";
            this.LocalHostBypassUseTAPAdapter.UseVisualStyleBackColor = true;
            // 
            // LocalHostBypassUseWinDivertClient
            // 
            this.LocalHostBypassUseWinDivertClient.AutoSize = true;
            this.LocalHostBypassUseWinDivertClient.Location = new System.Drawing.Point(6, 72);
            this.LocalHostBypassUseWinDivertClient.Name = "LocalHostBypassUseWinDivertClient";
            this.LocalHostBypassUseWinDivertClient.Size = new System.Drawing.Size(133, 19);
            this.LocalHostBypassUseWinDivertClient.TabIndex = 42;
            this.LocalHostBypassUseWinDivertClient.Text = "Use WinDivert Client";
            this.LocalHostBypassUseWinDivertClient.UseVisualStyleBackColor = true;
            // 
            // LocalHostBypassIPSubnet
            // 
            this.LocalHostBypassIPSubnet.Location = new System.Drawing.Point(10, 62);
            this.LocalHostBypassIPSubnet.Name = "LocalHostBypassIPSubnet";
            this.LocalHostBypassIPSubnet.Size = new System.Drawing.Size(439, 23);
            this.LocalHostBypassIPSubnet.TabIndex = 40;
            this.LocalHostBypassIPSubnet.TextChanged += new System.EventHandler(this.ErrorTextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(173, 15);
            this.label12.TabIndex = 39;
            this.label12.Text = "LocalHost Bypass Subnet Mask:";
            // 
            // LocalHostBypassIPAddress
            // 
            this.LocalHostBypassIPAddress.Location = new System.Drawing.Point(10, 20);
            this.LocalHostBypassIPAddress.Name = "LocalHostBypassIPAddress";
            this.LocalHostBypassIPAddress.Size = new System.Drawing.Size(439, 23);
            this.LocalHostBypassIPAddress.TabIndex = 37;
            this.LocalHostBypassIPAddress.TextChanged += new System.EventHandler(this.ErrorTextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(160, 15);
            this.label13.TabIndex = 36;
            this.label13.Text = "LocalHost Bypass IP Address:";
            // 
            // tabPaths
            // 
            this.tabPaths.BackColor = System.Drawing.SystemColors.Control;
            this.tabPaths.Controls.Add(this.btnBrowseLicenseBackups);
            this.tabPaths.Controls.Add(this.PathstxtLicenseBackups);
            this.tabPaths.Controls.Add(this.label9);
            this.tabPaths.Controls.Add(this.btnBrowseKMSServerService);
            this.tabPaths.Controls.Add(this.PathstxtKMSServerService);
            this.tabPaths.Controls.Add(this.PathstxtAutoRearm);
            this.tabPaths.Controls.Add(this.PathstxtAutoKMS);
            this.tabPaths.Controls.Add(this.label7);
            this.tabPaths.Controls.Add(this.btnBrowseAutoRearm);
            this.tabPaths.Controls.Add(this.label26);
            this.tabPaths.Controls.Add(this.btnBrowseAutoKMS);
            this.tabPaths.Controls.Add(this.label25);
            this.tabPaths.Location = new System.Drawing.Point(4, 24);
            this.tabPaths.Name = "tabPaths";
            this.tabPaths.Size = new System.Drawing.Size(469, 284);
            this.tabPaths.TabIndex = 0;
            this.tabPaths.Text = "Paths";
            // 
            // btnBrowseLicenseBackups
            // 
            this.btnBrowseLicenseBackups.Location = new System.Drawing.Point(371, 140);
            this.btnBrowseLicenseBackups.Name = "btnBrowseLicenseBackups";
            this.btnBrowseLicenseBackups.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseLicenseBackups.TabIndex = 22;
            this.btnBrowseLicenseBackups.Text = "Browse";
            this.btnBrowseLicenseBackups.UseVisualStyleBackColor = true;
            this.btnBrowseLicenseBackups.Click += new System.EventHandler(this.BtnBrowseLicenseBackupsClick);
            // 
            // PathstxtLicenseBackups
            // 
            this.PathstxtLicenseBackups.BackColor = System.Drawing.SystemColors.Window;
            this.PathstxtLicenseBackups.Location = new System.Drawing.Point(10, 140);
            this.PathstxtLicenseBackups.Name = "PathstxtLicenseBackups";
            this.PathstxtLicenseBackups.ReadOnly = true;
            this.PathstxtLicenseBackups.Size = new System.Drawing.Size(355, 23);
            this.PathstxtLicenseBackups.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 15);
            this.label9.TabIndex = 20;
            this.label9.Text = "License Backups:";
            // 
            // btnBrowseKMSServerService
            // 
            this.btnBrowseKMSServerService.Location = new System.Drawing.Point(371, 100);
            this.btnBrowseKMSServerService.Name = "btnBrowseKMSServerService";
            this.btnBrowseKMSServerService.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseKMSServerService.TabIndex = 19;
            this.btnBrowseKMSServerService.Text = "Browse";
            this.btnBrowseKMSServerService.UseVisualStyleBackColor = true;
            this.btnBrowseKMSServerService.Click += new System.EventHandler(this.BtnBrowseKMSServerServiceClick);
            // 
            // PathstxtKMSServerService
            // 
            this.PathstxtKMSServerService.BackColor = System.Drawing.SystemColors.Window;
            this.PathstxtKMSServerService.Location = new System.Drawing.Point(10, 100);
            this.PathstxtKMSServerService.Name = "PathstxtKMSServerService";
            this.PathstxtKMSServerService.ReadOnly = true;
            this.PathstxtKMSServerService.Size = new System.Drawing.Size(355, 23);
            this.PathstxtKMSServerService.TabIndex = 18;
            // 
            // PathstxtAutoRearm
            // 
            this.PathstxtAutoRearm.BackColor = System.Drawing.SystemColors.Window;
            this.PathstxtAutoRearm.Location = new System.Drawing.Point(10, 60);
            this.PathstxtAutoRearm.Name = "PathstxtAutoRearm";
            this.PathstxtAutoRearm.ReadOnly = true;
            this.PathstxtAutoRearm.Size = new System.Drawing.Size(355, 23);
            this.PathstxtAutoRearm.TabIndex = 15;
            // 
            // PathstxtAutoKMS
            // 
            this.PathstxtAutoKMS.BackColor = System.Drawing.SystemColors.Window;
            this.PathstxtAutoKMS.Location = new System.Drawing.Point(10, 20);
            this.PathstxtAutoKMS.Name = "PathstxtAutoKMS";
            this.PathstxtAutoKMS.ReadOnly = true;
            this.PathstxtAutoKMS.Size = new System.Drawing.Size(355, 23);
            this.PathstxtAutoKMS.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 15);
            this.label7.TabIndex = 17;
            this.label7.Text = "KMS Server Service:";
            // 
            // btnBrowseAutoRearm
            // 
            this.btnBrowseAutoRearm.Location = new System.Drawing.Point(371, 60);
            this.btnBrowseAutoRearm.Name = "btnBrowseAutoRearm";
            this.btnBrowseAutoRearm.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseAutoRearm.TabIndex = 16;
            this.btnBrowseAutoRearm.Text = "Browse";
            this.btnBrowseAutoRearm.UseVisualStyleBackColor = true;
            this.btnBrowseAutoRearm.Click += new System.EventHandler(this.BtnBrowseAutoRearmClick);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(10, 44);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(70, 15);
            this.label26.TabIndex = 14;
            this.label26.Text = "AutoRearm:";
            // 
            // btnBrowseAutoKMS
            // 
            this.btnBrowseAutoKMS.Location = new System.Drawing.Point(371, 20);
            this.btnBrowseAutoKMS.Name = "btnBrowseAutoKMS";
            this.btnBrowseAutoKMS.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseAutoKMS.TabIndex = 13;
            this.btnBrowseAutoKMS.Text = "Browse";
            this.btnBrowseAutoKMS.UseVisualStyleBackColor = true;
            this.btnBrowseAutoKMS.Click += new System.EventHandler(this.BtnBrowseAutoKMSClick);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(10, 4);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(60, 15);
            this.label25.TabIndex = 11;
            this.label25.Text = "AutoKMS:";
            // 
            // btnLoadDefaults
            // 
            this.btnLoadDefaults.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadDefaults.ForeColor = System.Drawing.Color.Navy;
            this.btnLoadDefaults.Location = new System.Drawing.Point(261, 322);
            this.btnLoadDefaults.Name = "btnLoadDefaults";
            this.btnLoadDefaults.Size = new System.Drawing.Size(91, 25);
            this.btnLoadDefaults.TabIndex = 28;
            this.btnLoadDefaults.Text = "Load Defaults";
            this.btnLoadDefaults.UseVisualStyleBackColor = true;
            this.btnLoadDefaults.Click += new System.EventHandler(this.BtnLoadDefaultsClick);
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSettings.ForeColor = System.Drawing.Color.Navy;
            this.btnSaveSettings.Location = new System.Drawing.Point(134, 322);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(91, 25);
            this.btnSaveSettings.TabIndex = 27;
            this.btnSaveSettings.Text = "Save";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.BtnSaveSettingsClick);
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 357);
            this.Controls.Add(this.tabSettings);
            this.Controls.Add(this.btnLoadDefaults);
            this.Controls.Add(this.btnSaveSettings);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsWindowFormClosing);
            this.tabKMSOptions.ResumeLayout(false);
            this.tabKMSOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KMSSettingstxtKMSPortWindows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KMSSettingstxtKMSPortOffice)).EndInit();
            this.tabSettings.ResumeLayout(false);
            this.tabKMSServerService.ResumeLayout(false);
            this.tabKMSServerService.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KSStxtRenewalInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KSStxtActivationInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KSStxtKMSPort)).EndInit();
            this.tabLicenseDisplay.ResumeLayout(false);
            this.tabLicenseDisplay.PerformLayout();
            this.tabLocalHostBypass.ResumeLayout(false);
            this.tabLocalHostBypass.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPaths.ResumeLayout(false);
            this.tabPaths.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabKMSOptions;
        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabPaths;
        private System.Windows.Forms.Button btnBrowseKMSServerService;
        private System.Windows.Forms.TextBox PathstxtKMSServerService;
        private System.Windows.Forms.TextBox PathstxtAutoRearm;
        private System.Windows.Forms.TextBox PathstxtAutoKMS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnBrowseAutoRearm;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button btnBrowseAutoKMS;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button btnLoadDefaults;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.TabPage tabLicenseDisplay;
        private System.Windows.Forms.CheckBox LicenseDisplaycheckShowCMID;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.CheckBox LicenseDisplaycheckShowUnlicensed;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.NumericUpDown KMSSettingstxtKMSPortWindows;
        private System.Windows.Forms.TextBox KMSSettingstxtKMSServerWindows;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.NumericUpDown KMSSettingstxtKMSPortOffice;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.CheckBox KMSSettingscheckRemoveKMSConnection;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox KMSSettingstxtKMSServerOffice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox KMSSettingscheckUseKMSEmulator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox KMSSettingstxtKMSPIDWindows;
        private System.Windows.Forms.TextBox KMSSettingstxtKMSPIDOffice;
        private System.Windows.Forms.CheckBox KMSSettingscheckKillProcessOnPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabKMSServerService;
        private System.Windows.Forms.CheckBox KSScheckKillProcessOnPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox KSStxtKMSPID;
        private System.Windows.Forms.NumericUpDown KSStxtKMSPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnBrowseLicenseBackups;
        private System.Windows.Forms.TextBox PathstxtLicenseBackups;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown KSStxtRenewalInterval;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown KSStxtActivationInterval;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnKSSMoreSettings;
        private System.Windows.Forms.TabPage tabLocalHostBypass;
        private System.Windows.Forms.TextBox LocalHostBypassIPSubnet;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox LocalHostBypassIPAddress;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton LocalHostBypassUseTAPAdapter;
        private System.Windows.Forms.RadioButton LocalHostBypassUseWinDivertClient;
        private System.Windows.Forms.RadioButton LocalHostBypassUseDLLInjection;
        private System.Windows.Forms.TextBox KMSSettingstxtKMSHWID;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox KSStxtKMSHWID;
        private System.Windows.Forms.Label label15;
    }
}