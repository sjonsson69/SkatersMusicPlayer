namespace SkatersMusicPlayer
{
    partial class FormMusicPlayer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMusicPlayer));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("asdfasdf");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("asdfasdf");
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.volumeSlider1 = new NAudio.Gui.VolumeSlider();
            this.volumeMeter1 = new NAudio.Gui.VolumeMeter();
            this.volumeMeter2 = new NAudio.Gui.VolumeMeter();
            this.listViewSkaters = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxSkatersMusic = new System.Windows.Forms.GroupBox();
            this.groupBoxAutoPauseMusic = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelDelay = new System.Windows.Forms.Label();
            this.volumeSliderPause = new NAudio.Gui.VolumeSlider();
            this.numericUpDownPause = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAutoPauseMusic = new System.Windows.Forms.CheckBox();
            this.comboBoxClass = new System.Windows.Forms.ComboBox();
            this.buttonPause = new System.Windows.Forms.Button();
            this.labelCurrentTime = new System.Windows.Forms.Label();
            this.labelTotalTime = new System.Windows.Forms.Label();
            this.trackBarPosition = new System.Windows.Forms.TrackBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCompetitiontoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editClassesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSkatersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.importFromIndTA1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromIndTA2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromClubcompOLDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromISUCalcFSXMLtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromClubcompToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.autoconnectMusicToSkatersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxSoundLevels = new System.Windows.Forms.GroupBox();
            this.groupBoxVolume = new System.Windows.Forms.GroupBox();
            this.groupBoxWarmupMusic = new System.Windows.Forms.GroupBox();
            this.listViewWarmupMusic = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonWarmupStop = new System.Windows.Forms.Button();
            this.buttonWarmupPause = new System.Windows.Forms.Button();
            this.buttonWarmupPlay = new System.Windows.Forms.Button();
            this.groupBoxBreakMusic = new System.Windows.Forms.GroupBox();
            this.listViewBreakMusic = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonBreakStop = new System.Windows.Forms.Button();
            this.buttonBreakPause = new System.Windows.Forms.Button();
            this.buttonBreakPlay = new System.Windows.Forms.Button();
            this.openFileDialogIndTA = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialogCC = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialogISU = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialogISUCalcXML = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxSkatersMusic.SuspendLayout();
            this.groupBoxAutoPauseMusic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPosition)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBoxSoundLevels.SuspendLayout();
            this.groupBoxVolume.SuspendLayout();
            this.groupBoxWarmupMusic.SuspendLayout();
            this.groupBoxBreakMusic.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPlay
            // 
            this.buttonPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonPlay.Enabled = false;
            this.buttonPlay.ForeColor = System.Drawing.Color.White;
            this.buttonPlay.Location = new System.Drawing.Point(8, 376);
            this.buttonPlay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(100, 35);
            this.buttonPlay.TabIndex = 5;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = false;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonStop.Enabled = false;
            this.buttonStop.ForeColor = System.Drawing.Color.White;
            this.buttonStop.Location = new System.Drawing.Point(274, 376);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(100, 35);
            this.buttonStop.TabIndex = 7;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // volumeSlider1
            // 
            this.volumeSlider1.Location = new System.Drawing.Point(7, 27);
            this.volumeSlider1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.volumeSlider1.Name = "volumeSlider1";
            this.volumeSlider1.Size = new System.Drawing.Size(100, 23);
            this.volumeSlider1.TabIndex = 0;
            this.volumeSlider1.TabStop = false;
            this.volumeSlider1.VolumeChanged += new System.EventHandler(this.volumeSlider1_VolumeChanged);
            // 
            // volumeMeter1
            // 
            this.volumeMeter1.Amplitude = 0F;
            this.volumeMeter1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.volumeMeter1.ForeColor = System.Drawing.Color.Lime;
            this.volumeMeter1.Location = new System.Drawing.Point(7, 23);
            this.volumeMeter1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.volumeMeter1.MaxDb = 18F;
            this.volumeMeter1.MinDb = -60F;
            this.volumeMeter1.Name = "volumeMeter1";
            this.volumeMeter1.Size = new System.Drawing.Size(40, 394);
            this.volumeMeter1.TabIndex = 0;
            this.volumeMeter1.TabStop = false;
            this.volumeMeter1.Text = "volumeMeter1";
            // 
            // volumeMeter2
            // 
            this.volumeMeter2.Amplitude = 0F;
            this.volumeMeter2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.volumeMeter2.ForeColor = System.Drawing.Color.Lime;
            this.volumeMeter2.Location = new System.Drawing.Point(67, 23);
            this.volumeMeter2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.volumeMeter2.MaxDb = 18F;
            this.volumeMeter2.MinDb = -60F;
            this.volumeMeter2.Name = "volumeMeter2";
            this.volumeMeter2.Size = new System.Drawing.Size(40, 394);
            this.volumeMeter2.TabIndex = 1;
            this.volumeMeter2.TabStop = false;
            this.volumeMeter2.Text = "volumeMeter2";
            // 
            // listViewSkaters
            // 
            this.listViewSkaters.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewSkaters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewSkaters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader7});
            this.listViewSkaters.FullRowSelect = true;
            this.listViewSkaters.HideSelection = false;
            this.listViewSkaters.Location = new System.Drawing.Point(8, 57);
            this.listViewSkaters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listViewSkaters.MultiSelect = false;
            this.listViewSkaters.Name = "listViewSkaters";
            this.listViewSkaters.ShowItemToolTips = true;
            this.listViewSkaters.Size = new System.Drawing.Size(500, 254);
            this.listViewSkaters.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewSkaters.TabIndex = 1;
            this.listViewSkaters.UseCompatibleStateImageBehavior = false;
            this.listViewSkaters.View = System.Windows.Forms.View.Details;
            this.listViewSkaters.SelectedIndexChanged += new System.EventHandler(this.listViewSkaters_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "No";
            this.columnHeader1.Width = 35;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 71;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Club";
            this.columnHeader3.Width = 55;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Length";
            this.columnHeader4.Width = 73;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Music file";
            this.columnHeader7.Width = 84;
            // 
            // groupBoxSkatersMusic
            // 
            this.groupBoxSkatersMusic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSkatersMusic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBoxSkatersMusic.Controls.Add(this.groupBoxAutoPauseMusic);
            this.groupBoxSkatersMusic.Controls.Add(this.comboBoxClass);
            this.groupBoxSkatersMusic.Controls.Add(this.buttonPause);
            this.groupBoxSkatersMusic.Controls.Add(this.labelCurrentTime);
            this.groupBoxSkatersMusic.Controls.Add(this.labelTotalTime);
            this.groupBoxSkatersMusic.Controls.Add(this.trackBarPosition);
            this.groupBoxSkatersMusic.Controls.Add(this.listViewSkaters);
            this.groupBoxSkatersMusic.Controls.Add(this.buttonStop);
            this.groupBoxSkatersMusic.Controls.Add(this.buttonPlay);
            this.groupBoxSkatersMusic.Location = new System.Drawing.Point(133, 33);
            this.groupBoxSkatersMusic.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxSkatersMusic.Name = "groupBoxSkatersMusic";
            this.groupBoxSkatersMusic.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxSkatersMusic.Size = new System.Drawing.Size(516, 490);
            this.groupBoxSkatersMusic.TabIndex = 3;
            this.groupBoxSkatersMusic.TabStop = false;
            this.groupBoxSkatersMusic.Text = "Skaters music";
            // 
            // groupBoxAutoPauseMusic
            // 
            this.groupBoxAutoPauseMusic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAutoPauseMusic.Controls.Add(this.label1);
            this.groupBoxAutoPauseMusic.Controls.Add(this.labelDelay);
            this.groupBoxAutoPauseMusic.Controls.Add(this.volumeSliderPause);
            this.groupBoxAutoPauseMusic.Controls.Add(this.numericUpDownPause);
            this.groupBoxAutoPauseMusic.Controls.Add(this.checkBoxAutoPauseMusic);
            this.groupBoxAutoPauseMusic.Location = new System.Drawing.Point(8, 420);
            this.groupBoxAutoPauseMusic.Name = "groupBoxAutoPauseMusic";
            this.groupBoxAutoPauseMusic.Size = new System.Drawing.Size(500, 62);
            this.groupBoxAutoPauseMusic.TabIndex = 8;
            this.groupBoxAutoPauseMusic.TabStop = false;
            this.groupBoxAutoPauseMusic.Text = "Automatic pause music (replay skaters music)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(382, 13);
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
            this.volumeSliderPause.Location = new System.Drawing.Point(386, 33);
            this.volumeSliderPause.Name = "volumeSliderPause";
            this.volumeSliderPause.Size = new System.Drawing.Size(108, 23);
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
            this.checkBoxAutoPauseMusic.Location = new System.Drawing.Point(6, 25);
            this.checkBoxAutoPauseMusic.Name = "checkBoxAutoPauseMusic";
            this.checkBoxAutoPauseMusic.Size = new System.Drawing.Size(87, 24);
            this.checkBoxAutoPauseMusic.TabIndex = 0;
            this.checkBoxAutoPauseMusic.Text = "Enabled";
            this.checkBoxAutoPauseMusic.UseVisualStyleBackColor = true;
            // 
            // comboBoxClass
            // 
            this.comboBoxClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClass.FormattingEnabled = true;
            this.comboBoxClass.Items.AddRange(new object[] {
            "Ungdom 13 Flickor Short Program",
            "Juniorer Damer Short Program",
            "Seniorer Herrar Short Program",
            "Ungdom 13 Flickor Free Skating",
            "",
            "",
            ""});
            this.comboBoxClass.Location = new System.Drawing.Point(8, 21);
            this.comboBoxClass.Name = "comboBoxClass";
            this.comboBoxClass.Size = new System.Drawing.Size(500, 28);
            this.comboBoxClass.TabIndex = 0;
            this.comboBoxClass.SelectedIndexChanged += new System.EventHandler(this.comboBoxClass_SelectedIndexChanged);
            // 
            // buttonPause
            // 
            this.buttonPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonPause.Enabled = false;
            this.buttonPause.ForeColor = System.Drawing.Color.White;
            this.buttonPause.Location = new System.Drawing.Point(142, 376);
            this.buttonPause.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(100, 35);
            this.buttonPause.TabIndex = 6;
            this.buttonPause.Text = "Pause";
            this.buttonPause.UseVisualStyleBackColor = false;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // labelCurrentTime
            // 
            this.labelCurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCurrentTime.AutoSize = true;
            this.labelCurrentTime.Location = new System.Drawing.Point(7, 351);
            this.labelCurrentTime.Name = "labelCurrentTime";
            this.labelCurrentTime.Size = new System.Drawing.Size(129, 20);
            this.labelCurrentTime.TabIndex = 3;
            this.labelCurrentTime.Text = "labelCurrentTime";
            // 
            // labelTotalTime
            // 
            this.labelTotalTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTotalTime.Location = new System.Drawing.Point(362, 351);
            this.labelTotalTime.Name = "labelTotalTime";
            this.labelTotalTime.Size = new System.Drawing.Size(146, 20);
            this.labelTotalTime.TabIndex = 4;
            this.labelTotalTime.Text = "labelTotalTime";
            this.labelTotalTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // trackBarPosition
            // 
            this.trackBarPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarPosition.LargeChange = 10;
            this.trackBarPosition.Location = new System.Drawing.Point(8, 319);
            this.trackBarPosition.Maximum = 100;
            this.trackBarPosition.Name = "trackBarPosition";
            this.trackBarPosition.Size = new System.Drawing.Size(500, 45);
            this.trackBarPosition.TabIndex = 2;
            this.trackBarPosition.TabStop = false;
            this.trackBarPosition.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarPosition.Scroll += new System.EventHandler(this.trackBarPosition_Scroll);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Enabled = false;
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(159, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editCompetitiontoolStripMenuItem,
            this.editClassesMenuItem,
            this.editSkatersToolStripMenuItem,
            this.toolStripSeparator1,
            this.importFromIndTA1ToolStripMenuItem,
            this.importFromIndTA2ToolStripMenuItem,
            this.importFromClubcompOLDToolStripMenuItem,
            this.importFromISUCalcFSXMLtoolStripMenuItem,
            this.importFromClubcompToolStripMenuItem,
            this.toolStripSeparator2,
            this.autoconnectMusicToSkatersToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // editCompetitiontoolStripMenuItem
            // 
            this.editCompetitiontoolStripMenuItem.Name = "editCompetitiontoolStripMenuItem";
            this.editCompetitiontoolStripMenuItem.Size = new System.Drawing.Size(354, 24);
            this.editCompetitiontoolStripMenuItem.Text = "Edit Com&petition";
            this.editCompetitiontoolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // editClassesMenuItem
            // 
            this.editClassesMenuItem.Name = "editClassesMenuItem";
            this.editClassesMenuItem.Size = new System.Drawing.Size(354, 24);
            this.editClassesMenuItem.Text = "Edit &Classes";
            this.editClassesMenuItem.Click += new System.EventHandler(this.editClassesMenuItem_Click);
            // 
            // editSkatersToolStripMenuItem
            // 
            this.editSkatersToolStripMenuItem.Name = "editSkatersToolStripMenuItem";
            this.editSkatersToolStripMenuItem.Size = new System.Drawing.Size(354, 24);
            this.editSkatersToolStripMenuItem.Text = "Edit &Skaters";
            this.editSkatersToolStripMenuItem.Click += new System.EventHandler(this.editSkatersToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(351, 6);
            // 
            // importFromIndTA1ToolStripMenuItem
            // 
            this.importFromIndTA1ToolStripMenuItem.Enabled = false;
            this.importFromIndTA1ToolStripMenuItem.Name = "importFromIndTA1ToolStripMenuItem";
            this.importFromIndTA1ToolStripMenuItem.Size = new System.Drawing.Size(354, 24);
            this.importFromIndTA1ToolStripMenuItem.Text = "Import from &IndTA 1.0";
            this.importFromIndTA1ToolStripMenuItem.Visible = false;
            this.importFromIndTA1ToolStripMenuItem.Click += new System.EventHandler(this.importFromIndTAToolStripMenuItem_Click);
            // 
            // importFromIndTA2ToolStripMenuItem
            // 
            this.importFromIndTA2ToolStripMenuItem.Name = "importFromIndTA2ToolStripMenuItem";
            this.importFromIndTA2ToolStripMenuItem.Size = new System.Drawing.Size(354, 24);
            this.importFromIndTA2ToolStripMenuItem.Text = "Import from &IndTA 2.0";
            this.importFromIndTA2ToolStripMenuItem.Click += new System.EventHandler(this.importFromIndTA2ToolStripMenuItem_Click);
            // 
            // importFromClubcompOLDToolStripMenuItem
            // 
            this.importFromClubcompOLDToolStripMenuItem.Enabled = false;
            this.importFromClubcompOLDToolStripMenuItem.Name = "importFromClubcompOLDToolStripMenuItem";
            this.importFromClubcompOLDToolStripMenuItem.Size = new System.Drawing.Size(354, 24);
            this.importFromClubcompOLDToolStripMenuItem.Text = "OLD-Import from C&lubcomp / Starcomp";
            this.importFromClubcompOLDToolStripMenuItem.Visible = false;
            this.importFromClubcompOLDToolStripMenuItem.Click += new System.EventHandler(this.importFromClubcompOldToolStripMenuItem_Click);
            // 
            // importFromISUCalcFSXMLtoolStripMenuItem
            // 
            this.importFromISUCalcFSXMLtoolStripMenuItem.Name = "importFromISUCalcFSXMLtoolStripMenuItem";
            this.importFromISUCalcFSXMLtoolStripMenuItem.Size = new System.Drawing.Size(354, 24);
            this.importFromISUCalcFSXMLtoolStripMenuItem.Text = "Import from ISUCalcFS &XML";
            this.importFromISUCalcFSXMLtoolStripMenuItem.Click += new System.EventHandler(this.ImportFromISUCalcFSXMLtoolStripMenuItem_Click);
            // 
            // importFromClubcompToolStripMenuItem
            // 
            this.importFromClubcompToolStripMenuItem.Name = "importFromClubcompToolStripMenuItem";
            this.importFromClubcompToolStripMenuItem.Size = new System.Drawing.Size(354, 24);
            this.importFromClubcompToolStripMenuItem.Text = "Import from S&tarcomp";
            this.importFromClubcompToolStripMenuItem.Click += new System.EventHandler(this.importFromClubcomp2016ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(351, 6);
            // 
            // autoconnectMusicToSkatersToolStripMenuItem
            // 
            this.autoconnectMusicToSkatersToolStripMenuItem.Name = "autoconnectMusicToSkatersToolStripMenuItem";
            this.autoconnectMusicToSkatersToolStripMenuItem.Size = new System.Drawing.Size(354, 24);
            this.autoconnectMusicToSkatersToolStripMenuItem.Text = "Autoconnect music to skaters";
            this.autoconnectMusicToSkatersToolStripMenuItem.Click += new System.EventHandler(this.autoconnectMusicToSkatersToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(54, 24);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(133, 24);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1264, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(112, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel";
            // 
            // groupBoxSoundLevels
            // 
            this.groupBoxSoundLevels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxSoundLevels.Controls.Add(this.volumeMeter1);
            this.groupBoxSoundLevels.Controls.Add(this.volumeMeter2);
            this.groupBoxSoundLevels.Location = new System.Drawing.Point(12, 31);
            this.groupBoxSoundLevels.Name = "groupBoxSoundLevels";
            this.groupBoxSoundLevels.Size = new System.Drawing.Size(114, 425);
            this.groupBoxSoundLevels.TabIndex = 1;
            this.groupBoxSoundLevels.TabStop = false;
            this.groupBoxSoundLevels.Text = "Soundlevels";
            // 
            // groupBoxVolume
            // 
            this.groupBoxVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxVolume.Controls.Add(this.volumeSlider1);
            this.groupBoxVolume.Location = new System.Drawing.Point(12, 462);
            this.groupBoxVolume.Name = "groupBoxVolume";
            this.groupBoxVolume.Size = new System.Drawing.Size(114, 61);
            this.groupBoxVolume.TabIndex = 2;
            this.groupBoxVolume.TabStop = false;
            this.groupBoxVolume.Text = "Volume";
            // 
            // groupBoxWarmupMusic
            // 
            this.groupBoxWarmupMusic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxWarmupMusic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBoxWarmupMusic.Controls.Add(this.listViewWarmupMusic);
            this.groupBoxWarmupMusic.Controls.Add(this.buttonWarmupStop);
            this.groupBoxWarmupMusic.Controls.Add(this.buttonWarmupPause);
            this.groupBoxWarmupMusic.Controls.Add(this.buttonWarmupPlay);
            this.groupBoxWarmupMusic.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxWarmupMusic.Location = new System.Drawing.Point(656, 33);
            this.groupBoxWarmupMusic.Name = "groupBoxWarmupMusic";
            this.groupBoxWarmupMusic.Size = new System.Drawing.Size(294, 490);
            this.groupBoxWarmupMusic.TabIndex = 4;
            this.groupBoxWarmupMusic.TabStop = false;
            this.groupBoxWarmupMusic.Text = "Warmup music";
            // 
            // listViewWarmupMusic
            // 
            this.listViewWarmupMusic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewWarmupMusic.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5});
            this.listViewWarmupMusic.HideSelection = false;
            this.listViewWarmupMusic.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listViewWarmupMusic.Location = new System.Drawing.Point(7, 21);
            this.listViewWarmupMusic.MultiSelect = false;
            this.listViewWarmupMusic.Name = "listViewWarmupMusic";
            this.listViewWarmupMusic.ShowItemToolTips = true;
            this.listViewWarmupMusic.Size = new System.Drawing.Size(280, 418);
            this.listViewWarmupMusic.TabIndex = 2;
            this.listViewWarmupMusic.UseCompatibleStateImageBehavior = false;
            this.listViewWarmupMusic.View = System.Windows.Forms.View.Details;
            this.listViewWarmupMusic.SelectedIndexChanged += new System.EventHandler(this.listViewWarmupMusic_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Music";
            this.columnHeader5.Width = 231;
            // 
            // buttonWarmupStop
            // 
            this.buttonWarmupStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonWarmupStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonWarmupStop.Enabled = false;
            this.buttonWarmupStop.ForeColor = System.Drawing.Color.White;
            this.buttonWarmupStop.Location = new System.Drawing.Point(211, 447);
            this.buttonWarmupStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonWarmupStop.Name = "buttonWarmupStop";
            this.buttonWarmupStop.Size = new System.Drawing.Size(76, 35);
            this.buttonWarmupStop.TabIndex = 5;
            this.buttonWarmupStop.Text = "Stop";
            this.buttonWarmupStop.UseVisualStyleBackColor = false;
            this.buttonWarmupStop.Click += new System.EventHandler(this.buttonWarmupStop_Click);
            // 
            // buttonWarmupPause
            // 
            this.buttonWarmupPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonWarmupPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonWarmupPause.Enabled = false;
            this.buttonWarmupPause.ForeColor = System.Drawing.Color.White;
            this.buttonWarmupPause.Location = new System.Drawing.Point(111, 447);
            this.buttonWarmupPause.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonWarmupPause.Name = "buttonWarmupPause";
            this.buttonWarmupPause.Size = new System.Drawing.Size(76, 35);
            this.buttonWarmupPause.TabIndex = 4;
            this.buttonWarmupPause.Text = "Pause";
            this.buttonWarmupPause.UseVisualStyleBackColor = false;
            this.buttonWarmupPause.Click += new System.EventHandler(this.buttonWarmupPause_Click);
            // 
            // buttonWarmupPlay
            // 
            this.buttonWarmupPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonWarmupPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonWarmupPlay.Enabled = false;
            this.buttonWarmupPlay.ForeColor = System.Drawing.Color.White;
            this.buttonWarmupPlay.Location = new System.Drawing.Point(7, 447);
            this.buttonWarmupPlay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonWarmupPlay.Name = "buttonWarmupPlay";
            this.buttonWarmupPlay.Size = new System.Drawing.Size(76, 35);
            this.buttonWarmupPlay.TabIndex = 3;
            this.buttonWarmupPlay.Text = "Play";
            this.buttonWarmupPlay.UseVisualStyleBackColor = false;
            this.buttonWarmupPlay.Click += new System.EventHandler(this.buttonWarmupPlay_Click);
            // 
            // groupBoxBreakMusic
            // 
            this.groupBoxBreakMusic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxBreakMusic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBoxBreakMusic.Controls.Add(this.listViewBreakMusic);
            this.groupBoxBreakMusic.Controls.Add(this.buttonBreakStop);
            this.groupBoxBreakMusic.Controls.Add(this.buttonBreakPause);
            this.groupBoxBreakMusic.Controls.Add(this.buttonBreakPlay);
            this.groupBoxBreakMusic.Location = new System.Drawing.Point(956, 31);
            this.groupBoxBreakMusic.Name = "groupBoxBreakMusic";
            this.groupBoxBreakMusic.Size = new System.Drawing.Size(294, 492);
            this.groupBoxBreakMusic.TabIndex = 5;
            this.groupBoxBreakMusic.TabStop = false;
            this.groupBoxBreakMusic.Text = "Break music";
            // 
            // listViewBreakMusic
            // 
            this.listViewBreakMusic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewBreakMusic.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6});
            this.listViewBreakMusic.HideSelection = false;
            this.listViewBreakMusic.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.listViewBreakMusic.Location = new System.Drawing.Point(7, 23);
            this.listViewBreakMusic.MultiSelect = false;
            this.listViewBreakMusic.Name = "listViewBreakMusic";
            this.listViewBreakMusic.ShowItemToolTips = true;
            this.listViewBreakMusic.Size = new System.Drawing.Size(280, 418);
            this.listViewBreakMusic.TabIndex = 2;
            this.listViewBreakMusic.UseCompatibleStateImageBehavior = false;
            this.listViewBreakMusic.View = System.Windows.Forms.View.Details;
            this.listViewBreakMusic.SelectedIndexChanged += new System.EventHandler(this.listViewBreakMusic_SelectedIndexChanged);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Music";
            this.columnHeader6.Width = 231;
            // 
            // buttonBreakStop
            // 
            this.buttonBreakStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonBreakStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonBreakStop.Enabled = false;
            this.buttonBreakStop.ForeColor = System.Drawing.Color.White;
            this.buttonBreakStop.Location = new System.Drawing.Point(211, 449);
            this.buttonBreakStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonBreakStop.Name = "buttonBreakStop";
            this.buttonBreakStop.Size = new System.Drawing.Size(76, 35);
            this.buttonBreakStop.TabIndex = 5;
            this.buttonBreakStop.Text = "Stop";
            this.buttonBreakStop.UseVisualStyleBackColor = false;
            this.buttonBreakStop.Click += new System.EventHandler(this.buttonBreakStop_Click);
            // 
            // buttonBreakPause
            // 
            this.buttonBreakPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonBreakPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonBreakPause.Enabled = false;
            this.buttonBreakPause.ForeColor = System.Drawing.Color.White;
            this.buttonBreakPause.Location = new System.Drawing.Point(111, 449);
            this.buttonBreakPause.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonBreakPause.Name = "buttonBreakPause";
            this.buttonBreakPause.Size = new System.Drawing.Size(76, 35);
            this.buttonBreakPause.TabIndex = 4;
            this.buttonBreakPause.Text = "Pause";
            this.buttonBreakPause.UseVisualStyleBackColor = false;
            this.buttonBreakPause.Click += new System.EventHandler(this.buttonBreakPause_Click);
            // 
            // buttonBreakPlay
            // 
            this.buttonBreakPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonBreakPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonBreakPlay.Enabled = false;
            this.buttonBreakPlay.ForeColor = System.Drawing.Color.White;
            this.buttonBreakPlay.Location = new System.Drawing.Point(7, 449);
            this.buttonBreakPlay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonBreakPlay.Name = "buttonBreakPlay";
            this.buttonBreakPlay.Size = new System.Drawing.Size(76, 35);
            this.buttonBreakPlay.TabIndex = 3;
            this.buttonBreakPlay.Text = "Play";
            this.buttonBreakPlay.UseVisualStyleBackColor = false;
            this.buttonBreakPlay.Click += new System.EventHandler(this.buttonBreakPlay_Click);
            // 
            // openFileDialogIndTA
            // 
            this.openFileDialogIndTA.Filter = "Anmälningslista|*.xml|All files|*.*";
            this.openFileDialogIndTA.RestoreDirectory = true;
            // 
            // folderBrowserDialogCC
            // 
            this.folderBrowserDialogCC.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialogCC.SelectedPath = "C:\\Competition";
            this.folderBrowserDialogCC.ShowNewFolderButton = false;
            // 
            // folderBrowserDialogISU
            // 
            this.folderBrowserDialogISU.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialogISU.SelectedPath = "C:\\ISUCalcFS\\";
            this.folderBrowserDialogISU.ShowNewFolderButton = false;
            // 
            // openFileDialogISUCalcXML
            // 
            this.openFileDialogISUCalcXML.Filter = "ISC Exportfil|*.xml|All files|*.*";
            this.openFileDialogISUCalcXML.RestoreDirectory = true;
            // 
            // FormMusicPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1264, 561);
            this.Controls.Add(this.groupBoxBreakMusic);
            this.Controls.Add(this.groupBoxWarmupMusic);
            this.Controls.Add(this.groupBoxVolume);
            this.Controls.Add(this.groupBoxSoundLevels);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBoxSkatersMusic);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMusicPlayer";
            this.Text = "Skaters MusicPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMusicPlayer_FormClosing);
            this.groupBoxSkatersMusic.ResumeLayout(false);
            this.groupBoxSkatersMusic.PerformLayout();
            this.groupBoxAutoPauseMusic.ResumeLayout(false);
            this.groupBoxAutoPauseMusic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPosition)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxSoundLevels.ResumeLayout(false);
            this.groupBoxVolume.ResumeLayout(false);
            this.groupBoxWarmupMusic.ResumeLayout(false);
            this.groupBoxBreakMusic.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonStop;
        private NAudio.Gui.VolumeSlider volumeSlider1;
        private NAudio.Gui.VolumeMeter volumeMeter1;
        private NAudio.Gui.VolumeMeter volumeMeter2;
        private System.Windows.Forms.ListView listViewSkaters;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox groupBoxSkatersMusic;
        private System.Windows.Forms.TrackBar trackBarPosition;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelCurrentTime;
        private System.Windows.Forms.Label labelTotalTime;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.ComboBox comboBoxClass;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupBoxAutoPauseMusic;
        private NAudio.Gui.VolumeSlider volumeSliderPause;
        private System.Windows.Forms.NumericUpDown numericUpDownPause;
        private System.Windows.Forms.CheckBox checkBoxAutoPauseMusic;
        private System.Windows.Forms.Label labelDelay;
        private System.Windows.Forms.GroupBox groupBoxSoundLevels;
        private System.Windows.Forms.GroupBox groupBoxVolume;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxWarmupMusic;
        private System.Windows.Forms.Button buttonWarmupStop;
        private System.Windows.Forms.Button buttonWarmupPause;
        private System.Windows.Forms.Button buttonWarmupPlay;
        private System.Windows.Forms.GroupBox groupBoxBreakMusic;
        private System.Windows.Forms.Button buttonBreakStop;
        private System.Windows.Forms.Button buttonBreakPause;
        private System.Windows.Forms.Button buttonBreakPlay;
        private System.Windows.Forms.ListView listViewWarmupMusic;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ListView listViewBreakMusic;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ToolStripMenuItem editClassesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSkatersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCompetitiontoolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem importFromIndTA1ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogIndTA;
        private System.Windows.Forms.ToolStripMenuItem importFromClubcompOLDToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem autoconnectMusicToSkatersToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogCC;
        private System.Windows.Forms.ToolStripMenuItem importFromIndTA2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFromClubcompToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogISU;
        private System.Windows.Forms.ToolStripMenuItem importFromISUCalcFSXMLtoolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogISUCalcXML;
    }
}

