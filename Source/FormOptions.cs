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

            tbFSMServer.Text = settings.FSMServer;
            nUDFSMPort.Value = settings.FSMPort;
            tbFSMUsername.Text = settings.FSMUsername;
            tbFSMPassword.Text = settings.FSMPassword;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            settings.pauseMusicEnabled = checkBoxAutoPauseMusic.Checked;
            settings.pauseMusicDelay = numericUpDownPause.Value;
            settings.pauseVolume = volumeSliderPause.Volume;

            settings.warmupMusicDirectory = tbWarmupDir.Text;
            settings.breakMusicDirectory = tbBreakDir.Text;

            settings.FSMServer = tbFSMServer.Text;
            settings.FSMPort = (uint)decimal.ToInt32(nUDFSMPort.Value);
            settings.FSMUsername = tbFSMUsername.Text;
            settings.FSMPassword = tbFSMPassword.Text;

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
