using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using System.Xml;

namespace SkatersMusicPlayer
{
    public partial class FormMusicPlayer : Form
    {
        enum player { Nothing, Participant, Warmup, Break, Spotify };

        //Constants for messages
        public XmlDocument doc = new XmlDocument() { XmlResolver = null };

        private IWavePlayer waveOutDeviceParticipant = null;
        private AudioFileReader audioFileReaderParticipant = null;
        private FadeInOutSampleProvider fadeInOut;
        private Action<float> setVolumeDelegateParticipant;
        private bool pausePlaying = false;
        private bool stopPressed = false;

        private IWavePlayer waveOutDeviceWarmup = null;
        private AudioFileReader audioFileReaderWarmup = null;
        private Action<float> setVolumeDelegateWarmup;
        private IWavePlayer waveOutDeviceBreak = null;
        private AudioFileReader audioFileReaderBreak = null;
        private Action<float> setVolumeDelegateBreak;

        public FormMusicPlayer()
        {
            InitializeComponent();

            labelCurrentTime.Text = string.Empty;
            labelTotalTime.Text = string.Empty;

            loadSettings();

            loadXMLfile();

        }

        // Load settings from config-file
        private void loadSettings()
        {
            // Load PauseMusic settings
            checkBoxAutoPauseMusic.Checked = getConfigurationValue("PauseMusicEnabled", "TRUE").ToUpper() == "TRUE";
            try
            {   //Try do parse delay value.
                numericUpDownPause.Value = decimal.Parse(getConfigurationValue("PauseMusicDelay", "0"));
            }
            catch (Exception)
            {//Do nothing
            }
            try
            {   //Try do parse volume value.
                volumeSliderPause.Volume = float.Parse(getConfigurationValue("PauseVolume", "0,1"));
            }
            catch (Exception)
            {//Do nothing
            }


            // Load music folders for Warmup music and Break music
            loadMusicFolder(getConfigurationValue("WarmupMusicDirectory", string.Empty), true, listViewWarmupMusic);
            loadMusicFolder(getConfigurationValue("BreakMusicDirectory", string.Empty), true, listViewBreakMusic);

            //Kolla om vi fått Break music. Om inte så ta bort boxen och flytta resten
            if (listViewBreakMusic.Items.Count == 0)
            {
                groupBoxBreakMusic.Visible = false;
                groupBoxWarmupMusic.Left += groupBoxBreakMusic.Width + 6;
                groupBoxParticipantMusic.Width += groupBoxBreakMusic.Width + 6;
            }
            // Kolla om vi fått Warmup music. Om inte så ta bort boxen och bredda resten
            if (listViewWarmupMusic.Items.Count == 0)
            {
                groupBoxWarmupMusic.Visible = false;
                groupBoxParticipantMusic.Width += groupBoxWarmupMusic.Width + 6;
            }

            enableButtons();
        }


        #region CreateWaveOut
        private void createWaveOutParticipant()
        {
            closeWaveOutParticipant();

            waveOutDeviceParticipant = new WaveOut();
            waveOutDeviceParticipant.PlaybackStopped += onPlaybackStopped;
        }

        private void createWaveOutWarmup()
        {
            closeWaveOutWarmup();

            waveOutDeviceWarmup = new WaveOut();
            waveOutDeviceWarmup.PlaybackStopped += onPlaybackStoppedWarmup;
        }

        private void createWaveOutBreak()
        {
            closeWaveOutBreak();

            waveOutDeviceBreak = new WaveOut();
            waveOutDeviceBreak.PlaybackStopped += onPlaybackStoppedBreak;
        }

        #endregion
        #region CloseWaveOut
        private void closeWaveOutParticipant()
        {
            if (waveOutDeviceParticipant != null)
            {
                waveOutDeviceParticipant.Stop();
            }
            if (audioFileReaderParticipant != null)
            {
                // this one really closes the file and ACM conversion
                audioFileReaderParticipant.Dispose();
                setVolumeDelegateParticipant = null;
                audioFileReaderParticipant = null;
            }
            if (waveOutDeviceParticipant != null)
            {
                waveOutDeviceParticipant.Dispose();
                waveOutDeviceParticipant = null;
            }
        }

