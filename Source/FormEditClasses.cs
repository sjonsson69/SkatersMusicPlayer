using System;
using System.Windows.Forms;
using System.Xml;

namespace SkatersMusicPlayer
{
    public partial class formEditCategories : Form
    {
        XmlDocument docCategories = null;

        public static void loadCategoriesToGrid(XmlDocument doc, DataGridView DV)
        {
            if (doc != null && DV != null)
            {
                try
                {
                    DV.Rows.Clear();

                    if (doc.DocumentElement != null)
                    {
                        foreach (XmlNode tableNode in doc.DocumentElement.GetElementsByTagName(Properties.Resources.XMLTAG_CATEGORY))
                        {
                            DV.Rows.Add();
                            DV[0, DV.Rows.Count - 2].Tag = tableNode.InnerXml;  // Store the InnerXML for each Category
                            DV[0, DV.Rows.Count - 2].Value = tableNode.Attributes.GetNamedItem("Name").Value;

                            // Does the Category have differens segments?
                            if (tableNode.Attributes.GetNamedItem("HasShort") != null)
                            {
                                DV[1, DV.Rows.Count - 2].Value = "true";
                            }
                            DV[2, DV.Rows.Count - 2].Value = "true";
                            DV[3, DV.Rows.Count - 2].Value = tableNode.ChildNodes.Count; // Number of participants in category

                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error loading Categories\n" + e.Message, Properties.Resources.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public formEditCategories(XmlDocument doc)
        {
            InitializeComponent();

            docCategories = doc;
            loadCategoriesToGrid(doc, dataGridView1);

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Save categories
            if (docCategories.DocumentElement != null)
            {
                // Remove all categories from XML tree
                while (docCategories.DocumentElement.HasChildNodes)
                {
                    // Remove category from XML tree
                    docCategories.DocumentElement.RemoveChild(docCategories.DocumentElement.FirstChild);
                }

                //Loop through all rows and add category
                for (int r = 0; r < dataGridView1.Rows.Count - 1; r++)
                {
                    XmlNode categoryNode = docCategories.CreateElement(Properties.Resources.XMLTAG_CATEGORY);
                    XmlAttribute attributeName = docCategories.CreateAttribute("Name");
                    attributeName.Value = (string)dataGridView1[0, r].Value;
                    categoryNode.Attributes.Append(attributeName);
                    if (dataGridView1[1, r].Value != null && dataGridView1[1, r].Value.ToString() == "true")
                    {
                        XmlAttribute attributeShort = docCategories.CreateAttribute("HasShort");
                        attributeShort.Value = (string)dataGridView1[1, r].Value;
                        categoryNode.Attributes.Append(attributeShort);
                    }
                    categoryNode.InnerXml = (string)dataGridView1[0, r].Tag;  // Store the InnerXML for each Category

                    docCategories.DocumentElement.AppendChild(categoryNode);
                }
                docCategories.Save(Properties.Resources.XML_FILENAME);
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
            {// Category contains participants, that will be removed
                if (MessageBox.Show("Are you sure you want to remove " + e.Row.Cells[0].Value + "?\n\nIt contains " + e.Row.Cells[3].Value + " participants that also will be deleted!", Properties.Resources.CAPTION_DELETE_CATEGORY, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }

            }
        }
    }
}
