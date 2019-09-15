namespace SkatersMusicPlayer
{
    partial class FormEditParticipants
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.dataGridViewParticipants = new System.Windows.Forms.DataGridView();
            this.ColumnStartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnClub = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MusicTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSelectFile = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMusic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnChecksum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStartNoSecond = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMusicFileSecond = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnChecksumSecond = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MusicTitleSecond = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParticipants)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(12, 12);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(984, 28);
            this.comboBoxCategory.TabIndex = 0;
            this.comboBoxCategory.SelectedIndexChanged += new System.EventHandler(this.comboBoxCategory_SelectedIndexChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(906, 560);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(90, 29);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "&Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(12, 560);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 29);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // dataGridViewParticipants
            // 
            this.dataGridViewParticipants.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewParticipants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewParticipants.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnStartNo,
            this.ColumnFName,
            this.ColumnLName,
            this.ColumnClub,
            this.ColumnID,
            this.ColumnBirthDate,
            this.MusicTitle,
            this.ColumnSelectFile,
            this.ColumnLength,
            this.ColumnMusic,
            this.ColumnChecksum,
            this.ColumnStartNoSecond,
            this.ColumnMusicFileSecond,
            this.ColumnChecksumSecond,
            this.MusicTitleSecond});
            this.dataGridViewParticipants.Location = new System.Drawing.Point(12, 47);
            this.dataGridViewParticipants.MultiSelect = false;
            this.dataGridViewParticipants.Name = "dataGridViewParticipants";
            this.dataGridViewParticipants.Size = new System.Drawing.Size(984, 507);
            this.dataGridViewParticipants.TabIndex = 1;
            this.dataGridViewParticipants.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewParticipants_CellContentClick);
            this.dataGridViewParticipants.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView1_UserDeletingRow);
            // 
            // ColumnStartNo
            // 
            this.ColumnStartNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnStartNo.HeaderText = "Start#";
            this.ColumnStartNo.Name = "ColumnStartNo";
            this.ColumnStartNo.Width = 78;
            // 
            // ColumnFName
            // 
            this.ColumnFName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnFName.HeaderText = "Firstname";
            this.ColumnFName.Name = "ColumnFName";
            this.ColumnFName.Width = 105;
            // 
            // ColumnLName
            // 
            this.ColumnLName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnLName.HeaderText = "Lastname";
            this.ColumnLName.Name = "ColumnLName";
            this.ColumnLName.Width = 105;
            // 
            // ColumnClub
            // 
            this.ColumnClub.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnClub.HeaderText = "Club";
            this.ColumnClub.Name = "ColumnClub";
            this.ColumnClub.Width = 66;
            // 
            // ColumnID
            // 
            this.ColumnID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ColumnID.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnID.HeaderText = "ID#";
            this.ColumnID.Name = "ColumnID";
            this.ColumnID.ReadOnly = true;
            this.ColumnID.Visible = false;
            // 
            // ColumnBirthDate
            // 
            this.ColumnBirthDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnBirthDate.HeaderText = "BirthDate";
            this.ColumnBirthDate.Name = "ColumnBirthDate";
            this.ColumnBirthDate.ReadOnly = true;
            this.ColumnBirthDate.Visible = false;
            // 
            // MusicTitle
            // 
            this.MusicTitle.HeaderText = "Music title";
            this.MusicTitle.Name = "MusicTitle";
            this.MusicTitle.Width = 125;
            // 
            // ColumnSelectFile
            // 
            this.ColumnSelectFile.HeaderText = "File";
            this.ColumnSelectFile.Name = "ColumnSelectFile";
            this.ColumnSelectFile.Text = "...";
            this.ColumnSelectFile.Width = 50;
            // 
            // ColumnLength
            // 
            this.ColumnLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ColumnLength.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnLength.HeaderText = "Length";
            this.ColumnLength.Name = "ColumnLength";
            this.ColumnLength.ReadOnly = true;
            this.ColumnLength.Width = 84;
            // 
            // ColumnMusic
            // 
            this.ColumnMusic.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnMusic.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnMusic.HeaderText = "Music file";
            this.ColumnMusic.Name = "ColumnMusic";
            this.ColumnMusic.ReadOnly = true;
            this.ColumnMusic.Width = 99;
            // 
            // ColumnChecksum
            // 
            this.ColumnChecksum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnChecksum.HeaderText = "Checksum";
            this.ColumnChecksum.Name = "ColumnChecksum";
            this.ColumnChecksum.ReadOnly = true;
            this.ColumnChecksum.Visible = false;
            // 
            // ColumnStartNoSecond
            // 
            this.ColumnStartNoSecond.HeaderText = "StartNoSecond";
            this.ColumnStartNoSecond.Name = "ColumnStartNoSecond";
            this.ColumnStartNoSecond.ReadOnly = true;
            this.ColumnStartNoSecond.Visible = false;
            // 
            // ColumnMusicFileSecond
            // 
            this.ColumnMusicFileSecond.HeaderText = "MusicFileSecond";
            this.ColumnMusicFileSecond.Name = "ColumnMusicFileSecond";
            this.ColumnMusicFileSecond.ReadOnly = true;
            this.ColumnMusicFileSecond.Visible = false;
            // 
            // ColumnChecksumSecond
            // 
            this.ColumnChecksumSecond.HeaderText = "ChecksumSecond";
            this.ColumnChecksumSecond.Name = "ColumnChecksumSecond";
            this.ColumnChecksumSecond.ReadOnly = true;
            this.ColumnChecksumSecond.Visible = false;
            // 
            // MusicTitleSecond
            // 
            this.MusicTitleSecond.HeaderText = "Music title Second";
            this.MusicTitleSecond.Name = "MusicTitleSecond";
            this.MusicTitleSecond.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Title = "Select music file";
            // 
            // FormEditParticipants
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1008, 601);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridViewParticipants);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.comboBoxCategory);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormEditParticipants";
            this.ShowInTaskbar = false;
            this.Text = "Edit Participants";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParticipants)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridView dataGridViewParticipants;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnClub;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn MusicTitle;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnSelectFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMusic;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnChecksum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStartNoSecond;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMusicFileSecond;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnChecksumSecond;
        private System.Windows.Forms.DataGridViewTextBoxColumn MusicTitleSecond;
    }
}