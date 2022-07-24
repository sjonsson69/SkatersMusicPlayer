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
            checkBoxAutoPauseMusic.Checked = settings.pauseMusicEnabled;
               numericUpDownPause.Value = settings.pauseMusicDelay;
                volumeSliderPause.Volume = settings.pauseVolume;

            // Load music folders for Warmup music and Break music
            tbWarmupDir.Text = settings.warmupMusicDirectory;
            tbBreakDir.Text = settings.breakMusicDirectory;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Save changes to config-file
            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //config.AppSettings.Settings["PauseMusicEnabled"].Value = (checkBoxAutoPauseMusic.Checked ? "true" : "false");
            //config.AppSettings.Settings["PauseMusicDelay"].Value = numericUpDownPause.Value.ToString();
            //config.AppSettings.Settings["PauseVolume"].Value = volumeSliderPause.Volume.ToString();

            //config.AppSettings.Settings["WarmupMusicDirectory"].Value = tbWarmupDir.Text;
            //config.AppSettings.Settings["BreakMusicDirectory"].Value = tbBreakDir.Text;

            ////Save values
            //config.Save(ConfigurationSaveMode.Minimal);
            //ConfigurationManager.RefreshSection("appSettings");

            settings.pauseMusicEnabled = checkBoxAutoPauseMusic.Checked;
            settings.pauseMusicDelay = numericUpDownPause.Value;
            settings.pauseVolume = volumeSliderPause.Volume;
            settings.warmupMusicDirectory = tbWarmupDir.Text;
            settings.breakMusicDirectory = tbBreakDir.Text;


            // Return OK to main window
            DialogResult = DialogResult.OK;
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
