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
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("asdfasdf");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("asdfasdf");
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.volumeSlider1 = new NAudio.Gui.VolumeSlider();
            this.volumeMeter1 = new NAudio.Gui.VolumeMeter();
            this.volumeMeter2 = new NAudio.Gui.VolumeMeter();
            this.listViewParticipants = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxParticipantMusic = new System.Windows.Forms.GroupBox();
            this.groupBoxAutoPauseMusic = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelDelay = new System.Windows.Forms.Label();
            this.volumeSliderPause = new NAudio.Gui.VolumeSlider();
            this.numericUpDownPause = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAutoPauseMusic = new System.Windows.Forms.CheckBox();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
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
            this.editEventtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCategoriesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editParticipantsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromIndTA2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromISUCalcFSXMLtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromStarFSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.UnzipMusicfiletoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoconnectMusicToParticipantsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.openFileDialogISUCalcXML = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogMusicarchive = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogStarFS = new System.Windows.Forms.OpenFileDialog();
            this.fSManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxParticipantMusic.SuspendLayout();
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
            // listViewParticipants
            // 
            this.listViewParticipants.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewParticipants.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewParticipants.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader7,
            this.columnHeader8});
            this.listViewParticipants.FullRowSelect = true;
            this.listViewParticipants.HideSelection = false;
            this.listViewParticipants.Location = new System.Drawing.Point(8, 57);
            this.listViewParticipants.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listViewParticipants.MultiSelect = false;
            this.listViewParticipants.Name = "listViewParticipants";
            this.listViewParticipants.ShowItemToolTips = true;
            this.listViewParticipants.Size = new System.Drawing.Size(500, 254);
            this.listViewParticipants.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewParticipants.TabIndex = 1;
            this.listViewParticipants.UseCompatibleStateImageBehavior = false;
            this.listViewParticipants.View = System.Windows.Forms.View.Details;
            this.listViewParticipants.SelectedIndexChanged += new System.EventHandler(this.listViewParticipants_SelectedIndexChanged);
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
            this.columnHeader4.DisplayIndex = 4;
            this.columnHeader4.Text = "Length";
            this.columnHeader4.Width = 73;
            // 
            // columnHeader7
            // 
            this.columnHeader7.DisplayIndex = 5;
            this.columnHeader7.Text = "Music file";
            this.columnHeader7.Width = 84;
            // 
            // columnHeader8
            // 
            this.columnHeader8.DisplayIndex = 3;
            this.columnHeader8.Text = "Music title";
            this.columnHeader8.Width = 90;
            // 
            // groupBoxParticipantMusic
            // 
            this.groupBoxParticipantMusic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxParticipantMusic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBoxParticipantMusic.Controls.Add(this.groupBoxAutoPauseMusic);
            this.groupBoxParticipantMusic.Controls.Add(this.comboBoxCategory);
            this.groupBoxParticipantMusic.Controls.Add(this.buttonPause);
            this.groupBoxParticipantMusic.Controls.Add(this.labelCurrentTime);
            this.groupBoxParticipantMusic.Controls.Add(this.labelTotalTime);
            this.groupBoxParticipantMusic.Controls.Add(this.trackBarPosition);
            this.groupBoxParticipantMusic.Controls.Add(this.listViewParticipants);
            this.groupBoxParticipantMusic.Controls.Add(this.buttonStop);
            this.groupBoxParticipantMusic.Controls.Add(this.buttonPlay);
            this.groupBoxParticipantMusic.Location = new System.Drawing.Point(133, 33);
            this.groupBoxParticipantMusic.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxParticipantMusic.Name = "groupBoxParticipantMusic";
            this.groupBoxParticipantMusic.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxParticipantMusic.Size = new System.Drawing.Size(516, 490);
            this.groupBoxParticipantMusic.TabIndex = 3;
            this.groupBoxParticipantMusic.TabStop = false;
            this.groupBoxParticipantMusic.Text = "Participant music";
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
            this.groupBoxAutoPauseMusic.Text = "Automatic pause music (replay participants music)";
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
            // comboBoxCategory
            // 
            this.comboBoxCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(8, 21);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(500, 28);
            this.comboBoxCategory.TabIndex = 0;
            this.comboBoxCategory.SelectedIndexChanged += new System.EventHandler(this.comboBoxCategory_SelectedIndexChanged);
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
            this.importToolStripMenuItem,
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
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
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
            this.editEventtoolStripMenuItem,
            this.editCategoriesMenuItem,
            this.editParticipantsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // editEventtoolStripMenuItem
            // 
            this.editEventtoolStripMenuItem.Name = "editEventtoolStripMenuItem";
            this.editEventtoolStripMenuItem.Size = new System.Drawing.Size(161, 24);
            this.editEventtoolStripMenuItem.Text = "&Event";
            this.editEventtoolStripMenuItem.Click += new System.EventHandler(this.editEventMenuItem_Click);
            // 
            // editCategoriesMenuItem
            // 
            this.editCategoriesMenuItem.Name = "editCategoriesMenuItem";
            this.editCategoriesMenuItem.Size = new System.Drawing.Size(161, 24);
            this.editCategoriesMenuItem.Text = "&Categories";
            this.editCategoriesMenuItem.Click += new System.EventHandler(this.editCategoriesMenuItem_Click);
            // 
            // editParticipantsToolStripMenuItem
            // 
            this.editParticipantsToolStripMenuItem.Name = "editParticipantsToolStripMenuItem";
            this.editParticipantsToolStripMenuItem.Size = new System.Drawing.Size(161, 24);
            this.editParticipantsToolStripMenuItem.Text = "&Participants";
            this.editParticipantsToolStripMenuItem.Click += new System.EventHandler(this.editParticipantsToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importFromIndTA2ToolStripMenuItem,
            this.fSManagerToolStripMenuItem,
            this.importFromStarFSToolStripMenuItem,
            this.toolStripSeparator3,
            this.UnzipMusicfiletoolStripMenuItem,
            this.autoconnectMusicToParticipantsToolStripMenuItem,
            this.importFromISUCalcFSXMLtoolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.importToolStripMenuItem.Text = "&Import";
            // 
            // importFromIndTA2ToolStripMenuItem
            // 
            this.importFromIndTA2ToolStripMenuItem.Name = "importFromIndTA2ToolStripMenuItem";
            this.importFromIndTA2ToolStripMenuItem.Size = new System.Drawing.Size(318, 24);
            this.importFromIndTA2ToolStripMenuItem.Text = "&IndTA 2.0";
            this.importFromIndTA2ToolStripMenuItem.Click += new System.EventHandler(this.importFromIndTA2ToolStripMenuItem_Click);
            // 
            // importFromISUCalcFSXMLtoolStripMenuItem
            // 
            this.importFromISUCalcFSXMLtoolStripMenuItem.Name = "importFromISUCalcFSXMLtoolStripMenuItem";
            this.importFromISUCalcFSXMLtoolStripMenuItem.Size = new System.Drawing.Size(318, 24);
            this.importFromISUCalcFSXMLtoolStripMenuItem.Text = "ISUCalcFS &XML";
            this.importFromISUCalcFSXMLtoolStripMenuItem.Click += new System.EventHandler(this.importFromISUCalcFSXMLtoolStripMenuItem_Click);
            // 
            // importFromStarFSToolStripMenuItem
            // 
            this.importFromStarFSToolStripMenuItem.Name = "importFromStarFSToolStripMenuItem";
            this.importFromStarFSToolStripMenuItem.Size = new System.Drawing.Size(318, 24);
            this.importFromStarFSToolStripMenuItem.Text = "&StarFS";
            this.importFromStarFSToolStripMenuItem.Click += new System.EventHandler(this.importFromStarFSToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(315, 6);
            // 
            // UnzipMusicfiletoolStripMenuItem
            // 
            this.UnzipMusicfiletoolStripMenuItem.Name = "UnzipMusicfiletoolStripMenuItem";
            this.UnzipMusicfiletoolStripMenuItem.Size = new System.Drawing.Size(318, 24);
            this.UnzipMusicfiletoolStripMenuItem.Text = "&Unzip musicfile from IndTA";
            this.UnzipMusicfiletoolStripMenuItem.Click += new System.EventHandler(this.unzipMusicfiletoolStripMenuItem_Click);
            // 
            // autoconnectMusicToParticipantsToolStripMenuItem
            // 
            this.autoconnectMusicToParticipantsToolStripMenuItem.Name = "autoconnectMusicToParticipantsToolStripMenuItem";
            this.autoconnectMusicToParticipantsToolStripMenuItem.Size = new System.Drawing.Size(318, 24);
            this.autoconnectMusicToParticipantsToolStripMenuItem.Text = "&Autoconnect music to participants";
            this.autoconnectMusicToParticipantsToolStripMenuItem.Click += new System.EventHandler(this.autoconnectMusicToParticipantsToolStripMenuItem_Click);
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
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(133, 24);
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
            listViewItem3});
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
            listViewItem4});
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
            // openFileDialogISUCalcXML
            // 
            this.openFileDialogISUCalcXML.Filter = "ISU Exportfil|*.xml|All files|*.*";
            this.openFileDialogISUCalcXML.RestoreDirectory = true;
            // 
            // openFileDialogMusicarchive
            // 
            this.openFileDialogMusicarchive.Filter = "Musicfiles|musikfil*.zip|zip-files|*.zip|All files|*.*";
            this.openFileDialogMusicarchive.ReadOnlyChecked = true;
            this.openFileDialogMusicarchive.RestoreDirectory = true;
            // 
            // openFileDialogStarFS
            // 
            this.openFileDialogStarFS.Filter = "StarFS competitiondatabase|*.db|All files|*.*";
            this.openFileDialogStarFS.RestoreDirectory = true;
            // 
            // fSManagerToolStripMenuItem
            // 
            this.fSManagerToolStripMenuItem.Name = "fSManagerToolStripMenuItem";
            this.fSManagerToolStripMenuItem.Size = new System.Drawing.Size(318, 24);
            this.fSManagerToolStripMenuItem.Text = "&FS Manager";
            this.fSManagerToolStripMenuItem.Click += new System.EventHandler(this.fSManagerToolStripMenuItem_Click);
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
            this.Controls.Add(this.groupBoxParticipantMusic);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMusicPlayer";
            this.Text = "Skaters MusicPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formMusicPlayer_FormClosing);
            this.groupBoxParticipantMusic.ResumeLayout(false);
            this.groupBoxParticipantMusic.PerformLayout();
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
        private System.Windows.Forms.ListView listViewParticipants;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox groupBoxParticipantMusic;
        private System.Windows.Forms.TrackBar trackBarPosition;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelCurrentTime;
        private System.Windows.Forms.Label labelTotalTime;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.ComboBox comboBoxCategory;
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
        private System.Windows.Forms.ToolStripMenuItem editCategoriesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editParticipantsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editEventtoolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogIndTA;
        private System.Windows.Forms.OpenFileDialog openFileDialogISUCalcXML;
        private System.Windows.Forms.OpenFileDialog openFileDialogMusicarchive;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFromIndTA2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFromISUCalcFSXMLtoolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem UnzipMusicfiletoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoconnectMusicToParticipantsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFromStarFSToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogStarFS;
        private System.Windows.Forms.ToolStripMenuItem fSManagerToolStripMenuItem;
    }
}

