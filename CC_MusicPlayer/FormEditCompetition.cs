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

namespace Skaters_MusicPlayer
{
    public partial class FormEditCompetition : Form
    {
        XmlDocument DocCompetition = null;

        public void LoadCompetition(XmlDocument doc, TextBox TB)
        {
            try
            {
                TB.Text = "";
                TB.Tag = null;

                if (doc.DocumentElement != null)
                {
                    TB.Tag = doc.DocumentElement.InnerXml;  // Store the InnerXML for each Class
                    TB.Text = doc.DocumentElement.GetAttribute("Name");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading Competition\n"+e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public FormEditCompetition(XmlDocument doc)
        {
            InitializeComponent();

            DocCompetition = doc;
            LoadCompetition(doc, textBoxCompetition);
        }

        private void buttonClassSave_Click(object sender, EventArgs e)
        {
            DocCompetition.DocumentElement.SetAttribute("Name", textBoxCompetition.Text);
            DocCompetition.Save("competition.xml");

            this.DialogResult = DialogResult.OK;
        }
    }
}
