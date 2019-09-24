using NAudio.Wave;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace SkatersMusicPlayer
{
    public partial class FormEditParticipants : Form
    {
        private readonly FormMusicPlayer fmp = null;
        public FormEditParticipants(FormMusicPlayer Owner, string defaultCategory)
        {
            if (Owner != null)
            {
                InitializeComponent();

                fmp = Owner;
                // Load the categories to the combobox
                FormMusicPlayer.loadCategories(fmp.doc, this.comboBoxCategory);

                try
                {
                    comboBoxCategory.SelectedIndex = comboBoxCategory.FindStringExact(defaultCategory);
                }
                catch (Exception)
                {
                }
            }
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormMusicPlayer.loadParticipantsDV(fmp.doc, comboBoxCategory.SelectedItem.ToString(), dataGridViewParticipants);
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
                        if (openFileDialog1.FileName.Substring(0, Application.StartupPath.Length) == Application.StartupPath)
                        {// Remove startpath
                            openFileDialog1.FileName = openFileDialog1.FileName.Substring(Application.StartupPath.Length + 1);  //Also remove the backslash from path
                        }

                        // Calculate MD5 for musicfile to verify that no other participant already has this file
                        string MD5 = FormMusicPlayer.getMD5HashFromFile(openFileDialog1.FileName);

                        // First check so no other participant already has this file
                        for (int i = 0; i < senderGrid.Rows.Count - 1; i++)
                        {
                            if ((i != e.RowIndex && senderGrid[9, i].Value.ToString() == openFileDialog1.FileName) || (senderGrid[12, i].Value.ToString() == openFileDialog1.FileName))
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
                                if ((i != e.RowIndex && senderGrid[10, i].Value.ToString() == MD5) || (senderGrid[13, i].Value.ToString() == MD5))
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
                            senderGrid[10, e.RowIndex].Value = FormMusicPlayer.getMD5HashFromFile(openFileDialog1.FileName);
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
            if (fmp.doc.DocumentElement != null && comboBoxCategory.Text.Length > 5)
            {
                // Split selected itemtext into Category and Segment.
                string MainCategory = comboBoxCategory.Text.Substring(0, comboBoxCategory.Text.Length - 8);
                string Segment = comboBoxCategory.Text.Substring(comboBoxCategory.Text.Length - 5, 5).Trim();

                // Loop throu all Categoriess to find the selected one
                foreach (XmlNode tableNode in fmp.doc.DocumentElement.GetElementsByTagName(Properties.Resources.XMLTAG_CATEGORY))
                {
                    // Is it the correct category?
                    if (tableNode.Attributes.GetNamedItem("Name").Value == MainCategory)
                    {
                        // Delete all participants
                        while (tableNode.HasChildNodes)
                        {
                            tableNode.RemoveChild(tableNode.FirstChild);
                        }
                        //Loop through all rows and add categories
                        for (int r = 0; r < dataGridViewParticipants.Rows.Count - 1; r++)
                        {
                            XmlNode participantNode = fmp.doc.CreateElement(Properties.Resources.XMLTAG_PARTICIPANT);

                            XmlAttribute attributeID = fmp.doc.CreateAttribute("ID");
                            attributeID.Value = (string)dataGridViewParticipants[4, r].Value;
                            participantNode.Attributes.Append(attributeID);
                            XmlAttribute attributeBD = fmp.doc.CreateAttribute("BirthDate");
                            attributeBD.Value = (string)dataGridViewParticipants[5, r].Value;
                            participantNode.Attributes.Append(attributeBD);

                            createParticipantsElement(participantNode, "FirstName", 1, r);
                            createParticipantsElement(participantNode, "LastName", 2, r);
                            createParticipantsElement(participantNode, "Club", 3, r);

                            // Create segment
                            XmlNode shortNode = fmp.doc.CreateElement("Short");
                            XmlNode freeNode = fmp.doc.CreateElement("Free");
                            if (Segment == "Short")
                            {
                                createParticipantsElement(shortNode, "StartNo", 0, r);
                                createParticipantsElement(shortNode, "MusicFile", 9, r, "MD5", 10, r);
                                createParticipantsElement(shortNode, "MusicName", 6, r);
                                createParticipantsElement(freeNode, "StartNo", 11, r);
                                createParticipantsElement(freeNode, "MusicFile", 12, r, "MD5", 13, r);
                                createParticipantsElement(freeNode, "MusicName", 14, r);
                            }
                            else
                            {
                                createParticipantsElement(shortNode, "StartNo", 11, r);
                                createParticipantsElement(shortNode, "MusicFile", 12, r, "MD5", 13, r);
                                createParticipantsElement(shortNode, "MusicName", 14, r);
                                createParticipantsElement(freeNode, "StartNo", 0, r);
                                createParticipantsElement(freeNode, "MusicFile", 9, r, "MD5", 10, r);
                                createParticipantsElement(freeNode, "MusicName", 6, r);
                            }

                            participantNode.AppendChild(shortNode);
                            participantNode.AppendChild(freeNode);
                            tableNode.AppendChild(participantNode);
                        }
                        fmp.doc.Save(Properties.Resources.XML_FILENAME);

                    }
                }
            }

            //End form
            this.DialogResult = DialogResult.OK;
        }

        private void createParticipantsElement(XmlNode participantNode, string name, int col, int row)
        {
            XmlNode node = fmp.doc.CreateElement(name);
            string v = (string)dataGridViewParticipants[col, row].Value;
            if (!string.IsNullOrEmpty(v))
            {   // Spara bara om det finns ett värde
                if (col == 0 || col == 10)
                {// if it is startno, rightjustify value
                    v = v.PadLeft(3, ' ');
                }
                node.InnerText = v;
                participantNode.AppendChild(node);
            }
        }

        private void createParticipantsElement(XmlNode participantNode, string name, int col, int row, string attr, int colAttr, int rowAttr)
        {
            string v = (string)dataGridViewParticipants[col, row].Value;
            if (!string.IsNullOrEmpty(v))
            {   // Spara bara om det finns ett värde
                XmlNode node = fmp.doc.CreateElement(name);
                node.InnerText = (string)dataGridViewParticipants[col, row].Value;

                string va = (string)dataGridViewParticipants[colAttr, rowAttr].Value;
                if (!string.IsNullOrEmpty(attr) && !string.IsNullOrEmpty(va))
                {   //Det finns att attribut också
                    XmlAttribute attribute = fmp.doc.CreateAttribute(attr);
                    attribute.Value = (string)dataGridViewParticipants[colAttr, rowAttr].Value;
                    node.Attributes.Append(attribute);
                }

                participantNode.AppendChild(node);
            }
        }

    }
}
