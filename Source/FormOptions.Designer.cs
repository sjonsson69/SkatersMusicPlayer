namespace SkatersMusicPlayer
{
    partial class formOptions
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxAutoPauseMusic = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelDelay = new System.Windows.Forms.Label();
            this.volumeSliderPause = new NAudio.Gui.VolumeSlider();
            this.numericUpDownPause = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAutoPauseMusic = new System.Windows.Forms.CheckBox();
            this.groupBoxDirectories = new System.Windows.Forms.GroupBox();
            this.btnBreakDir = new System.Windows.Forms.Button();
            this.btnWarmupDir = new System.Windows.Forms.Button();
            this.tbBreakDir = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbWarmupDir = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBoxFSManager = new System.Windows.Forms.GroupBox();
            this.tbFSMServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbFSMServerPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbFSMUsername = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbFSMPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBoxAutoPauseMusic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPause)).BeginInit();
            this.groupBoxDirectories.SuspendLayout();
            this.groupBoxFSManager.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(906, 352);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(90, 29);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "&Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(12, 352);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 29);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBoxAutoPauseMusic
            // 
            this.groupBoxAutoPauseMusic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAutoPauseMusic.Controls.Add(this.label1);
            this.groupBoxAutoPauseMusic.Controls.Add(this.labelDelay);
            this.groupBoxAutoPauseMusic.Controls.Add(this.volumeSliderPause);
            this.groupBoxAutoPauseMusic.Controls.Add(this.numericUpDownPause);
            this.groupBoxAutoPauseMusic.Controls.Add(this.checkBoxAutoPauseMusic);
            this.groupBoxAutoPauseMusic.Location = new System.Drawing.Point(12, 12);
            this.groupBoxAutoPauseMusic.Name = "groupBoxAutoPauseMusic";
            this.groupBoxAutoPauseMusic.Size = new System.Drawing.Size(984, 62);
            this.groupBoxAutoPauseMusic.TabIndex = 0;
            this.groupBoxAutoPauseMusic.TabStop = false;
            this.groupBoxAutoPauseMusic.Text = "Automatic pause music (replay participants music)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(384, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pause Volume";
            // 
            // labelDelay
            // 
            this.labelDelay.AutoSize = true;
            this.labelDelay.Location = new System.Drawing.Point(130, 27);
            this.labelDelay.Name = "labelDelay";
            this.labelDelay.Size = new System.Drawing.Size(87, 20);
            this.labelDelay.TabIndex = 1;
            this.labelDelay.Text = "Fade delay";
            // 
            // volumeSliderPause
            // 
            this.volumeSliderPause.Location = new System.Drawing.Point(502, 26);
            this.volumeSliderPause.Name = "volumeSliderPause";
            this.volumeSliderPause.Size = new System.Drawing.Size(476, 23);
            this.volumeSliderPause.TabIndex = 4;
            this.volumeSliderPause.TabStop = false;
            this.volumeSliderPause.Volume = 0.1779F;
            // 
            // numericUpDownPause
            // 
            this.numericUpDownPause.Location = new System.Drawing.Point(223, 25);
            this.numericUpDownPause.Name = "numericUpDownPause";
            this.numericUpDownPause.Size = new System.Drawing.Size(100, 26);
            this.numericUpDownPause.TabIndex = 2;
            // 
            // checkBoxAutoPauseMusic
            // 
            this.checkBoxAutoPauseMusic.AutoSize = true;
            this.checkBoxAutoPauseMusic.Checked = true;
            this.checkBoxAutoPauseMusic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoPauseMusic.Location = new System.Drawing.Point(10, 25);
            this.checkBoxAutoPauseMusic.Name = "checkBoxAutoPauseMusic";
            this.checkBoxAutoPauseMusic.Size = new System.Drawing.Size(87, 24);
            this.checkBoxAutoPauseMusic.TabIndex = 0;
            this.checkBoxAutoPauseMusic.Text = "Enabled";
            this.checkBoxAutoPauseMusic.UseVisualStyleBackColor = true;
            // 
            // groupBoxDirectories
            // 
            this.groupBoxDirectories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDirectories.Controls.Add(this.btnBreakDir);
            this.groupBoxDirectories.Controls.Add(this.btnWarmupDir);
            this.groupBoxDirectories.Controls.Add(this.tbBreakDir);
            this.groupBoxDirectories.Controls.Add(this.label3);
            this.groupBoxDirectories.Controls.Add(this.tbWarmupDir);
            this.groupBoxDirectories.Controls.Add(this.label2);
            this.groupBoxDirectories.Location = new System.Drawing.Point(12, 81);
            this.groupBoxDirectories.Name = "groupBoxDirectories";
            this.groupBoxDirectories.Size = new System.Drawing.Size(984, 96);
            this.groupBoxDirectories.TabIndex = 1;
            this.groupBoxDirectories.TabStop = false;
            this.groupBoxDirectories.Text = "Directories (normally, don\'t change these!)";
            // 
            // btnBreakDir
            // 
            this.btnBreakDir.Location = new System.Drawing.Point(947, 58);
            this.btnBreakDir.Name = "btnBreakDir";
            this.btnBreakDir.Size = new System.Drawing.Size(31, 26);
            this.btnBreakDir.TabIndex = 5;
            this.btnBreakDir.Text = "...";
            this.btnBreakDir.UseVisualStyleBackColor = true;
            this.btnBreakDir.Click += new System.EventHandler(this.btnBreakDir_Click);
            // 
            // btnWarmupDir
            // 
            this.btnWarmupDir.Location = new System.Drawing.Point(947, 26);
            this.btnWarmupDir.Name = "btnWarmupDir";
            this.btnWarmupDir.Size = new System.Drawing.Size(31, 26);
            this.btnWarmupDir.TabIndex = 2;
            this.btnWarmupDir.Text = "...";
            this.btnWarmupDir.UseVisualStyleBackColor = true;
            this.btnWarmupDir.Click += new System.EventHandler(this.btnWarmupDir_Click);
            // 
            // tbBreakDir
            // 
            this.tbBreakDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBreakDir.Location = new System.Drawing.Point(223, 58);
            this.tbBreakDir.Name = "tbBreakDir";
            this.tbBreakDir.Size = new System.Drawing.Size(718, 26);
            this.tbBreakDir.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Break Music Directory";
            // 
            // tbWarmupDir
            // 
            this.tbWarmupDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWarmupDir.Location = new System.Drawing.Point(223, 26);
            this.tbWarmupDir.Name = "tbWarmupDir";
            this.tbWarmupDir.Size = new System.Drawing.Size(718, 26);
            this.tbWarmupDir.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Warmup Music Directory";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // groupBoxFSManager
            // 
            this.groupBoxFSManager.Controls.Add(this.tbFSMPassword);
            this.groupBoxFSManager.Controls.Add(this.label7);
            this.groupBoxFSManager.Controls.Add(this.tbFSMUsername);
            this.groupBoxFSManager.Controls.Add(this.label6);
            this.groupBoxFSManager.Controls.Add(this.tbFSMServerPort);
            this.groupBoxFSManager.Controls.Add(this.label5);
            this.groupBoxFSManager.Controls.Add(this.tbFSMServer);
            this.groupBoxFSManager.Controls.Add(this.label4);
            this.groupBoxFSManager.Location = new System.Drawing.Point(12, 183);
            this.groupBoxFSManager.Name = "groupBoxFSManager";
            this.groupBoxFSManager.Size = new System.Drawing.Size(984, 158);
            this.groupBoxFSManager.TabIndex = 5;
            this.groupBoxFSManager.TabStop = false;
            this.groupBoxFSManager.Text = "FS Manager - Server settings";
            // 
            // tbFSMServer
            // 
            this.tbFSMServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFSMServer.Location = new System.Drawing.Point(223, 25);
            this.tbFSMServer.Name = "tbFSMServer";
            this.tbFSMServer.Size = new System.Drawing.Size(755, 26);
            this.tbFSMServer.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Server address";
            // 
            // tbFSMServerPort
            // 
            this.tbFSMServerPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFSMServerPort.Location = new System.Drawing.Point(223, 57);
            this.tbFSMServerPort.Name = "tbFSMServerPort";
            this.tbFSMServerPort.Size = new System.Drawing.Size(755, 26);
            this.tbFSMServerPort.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Server port";
            // 
            // tbFSMUsername
            // 
            this.tbFSMUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFSMUsername.Location = new System.Drawing.Point(223, 89);
            this.tbFSMUsername.Name = "tbFSMUsername";
            this.tbFSMUsername.Size = new System.Drawing.Size(755, 26);
            this.tbFSMUsername.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Username";
            // 
            // tbFSMPassword
            // 
            this.tbFSMPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFSMPassword.Location = new System.Drawing.Point(223, 121);
            this.tbFSMPassword.Name = "tbFSMPassword";
            this.tbFSMPassword.PasswordChar = '*';
            this.tbFSMPassword.Size = new System.Drawing.Size(755, 26);
            this.tbFSMPassword.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Password";
            // 
            // formOptions
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1008, 393);
            this.ControlBox = false;
            this.Controls.Add(this.groupBoxFSManager);
            this.Controls.Add(this.groupBoxDirectories);
            this.Controls.Add(this.groupBoxAutoPauseMusic);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "formOptions";
            this.ShowInTaskbar = false;
            this.Text = "Options";
            this.groupBoxAutoPauseMusic.ResumeLayout(false);
            this.groupBoxAutoPauseMusic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPause)).EndInit();
            this.groupBoxDirectories.ResumeLayout(false);
            this.groupBoxDirectories.PerformLayout();
            this.groupBoxFSManager.ResumeLayout(false);
            this.groupBoxFSManager.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBoxAutoPauseMusic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelDelay;
        private NAudio.Gui.VolumeSlider volumeSliderPause;
        private System.Windows.Forms.NumericUpDown numericUpDownPause;
        private System.Windows.Forms.CheckBox checkBoxAutoPauseMusic;
        private System.Windows.Forms.GroupBox groupBoxDirectories;
        private System.Windows.Forms.TextBox tbWarmupDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbBreakDir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBreakDir;
        private System.Windows.Forms.Button btnWarmupDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBoxFSManager;
        private System.Windows.Forms.TextBox tbFSMPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbFSMUsername;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbFSMServerPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbFSMServer;
        private System.Windows.Forms.Label label4;
    }
}