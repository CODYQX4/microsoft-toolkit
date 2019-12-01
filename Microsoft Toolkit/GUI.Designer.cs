using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MicrosoftToolkit
{
    partial class GUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.btnCredits = new System.Windows.Forms.Button();
            this.WindowsButton = new System.Windows.Forms.Button();
            this.OfficeButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.workerAutoKMSInstall = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.workerAutoKMSUninstall = new System.ComponentModel.BackgroundWorker();
            this.workerRearm = new System.ComponentModel.BackgroundWorker();
            this.workerAttemptActivation = new System.ComponentModel.BackgroundWorker();
            this.workerCheckActivation = new System.ComponentModel.BackgroundWorker();
            this.btnReadme = new System.Windows.Forms.Button();
            this.workerInstallKey = new System.ComponentModel.BackgroundWorker();
            this.workerUninstallKey = new System.ComponentModel.BackgroundWorker();
            this.workerCheckKey = new System.ComponentModel.BackgroundWorker();
            this.workerBackup = new System.ComponentModel.BackgroundWorker();
            this.workerRestore = new System.ComponentModel.BackgroundWorker();
            this.workerRearmCount = new System.ComponentModel.BackgroundWorker();
            this.workerAutoRearmInstall = new System.ComponentModel.BackgroundWorker();
            this.workerAutoRearmUninstall = new System.ComponentModel.BackgroundWorker();
            this.workerMakeISO = new System.ComponentModel.BackgroundWorker();
            this.workerExtractSetup = new System.ComponentModel.BackgroundWorker();
            this.workerChangeChannel = new System.ComponentModel.BackgroundWorker();
            this.workerAddLP = new System.ComponentModel.BackgroundWorker();
            this.workerAddUpdate = new System.ComponentModel.BackgroundWorker();
            this.workerRemoveLP = new System.ComponentModel.BackgroundWorker();
            this.workerBackupCheckKeyList = new System.ComponentModel.BackgroundWorker();
            this.workerCheckKeyList = new System.ComponentModel.BackgroundWorker();
            this.workerPhoneActivate = new System.ComponentModel.BackgroundWorker();
            this.btnAddLP = new System.Windows.Forms.Button();
            this.btnChangeChannel = new System.Windows.Forms.Button();
            this.btnRemoveLP = new System.Windows.Forms.Button();
            this.btnMakeISO = new System.Windows.Forms.Button();
            this.btnRunOCT = new System.Windows.Forms.Button();
            this.btnAddUpdate = new System.Windows.Forms.Button();
            this.btnBrowseToSetup = new System.Windows.Forms.Button();
            this.btnExtractSetup = new System.Windows.Forms.Button();
            this.workerAutoKMSCustomTaskInstall = new System.ComponentModel.BackgroundWorker();
            this.workerAutoKMSCustomTaskUninstall = new System.ComponentModel.BackgroundWorker();
            this.workerBackupReinstallKeys = new System.ComponentModel.BackgroundWorker();
            this.tabCustomizeSetup = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.comboInstallerApp = new System.Windows.Forms.ComboBox();
            this.comboInstalledLPs = new System.Windows.Forms.ComboBox();
            this.comboInstallerChannel = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabBackups = new System.Windows.Forms.TabPage();
            this.grpBackupFunctions = new System.Windows.Forms.GroupBox();
            this.textBackupName = new System.Windows.Forms.ComboBox();
            this.btnBackupReinstallKeys = new System.Windows.Forms.Button();
            this.btnBackupCheckKeyList = new System.Windows.Forms.Button();
            this.lblBackupName = new System.Windows.Forms.Label();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.tabKey = new System.Windows.Forms.TabPage();
            this.grpKeySelection = new System.Windows.Forms.GroupBox();
            this.lblKeyProduct = new System.Windows.Forms.Label();
            this.cboKeyProduct = new System.Windows.Forms.ComboBox();
            this.cboKeyEdition = new System.Windows.Forms.ComboBox();
            this.lblKeyEdition = new System.Windows.Forms.Label();
            this.grpKeyFunctions = new System.Windows.Forms.GroupBox();
            this.BtnKeysSystemShow = new System.Windows.Forms.Button();
            this.BtnKeysSystemCheck = new System.Windows.Forms.Button();
            this.chkEnterCustomKey = new System.Windows.Forms.CheckBox();
            this.btnCheckKeyList = new System.Windows.Forms.Button();
            this.btnCheckKey = new System.Windows.Forms.Button();
            this.btnInstallKey = new System.Windows.Forms.Button();
            this.btnUninstallKey = new System.Windows.Forms.Button();
            this.txtCustomKey = new System.Windows.Forms.TextBox();
            this.tabActivationTools = new System.Windows.Forms.TabPage();
            this.grpActivationFunctions = new System.Windows.Forms.GroupBox();
            this.btnEZActivator = new System.Windows.Forms.Button();
            this.btnPhoneActivation = new System.Windows.Forms.Button();
            this.btnAttemptActivation = new System.Windows.Forms.Button();
            this.btnGetRearmCount = new System.Windows.Forms.Button();
            this.btnRearm = new System.Windows.Forms.Button();
            this.btnCheckActivation = new System.Windows.Forms.Button();
            this.grpActivationToolBox = new System.Windows.Forms.GroupBox();
            this.lblActivationTool = new System.Windows.Forms.Label();
            this.cboActivationToolBox = new System.Windows.Forms.ComboBox();
            this.btnActivationToolUninstall = new System.Windows.Forms.Button();
            this.btnActivationToolInstall = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.tabMainWindows = new System.Windows.Forms.TabPage();
            this.grpWindowsInformation = new System.Windows.Forms.GroupBox();
            this.lblWindowsNumber = new System.Windows.Forms.Label();
            this.lblWindowsNumberDescription = new System.Windows.Forms.Label();
            this.lblWindowsProduct = new System.Windows.Forms.Label();
            this.lblWindowsProductDescription = new System.Windows.Forms.Label();
            this.lblWindowsArchitecture = new System.Windows.Forms.Label();
            this.lblWindowsArchitectureDescription = new System.Windows.Forms.Label();
            this.lblWindowsSupported = new System.Windows.Forms.Label();
            this.lblWindowsSupportedDescription = new System.Windows.Forms.Label();
            this.tabMainOffice = new System.Windows.Forms.TabPage();
            this.grpOfficeUninstaller = new System.Windows.Forms.GroupBox();
            this.cboOfficeUninstaller = new System.Windows.Forms.ComboBox();
            this.btnOfficeUninstaller = new System.Windows.Forms.Button();
            this.grpOfficeInformation = new System.Windows.Forms.GroupBox();
            this.lblOfficeInstallType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOfficeNumber = new System.Windows.Forms.Label();
            this.lblOfficeNumberDescription = new System.Windows.Forms.Label();
            this.lblOfficeProduct = new System.Windows.Forms.Label();
            this.lblOfficeProductDescription = new System.Windows.Forms.Label();
            this.lblOfficeArchitecture = new System.Windows.Forms.Label();
            this.lblOfficeArchitectureDescription = new System.Windows.Forms.Label();
            this.lblOfficeSupported = new System.Windows.Forms.Label();
            this.lblOfficeSupportedDescription = new System.Windows.Forms.Label();
            this.tabFunctions = new System.Windows.Forms.TabControl();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.consoleBox = new System.Windows.Forms.GroupBox();
            this.workerKeysSystemCheck = new System.ComponentModel.BackgroundWorker();
            this.workerKeysSystemShow = new System.ComponentModel.BackgroundWorker();
            this.workerOfficeUninstaller = new System.ComponentModel.BackgroundWorker();
            this.workerKMSServerServiceInstall = new System.ComponentModel.BackgroundWorker();
            this.workerKMSServerServiceUninstall = new System.ComponentModel.BackgroundWorker();
            this.btnSettings = new System.Windows.Forms.Button();
            this.workerEZActivator = new System.ComponentModel.BackgroundWorker();
            this.tabCustomizeSetup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabBackups.SuspendLayout();
            this.grpBackupFunctions.SuspendLayout();
            this.tabKey.SuspendLayout();
            this.grpKeySelection.SuspendLayout();
            this.grpKeyFunctions.SuspendLayout();
            this.tabActivationTools.SuspendLayout();
            this.grpActivationFunctions.SuspendLayout();
            this.grpActivationToolBox.SuspendLayout();
            this.tabMainWindows.SuspendLayout();
            this.grpWindowsInformation.SuspendLayout();
            this.tabMainOffice.SuspendLayout();
            this.grpOfficeUninstaller.SuspendLayout();
            this.grpOfficeInformation.SuspendLayout();
            this.tabFunctions.SuspendLayout();
            this.consoleBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCredits
            // 
            this.btnCredits.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCredits.ForeColor = System.Drawing.Color.Navy;
            this.btnCredits.Location = new System.Drawing.Point(3, 342);
            this.btnCredits.Name = "btnCredits";
            this.btnCredits.Size = new System.Drawing.Size(75, 23);
            this.btnCredits.TabIndex = 18;
            this.btnCredits.Text = "Credits";
            this.btnCredits.UseVisualStyleBackColor = true;
            this.btnCredits.Click += new System.EventHandler(this.BtnCreditsClick);
            // 
            // WindowsButton
            // 
            this.WindowsButton.BackColor = System.Drawing.Color.Transparent;
            this.WindowsButton.Image = ((System.Drawing.Image)(resources.GetObject("WindowsButton.Image")));
            this.WindowsButton.Location = new System.Drawing.Point(373, 342);
            this.WindowsButton.Name = "WindowsButton";
            this.WindowsButton.Size = new System.Drawing.Size(24, 23);
            this.WindowsButton.TabIndex = 37;
            this.WindowsButton.UseVisualStyleBackColor = false;
            this.WindowsButton.Click += new System.EventHandler(this.WindowsButtonClick);
            // 
            // OfficeButton
            // 
            this.OfficeButton.Image = ((System.Drawing.Image)(resources.GetObject("OfficeButton.Image")));
            this.OfficeButton.Location = new System.Drawing.Point(343, 342);
            this.OfficeButton.Name = "OfficeButton";
            this.OfficeButton.Size = new System.Drawing.Size(24, 23);
            this.OfficeButton.TabIndex = 36;
            this.OfficeButton.UseVisualStyleBackColor = true;
            this.OfficeButton.Click += new System.EventHandler(this.OfficeButtonClick);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Volume",
            "Retail",
            "Other"});
            this.comboBox1.Location = new System.Drawing.Point(16, 26);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(254, 21);
            this.comboBox1.TabIndex = 28;
            // 
            // workerAutoKMSInstall
            // 
            this.workerAutoKMSInstall.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerAutoKMSInstallDoWork);
            this.workerAutoKMSInstall.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerAutoKMSInstallRunWorkerCompleted);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(168, 344);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(169, 18);
            this.progressBar.TabIndex = 24;
            // 
            // workerAutoKMSUninstall
            // 
            this.workerAutoKMSUninstall.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerAutoKMSUninstallDoWork);
            this.workerAutoKMSUninstall.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerAutoKMSUninstallRunWorkerCompleted);
            // 
            // workerRearm
            // 
            this.workerRearm.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerRearmDoWork);
            this.workerRearm.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerRearmRunWorkerCompleted);
            // 
            // workerAttemptActivation
            // 
            this.workerAttemptActivation.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerAttemptActivationDoWork);
            this.workerAttemptActivation.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerAttemptActivationRunWorkerCompleted);
            // 
            // workerCheckActivation
            // 
            this.workerCheckActivation.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerCheckActivationDoWork);
            this.workerCheckActivation.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerCheckActivationRunWorkerCompleted);
            // 
            // btnReadme
            // 
            this.btnReadme.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadme.ForeColor = System.Drawing.Color.Navy;
            this.btnReadme.Location = new System.Drawing.Point(87, 342);
            this.btnReadme.Name = "btnReadme";
            this.btnReadme.Size = new System.Drawing.Size(75, 23);
            this.btnReadme.TabIndex = 25;
            this.btnReadme.Text = "Readme";
            this.btnReadme.UseVisualStyleBackColor = true;
            this.btnReadme.Click += new System.EventHandler(this.BtnReadmeClick);
            // 
            // workerInstallKey
            // 
            this.workerInstallKey.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerInstallKeyDoWork);
            this.workerInstallKey.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerInstallKeyRunWorkerCompleted);
            // 
            // workerUninstallKey
            // 
            this.workerUninstallKey.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerUninstallKeyDoWork);
            this.workerUninstallKey.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerUninstallKeyRunWorkerCompleted);
            // 
            // workerCheckKey
            // 
            this.workerCheckKey.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerCheckKeyDoWork);
            this.workerCheckKey.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerCheckKeyRunWorkerCompleted);
            // 
            // workerBackup
            // 
            this.workerBackup.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerBackupDoWork);
            this.workerBackup.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerBackupRunWorkerCompleted);
            // 
            // workerRestore
            // 
            this.workerRestore.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerRestoreDoWork);
            this.workerRestore.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerRestoreRunWorkerCompleted);
            // 
            // workerRearmCount
            // 
            this.workerRearmCount.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerGetRearmCountDoWork);
            this.workerRearmCount.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerGetRearmCountRunWorkerCompleted);
            // 
            // workerAutoRearmInstall
            // 
            this.workerAutoRearmInstall.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerAutoRearmInstallDoWork);
            this.workerAutoRearmInstall.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerAutoRearmInstallRunWorkerCompleted);
            // 
            // workerAutoRearmUninstall
            // 
            this.workerAutoRearmUninstall.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerAutoRearmUninstallDoWork);
            this.workerAutoRearmUninstall.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerAutoRearmUninstallRunWorkerCompleted);
            // 
            // workerMakeISO
            // 
            this.workerMakeISO.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerMakeISODoWork);
            this.workerMakeISO.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerMakeISORunWorkerCompleted);
            // 
            // workerExtractSetup
            // 
            this.workerExtractSetup.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerExtractSetupDoWork);
            this.workerExtractSetup.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerExtractSetupRunWorkerCompleted);
            // 
            // workerChangeChannel
            // 
            this.workerChangeChannel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerChangeChannelDoWork);
            this.workerChangeChannel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerChangeChannelRunWorkerCompleted);
            // 
            // workerAddLP
            // 
            this.workerAddLP.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerAddLPDoWork);
            this.workerAddLP.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerAddLPRunWorkerCompleted);
            // 
            // workerAddUpdate
            // 
            this.workerAddUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerAddUpdateDoWork);
            this.workerAddUpdate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerAddUpdateRunWorkerCompleted);
            // 
            // workerRemoveLP
            // 
            this.workerRemoveLP.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerRemoveLPDoWork);
            this.workerRemoveLP.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerRemoveLPRunWorkerCompleted);
            // 
            // workerBackupCheckKeyList
            // 
            this.workerBackupCheckKeyList.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerBackupCheckKeyListDoWork);
            this.workerBackupCheckKeyList.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerBackupCheckKeyListRunWorkerCompleted);
            // 
            // workerCheckKeyList
            // 
            this.workerCheckKeyList.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerCheckKeyListDoWork);
            this.workerCheckKeyList.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerCheckKeyListRunWorkerCompleted);
            // 
            // workerPhoneActivate
            // 
            this.workerPhoneActivate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerPhoneActivationDoWork);
            this.workerPhoneActivate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerPhoneActivationRunWorkerCompleted);
            // 
            // btnAddLP
            // 
            this.btnAddLP.Location = new System.Drawing.Point(6, 46);
            this.btnAddLP.Name = "btnAddLP";
            this.btnAddLP.Size = new System.Drawing.Size(113, 23);
            this.btnAddLP.TabIndex = 0;
            this.btnAddLP.Text = "Add Languages";
            this.btnAddLP.UseVisualStyleBackColor = true;
            this.btnAddLP.Click += new System.EventHandler(this.BtnAddLPClick);
            // 
            // btnChangeChannel
            // 
            this.btnChangeChannel.Location = new System.Drawing.Point(122, 76);
            this.btnChangeChannel.Name = "btnChangeChannel";
            this.btnChangeChannel.Size = new System.Drawing.Size(103, 23);
            this.btnChangeChannel.TabIndex = 2;
            this.btnChangeChannel.Text = "Change Channel";
            this.btnChangeChannel.UseVisualStyleBackColor = true;
            this.btnChangeChannel.Click += new System.EventHandler(this.BtnChangeChannelClick);
            // 
            // btnRemoveLP
            // 
            this.btnRemoveLP.Location = new System.Drawing.Point(6, 17);
            this.btnRemoveLP.Name = "btnRemoveLP";
            this.btnRemoveLP.Size = new System.Drawing.Size(113, 23);
            this.btnRemoveLP.TabIndex = 1;
            this.btnRemoveLP.Text = "Remove Language";
            this.btnRemoveLP.UseVisualStyleBackColor = true;
            this.btnRemoveLP.Click += new System.EventHandler(this.BtnRemoveLPClick);
            // 
            // btnMakeISO
            // 
            this.btnMakeISO.Location = new System.Drawing.Point(122, 16);
            this.btnMakeISO.Name = "btnMakeISO";
            this.btnMakeISO.Size = new System.Drawing.Size(103, 23);
            this.btnMakeISO.TabIndex = 34;
            this.btnMakeISO.Text = "Make ISO";
            this.btnMakeISO.UseVisualStyleBackColor = true;
            this.btnMakeISO.Click += new System.EventHandler(this.BtnMakeISOClick);
            // 
            // btnRunOCT
            // 
            this.btnRunOCT.Location = new System.Drawing.Point(122, 46);
            this.btnRunOCT.Name = "btnRunOCT";
            this.btnRunOCT.Size = new System.Drawing.Size(103, 23);
            this.btnRunOCT.TabIndex = 35;
            this.btnRunOCT.Text = "Run OCT";
            this.btnRunOCT.UseVisualStyleBackColor = true;
            this.btnRunOCT.Click += new System.EventHandler(this.BtnRunOCTClick);
            // 
            // btnAddUpdate
            // 
            this.btnAddUpdate.Location = new System.Drawing.Point(6, 76);
            this.btnAddUpdate.Name = "btnAddUpdate";
            this.btnAddUpdate.Size = new System.Drawing.Size(113, 23);
            this.btnAddUpdate.TabIndex = 36;
            this.btnAddUpdate.Text = "Add Updates";
            this.btnAddUpdate.UseVisualStyleBackColor = true;
            this.btnAddUpdate.Click += new System.EventHandler(this.BtnAddUpdateClick);
            // 
            // btnBrowseToSetup
            // 
            this.btnBrowseToSetup.Location = new System.Drawing.Point(10, 129);
            this.btnBrowseToSetup.Name = "btnBrowseToSetup";
            this.btnBrowseToSetup.Size = new System.Drawing.Size(108, 22);
            this.btnBrowseToSetup.TabIndex = 37;
            this.btnBrowseToSetup.Text = "Set Installer Path";
            this.btnBrowseToSetup.UseVisualStyleBackColor = true;
            this.btnBrowseToSetup.Click += new System.EventHandler(this.BtnBrowseToSetupClick);
            // 
            // btnExtractSetup
            // 
            this.btnExtractSetup.Location = new System.Drawing.Point(125, 129);
            this.btnExtractSetup.Name = "btnExtractSetup";
            this.btnExtractSetup.Size = new System.Drawing.Size(89, 22);
            this.btnExtractSetup.TabIndex = 38;
            this.btnExtractSetup.Text = "Extract Setup";
            this.btnExtractSetup.UseVisualStyleBackColor = true;
            this.btnExtractSetup.Click += new System.EventHandler(this.BtnExtractSetupClick);
            // 
            // workerAutoKMSCustomTaskInstall
            // 
            this.workerAutoKMSCustomTaskInstall.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerAutoKMSCustomTaskInstallDoWork);
            this.workerAutoKMSCustomTaskInstall.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerAutoKMSCustomTaskInstallRunWorkerCompleted);
            // 
            // workerAutoKMSCustomTaskUninstall
            // 
            this.workerAutoKMSCustomTaskUninstall.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerAutoKMSCustomTaskUninstallDoWork);
            this.workerAutoKMSCustomTaskUninstall.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerAutoKMSCustomTaskUninstallRunWorkerCompleted);
            // 
            // workerBackupReinstallKeys
            // 
            this.workerBackupReinstallKeys.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerBackupReinstallKeysDoWork);
            this.workerBackupReinstallKeys.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerBackupReinstallKeysRunWorkerCompleted);
            // 
            // tabCustomizeSetup
            // 
            this.tabCustomizeSetup.BackColor = System.Drawing.SystemColors.Control;
            this.tabCustomizeSetup.Controls.Add(this.groupBox2);
            this.tabCustomizeSetup.Controls.Add(this.groupBox1);
            this.tabCustomizeSetup.Location = new System.Drawing.Point(4, 24);
            this.tabCustomizeSetup.Name = "tabCustomizeSetup";
            this.tabCustomizeSetup.Padding = new System.Windows.Forms.Padding(3);
            this.tabCustomizeSetup.Size = new System.Drawing.Size(462, 167);
            this.tabCustomizeSetup.TabIndex = 5;
            this.tabCustomizeSetup.Text = "Customize Setup";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblVersion);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnExtractSetup);
            this.groupBox2.Controls.Add(this.btnBrowseToSetup);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.comboInstallerApp);
            this.groupBox2.Controls.Add(this.comboInstalledLPs);
            this.groupBox2.Controls.Add(this.comboInstallerChannel);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 162);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Office Setup Files";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(58, 20);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(12, 15);
            this.lblVersion.TabIndex = 42;
            this.lblVersion.Text = "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 41;
            this.label1.Text = "Version:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 108);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 15);
            this.label17.TabIndex = 40;
            this.label17.Text = "Edition:";
            // 
            // comboInstallerApp
            // 
            this.comboInstallerApp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboInstallerApp.FormattingEnabled = true;
            this.comboInstallerApp.Location = new System.Drawing.Point(61, 104);
            this.comboInstallerApp.Name = "comboInstallerApp";
            this.comboInstallerApp.Size = new System.Drawing.Size(153, 23);
            this.comboInstallerApp.TabIndex = 32;
            // 
            // comboInstalledLPs
            // 
            this.comboInstalledLPs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboInstalledLPs.FormattingEnabled = true;
            this.comboInstalledLPs.Location = new System.Drawing.Point(61, 46);
            this.comboInstalledLPs.Name = "comboInstalledLPs";
            this.comboInstalledLPs.Size = new System.Drawing.Size(153, 23);
            this.comboInstalledLPs.TabIndex = 37;
            // 
            // comboInstallerChannel
            // 
            this.comboInstallerChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboInstallerChannel.FormattingEnabled = true;
            this.comboInstallerChannel.Location = new System.Drawing.Point(61, 76);
            this.comboInstallerChannel.Name = "comboInstallerChannel";
            this.comboInstallerChannel.Size = new System.Drawing.Size(153, 23);
            this.comboInstallerChannel.TabIndex = 31;
            this.comboInstallerChannel.SelectedIndexChanged += new System.EventHandler(this.CboInstallerChannelSelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 15);
            this.label6.TabIndex = 38;
            this.label6.Text = "Lang:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 78);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(54, 15);
            this.label16.TabIndex = 39;
            this.label16.Text = "Channel:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddUpdate);
            this.groupBox1.Controls.Add(this.btnRunOCT);
            this.groupBox1.Controls.Add(this.btnMakeISO);
            this.groupBox1.Controls.Add(this.btnRemoveLP);
            this.groupBox1.Controls.Add(this.btnChangeChannel);
            this.groupBox1.Controls.Add(this.btnAddLP);
            this.groupBox1.Location = new System.Drawing.Point(227, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 162);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Customization";
            // 
            // tabBackups
            // 
            this.tabBackups.BackColor = System.Drawing.SystemColors.Control;
            this.tabBackups.Controls.Add(this.grpBackupFunctions);
            this.tabBackups.Location = new System.Drawing.Point(4, 24);
            this.tabBackups.Name = "tabBackups";
            this.tabBackups.Size = new System.Drawing.Size(462, 167);
            this.tabBackups.TabIndex = 2;
            this.tabBackups.Text = "License Backup";
            // 
            // grpBackupFunctions
            // 
            this.grpBackupFunctions.Controls.Add(this.textBackupName);
            this.grpBackupFunctions.Controls.Add(this.btnBackupReinstallKeys);
            this.grpBackupFunctions.Controls.Add(this.btnBackupCheckKeyList);
            this.grpBackupFunctions.Controls.Add(this.lblBackupName);
            this.grpBackupFunctions.Controls.Add(this.btnBackup);
            this.grpBackupFunctions.Controls.Add(this.btnRestore);
            this.grpBackupFunctions.Location = new System.Drawing.Point(227, 3);
            this.grpBackupFunctions.Name = "grpBackupFunctions";
            this.grpBackupFunctions.Size = new System.Drawing.Size(229, 162);
            this.grpBackupFunctions.TabIndex = 39;
            this.grpBackupFunctions.TabStop = false;
            this.grpBackupFunctions.Text = "Activation Backup/Restore Functions";
            // 
            // textBackupName
            // 
            this.textBackupName.FormattingEnabled = true;
            this.textBackupName.Location = new System.Drawing.Point(6, 125);
            this.textBackupName.Name = "textBackupName";
            this.textBackupName.Size = new System.Drawing.Size(207, 23);
            this.textBackupName.TabIndex = 32;
            // 
            // btnBackupReinstallKeys
            // 
            this.btnBackupReinstallKeys.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBackupReinstallKeys.Location = new System.Drawing.Point(117, 50);
            this.btnBackupReinstallKeys.Name = "btnBackupReinstallKeys";
            this.btnBackupReinstallKeys.Size = new System.Drawing.Size(96, 22);
            this.btnBackupReinstallKeys.TabIndex = 25;
            this.btnBackupReinstallKeys.Text = "Restore Keys Only";
            this.btnBackupReinstallKeys.UseVisualStyleBackColor = true;
            this.btnBackupReinstallKeys.Click += new System.EventHandler(this.BtnBackupReinstallKeysClick);
            // 
            // btnBackupCheckKeyList
            // 
            this.btnBackupCheckKeyList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBackupCheckKeyList.Location = new System.Drawing.Point(117, 22);
            this.btnBackupCheckKeyList.Name = "btnBackupCheckKeyList";
            this.btnBackupCheckKeyList.Size = new System.Drawing.Size(96, 22);
            this.btnBackupCheckKeyList.TabIndex = 24;
            this.btnBackupCheckKeyList.Text = "Check KeyList";
            this.btnBackupCheckKeyList.UseVisualStyleBackColor = true;
            this.btnBackupCheckKeyList.Click += new System.EventHandler(this.BtnBackupCheckKeyListClick);
            // 
            // lblBackupName
            // 
            this.lblBackupName.AutoSize = true;
            this.lblBackupName.Location = new System.Drawing.Point(6, 107);
            this.lblBackupName.Name = "lblBackupName";
            this.lblBackupName.Size = new System.Drawing.Size(82, 15);
            this.lblBackupName.TabIndex = 16;
            this.lblBackupName.Text = "Backup name:";
            // 
            // btnBackup
            // 
            this.btnBackup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBackup.Location = new System.Drawing.Point(6, 22);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(96, 22);
            this.btnBackup.TabIndex = 12;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.BtnBackupClick);
            // 
            // btnRestore
            // 
            this.btnRestore.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRestore.Location = new System.Drawing.Point(6, 50);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(96, 22);
            this.btnRestore.TabIndex = 13;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.BtnRestoreClick);
            // 
            // tabKey
            // 
            this.tabKey.BackColor = System.Drawing.SystemColors.Control;
            this.tabKey.Controls.Add(this.grpKeySelection);
            this.tabKey.Controls.Add(this.grpKeyFunctions);
            this.tabKey.Location = new System.Drawing.Point(4, 24);
            this.tabKey.Name = "tabKey";
            this.tabKey.Padding = new System.Windows.Forms.Padding(3);
            this.tabKey.Size = new System.Drawing.Size(462, 167);
            this.tabKey.TabIndex = 1;
            this.tabKey.Text = "Product Keys";
            // 
            // grpKeySelection
            // 
            this.grpKeySelection.Controls.Add(this.lblKeyProduct);
            this.grpKeySelection.Controls.Add(this.cboKeyProduct);
            this.grpKeySelection.Controls.Add(this.cboKeyEdition);
            this.grpKeySelection.Controls.Add(this.lblKeyEdition);
            this.grpKeySelection.Location = new System.Drawing.Point(3, 3);
            this.grpKeySelection.Name = "grpKeySelection";
            this.grpKeySelection.Size = new System.Drawing.Size(220, 162);
            this.grpKeySelection.TabIndex = 36;
            this.grpKeySelection.TabStop = false;
            this.grpKeySelection.Text = "Product Selection";
            // 
            // lblKeyProduct
            // 
            this.lblKeyProduct.AutoSize = true;
            this.lblKeyProduct.Location = new System.Drawing.Point(8, 22);
            this.lblKeyProduct.Name = "lblKeyProduct";
            this.lblKeyProduct.Size = new System.Drawing.Size(52, 15);
            this.lblKeyProduct.TabIndex = 40;
            this.lblKeyProduct.Text = "Product:";
            // 
            // cboKeyProduct
            // 
            this.cboKeyProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKeyProduct.DropDownWidth = 200;
            this.cboKeyProduct.FormattingEnabled = true;
            this.cboKeyProduct.Location = new System.Drawing.Point(11, 40);
            this.cboKeyProduct.Name = "cboKeyProduct";
            this.cboKeyProduct.Size = new System.Drawing.Size(200, 23);
            this.cboKeyProduct.TabIndex = 39;
            this.cboKeyProduct.SelectedIndexChanged += new System.EventHandler(this.CboKeyProductSelectedIndexChanged);
            // 
            // cboKeyEdition
            // 
            this.cboKeyEdition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKeyEdition.DropDownWidth = 200;
            this.cboKeyEdition.FormattingEnabled = true;
            this.cboKeyEdition.Location = new System.Drawing.Point(11, 84);
            this.cboKeyEdition.Name = "cboKeyEdition";
            this.cboKeyEdition.Size = new System.Drawing.Size(200, 23);
            this.cboKeyEdition.TabIndex = 38;
            // 
            // lblKeyEdition
            // 
            this.lblKeyEdition.AutoSize = true;
            this.lblKeyEdition.Location = new System.Drawing.Point(8, 66);
            this.lblKeyEdition.Name = "lblKeyEdition";
            this.lblKeyEdition.Size = new System.Drawing.Size(47, 15);
            this.lblKeyEdition.TabIndex = 37;
            this.lblKeyEdition.Text = "Edition:";
            // 
            // grpKeyFunctions
            // 
            this.grpKeyFunctions.Controls.Add(this.BtnKeysSystemShow);
            this.grpKeyFunctions.Controls.Add(this.BtnKeysSystemCheck);
            this.grpKeyFunctions.Controls.Add(this.chkEnterCustomKey);
            this.grpKeyFunctions.Controls.Add(this.btnCheckKeyList);
            this.grpKeyFunctions.Controls.Add(this.btnCheckKey);
            this.grpKeyFunctions.Controls.Add(this.btnInstallKey);
            this.grpKeyFunctions.Controls.Add(this.btnUninstallKey);
            this.grpKeyFunctions.Controls.Add(this.txtCustomKey);
            this.grpKeyFunctions.Location = new System.Drawing.Point(227, 3);
            this.grpKeyFunctions.Name = "grpKeyFunctions";
            this.grpKeyFunctions.Size = new System.Drawing.Size(229, 162);
            this.grpKeyFunctions.TabIndex = 35;
            this.grpKeyFunctions.TabStop = false;
            this.grpKeyFunctions.Text = "Key Functions";
            // 
            // BtnKeysSystemShow
            // 
            this.BtnKeysSystemShow.Location = new System.Drawing.Point(117, 78);
            this.BtnKeysSystemShow.Name = "BtnKeysSystemShow";
            this.BtnKeysSystemShow.Size = new System.Drawing.Size(96, 23);
            this.BtnKeysSystemShow.TabIndex = 39;
            this.BtnKeysSystemShow.Text = "Show System";
            this.BtnKeysSystemShow.UseVisualStyleBackColor = true;
            this.BtnKeysSystemShow.Click += new System.EventHandler(this.BtnKeysSystemShowClick);
            // 
            // BtnKeysSystemCheck
            // 
            this.BtnKeysSystemCheck.Location = new System.Drawing.Point(6, 78);
            this.BtnKeysSystemCheck.Name = "BtnKeysSystemCheck";
            this.BtnKeysSystemCheck.Size = new System.Drawing.Size(96, 23);
            this.BtnKeysSystemCheck.TabIndex = 38;
            this.BtnKeysSystemCheck.Text = "Check System";
            this.BtnKeysSystemCheck.UseVisualStyleBackColor = true;
            this.BtnKeysSystemCheck.Click += new System.EventHandler(this.BtnKeysSystemCheckClick);
            // 
            // chkEnterCustomKey
            // 
            this.chkEnterCustomKey.AutoSize = true;
            this.chkEnterCustomKey.Location = new System.Drawing.Point(6, 107);
            this.chkEnterCustomKey.Name = "chkEnterCustomKey";
            this.chkEnterCustomKey.Size = new System.Drawing.Size(123, 19);
            this.chkEnterCustomKey.TabIndex = 38;
            this.chkEnterCustomKey.Text = "Enter Custom Key:";
            this.chkEnterCustomKey.UseVisualStyleBackColor = true;
            this.chkEnterCustomKey.CheckedChanged += new System.EventHandler(this.ChkCustomKeyCheckedChanged);
            // 
            // btnCheckKeyList
            // 
            this.btnCheckKeyList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCheckKeyList.Location = new System.Drawing.Point(117, 50);
            this.btnCheckKeyList.Name = "btnCheckKeyList";
            this.btnCheckKeyList.Size = new System.Drawing.Size(96, 23);
            this.btnCheckKeyList.TabIndex = 25;
            this.btnCheckKeyList.Text = "Check KeyList";
            this.btnCheckKeyList.UseVisualStyleBackColor = true;
            this.btnCheckKeyList.Click += new System.EventHandler(this.BtnCheckKeyListClick);
            // 
            // btnCheckKey
            // 
            this.btnCheckKey.Location = new System.Drawing.Point(6, 50);
            this.btnCheckKey.Name = "btnCheckKey";
            this.btnCheckKey.Size = new System.Drawing.Size(96, 23);
            this.btnCheckKey.TabIndex = 24;
            this.btnCheckKey.Text = "Check";
            this.btnCheckKey.UseVisualStyleBackColor = true;
            this.btnCheckKey.Click += new System.EventHandler(this.BtnCheckKeyClick);
            // 
            // btnInstallKey
            // 
            this.btnInstallKey.Location = new System.Drawing.Point(6, 22);
            this.btnInstallKey.Name = "btnInstallKey";
            this.btnInstallKey.Size = new System.Drawing.Size(96, 23);
            this.btnInstallKey.TabIndex = 22;
            this.btnInstallKey.Text = "Install";
            this.btnInstallKey.UseVisualStyleBackColor = true;
            this.btnInstallKey.Click += new System.EventHandler(this.BtnInstallKeyClick);
            // 
            // btnUninstallKey
            // 
            this.btnUninstallKey.Location = new System.Drawing.Point(117, 22);
            this.btnUninstallKey.Name = "btnUninstallKey";
            this.btnUninstallKey.Size = new System.Drawing.Size(96, 23);
            this.btnUninstallKey.TabIndex = 23;
            this.btnUninstallKey.Text = "Uninstall";
            this.btnUninstallKey.UseVisualStyleBackColor = true;
            this.btnUninstallKey.Click += new System.EventHandler(this.BtnUninstallKeyClick);
            // 
            // txtCustomKey
            // 
            this.txtCustomKey.Enabled = false;
            this.txtCustomKey.Location = new System.Drawing.Point(3, 130);
            this.txtCustomKey.MaxLength = 29;
            this.txtCustomKey.Name = "txtCustomKey";
            this.txtCustomKey.Size = new System.Drawing.Size(210, 23);
            this.txtCustomKey.TabIndex = 16;
            this.txtCustomKey.TextChanged += new System.EventHandler(this.TxtCustomKeyTextChanged);
            // 
            // tabActivationTools
            // 
            this.tabActivationTools.BackColor = System.Drawing.SystemColors.Control;
            this.tabActivationTools.Controls.Add(this.grpActivationFunctions);
            this.tabActivationTools.Controls.Add(this.grpActivationToolBox);
            this.tabActivationTools.Controls.Add(this.label12);
            this.tabActivationTools.Location = new System.Drawing.Point(4, 24);
            this.tabActivationTools.Name = "tabActivationTools";
            this.tabActivationTools.Padding = new System.Windows.Forms.Padding(3);
            this.tabActivationTools.Size = new System.Drawing.Size(462, 167);
            this.tabActivationTools.TabIndex = 0;
            this.tabActivationTools.Text = "Activation";
            // 
            // grpActivationFunctions
            // 
            this.grpActivationFunctions.Controls.Add(this.btnEZActivator);
            this.grpActivationFunctions.Controls.Add(this.btnPhoneActivation);
            this.grpActivationFunctions.Controls.Add(this.btnAttemptActivation);
            this.grpActivationFunctions.Controls.Add(this.btnGetRearmCount);
            this.grpActivationFunctions.Controls.Add(this.btnRearm);
            this.grpActivationFunctions.Controls.Add(this.btnCheckActivation);
            this.grpActivationFunctions.Location = new System.Drawing.Point(227, 3);
            this.grpActivationFunctions.Name = "grpActivationFunctions";
            this.grpActivationFunctions.Size = new System.Drawing.Size(229, 162);
            this.grpActivationFunctions.TabIndex = 40;
            this.grpActivationFunctions.TabStop = false;
            this.grpActivationFunctions.Text = "Activation/Licensing Functions";
            // 
            // btnEZActivator
            // 
            this.btnEZActivator.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEZActivator.ForeColor = System.Drawing.Color.Green;
            this.btnEZActivator.Location = new System.Drawing.Point(117, 79);
            this.btnEZActivator.Name = "btnEZActivator";
            this.btnEZActivator.Size = new System.Drawing.Size(96, 23);
            this.btnEZActivator.TabIndex = 29;
            this.btnEZActivator.Text = "EZ-Activator";
            this.btnEZActivator.UseVisualStyleBackColor = true;
            this.btnEZActivator.Click += new System.EventHandler(this.BtnEZActivatorClick);
            // 
            // btnPhoneActivation
            // 
            this.btnPhoneActivation.Location = new System.Drawing.Point(117, 22);
            this.btnPhoneActivation.Name = "btnPhoneActivation";
            this.btnPhoneActivation.Size = new System.Drawing.Size(96, 23);
            this.btnPhoneActivation.TabIndex = 28;
            this.btnPhoneActivation.Text = "Phone";
            this.btnPhoneActivation.UseVisualStyleBackColor = true;
            this.btnPhoneActivation.Click += new System.EventHandler(this.BtnPhoneActivationClick);
            // 
            // btnAttemptActivation
            // 
            this.btnAttemptActivation.Location = new System.Drawing.Point(6, 22);
            this.btnAttemptActivation.Name = "btnAttemptActivation";
            this.btnAttemptActivation.Size = new System.Drawing.Size(96, 23);
            this.btnAttemptActivation.TabIndex = 13;
            this.btnAttemptActivation.Text = "Activate";
            this.btnAttemptActivation.UseVisualStyleBackColor = true;
            this.btnAttemptActivation.Click += new System.EventHandler(this.BtnAttemptActivationClick);
            // 
            // btnGetRearmCount
            // 
            this.btnGetRearmCount.Location = new System.Drawing.Point(117, 50);
            this.btnGetRearmCount.Name = "btnGetRearmCount";
            this.btnGetRearmCount.Size = new System.Drawing.Size(96, 23);
            this.btnGetRearmCount.TabIndex = 18;
            this.btnGetRearmCount.Text = "Available";
            this.btnGetRearmCount.UseVisualStyleBackColor = true;
            this.btnGetRearmCount.Click += new System.EventHandler(this.BtnGetRearmCountClick);
            // 
            // btnRearm
            // 
            this.btnRearm.Location = new System.Drawing.Point(6, 50);
            this.btnRearm.Name = "btnRearm";
            this.btnRearm.Size = new System.Drawing.Size(96, 23);
            this.btnRearm.TabIndex = 17;
            this.btnRearm.Text = "Rearm";
            this.btnRearm.UseVisualStyleBackColor = true;
            this.btnRearm.Click += new System.EventHandler(this.BtnRearmClick);
            // 
            // btnCheckActivation
            // 
            this.btnCheckActivation.Location = new System.Drawing.Point(6, 78);
            this.btnCheckActivation.Name = "btnCheckActivation";
            this.btnCheckActivation.Size = new System.Drawing.Size(96, 23);
            this.btnCheckActivation.TabIndex = 15;
            this.btnCheckActivation.Text = "Check";
            this.btnCheckActivation.UseVisualStyleBackColor = true;
            this.btnCheckActivation.Click += new System.EventHandler(this.BtnCheckActivationClick);
            // 
            // grpActivationToolBox
            // 
            this.grpActivationToolBox.Controls.Add(this.lblActivationTool);
            this.grpActivationToolBox.Controls.Add(this.cboActivationToolBox);
            this.grpActivationToolBox.Controls.Add(this.btnActivationToolUninstall);
            this.grpActivationToolBox.Controls.Add(this.btnActivationToolInstall);
            this.grpActivationToolBox.Location = new System.Drawing.Point(3, 3);
            this.grpActivationToolBox.Name = "grpActivationToolBox";
            this.grpActivationToolBox.Size = new System.Drawing.Size(220, 162);
            this.grpActivationToolBox.TabIndex = 39;
            this.grpActivationToolBox.TabStop = false;
            this.grpActivationToolBox.Text = "Activator/Tool Selection";
            // 
            // lblActivationTool
            // 
            this.lblActivationTool.AutoSize = true;
            this.lblActivationTool.Location = new System.Drawing.Point(8, 22);
            this.lblActivationTool.Name = "lblActivationTool";
            this.lblActivationTool.Size = new System.Drawing.Size(33, 15);
            this.lblActivationTool.TabIndex = 40;
            this.lblActivationTool.Text = "Tool:";
            // 
            // cboActivationToolBox
            // 
            this.cboActivationToolBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboActivationToolBox.FormattingEnabled = true;
            this.cboActivationToolBox.Location = new System.Drawing.Point(61, 20);
            this.cboActivationToolBox.Name = "cboActivationToolBox";
            this.cboActivationToolBox.Size = new System.Drawing.Size(153, 23);
            this.cboActivationToolBox.TabIndex = 27;
            this.cboActivationToolBox.SelectedIndexChanged += new System.EventHandler(this.ActivationToolBoxSelectedIndexChanged);
            // 
            // btnActivationToolUninstall
            // 
            this.btnActivationToolUninstall.Location = new System.Drawing.Point(139, 50);
            this.btnActivationToolUninstall.Name = "btnActivationToolUninstall";
            this.btnActivationToolUninstall.Size = new System.Drawing.Size(75, 23);
            this.btnActivationToolUninstall.TabIndex = 26;
            this.btnActivationToolUninstall.Text = "Uninstall";
            this.btnActivationToolUninstall.UseVisualStyleBackColor = true;
            this.btnActivationToolUninstall.Click += new System.EventHandler(this.BtnActivationToolUninstallClick);
            // 
            // btnActivationToolInstall
            // 
            this.btnActivationToolInstall.Location = new System.Drawing.Point(61, 50);
            this.btnActivationToolInstall.Name = "btnActivationToolInstall";
            this.btnActivationToolInstall.Size = new System.Drawing.Size(75, 23);
            this.btnActivationToolInstall.TabIndex = 25;
            this.btnActivationToolInstall.Text = "Install";
            this.btnActivationToolInstall.UseVisualStyleBackColor = true;
            this.btnActivationToolInstall.Click += new System.EventHandler(this.BtnActivationToolInstallClick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(99, 117);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 15);
            this.label12.TabIndex = 16;
            this.label12.Text = "Rearm Office 2010:";
            // 
            // tabMainWindows
            // 
            this.tabMainWindows.BackColor = System.Drawing.SystemColors.Control;
            this.tabMainWindows.Controls.Add(this.grpWindowsInformation);
            this.tabMainWindows.Location = new System.Drawing.Point(4, 24);
            this.tabMainWindows.Name = "tabMainWindows";
            this.tabMainWindows.Size = new System.Drawing.Size(462, 167);
            this.tabMainWindows.TabIndex = 6;
            this.tabMainWindows.Text = "Main";
            // 
            // grpWindowsInformation
            // 
            this.grpWindowsInformation.Controls.Add(this.lblWindowsNumber);
            this.grpWindowsInformation.Controls.Add(this.lblWindowsNumberDescription);
            this.grpWindowsInformation.Controls.Add(this.lblWindowsProduct);
            this.grpWindowsInformation.Controls.Add(this.lblWindowsProductDescription);
            this.grpWindowsInformation.Controls.Add(this.lblWindowsArchitecture);
            this.grpWindowsInformation.Controls.Add(this.lblWindowsArchitectureDescription);
            this.grpWindowsInformation.Controls.Add(this.lblWindowsSupported);
            this.grpWindowsInformation.Controls.Add(this.lblWindowsSupportedDescription);
            this.grpWindowsInformation.Location = new System.Drawing.Point(3, 3);
            this.grpWindowsInformation.Name = "grpWindowsInformation";
            this.grpWindowsInformation.Size = new System.Drawing.Size(220, 162);
            this.grpWindowsInformation.TabIndex = 1;
            this.grpWindowsInformation.TabStop = false;
            this.grpWindowsInformation.Text = "Windows Information";
            // 
            // lblWindowsNumber
            // 
            this.lblWindowsNumber.AutoSize = true;
            this.lblWindowsNumber.Location = new System.Drawing.Point(83, 87);
            this.lblWindowsNumber.Name = "lblWindowsNumber";
            this.lblWindowsNumber.Size = new System.Drawing.Size(12, 15);
            this.lblWindowsNumber.TabIndex = 11;
            this.lblWindowsNumber.Text = "-";
            // 
            // lblWindowsNumberDescription
            // 
            this.lblWindowsNumberDescription.AutoSize = true;
            this.lblWindowsNumberDescription.Location = new System.Drawing.Point(6, 87);
            this.lblWindowsNumberDescription.Name = "lblWindowsNumberDescription";
            this.lblWindowsNumberDescription.Size = new System.Drawing.Size(54, 15);
            this.lblWindowsNumberDescription.TabIndex = 10;
            this.lblWindowsNumberDescription.Text = "Number:";
            // 
            // lblWindowsProduct
            // 
            this.lblWindowsProduct.AutoSize = true;
            this.lblWindowsProduct.Location = new System.Drawing.Point(83, 63);
            this.lblWindowsProduct.Name = "lblWindowsProduct";
            this.lblWindowsProduct.Size = new System.Drawing.Size(12, 15);
            this.lblWindowsProduct.TabIndex = 9;
            this.lblWindowsProduct.Text = "-";
            // 
            // lblWindowsProductDescription
            // 
            this.lblWindowsProductDescription.AutoSize = true;
            this.lblWindowsProductDescription.Location = new System.Drawing.Point(6, 63);
            this.lblWindowsProductDescription.Name = "lblWindowsProductDescription";
            this.lblWindowsProductDescription.Size = new System.Drawing.Size(52, 15);
            this.lblWindowsProductDescription.TabIndex = 8;
            this.lblWindowsProductDescription.Text = "Product:";
            // 
            // lblWindowsArchitecture
            // 
            this.lblWindowsArchitecture.AutoSize = true;
            this.lblWindowsArchitecture.Location = new System.Drawing.Point(83, 42);
            this.lblWindowsArchitecture.Name = "lblWindowsArchitecture";
            this.lblWindowsArchitecture.Size = new System.Drawing.Size(12, 15);
            this.lblWindowsArchitecture.TabIndex = 3;
            this.lblWindowsArchitecture.Text = "-";
            // 
            // lblWindowsArchitectureDescription
            // 
            this.lblWindowsArchitectureDescription.AutoSize = true;
            this.lblWindowsArchitectureDescription.Location = new System.Drawing.Point(6, 42);
            this.lblWindowsArchitectureDescription.Name = "lblWindowsArchitectureDescription";
            this.lblWindowsArchitectureDescription.Size = new System.Drawing.Size(75, 15);
            this.lblWindowsArchitectureDescription.TabIndex = 2;
            this.lblWindowsArchitectureDescription.Text = "Architecture:";
            // 
            // lblWindowsSupported
            // 
            this.lblWindowsSupported.AutoSize = true;
            this.lblWindowsSupported.Location = new System.Drawing.Point(83, 18);
            this.lblWindowsSupported.Name = "lblWindowsSupported";
            this.lblWindowsSupported.Size = new System.Drawing.Size(12, 15);
            this.lblWindowsSupported.TabIndex = 1;
            this.lblWindowsSupported.Text = "-";
            // 
            // lblWindowsSupportedDescription
            // 
            this.lblWindowsSupportedDescription.AutoSize = true;
            this.lblWindowsSupportedDescription.Location = new System.Drawing.Point(6, 18);
            this.lblWindowsSupportedDescription.Name = "lblWindowsSupportedDescription";
            this.lblWindowsSupportedDescription.Size = new System.Drawing.Size(65, 15);
            this.lblWindowsSupportedDescription.TabIndex = 0;
            this.lblWindowsSupportedDescription.Text = "Supported:";
            // 
            // tabMainOffice
            // 
            this.tabMainOffice.BackColor = System.Drawing.SystemColors.Control;
            this.tabMainOffice.Controls.Add(this.grpOfficeUninstaller);
            this.tabMainOffice.Controls.Add(this.grpOfficeInformation);
            this.tabMainOffice.Location = new System.Drawing.Point(4, 24);
            this.tabMainOffice.Name = "tabMainOffice";
            this.tabMainOffice.Size = new System.Drawing.Size(462, 167);
            this.tabMainOffice.TabIndex = 4;
            this.tabMainOffice.Text = "Main";
            // 
            // grpOfficeUninstaller
            // 
            this.grpOfficeUninstaller.Controls.Add(this.cboOfficeUninstaller);
            this.grpOfficeUninstaller.Controls.Add(this.btnOfficeUninstaller);
            this.grpOfficeUninstaller.Location = new System.Drawing.Point(227, 3);
            this.grpOfficeUninstaller.Name = "grpOfficeUninstaller";
            this.grpOfficeUninstaller.Size = new System.Drawing.Size(229, 162);
            this.grpOfficeUninstaller.TabIndex = 4;
            this.grpOfficeUninstaller.TabStop = false;
            this.grpOfficeUninstaller.Text = "Office Uninstaller";
            // 
            // cboOfficeUninstaller
            // 
            this.cboOfficeUninstaller.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOfficeUninstaller.FormattingEnabled = true;
            this.cboOfficeUninstaller.Location = new System.Drawing.Point(6, 27);
            this.cboOfficeUninstaller.Name = "cboOfficeUninstaller";
            this.cboOfficeUninstaller.Size = new System.Drawing.Size(217, 23);
            this.cboOfficeUninstaller.TabIndex = 28;
            // 
            // btnOfficeUninstaller
            // 
            this.btnOfficeUninstaller.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOfficeUninstaller.ForeColor = System.Drawing.Color.Red;
            this.btnOfficeUninstaller.Location = new System.Drawing.Point(6, 132);
            this.btnOfficeUninstaller.Name = "btnOfficeUninstaller";
            this.btnOfficeUninstaller.Size = new System.Drawing.Size(217, 23);
            this.btnOfficeUninstaller.TabIndex = 5;
            this.btnOfficeUninstaller.Text = "Uninstall Office";
            this.btnOfficeUninstaller.UseVisualStyleBackColor = true;
            this.btnOfficeUninstaller.Click += new System.EventHandler(this.BtnOfficeUninstallerClick);
            // 
            // grpOfficeInformation
            // 
            this.grpOfficeInformation.Controls.Add(this.lblOfficeInstallType);
            this.grpOfficeInformation.Controls.Add(this.label2);
            this.grpOfficeInformation.Controls.Add(this.lblOfficeNumber);
            this.grpOfficeInformation.Controls.Add(this.lblOfficeNumberDescription);
            this.grpOfficeInformation.Controls.Add(this.lblOfficeProduct);
            this.grpOfficeInformation.Controls.Add(this.lblOfficeProductDescription);
            this.grpOfficeInformation.Controls.Add(this.lblOfficeArchitecture);
            this.grpOfficeInformation.Controls.Add(this.lblOfficeArchitectureDescription);
            this.grpOfficeInformation.Controls.Add(this.lblOfficeSupported);
            this.grpOfficeInformation.Controls.Add(this.lblOfficeSupportedDescription);
            this.grpOfficeInformation.Location = new System.Drawing.Point(3, 3);
            this.grpOfficeInformation.Name = "grpOfficeInformation";
            this.grpOfficeInformation.Size = new System.Drawing.Size(220, 162);
            this.grpOfficeInformation.TabIndex = 0;
            this.grpOfficeInformation.TabStop = false;
            this.grpOfficeInformation.Text = "Office Information";
            // 
            // lblOfficeInstallType
            // 
            this.lblOfficeInstallType.AutoSize = true;
            this.lblOfficeInstallType.Location = new System.Drawing.Point(83, 111);
            this.lblOfficeInstallType.Name = "lblOfficeInstallType";
            this.lblOfficeInstallType.Size = new System.Drawing.Size(12, 15);
            this.lblOfficeInstallType.TabIndex = 11;
            this.lblOfficeInstallType.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Type:";
            // 
            // lblOfficeNumber
            // 
            this.lblOfficeNumber.AutoSize = true;
            this.lblOfficeNumber.Location = new System.Drawing.Point(83, 87);
            this.lblOfficeNumber.Name = "lblOfficeNumber";
            this.lblOfficeNumber.Size = new System.Drawing.Size(12, 15);
            this.lblOfficeNumber.TabIndex = 9;
            this.lblOfficeNumber.Text = "-";
            // 
            // lblOfficeNumberDescription
            // 
            this.lblOfficeNumberDescription.AutoSize = true;
            this.lblOfficeNumberDescription.Location = new System.Drawing.Point(6, 87);
            this.lblOfficeNumberDescription.Name = "lblOfficeNumberDescription";
            this.lblOfficeNumberDescription.Size = new System.Drawing.Size(54, 15);
            this.lblOfficeNumberDescription.TabIndex = 8;
            this.lblOfficeNumberDescription.Text = "Number:";
            // 
            // lblOfficeProduct
            // 
            this.lblOfficeProduct.AutoSize = true;
            this.lblOfficeProduct.Location = new System.Drawing.Point(83, 63);
            this.lblOfficeProduct.Name = "lblOfficeProduct";
            this.lblOfficeProduct.Size = new System.Drawing.Size(12, 15);
            this.lblOfficeProduct.TabIndex = 7;
            this.lblOfficeProduct.Text = "-";
            // 
            // lblOfficeProductDescription
            // 
            this.lblOfficeProductDescription.AutoSize = true;
            this.lblOfficeProductDescription.Location = new System.Drawing.Point(6, 63);
            this.lblOfficeProductDescription.Name = "lblOfficeProductDescription";
            this.lblOfficeProductDescription.Size = new System.Drawing.Size(52, 15);
            this.lblOfficeProductDescription.TabIndex = 6;
            this.lblOfficeProductDescription.Text = "Product:";
            // 
            // lblOfficeArchitecture
            // 
            this.lblOfficeArchitecture.AutoSize = true;
            this.lblOfficeArchitecture.Location = new System.Drawing.Point(83, 42);
            this.lblOfficeArchitecture.Name = "lblOfficeArchitecture";
            this.lblOfficeArchitecture.Size = new System.Drawing.Size(12, 15);
            this.lblOfficeArchitecture.TabIndex = 3;
            this.lblOfficeArchitecture.Text = "-";
            // 
            // lblOfficeArchitectureDescription
            // 
            this.lblOfficeArchitectureDescription.AutoSize = true;
            this.lblOfficeArchitectureDescription.Location = new System.Drawing.Point(6, 42);
            this.lblOfficeArchitectureDescription.Name = "lblOfficeArchitectureDescription";
            this.lblOfficeArchitectureDescription.Size = new System.Drawing.Size(75, 15);
            this.lblOfficeArchitectureDescription.TabIndex = 2;
            this.lblOfficeArchitectureDescription.Text = "Architecture:";
            // 
            // lblOfficeSupported
            // 
            this.lblOfficeSupported.AutoSize = true;
            this.lblOfficeSupported.Location = new System.Drawing.Point(83, 18);
            this.lblOfficeSupported.Name = "lblOfficeSupported";
            this.lblOfficeSupported.Size = new System.Drawing.Size(12, 15);
            this.lblOfficeSupported.TabIndex = 1;
            this.lblOfficeSupported.Text = "-";
            // 
            // lblOfficeSupportedDescription
            // 
            this.lblOfficeSupportedDescription.AutoSize = true;
            this.lblOfficeSupportedDescription.Location = new System.Drawing.Point(6, 18);
            this.lblOfficeSupportedDescription.Name = "lblOfficeSupportedDescription";
            this.lblOfficeSupportedDescription.Size = new System.Drawing.Size(65, 15);
            this.lblOfficeSupportedDescription.TabIndex = 0;
            this.lblOfficeSupportedDescription.Text = "Supported:";
            // 
            // tabFunctions
            // 
            this.tabFunctions.Controls.Add(this.tabMainOffice);
            this.tabFunctions.Controls.Add(this.tabMainWindows);
            this.tabFunctions.Controls.Add(this.tabActivationTools);
            this.tabFunctions.Controls.Add(this.tabKey);
            this.tabFunctions.Controls.Add(this.tabBackups);
            this.tabFunctions.Controls.Add(this.tabCustomizeSetup);
            this.tabFunctions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabFunctions.Location = new System.Drawing.Point(5, 2);
            this.tabFunctions.Name = "tabFunctions";
            this.tabFunctions.SelectedIndex = 0;
            this.tabFunctions.Size = new System.Drawing.Size(470, 195);
            this.tabFunctions.TabIndex = 21;
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.Color.Black;
            this.txtConsole.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsole.ForeColor = System.Drawing.Color.Lime;
            this.txtConsole.Location = new System.Drawing.Point(7, 16);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(460, 115);
            this.txtConsole.TabIndex = 0;
            // 
            // consoleBox
            // 
            this.consoleBox.Controls.Add(this.txtConsole);
            this.consoleBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleBox.Location = new System.Drawing.Point(3, 198);
            this.consoleBox.Name = "consoleBox";
            this.consoleBox.Size = new System.Drawing.Size(472, 138);
            this.consoleBox.TabIndex = 22;
            this.consoleBox.TabStop = false;
            this.consoleBox.Text = "Information Console";
            // 
            // workerKeysSystemCheck
            // 
            this.workerKeysSystemCheck.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerKeysSystemCheckDoWork);
            this.workerKeysSystemCheck.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerKeysSystemCheckRunWorkerCompleted);
            // 
            // workerKeysSystemShow
            // 
            this.workerKeysSystemShow.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerKeysSystemShowDoWork);
            this.workerKeysSystemShow.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerKeysSystemShowRunWorkerCompleted);
            // 
            // workerOfficeUninstaller
            // 
            this.workerOfficeUninstaller.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerOfficeUninstallerDoWork);
            this.workerOfficeUninstaller.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerOfficeUninstallerRunWorkerCompleted);
            // 
            // workerKMSServerServiceInstall
            // 
            this.workerKMSServerServiceInstall.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerKMSServerServiceInstallDoWork);
            this.workerKMSServerServiceInstall.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerKMSServerServiceInstallRunWorkerCompleted);
            // 
            // workerKMSServerServiceUninstall
            // 
            this.workerKMSServerServiceUninstall.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerKMSServerServiceUninstallDoWork);
            this.workerKMSServerServiceUninstall.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerKMSServerServiceUninstallRunWorkerCompleted);
            // 
            // btnSettings
            // 
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.ForeColor = System.Drawing.Color.Navy;
            this.btnSettings.Location = new System.Drawing.Point(402, 342);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 38;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.BtnSettingsClick);
            // 
            // workerEZActivator
            // 
            this.workerEZActivator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerEZActivatorDoWork);
            this.workerEZActivator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerEZActivatorRunWorkerCompleted);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 369);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.WindowsButton);
            this.Controls.Add(this.btnReadme);
            this.Controls.Add(this.OfficeButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.consoleBox);
            this.Controls.Add(this.tabFunctions);
            this.Controls.Add(this.btnCredits);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Microsoft Toolkit 2.7";
            this.tabCustomizeSetup.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabBackups.ResumeLayout(false);
            this.grpBackupFunctions.ResumeLayout(false);
            this.grpBackupFunctions.PerformLayout();
            this.tabKey.ResumeLayout(false);
            this.grpKeySelection.ResumeLayout(false);
            this.grpKeySelection.PerformLayout();
            this.grpKeyFunctions.ResumeLayout(false);
            this.grpKeyFunctions.PerformLayout();
            this.tabActivationTools.ResumeLayout(false);
            this.tabActivationTools.PerformLayout();
            this.grpActivationFunctions.ResumeLayout(false);
            this.grpActivationToolBox.ResumeLayout(false);
            this.grpActivationToolBox.PerformLayout();
            this.tabMainWindows.ResumeLayout(false);
            this.grpWindowsInformation.ResumeLayout(false);
            this.grpWindowsInformation.PerformLayout();
            this.tabMainOffice.ResumeLayout(false);
            this.grpOfficeUninstaller.ResumeLayout(false);
            this.grpOfficeInformation.ResumeLayout(false);
            this.grpOfficeInformation.PerformLayout();
            this.tabFunctions.ResumeLayout(false);
            this.consoleBox.ResumeLayout(false);
            this.consoleBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnCredits;
        private BackgroundWorker workerAutoKMSInstall;
        private ProgressBar progressBar;
        private BackgroundWorker workerAutoKMSUninstall;
        private BackgroundWorker workerRearm;
        private BackgroundWorker workerAttemptActivation;
        private BackgroundWorker workerCheckActivation;
        private Button btnReadme;
        private BackgroundWorker workerInstallKey;
        private BackgroundWorker workerUninstallKey;
        private BackgroundWorker workerCheckKey;
        private BackgroundWorker workerBackup;
        private BackgroundWorker workerRestore;
        private ComboBox comboBox1;
        private BackgroundWorker workerRearmCount;
        private BackgroundWorker workerAutoRearmInstall;
        private BackgroundWorker workerAutoRearmUninstall;
        private BackgroundWorker workerMakeISO;
        private BackgroundWorker workerExtractSetup;
        private BackgroundWorker workerChangeChannel;
        private BackgroundWorker workerAddLP;
        private BackgroundWorker workerAddUpdate;
        private BackgroundWorker workerRemoveLP;
        private BackgroundWorker workerBackupCheckKeyList;
        private BackgroundWorker workerCheckKeyList;
        private BackgroundWorker workerPhoneActivate;
        private BackgroundWorker workerAutoKMSCustomTaskInstall;
        private BackgroundWorker workerAutoKMSCustomTaskUninstall;
        private BackgroundWorker workerBackupReinstallKeys;
        private Button WindowsButton;
        private Button OfficeButton;
        private TabPage tabCustomizeSetup;
        private GroupBox groupBox2;
        private Button btnExtractSetup;
        private Button btnBrowseToSetup;
        private Label label17;
        private ComboBox comboInstallerApp;
        private ComboBox comboInstalledLPs;
        private ComboBox comboInstallerChannel;
        private Label label6;
        private Label label16;
        private GroupBox groupBox1;
        private Button btnAddUpdate;
        private Button btnRunOCT;
        private Button btnMakeISO;
        private Button btnRemoveLP;
        private Button btnChangeChannel;
        private Button btnAddLP;
        private TabPage tabBackups;
        private GroupBox grpBackupFunctions;
        private Button btnBackupReinstallKeys;
        private Button btnBackupCheckKeyList;
        private Label lblBackupName;
        private Button btnBackup;
        private Button btnRestore;
        private TabPage tabKey;
        private GroupBox grpKeySelection;
        private Label lblKeyProduct;
        private ComboBox cboKeyProduct;
        private ComboBox cboKeyEdition;
        private Label lblKeyEdition;
        private GroupBox grpKeyFunctions;
        private CheckBox chkEnterCustomKey;
        private Button btnCheckKeyList;
        private Button btnCheckKey;
        private Button btnInstallKey;
        private Button btnUninstallKey;
        private TextBox txtCustomKey;
        private TabPage tabActivationTools;
        private GroupBox grpActivationFunctions;
        private Button btnPhoneActivation;
        private Button btnAttemptActivation;
        private Button btnGetRearmCount;
        private Button btnRearm;
        private Button btnCheckActivation;
        private GroupBox grpActivationToolBox;
        private ComboBox cboActivationToolBox;
        private Button btnActivationToolUninstall;
        private Button btnActivationToolInstall;
        private Label label12;
        private TabPage tabMainWindows;
        private GroupBox grpWindowsInformation;
        private Label lblWindowsArchitecture;
        private Label lblWindowsArchitectureDescription;
        private Label lblWindowsSupported;
        private Label lblWindowsSupportedDescription;
        private TabPage tabMainOffice;
        private GroupBox grpOfficeUninstaller;
        private ComboBox cboOfficeUninstaller;
        private Button btnOfficeUninstaller;
        private GroupBox grpOfficeInformation;
        private Label lblOfficeArchitecture;
        private Label lblOfficeArchitectureDescription;
        private Label lblOfficeSupported;
        private Label lblOfficeSupportedDescription;
        private TabControl tabFunctions;
        private TextBox txtConsole;
        private GroupBox consoleBox;
        private Label lblWindowsProduct;
        private Label lblWindowsProductDescription;
        private Label lblOfficeProduct;
        private Label lblOfficeProductDescription;
        private ComboBox textBackupName;
        private readonly int TabHeight;
        private Button BtnKeysSystemCheck;
        private BackgroundWorker workerKeysSystemCheck;
        private Button BtnKeysSystemShow;
        private BackgroundWorker workerKeysSystemShow;
        private Label lblOfficeNumber;
        private Label lblOfficeNumberDescription;
        private Label lblWindowsNumber;
        private Label lblWindowsNumberDescription;
        private BackgroundWorker workerOfficeUninstaller;
        private Label lblActivationTool;
        private BackgroundWorker workerKMSServerServiceInstall;
        private BackgroundWorker workerKMSServerServiceUninstall;
        private Button btnSettings;
        private Button btnEZActivator;
        private BackgroundWorker workerEZActivator;
        private Label lblVersion;
        private Label label1;
        private Label lblOfficeInstallType;
        private Label label2;
    }
}