        private void closeWaveOutWarmup()
        {
            if (waveOutDeviceWarmup != null)
            {
                waveOutDeviceWarmup.Stop();
            }
            if (audioFileReaderWarmup != null)
            {
                // this one really closes the file and ACM conversion
                audioFileReaderWarmup.Dispose();
                setVolumeDelegateWarmup = null;
                audioFileReaderWarmup = null;
            }
            if (waveOutDeviceWarmup != null)
            {
                waveOutDeviceWarmup.Dispose();
                waveOutDeviceWarmup = null;
            }
        }

        private void closeWaveOutBreak()
        {
            if (waveOutDeviceBreak != null)
            {
                waveOutDeviceBreak.Stop();
            }
            if (audioFileReaderBreak != null)
            {
                // this one really closes the file and ACM conversion
                audioFileReaderBreak.Dispose();
                setVolumeDelegateBreak = null;
                audioFileReaderBreak = null;
            }
            if (waveOutDeviceBreak != null)
            {
                waveOutDeviceBreak.Dispose();
                waveOutDeviceBreak = null;
            }
        }

        #endregion
        #region NextMusic
        private void nextParticipant()
        {
            if (listViewParticipants.Tag != null)
            {
                int No = (int)listViewParticipants.Tag;
                listViewParticipants.Items[No].SubItems[0].BackColor = SystemColors.Window;
                listViewParticipants.Items[No].SubItems[0].ForeColor = Color.Gray;
                No++;
                if (No < listViewParticipants.Items.Count)
                {   //Step to next participant
                    listViewParticipants.Items[No].Selected = true;
                }
                else
                {   //No more participants. Unload music
                    closeWaveOutParticipant();
                    enableButtons();
                }
            }
        }

        private void nextWarmup()
        {
            int No = listViewWarmupMusic.SelectedItems[0].Index;
            No++;
            if (No >= listViewWarmupMusic.Items.Count) No = 0;
            //Step to next file
            listViewWarmupMusic.Items[No].Selected = true;
        }

        private void nextBreak()
        {
            int No = listViewBreakMusic.SelectedItems[0].Index;
            No++;
            if (No >= listViewBreakMusic.Items.Count) No = 0;
            //Step to next file
            listViewBreakMusic.Items[No].Selected = true;
        }

        #endregion
        #region OnPlaybackStopped
        void onPlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (e.Exception != null)
            {
                MessageBox.Show(e.Exception.Message, Properties.Resources.PLAYBACK_DEVICE_ERROR);
            }
            if (audioFileReaderParticipant != null)
            {
                // Reset music
                audioFileReaderParticipant.Position = 0;
                volumeMeter1.Amplitude = 0;
                volumeMeter2.Amplitude = 0;

                if (!stopPressed)  // Has the music ended? (That is, we haven't pressed the stop button)
                {
                    if (pausePlaying)  // We were playing pause music. Restart!
                    {
                        waveOutDeviceParticipant.Play();
                    }
                    else
                    {
                        if (checkBoxAutoPauseMusic.Checked)  // Should we play automatic pausemusic?
                        {
                            volumeSlider1.Volume = volumeSliderPause.Volume;
                            pausePlaying = true;
                            fadeInOut.BeginFadeIn((double)numericUpDownPause.Value * 1000.0);
                            waveOutDeviceParticipant.Play();
                        }
                        else
                        {
                            // Change to next participants music
                            nextParticipant();
                        }
                    }
                }
                else  // We have pressed stop!
                {
                    if (pausePlaying)
                    {
                        nextParticipant();
                    }
                }
            }
            stopPressed = false;  // Mark that we have handled the stop button.
        }

        void onPlaybackStoppedWarmup(object sender, StoppedEventArgs e)
        {
            if (e.Exception != null)
            {
                MessageBox.Show(e.Exception.Message, Properties.Resources.PLAYBACK_DEVICE_ERROR);
            }
            if (audioFileReaderWarmup != null)
            {
                // Nollställ låt
                audioFileReaderWarmup.Position = 0;
                volumeMeter1.Amplitude = 0;
                volumeMeter2.Amplitude = 0;

                if (!stopPressed)  // Has the music ended? (That is, we haven't pressed the stop button)
                {
                    // Change to next music
                    nextWarmup();
                    waveOutDeviceWarmup.Play();
                    enableButtons();
                }
            }
            stopPressed = false;  // Mark that we have handled the stop button.
        }

