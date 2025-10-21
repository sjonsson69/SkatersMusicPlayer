using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkatersMusicPlayer
{
    public partial class FormMissingMusic : Form
    {
        public FormMissingMusic(string MissingFiles)
        {
            InitializeComponent();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(MissingFiles.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.File.WriteAllLines(saveFileDialog.FileName, listBox1.Items.Cast<string>());
                    MessageBox.Show("Missing files list saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
