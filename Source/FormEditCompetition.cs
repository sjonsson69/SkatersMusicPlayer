using System;
using System.Windows.Forms;
using System.Xml;

namespace SkatersMusicPlayer
{
    public partial class FormEditEvent : Form
    {
        XmlDocument docEvent = null;

        public static void loadEvent(XmlDocument doc, TextBox TB)
        {
            if (doc != null && TB != null)
            {
                try
                {
                    TB.Text = string.Empty;
                    TB.Tag = null;

                    if (doc.DocumentElement != null)
                    {
                        TB.Tag = doc.DocumentElement.InnerXml;  // Store the InnerXML for each Category
                        TB.Text = doc.DocumentElement.GetAttribute("Name");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error loading Event\n" + e.Message, Properties.Resources.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public FormEditEvent(XmlDocument doc)
        {
            InitializeComponent();

            docEvent = doc;
            loadEvent(doc, textBoxEvent);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            docEvent.DocumentElement.SetAttribute("Name", textBoxEvent.Text);
            docEvent.Save(Properties.Resources.XML_FILENAME);

            this.DialogResult = DialogResult.OK;
        }
    }
}