        void onPlaybackStoppedBreak(object sender, StoppedEventArgs e)
        {
            if (e.Exception != null)
            {
                _ = MessageBox.Show(e.Exception.Message, Properties.Resources.PLAYBACK_DEVICE_ERROR);
            }
            if (audioFileReaderBreak != null)
            {
                // Nollställ låt
                audioFileReaderBreak.Position = 0;
                volumeMeter1.Amplitude = 0;
                volumeMeter2.Amplitude = 0;

                if (!stopPressed)  // Has the music ended? (That is, we haven't pressed the stop button)
                {
                    // Change to next music
                    nextBreak();
                    waveOutDeviceBreak.Play();
                    enableButtons();
                }
            }
            stopPressed = false;  // Mark that we have handled the stop button.
        }
        #endregion
        #region Buttons

        private void enableButtons()
        {
            player WhatsPlaying = player.Nothing;

            //What is playing?
            if (waveOutDeviceWarmup != null && audioFileReaderWarmup != null)
            {
                if (waveOutDeviceWarmup.PlaybackState != PlaybackState.Stopped)
                {
                    WhatsPlaying = player.Warmup;
                }
            }
            if (waveOutDeviceBreak != null || audioFileReaderBreak != null)
            {
                if (waveOutDeviceBreak.PlaybackState != PlaybackState.Stopped)
                {
                    WhatsPlaying = player.Break;
                }
            }
            if (waveOutDeviceParticipant != null && audioFileReaderParticipant != null)
            {
                if (waveOutDeviceParticipant.PlaybackState != PlaybackState.Stopped)
                {
                    WhatsPlaying = player.Participant;
                }
            }


            //Enable/Disable  group boxes if they are active or if nothing else is playing
            groupBoxWarmupMusic.Enabled = (WhatsPlaying == player.Nothing || WhatsPlaying == player.Warmup);
            groupBoxBreakMusic.Enabled = (WhatsPlaying == player.Nothing || WhatsPlaying == player.Break);


            // Do we have a warmup music loaded?
            if (waveOutDeviceWarmup == null || audioFileReaderWarmup == null)
            {//No music ready
                //Disable all buttons
                buttonWarmupPlay.Enabled = false;
                buttonWarmupPause.Enabled = false;
                buttonWarmupStop.Enabled = false;
            }
            else
            {//Yes!
                if (waveOutDeviceWarmup.PlaybackState == PlaybackState.Playing)
                {
                    buttonWarmupPlay.Enabled = false;
                    buttonWarmupPause.Enabled = true;
                    buttonWarmupStop.Enabled = true;
                }
                else if (waveOutDeviceWarmup.PlaybackState == PlaybackState.Paused)
                {
                    buttonWarmupPlay.Enabled = true;
                    buttonWarmupPause.Enabled = false;
                    buttonWarmupStop.Enabled = true;
                }
                else if (waveOutDeviceWarmup.PlaybackState == PlaybackState.Stopped)
                {
                    buttonWarmupPlay.Enabled = true;
                    buttonWarmupPause.Enabled = false;
                    buttonWarmupStop.Enabled = false;
                }
            }

            // Do we have a Break music loaded?
            if (waveOutDeviceBreak == null || audioFileReaderBreak == null)
            {//No music ready
                //Disable all buttons
                buttonBreakPlay.Enabled = false;
                buttonBreakPause.Enabled = false;
                buttonBreakStop.Enabled = false;
            }
            else
            {//Yes!
                if (waveOutDeviceBreak.PlaybackState == PlaybackState.Playing)
                {
                    buttonBreakPlay.Enabled = false;
                    buttonBreakPause.Enabled = true;
                    buttonBreakStop.Enabled = true;
                }
                else if (waveOutDeviceBreak.PlaybackState == PlaybackState.Paused)
                {
                    buttonBreakPlay.Enabled = true;
                    buttonBreakPause.Enabled = false;
                    buttonBreakStop.Enabled = true;
                }
                else if (waveOutDeviceBreak.PlaybackState == PlaybackState.Stopped)
                {
                    buttonBreakPlay.Enabled = true;
                    buttonBreakPause.Enabled = false;
                    buttonBreakStop.Enabled = false;
                }
            }


            // Do we have a participants music loaded?
            if (waveOutDeviceParticipant == null || audioFileReaderParticipant == null)
            {//No music ready
                //Disable all buttons
                buttonPlay.Enabled = false;
                buttonPause.Enabled = false;
                buttonStop.Enabled = false;
            }
            else
            {//Yes!
                if (waveOutDeviceParticipant.PlaybackState == PlaybackState.Playing)
                {
                    buttonPlay.Enabled = false;
                    buttonPause.Enabled = true;
                    buttonStop.Enabled = true;
                }
                else if (waveOutDeviceParticipant.PlaybackState == PlaybackState.Paused)
                {
                    buttonPlay.Enabled = true;
                    buttonPause.Enabled = false;
                    buttonStop.Enabled = true;
                }
                else if (waveOutDeviceParticipant.PlaybackState == PlaybackState.Stopped)
                {
                    buttonPlay.Enabled = true;
                    buttonPause.Enabled = false;
                    buttonStop.Enabled = false;
                }

            }

        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceParticipant != null && audioFileReaderParticipant != null)
            {
                // Stop all other units playing!
                if (waveOutDeviceWarmup != null && waveOutDeviceWarmup.PlaybackState == PlaybackState.Playing)
                {
                    stopPressed = true;  // Mark that we manually stopped the playback
                    waveOutDeviceWarmup.Stop();
                }
                if (waveOutDeviceBreak != null && waveOutDeviceBreak.PlaybackState == PlaybackState.Playing)
                {
                    stopPressed = true;  // Mark that we manually stopped the playback
                    waveOutDeviceBreak.Stop();
                }

                // Start participants music
                waveOutDeviceParticipant.Play();
                enableButtons();
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceParticipant != null)
            {
                if (waveOutDeviceParticipant.PlaybackState == PlaybackState.Playing)
                {
                    waveOutDeviceParticipant.Pause();
                    enableButtons();
                }
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceParticipant != null)
            {
                stopPressed = true; // markera att vi har tryckt på stopknappen så inte automatiken fortsätter...
                waveOutDeviceParticipant.Stop();
                enableButtons();
            }
        }

        private void buttonWarmupPlay_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceWarmup != null && audioFileReaderWarmup != null)
            {
                if (waveOutDeviceBreak != null && waveOutDeviceBreak.PlaybackState == PlaybackState.Playing)
                {
                    stopPressed = true;  // Mark that we manually stopped the playback
                    waveOutDeviceBreak.Stop();
                }
                waveOutDeviceWarmup.Play();
                enableButtons();
            }
        }

        private void buttonWarmupPause_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceWarmup != null)
            {
                if (waveOutDeviceWarmup.PlaybackState == PlaybackState.Playing)
                {
                    waveOutDeviceWarmup.Pause();
                    enableButtons();
                }
            }
        }

        private void buttonWarmupStop_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceWarmup != null)
            {
                stopPressed = true; // markera att vi har tryckt på stopknappen så inte automatiken fortsätter...
                waveOutDeviceWarmup.Stop();
                enableButtons();
            }
        }

        private void buttonBreakPlay_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceBreak != null && audioFileReaderBreak != null)
            {
                if (waveOutDeviceWarmup != null && waveOutDeviceWarmup.PlaybackState == PlaybackState.Playing)
                {
                    stopPressed = true;  // Mark that we manually stopped the playback
                    waveOutDeviceWarmup.Stop();
                }
                waveOutDeviceBreak.Play();
                enableButtons();
            }
        }

        private void buttonBreakPause_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceBreak != null)
            {
                if (waveOutDeviceBreak.PlaybackState == PlaybackState.Playing)
                {
                    waveOutDeviceBreak.Pause();
                    enableButtons();
                }
            }
        }

        private void buttonBreakStop_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceBreak != null)
            {
                stopPressed = true; // markera att vi har tryckt på stopknappen så inte automatiken fortsätter...
                waveOutDeviceBreak.Stop();
                enableButtons();
            }
        }

        #endregion
        void onPostVolumeMeter(object sender, StreamVolumeEventArgs e)
        {
            // we know it is stereo
            volumeMeter1.Amplitude = e.MaxSampleValues[0];
            volumeMeter2.Amplitude = e.MaxSampleValues[1];
        }

        private void volumeSlider1_VolumeChanged(object sender, EventArgs e)
        {
            setVolumeDelegateParticipant?.Invoke(volumeSlider1.Volume);
            setVolumeDelegateWarmup?.Invoke(volumeSlider1.Volume);
            setVolumeDelegateBreak?.Invoke(volumeSlider1.Volume);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (waveOutDeviceParticipant != null && audioFileReaderParticipant != null)
                {
                    //TimeSpan currentTime = (waveOutDevice.PlaybackState == PlaybackState.Stopped) ? TimeSpan.Zero : audioFileReader.CurrentTime;
                    TimeSpan currentTime = audioFileReaderParticipant.CurrentTime;
                    trackBarPosition.Value = Math.Min(trackBarPosition.Maximum, (int)(100 * currentTime.TotalSeconds / audioFileReaderParticipant.TotalTime.TotalSeconds));
                    labelCurrentTime.Text = string.Format("{0:00}:{1:00}", (int)currentTime.TotalMinutes, currentTime.Seconds);
                }
                else
                {
                    trackBarPosition.Value = 0;
                }

                // Update statuslabel
                if (string.IsNullOrEmpty(toolStripStatusLabel1.Text))
                {
                    if (waveOutDeviceParticipant != null && waveOutDeviceParticipant.PlaybackState == PlaybackState.Playing)
                    {// We're playing participants music
                        if (pausePlaying)
                        {//Playing pausemusic
                            toolStripStatusLabel1.Text = Properties.Resources.PLAYING_PAUSE_MUSIC;
                        }
                        else
                        {//Playing participants music
                            toolStripStatusLabel1.Text = Properties.Resources.PLAYING_PARTICIPANT_MUSIC;
                        }
                    }
                    else if (waveOutDeviceWarmup != null && waveOutDeviceWarmup.PlaybackState == PlaybackState.Playing)
                    {
                        toolStripStatusLabel1.Text = Properties.Resources.PLAYING_WARMUP_MUSIC;
                    }
                    else if (waveOutDeviceBreak != null && waveOutDeviceBreak.PlaybackState == PlaybackState.Playing)
                    {
                        toolStripStatusLabel1.Text = Properties.Resources.PLAYING_BREAK_MUSIC;
                    }
                }
                else
                {
                    toolStripStatusLabel1.Text = string.Empty;
                }
            }
            catch (Exception)
            {
                //Do nothing
            }
        }

        private void trackBarPosition_Scroll(object sender, EventArgs e)
        {
            if (waveOutDeviceParticipant != null)
            {
                audioFileReaderParticipant.CurrentTime = TimeSpan.FromSeconds(audioFileReaderParticipant.TotalTime.TotalSeconds * trackBarPosition.Value / 100.0);
            }
        }


        #region ListViewSelected
        private void listViewParticipants_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listViewParticipants.SelectedItems.Count == 1)
                {
                    if (listViewParticipants.Tag != null)
                    {
                        listViewParticipants.Items[(int)listViewParticipants.Tag].SubItems[0].BackColor = SystemColors.Window;
                        listViewParticipants.Items[(int)listViewParticipants.Tag].SubItems[0].ForeColor = Color.Gray;
                        listViewParticipants.Tag = null;
                    }

                    createWaveOutParticipant();

                    //Check that there is a file in the grid. Gives a strange error message it we let AudioFileReader throw exception
                    if (string.IsNullOrEmpty(listViewParticipants.SelectedItems[0].SubItems[4].Text))
                    {
                        throw new Exception(Properties.Resources.CAPTION_NO_FILE_FOR_PARTICIPANT);
                    }


                    audioFileReaderParticipant = new AudioFileReader(listViewParticipants.SelectedItems[0].SubItems[4].Text);
                    var sampleChannel = new SampleChannel(audioFileReaderParticipant, true);
                    this.setVolumeDelegateParticipant = (vol) => sampleChannel.Volume = vol;
                    var postVolumeMeter = new MeteringSampleProvider(sampleChannel);
                    postVolumeMeter.StreamVolume += onPostVolumeMeter;
                    fadeInOut = new FadeInOutSampleProvider(postVolumeMeter, false);

                    labelTotalTime.Text = $"{(int)audioFileReaderParticipant.TotalTime.TotalMinutes:00}:{audioFileReaderParticipant.TotalTime.Seconds:00}";

                    //waveOutDevice.Init(postVolumeMeter);
                    waveOutDeviceParticipant.Init(fadeInOut);
                    volumeSlider1.Volume = 1.0f;
                    pausePlaying = false;

                    listViewParticipants.Tag = listViewParticipants.SelectedItems[0].Index;
                    listViewParticipants.SelectedItems[0].BackColor = Color.LightGreen;
                    listViewParticipants.SelectedItems[0].EnsureVisible();
                    listViewParticipants.SelectedItems[0].Selected = false;

                    enableButtons();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't load music\n" + listViewParticipants.SelectedItems[0].SubItems[4].Text + "\n\n" + ex.Message, Properties.Resources.CAPTION_ERROR_LOADING_MUSIC, MessageBoxButtons.OK);
            }
        }

        private void listViewWarmupMusic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewWarmupMusic.SelectedItems.Count == 1)
            {
                try
                {
                    createWaveOutWarmup();

                    audioFileReaderWarmup = new AudioFileReader(listViewWarmupMusic.SelectedItems[0].SubItems[1].Text);
                    var sampleChannel = new SampleChannel(audioFileReaderWarmup, true);
                    this.setVolumeDelegateWarmup = (vol) => sampleChannel.Volume = vol;
                    var postVolumeMeter = new MeteringSampleProvider(sampleChannel);
                    postVolumeMeter.StreamVolume += onPostVolumeMeter;

                    //waveOutDevice.Init(postVolumeMeter);
                    waveOutDeviceWarmup.Init(postVolumeMeter);
                    volumeSlider1.Volume = 1.0f;
                }
                catch (Exception)
                {
                    //Do nothing!
                }

                listViewWarmupMusic.SelectedItems[0].EnsureVisible();

                enableButtons();
            }
        }

        private void listViewBreakMusic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewBreakMusic.SelectedItems.Count == 1)
            {
                try
                {
                    createWaveOutBreak();

                    audioFileReaderBreak = new AudioFileReader(listViewBreakMusic.SelectedItems[0].SubItems[1].Text);
                    var sampleChannel = new SampleChannel(audioFileReaderBreak, true);
                    this.setVolumeDelegateBreak = (vol) => sampleChannel.Volume = vol;
                    var postVolumeMeter = new MeteringSampleProvider(sampleChannel);
                    postVolumeMeter.StreamVolume += onPostVolumeMeter;

                    //waveOutDevice.Init(postVolumeMeter);
                    waveOutDeviceBreak.Init(postVolumeMeter);
                    volumeSlider1.Volume = 1.0f;
                }
                catch (Exception)
                {
                    //Do nothing!
                }

                listViewBreakMusic.SelectedItems[0].EnsureVisible();

                enableButtons();
            }
        }

        #endregion

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadParticipants(doc, comboBoxCategory.SelectedItem.ToString(), listViewParticipants);
            if (listViewParticipants.Items.Count != 0)
            {// Select first participant
                listViewParticipants.Items[0].Selected = true;
            }
            buttonPlay.Focus();
        }

        #region MenuItems
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.QUESTION_DELETE_COMPETITION, Properties.Resources.CAPTION_NEW_COMPETITION, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                // Remove all categories from XML tree
                while (doc.DocumentElement.HasChildNodes)
                {
                    // Remove category from XML tree
                    doc.DocumentElement.RemoveChild(doc.DocumentElement.FirstChild);
                    doc.DocumentElement.SetAttribute("Name", "New event");
                    doc.Save(Properties.Resources.XML_FILENAME);
                    loadXMLfile();
                }
            }

        }

        private void editEventMenuItem_Click(object sender, EventArgs e)
        {
            using (FormEditEvent EC = new FormEditEvent(doc))
            {
                if (EC.ShowDialog() == DialogResult.OK)
                {
                    loadXMLfile();
                }
            }
        }

        private void editCategoriesMenuItem_Click(object sender, EventArgs e)
        {
            using (formEditCategories EC = new formEditCategories(doc))
            {
                if (EC.ShowDialog() == DialogResult.OK)
                {
                    loadXMLfile();
                }
            }
        }

        private void editParticipantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string category = string.Empty;
            if (comboBoxCategory.SelectedItem != null)
            {
                category = comboBoxCategory.SelectedItem.ToString();
            }
            using (FormEditParticipants ES = new FormEditParticipants(this, category))
            {
                if (ES.ShowDialog() == DialogResult.OK)
                {
                    loadXMLfile();
                    try
                    {//Try to reload category
                        comboBoxCategory.SelectedIndex = comboBoxCategory.FindStringExact(category);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formMusicPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.QUESTION_END_PROGRAM, Properties.Resources.CAPTION_END_PROGRAM, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void importFromIndTA2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogIndTA.ShowDialog() == DialogResult.OK)
            {
                loadIndTA2(doc, openFileDialogIndTA.FileName);
                loadXMLfile();
                _ = MessageBox.Show(Properties.Resources.QUESTION_IMPORTED_VERIFY_SHORT, Properties.Resources.CAPTION_FILE_IMPORTED, MessageBoxButtons.OK, MessageBoxIcon.Information);
                editCategoriesMenuItem_Click(sender, e);
            }
        }

        private void importFromISUCalcFSXMLtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogISUCalcXML.ShowDialog() == DialogResult.OK)
            {
                loadISUCalcXML(doc, openFileDialogISUCalcXML.FileName);
                loadXMLfile();
                MessageBox.Show(Properties.Resources.QUESTION_IMPORTED_VERIFY_SHORT, Properties.Resources.CAPTION_FILE_IMPORTED, MessageBoxButtons.OK, MessageBoxIcon.Information);
                editCategoriesMenuItem_Click(sender, e);
            }
        }

        private void importFromClubcomp2016ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogCC.ShowDialog() == DialogResult.OK)
            {
                loadClubStarComp2016(doc, folderBrowserDialogCC.SelectedPath);
                loadXMLfile();
                _ = MessageBox.Show(Properties.Resources.QUESTION_IMPORTED_VERIFY_SHORT, Properties.Resources.CAPTION_FILE_IMPORTED, MessageBoxButtons.OK, MessageBoxIcon.Information);
                editCategoriesMenuItem_Click(sender, e);
            }
        }

        private void unzipMusicfiletoolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get Filename to unpack
            if (openFileDialogMusicarchive.ShowDialog() == DialogResult.OK)
            {
                int NumberOfFiles = 0;

                // Unzip the file
                NumberOfFiles = unzipMusicFiles(NumberOfFiles, openFileDialogMusicarchive.FileName);

                // Notify, and ask if autoconnect
                if (NumberOfFiles == 0)
                {
                    MessageBox.Show(Properties.Resources.NO_MUSIC_FILES_UNZIPPED, Properties.Resources.CAPTION_NO_FILES_UNZIPPED, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                if (MessageBox.Show(NumberOfFiles.ToString() + " Music files are now unpacked. You can autoconnect music to participants if you want\n\nDo you want to autoconnect?", Properties.Resources.CAPTION_FILES_UNZIPPED, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    autoconnectMusicToParticipantsToolStripMenuItem_Click(sender, e);
                }
            }
        }

        private void autoconnectMusicToParticipantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string category = string.Empty;
            if (comboBoxCategory.SelectedItem != null)
            {
                category = comboBoxCategory.SelectedItem.ToString();
            }
            autoconnectMusic();
            loadXMLfile();
            try
            {//Try to reload category
                comboBoxCategory.SelectedIndex = comboBoxCategory.FindStringExact(category);
            }
            catch (Exception)
            {
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (formOptions FO = new formOptions())
            {
                if (FO.ShowDialog() == DialogResult.OK)
                {
                    loadSettings();
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox.aboutBox ab = new AboutBox.aboutBox())
            {
                ab.ShowDialog();
            }
        }
        #endregion
    }
}
