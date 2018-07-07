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
using System.IO;

namespace SkatersMusicPlayer
{
    public partial class FormEditClasses : Form
    {
        XmlDocument DocClasses = null;

        public void LoadClassesToGrid(XmlDocument doc, DataGridView DV)
        {
            try
            {
                DV.Rows.Clear();

                if (doc.DocumentElement != null)
                {
                    foreach (XmlNode tableNode in doc.DocumentElement.GetElementsByTagName("Class"))
                    {
                        DV.Rows.Add();
                        DV[0, DV.Rows.Count - 2].Tag = tableNode.InnerXml;  // Store the InnerXML for each Class
                        DV[0, DV.Rows.Count - 2].Value = tableNode.Attributes.GetNamedItem("Name").Value;

                        // Does the Class have differens classes?
                        if (tableNode.Attributes.GetNamedItem("HasShort") != null)
                        {
                            DV[1, DV.Rows.Count - 2].Value = "true";
                        }
                        DV[2, DV.Rows.Count - 2].Value = "true";
                        DV[3, DV.Rows.Count - 2].Value = tableNode.ChildNodes.Count; // Number of skaters in class

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading Classes\n" + e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public FormEditClasses(XmlDocument doc)
        {
            InitializeComponent();

            DocClasses = doc;
            LoadClassesToGrid(doc, dataGridView1);

        }

        private void buttonClassSave_Click(object sender, EventArgs e)
        {
            // Save classes
            if (DocClasses.DocumentElement != null)
            {
                // Remove all classes from XML tree
                while (DocClasses.DocumentElement.HasChildNodes)
                {
                    try
                    {
                        //Try to remove folder. If it can't an exception will be thrown, but we ignore that...
                        System.IO.Directory.Delete(Application.StartupPath + @"\CompetitionMusic\" + DocClasses.DocumentElement.FirstChild.Attributes["Name"].Value.Replace(" ", "_"));
                    }
                    catch (Exception)
                    {
                        //Do nothing
                    }

                    // Remove class from XML tree
                    DocClasses.DocumentElement.RemoveChild(DocClasses.DocumentElement.FirstChild);
                }

                //Loop through all rows and add classes
                for (int r = 0; r < dataGridView1.Rows.Count - 1; r++)
                {
                    //Name="Ungdom C" HasShort="true">

                    // Try to create directory - No longer do this since IndTA2.0 doesn't supply files in folders
                    //System.IO.Directory.CreateDirectory(Application.StartupPath + @"\CompetitionMusic\" + dataGridView1[0, r].Value.ToString().Replace(" ", "_"));

                    XmlNode classNode = DocClasses.CreateElement("Class");
                    XmlAttribute attributeName = DocClasses.CreateAttribute("Name");
                    attributeName.Value = (string)dataGridView1[0, r].Value;
                    classNode.Attributes.Append(attributeName);
                    if (dataGridView1[1, r].Value != null && dataGridView1[1, r].Value.ToString() == "true")
                    {
                        XmlAttribute attributeShort = DocClasses.CreateAttribute("HasShort");
                        attributeShort.Value = (string)dataGridView1[1, r].Value;
                        classNode.Attributes.Append(attributeShort);
                    }
                    classNode.InnerXml = (string)dataGridView1[0, r].Tag;  // Store the InnerXML for each Class

                    DocClasses.DocumentElement.AppendChild(classNode);
                }
                DocClasses.Save("competition.xml");
            }

            //End form
            this.DialogResult = DialogResult.OK;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridView1[2, e.RowIndex].Value = "true";
            dataGridView1[3, e.RowIndex].Value = 0;
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if ((int)e.Row.Cells[3].Value != 0)
            {// Class contains skaters, that will be removed
                if (MessageBox.Show("Are you sure you want to remove " + e.Row.Cells[0].Value + "?\n\nIt contains " + e.Row.Cells[3].Value + " skater(s) that also will be deleted!", "Delete Class?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }

            }
        }
    }
}
