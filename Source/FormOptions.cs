using System;
using System.Configuration;
using System.Windows.Forms;

namespace SkatersMusicPlayer
{
    public partial class formOptions : Form
    {
        public formOptions()
        {
            InitializeComponent();

            // Load PauseMusic settings
            checkBoxAutoPauseMusic.Checked = FormMusicPlayer.getConfigurationValue("PauseMusicEnabled", "TRUE").ToUpper() == "TRUE";
            try
            {   //Try do parse delay value.
                numericUpDownPause.Value = decimal.Parse(FormMusicPlayer.getConfigurationValue("PauseMusicDelay", "0"));
            }
            catch (Exception)
            {//Do nothing
            }
            try
            {   //Try do parse volume value.
                volumeSliderPause.Volume = float.Parse(FormMusicPlayer.getConfigurationValue("PauseVolume", "0,1"));
            }
            catch (Exception)
            {//Do nothing
            }

            // Load music folders for Warmup music and Break music
            tbWarmupDir.Text = FormMusicPlayer.getConfigurationValue("WarmupMusicDirectory", string.Empty);
            tbBreakDir.Text = FormMusicPlayer.getConfigurationValue("BreakMusicDirectory", string.Empty);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Save changes to config-file
            ConfigurationManager.AppSettings["PauseMusicEnabled"] = (checkBoxAutoPauseMusic.Checked ? "true" : "false");
            ConfigurationManager.AppSettings["PauseMusicDelay"] = numericUpDownPause.Value.ToString();
            ConfigurationManager.AppSettings["PauseVolume"] = volumeSliderPause.Volume.ToString();

            ConfigurationManager.AppSettings["WarmupMusicDirectory"] = tbWarmupDir.Text;
            ConfigurationManager.AppSettings["BreakMusicDirectory"] = tbBreakDir.Text;

            // Return OK to main window
            this.DialogResult = DialogResult.OK;
        }

        private void btnWarmupDir_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = tbWarmupDir.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbWarmupDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnBreakDir_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = tbBreakDir.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbBreakDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
