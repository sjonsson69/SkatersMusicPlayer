using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skaters_MusicPlayer
{
    public partial class FormOptions : Form
    {
        public FormOptions(FormMusicPlayer parent)
        {
            InitializeComponent();

            // Load PauseMusic settings
            checkBoxAutoPauseMusic.Checked = (parent.GetConfigurationValue("PauseMusicEnabled", "TRUE").ToUpper() == "TRUE");
            try
            {   //Try do parse delay value.
                numericUpDownPause.Value = Decimal.Parse(parent.GetConfigurationValue("PauseMusicDelay", "0"));
            }
            catch (Exception)
            {//Do nothing
            }
            try
            {   //Try do parse volume value.
                volumeSliderPause.Volume = float.Parse(parent.GetConfigurationValue("PauseVolume", "0,1"));
            }
            catch (Exception)
            {//Do nothing
            }

            // Load music folders for Warmup music and Break music
            tbWarmupDir.Text=parent.GetConfigurationValue("WarmupMusicDirectory", "");
            tbBreakDir.Text = parent.GetConfigurationValue("BreakMusicDirectory", "");

            // Load SpotifiURI
            tbSpotifyURI.Text = parent.GetConfigurationValue("SpotifyURI", "");
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            ConfigurationManager.AppSettings["PauseMusicEnabled"] = (checkBoxAutoPauseMusic.Checked ? "true" : "false");
            ConfigurationManager.AppSettings["PauseMusicDelay"] = numericUpDownPause.Value.ToString();
            ConfigurationManager.AppSettings["PauseVolume"] = volumeSliderPause.Volume.ToString();

            this.DialogResult = DialogResult.OK;
        }
    }
}
