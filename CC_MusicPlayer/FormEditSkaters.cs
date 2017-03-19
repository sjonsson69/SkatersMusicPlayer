using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using NAudio;
using NAudio.Wave;

namespace Skaters_MusicPlayer
{
    public partial class FormEditSkaters : Form
    {
        private FormMusicPlayer fmp = null;
        public FormEditSkaters(FormMusicPlayer Owner, string defaultClass)
        {
            InitializeComponent();

            fmp = Owner;
            // Load the classes to the combobox
            fmp.LoadClasses(fmp.doc, this.comboBoxClass);

            try
            {
                comboBoxClass.SelectedIndex = comboBoxClass.FindStringExact(defaultClass);
            }
            catch (Exception)
            {
            }
        }

        private void comboBoxClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            fmp.LoadSkatersDV(fmp.doc, comboBoxClass.SelectedItem.ToString(), dataGridViewSkaters);
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove " + e.Row.Cells[1].Value + " " + e.Row.Cells[2].Value + "?", "Delete Skater?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void dataGridViewSkaters_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Initialize audioreader so we can verify if the files found are playable
            AudioFileReader audioFileReaderTest = null;
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.RowIndex < senderGrid.Rows.Count - 1)
            {
                //Button Clicked - Execute Code Here
                openFileDialog1.Title = "Select musicfile for:" + senderGrid[1, e.RowIndex].Value.ToString() + " " + senderGrid[2, e.RowIndex].Value.ToString();
                openFileDialog1.FileName = "";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Make the path relative if possible.
                        if (openFileDialog1.FileName.Substring(0, Application.StartupPath.Length) == Application.StartupPath)
                        {// Remove startpath
                            openFileDialog1.FileName = openFileDialog1.FileName.Substring(Application.StartupPath.Length + 1);  //Also remove the backslash from path
                        }

                        // Calculate MD5 for musicfile to verify that no other skater already has this file
                        string MD5 = fmp.GetMD5HashFromFile(openFileDialog1.FileName);

                        // First check so no other skater already has this file
                        for (int i = 0; i < senderGrid.Rows.Count - 1; i++)
                        {
                            if ((i != e.RowIndex && senderGrid[8, i].Value.ToString() == openFileDialog1.FileName) || (senderGrid[11, i].Value.ToString() == openFileDialog1.FileName))
                            {
                                MessageBox.Show("File already connected to skater " + senderGrid[1, i].Value + " " + senderGrid[2, i].Value + "\n\nFile not connected to this skater!", "Duplicate use of music file", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                MD5 = string.Empty;  //Remove MD5 to indicate that the file isn't connected
                            }
                        }

                        // Check MD5 for musicfile so no other skater already has this file
                        for (int i = 0; i < senderGrid.Rows.Count - 1; i++)
                        {
                            if (MD5 != string.Empty)
                            {
                                if ((i != e.RowIndex && senderGrid[9, i].Value.ToString() == MD5) || (senderGrid[12, i].Value.ToString() == MD5))
                                {
                                    MessageBox.Show("Identical file content already connected to skater " + senderGrid[1, i].Value + " " + senderGrid[2, i].Value + "\n\nFile not connected to this skater!", "Duplicate use of music file", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    MD5 = string.Empty;  //Remove MD5 to indicate that the file isn't connected
                                }
                            }
                        }

                        //Try to load the file to see if NAudio can read it. Gives an exception if we can't read it
                        audioFileReaderTest = new AudioFileReader(openFileDialog1.FileName);

                        // Do we have a MD5? If so connect file to skater
                        if (MD5 != string.Empty)
                        {
                            senderGrid[7, e.RowIndex].Value = String.Format("{0:00}:{1:00}", (int)audioFileReaderTest.TotalTime.TotalMinutes, audioFileReaderTest.TotalTime.Seconds);
                            senderGrid[8, e.RowIndex].Value = openFileDialog1.FileName;
                            senderGrid[9, e.RowIndex].Value = fmp.GetMD5HashFromFile(openFileDialog1.FileName);
                            senderGrid.Rows[e.RowIndex].DefaultCellStyle.ForeColor = SystemColors.ControlText;  // Restore color if it wasn't correct
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Can't verify file as a music file\n\n\n" + "Errormessage:" + ex.Message, "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (fmp.doc.DocumentElement != null && comboBoxClass.Text.Length > 5)
            {
                // Split selected itemtext into Main and Sub classes.
                string MainClass = comboBoxClass.Text.Substring(0, comboBoxClass.Text.Length - 8);
                string SubClass = comboBoxClass.Text.Substring(comboBoxClass.Text.Length - 5, 5).Trim();

                // Loop throu all Classes to find the selected one
                foreach (XmlNode tableNode in fmp.doc.DocumentElement.GetElementsByTagName("Class"))
                {
                    // Is it the correct class?
                    if (tableNode.Attributes.GetNamedItem("Name").Value == MainClass)
                    {
                        // Delete all skaters
                        while (tableNode.HasChildNodes)
                        {
                            tableNode.RemoveChild(tableNode.FirstChild);
                        }
                        //Loop through all rows and add classes
                        for (int r = 0; r < dataGridViewSkaters.Rows.Count - 1; r++)
                        {
                            XmlNode skaterNode = fmp.doc.CreateElement("Skater");

                            XmlAttribute attributeID = fmp.doc.CreateAttribute("ID");
                            attributeID.Value = (string)dataGridViewSkaters[4, r].Value;
                            skaterNode.Attributes.Append(attributeID);
                            XmlAttribute attributeBD = fmp.doc.CreateAttribute("BirthDate");
                            attributeBD.Value = (string)dataGridViewSkaters[5, r].Value;
                            skaterNode.Attributes.Append(attributeBD);

                            CreateSkatersElement(skaterNode, "FirstName", 1, r);
                            CreateSkatersElement(skaterNode, "LastName", 2, r);
                            CreateSkatersElement(skaterNode, "Club", 3, r);

                            // Create subclasses
                            XmlNode shortNode = fmp.doc.CreateElement("Short");
                            XmlNode freeNode = fmp.doc.CreateElement("Free");
                            if (SubClass == "Short")
                            {
                                CreateSkatersElement(shortNode, "StartNo", 0, r);
                                CreateSkatersElement(shortNode, "MusicFile", 8, r, "MD5", 9, r);
                                CreateSkatersElement(freeNode, "StartNo", 10, r);
                                CreateSkatersElement(freeNode, "MusicFile", 11, r, "MD5", 12, r);
                            }
                            else
                            {
                                CreateSkatersElement(shortNode, "StartNo", 10, r);
                                CreateSkatersElement(shortNode, "MusicFile", 11, r, "MD5", 12, r);
                                CreateSkatersElement(freeNode, "StartNo", 0, r);
                                CreateSkatersElement(freeNode, "MusicFile", 8, r, "MD5", 9, r);
                            }

                            skaterNode.AppendChild(shortNode);
                            skaterNode.AppendChild(freeNode);
                            tableNode.AppendChild(skaterNode);
                        }
                        fmp.doc.Save("competition.xml");

                    }
                }
            }

            //End form
            this.DialogResult = DialogResult.OK;
        }

        private void CreateSkatersElement(XmlNode skaterNode, string name, int col, int row)
        {
            XmlNode node = fmp.doc.CreateElement(name);
            string v = (string)dataGridViewSkaters[col, row].Value;
            if (v != string.Empty)
            {   // Spara bara om det finns ett värde
                if (col==0 || col==10)
                {// if it is startno, rightjustify value
                    v = v.PadLeft(3, ' ');
                }
                node.InnerText = v;
                //node.InnerText = (string)dataGridViewSkaters[col, row].Value;
                skaterNode.AppendChild(node);
            }
        }

        private void CreateSkatersElement(XmlNode skaterNode, string name, int col, int row, string attr, int colAttr, int rowAttr)
        {
            string v = (string)dataGridViewSkaters[col, row].Value;
            if (v != string.Empty)
            {   // Spara bara om det finns ett värde
                XmlNode node = fmp.doc.CreateElement(name);
                node.InnerText = (string)dataGridViewSkaters[col, row].Value;

                string va = (string)dataGridViewSkaters[colAttr, rowAttr].Value;
                if (attr != string.Empty && va != string.Empty)
                {   //Det finns att attribut också
                    XmlAttribute attribute = fmp.doc.CreateAttribute(attr);
                    attribute.Value = (string)dataGridViewSkaters[colAttr, rowAttr].Value;
                    node.Attributes.Append(attribute);
                }

                skaterNode.AppendChild(node);
            }
        }

    }
}
