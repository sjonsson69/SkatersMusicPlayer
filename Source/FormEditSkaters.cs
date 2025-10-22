using NAudio.Wave;
using System;
using System.Drawing;
using System.Windows.Forms;
using static SkatersMusicPlayer.formMusicPlayer;

namespace SkatersMusicPlayer
{
    public partial class FormEditParticipants : Form
    {
        private readonly formMusicPlayer fmp = null;
        public FormEditParticipants(formMusicPlayer Owner, string defaultCategory)
        {
            if (Owner != null)
            {
                InitializeComponent();

                fmp = Owner;
                // Load the categories to the combobox
                formMusicPlayer.loadCategories(fmp.compEvent, this.comboBoxCategory);
                try
                {
                    comboBoxCategory.SelectedIndex = comboBoxCategory.FindStringExact(defaultCategory);
                    if (comboBoxCategory.SelectedIndex == -1 && comboBoxCategory.Items.Count > 0)
                    {
                        comboBoxCategory.SelectedIndex = 0;  // Select first item if default not found
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            formMusicPlayer.loadParticipantsDV(fmp.compEvent, comboBoxCategory.SelectedItem.ToString(), dataGridViewParticipants);
            comboBoxCategory.Enabled = true;
            buttonSave.Enabled = false;  // Disable save button until something changes
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove " + e.Row.Cells[1].Value + " " + e.Row.Cells[2].Value + "?", Properties.Resources.CAPTION_DELETE_PARTICIPANT, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void dataGridViewParticipants_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Initialize audioreader so we can verify if the files found are playable
            AudioFileReader audioFileReaderTest = null;
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.RowIndex < senderGrid.Rows.Count - 1)
            {
                //Button Clicked - Execute Code Here
                openFileDialog1.Title = "Select musicfile for:" + senderGrid[1, e.RowIndex].Value.ToString() + " " + senderGrid[2, e.RowIndex].Value.ToString();
                openFileDialog1.FileName = string.Empty;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Make the path relative if possible.
                        if (openFileDialog1.FileName.Length > Application.StartupPath.Length
                            && openFileDialog1.FileName.Substring(0, Application.StartupPath.Length) == Application.StartupPath)
                        {// Remove startpath
                            openFileDialog1.FileName = openFileDialog1.FileName.Substring(Application.StartupPath.Length + 1);  //Also remove the backslash from path
                        }

                        // Calculate MD5 for musicfile to verify that no other participant already has this file
                        string MD5 = formMusicPlayer.getMD5HashFromFile(openFileDialog1.FileName);

                        // First check so no other participant already has this file
                        for (int i = 0; i < senderGrid.Rows.Count - 1; i++)
                        {
                            if ((i != e.RowIndex && senderGrid[9, i].Value.ToString() == openFileDialog1.FileName))
                            {
                                MessageBox.Show("File already connected to participant " + senderGrid[1, i].Value + " " + senderGrid[2, i].Value + "\n\nFile not connected to this participant!", Properties.Resources.CAPTION_DUPLICATE_USE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                MD5 = string.Empty;  //Remove MD5 to indicate that the file isn't connected
                            }
                        }

                        // Check MD5 for musicfile so no other participant already has this file
                        for (int i = 0; i < senderGrid.Rows.Count - 1; i++)
                        {
                            if (!string.IsNullOrEmpty(MD5))
                            {
                                if ((i != e.RowIndex && senderGrid[10, i].Value.ToString() == MD5))
                                {
                                    MessageBox.Show("Identical file content already connected to participant " + senderGrid[1, i].Value + " " + senderGrid[2, i].Value + "\n\nFile not connected to this participant!", Properties.Resources.CAPTION_DUPLICATE_USE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    MD5 = string.Empty;  //Remove MD5 to indicate that the file isn't connected
                                }
                            }
                        }

                        //Try to load the file to see if NAudio can read it. Gives an exception if we can't read it
                        audioFileReaderTest = new AudioFileReader(openFileDialog1.FileName);

                        // Do we have a MD5? If so connect file to participant
                        if (!string.IsNullOrEmpty(MD5))
                        {
                            senderGrid[8, e.RowIndex].Value = string.Format("{0:00}:{1:00}", (int)audioFileReaderTest.TotalTime.TotalMinutes, audioFileReaderTest.TotalTime.Seconds);
                            senderGrid[9, e.RowIndex].Value = openFileDialog1.FileName;
                            senderGrid[10, e.RowIndex].Value = formMusicPlayer.getMD5HashFromFile(openFileDialog1.FileName);
                            senderGrid.Rows[e.RowIndex].DefaultCellStyle.ForeColor = SystemColors.ControlText;  // Restore color if it wasn't correct
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Can't verify file as a music file\n\n\n" + "Errormessage:" + ex.Message, Properties.Resources.CAPTION_INVALID_FILE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (audioFileReaderTest != null)
                        {
                            // Dispose of the reader
                            audioFileReaderTest.Dispose();
                            audioFileReaderTest = null;
                        }
                    }
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (fmp.compEvent != null && comboBoxCategory.Text.Length > 5)
            {
                // Split selected itemtext into Category and Segment.
                string MainCategory = comboBoxCategory.Text.Substring(0, comboBoxCategory.Text.Length - 8);
                string Segment = comboBoxCategory.Text.Substring(comboBoxCategory.Text.Length - 5, 5).Trim();

                // Loop throu all Categories to find the selected one
                foreach (categorySegment cat in fmp.compEvent.categoriesAndSegments)
                {
                    if (combinedCategoryDisciplineSegmentName(cat) == comboBoxCategory.Text)
                    {
                        // Delete all participants in this category/segment
                        cat.participants.Clear();

                        //Loop through all rows and add categories
                        for (int r = 0; r < dataGridViewParticipants.Rows.Count - 1; r++)
                        {
                            participant p = new participant();
                            int startNumber = 0;
                            int.TryParse((string)dataGridViewParticipants[0, r].Value, out startNumber);
                            if (startNumber != 0)
                            {
                                p.startNumber = startNumber;
                            }
                            p.firstName = (string)dataGridViewParticipants[1, r].Value;
                            p.lastName = (string)dataGridViewParticipants[2, r].Value;
                            p.club = (string)dataGridViewParticipants[3, r].Value;
                            if (dataGridViewParticipants[4, r].Value != null)
                            {
                                p.id = (string)dataGridViewParticipants[4, r].Value;
                            }
                            if (dataGridViewParticipants[4, r].Value != null)
                            {
                                p.birthDate = (DateTime)dataGridViewParticipants[5, r].Value;
                            }
                            p.music = new competitionMusic
                            {
                                title = (string)dataGridViewParticipants[6, r].Value,
                                file = (string)dataGridViewParticipants[9, r].Value,
                                md5 = (string)dataGridViewParticipants[10, r].Value
                            };

                            cat.participants.Add(p);
                        }

                    }
                }

                serializeToFile(fmp.compEvent, Properties.Resources.JSON_FILENAME);
            }

            //End form
            this.DialogResult = DialogResult.OK;
        }

        private void dataGridViewParticipants_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            comboBoxCategory.Enabled = false;  // Disable changing category until save is done
            buttonSave.Enabled = true;  // Enable save button  
        }
    }
}
