using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
//using NAudio.Gui;
using SpotifyAPI.Local;
using SpotifyAPI.Local.Enums;
using SpotifyAPI.Local.Models;
using AboutBox;

namespace Skaters_MusicPlayer
{
    public partial class FormMusicPlayer : Form
    {
        enum Player { Nothing, Skater, Warmup, Break, Spotify };

        public XmlDocument doc = new XmlDocument();

        private IWavePlayer waveOutDeviceSkater = null;
        private AudioFileReader audioFileReaderSkater = null;
        private FadeInOutSampleProvider fadeInOut;
        private Action<float> setVolumeDelegateSkater;
        private bool PausePlaying = false;
        private bool StopPressed = false;

        private IWavePlayer waveOutDeviceWarmup = null;
        private AudioFileReader audioFileReaderWarmup = null;
        private Action<float> setVolumeDelegateWarmup;
        private IWavePlayer waveOutDeviceBreak = null;
        private AudioFileReader audioFileReaderBreak = null;
        private Action<float> setVolumeDelegateBreak;

        private readonly SpotifyLocalAPI spotify;
        private bool spotifyConnected;


        public FormMusicPlayer()
        {
            InitializeComponent();

            labelCurrentTime.Text = "";
            labelTotalTime.Text = "";

            try
            {
                // Do we have Spotify?
                spotify = new SpotifyLocalAPI();
                spotifyConnected = false;
                spotify.OnTrackChange += spotify_OnTrackChange;
                spotify.OnPlayStateChange += spotify_OnPlayStateChange;
                SpotifyConnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Can't connect to Spotify!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadSettings();

            LoadXMLfile();

        }

        // Load settings from config-file
        private void LoadSettings()
        {
            // Load PauseMusic settings
            checkBoxAutoPauseMusic.Checked = (GetConfigurationValue("PauseMusicEnabled", "TRUE").ToUpper() == "TRUE");
            try
            {   //Try do parse delay value.
                numericUpDownPause.Value = Decimal.Parse(GetConfigurationValue("PauseMusicDelay", "0"));
            }
            catch (Exception)
            {//Do nothing
            }
            try
            {   //Try do parse volume value.
                volumeSliderPause.Volume = float.Parse(GetConfigurationValue("PauseVolume", "0,1"));
            }
            catch (Exception)
            {//Do nothing
            }


            // Load music folders for Warmup music and Break music
            LoadMusicFolder(GetConfigurationValue("WarmupMusicDirectory", ""), true, listViewWarmupMusic);
            LoadMusicFolder(GetConfigurationValue("BreakMusicDirectory", ""), true, listViewBreakMusic);

            try
            {
                // Connected to Spotify? Load playlist
                if (spotifyConnected)
                {
                    string URI = GetConfigurationValue("SpotifyURI", "");
                    if (URI != String.Empty)
                    {
                        spotify.Mute();             //Turn off sound (will sometimes make a small "click" when loading a URI
                        spotify.PlayURL(URI, "");   //Loading will also start playing
                        spotify.Pause();            //So we stop it directly
                        spotify.UnMute();           //Turn on sound again
                    }
                    SpotifyUpdateTrack(spotify.GetStatus().Track);
                    spotify.ListenForEvents = true;
                    EnableButtons();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Can't load playlist!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region Spotify
        private void SpotifyConnect()
        {
            if (SpotifyLocalAPI.IsSpotifyRunning() && SpotifyLocalAPI.IsSpotifyWebHelperRunning())
            {
                // Try to connect to Spotify client
                spotifyConnected = spotify.Connect();
                if (!spotifyConnected)
                {
                    MessageBox.Show("Couldn't connect to the spotify client", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (spotifyConnected)
            {
                buttonSpotifyPlay.Visible = true;
                buttonSpotifyPrevious.Visible = true;
                buttonSpotifyNext.Visible = true;
                buttonSpotifyStop.Visible = true;
                groupBoxSpotifyMusic.Enabled = true;

            }
            else
            {
                groupBoxSpotifyMusic.Text = "Spotify not running!";
                tbTitle.Text = "Spotify not running!";
                buttonSpotifyPlay.Visible = false;
                buttonSpotifyPrevious.Visible = false;
                buttonSpotifyNext.Visible = false;
                buttonSpotifyStop.Visible = false;
                groupBoxSpotifyMusic.Enabled = false;

                //Shrink spotify box and expand Warmup/Break boxes
                groupBoxSpotifyMusic.Height = groupBoxSpotifyMusic.Height - 162;
                groupBoxSpotifyMusic.Top = groupBoxSpotifyMusic.Top + 162;
                groupBoxWarmupMusic.Height = groupBoxWarmupMusic.Height + 162;
                groupBoxBreakMusic.Height = groupBoxBreakMusic.Height + 162;
            }
        }

        public async void SpotifyUpdateTrack(Track track)
        {
            if (track == null)
            {
                return;  // Doesn't have track info
            }

            if (track.IsAd())
            {
                tbTitle.Text = "ADVERT";
                tbArtist.Text = string.Empty;
                tbAlbum.Text = string.Empty;
                pbWarmup.Hide();
                return; // Don't process. maybe null values
            }

            //createdLabel.Invoke((Action)delegate { createdLabel.Text = text; });
            tbTitle.Text = track.TrackResource.Name;
            tbArtist.Text = track.ArtistResource.Name;
            tbAlbum.Text = track.AlbumResource.Name;

            pbWarmup.Image = await track.GetAlbumArtAsync(AlbumArtSize.Size160);
            pbWarmup.Show();
        }

        void spotify_OnTrackChange(object sender, TrackChangeEventArgs e)
        {
            tbAlbum.Invoke((Action)delegate { SpotifyUpdateTrack(e.NewTrack); });
        }
        void spotify_OnPlayStateChange(object sender, PlayStateEventArgs e)
        {
            tbAlbum.Invoke((Action)delegate { EnableButtons(); });
        }



        #endregion
        #region CreateWaveOut
        private void CreateWaveOutSkater()
        {
            CloseWaveOutSkater();

            waveOutDeviceSkater = new WaveOut();
            waveOutDeviceSkater.PlaybackStopped += OnPlaybackStopped;
        }

        private void CreateWaveOutWarmup()
        {
            CloseWaveOutWarmup();

            waveOutDeviceWarmup = new WaveOut();
            waveOutDeviceWarmup.PlaybackStopped += OnPlaybackStoppedWarmup;
        }

        private void CreateWaveOutBreak()
        {
            CloseWaveOutBreak();

            waveOutDeviceBreak = new WaveOut();
            waveOutDeviceBreak.PlaybackStopped += OnPlaybackStoppedBreak;
        }

        #endregion
        #region CloseWaveOut
        private void CloseWaveOutSkater()
        {
            if (waveOutDeviceSkater != null)
            {
                waveOutDeviceSkater.Stop();
            }
            if (audioFileReaderSkater != null)
            {
                // this one really closes the file and ACM conversion
                audioFileReaderSkater.Dispose();
                setVolumeDelegateSkater = null;
                audioFileReaderSkater = null;
            }
            if (waveOutDeviceSkater != null)
            {
                waveOutDeviceSkater.Dispose();
                waveOutDeviceSkater = null;
            }
        }

        private void CloseWaveOutWarmup()
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

        private void CloseWaveOutBreak()
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
        private void NextSkater()
        {
            if (listViewSkaters.Tag != null)
            {
                int No = (int)listViewSkaters.Tag;
                listViewSkaters.Items[No].SubItems[0].BackColor = SystemColors.Window;
                listViewSkaters.Items[No].SubItems[0].ForeColor = Color.Gray;
                No++;
                if (No < listViewSkaters.Items.Count)
                {   //Step to next skater
                    listViewSkaters.Items[No].Selected = true;
                }
                else
                {   //No more skaters. Unload music
                    CloseWaveOutSkater();
                    EnableButtons();
                }
            }
        }

        private void NextWarmup()
        {
            int No = listViewWarmupMusic.SelectedItems[0].Index;
            No++;
            if (No >= listViewWarmupMusic.Items.Count) No = 0;
            //Step to next file
            listViewWarmupMusic.Items[No].Selected = true;
        }

        private void NextBreak()
        {
            int No = listViewBreakMusic.SelectedItems[0].Index;
            No++;
            if (No >= listViewBreakMusic.Items.Count) No = 0;
            //Step to next file
            listViewBreakMusic.Items[No].Selected = true;
        }

        #endregion
        #region OnPlaybackStopped
        void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (e.Exception != null)
            {
                MessageBox.Show(e.Exception.Message, "Playback Device Error");
            }
            if (audioFileReaderSkater != null)
            {
                // Reset music
                audioFileReaderSkater.Position = 0;
                volumeMeter1.Amplitude = 0;
                volumeMeter2.Amplitude = 0;

                if (!StopPressed)  // Has the music ended? (That is, we haven't pressed the stop button)
                {
                    if (PausePlaying)  // We were playing pause music. Restart!
                    {
                        waveOutDeviceSkater.Play();
                    }
                    else
                    {
                        if (checkBoxAutoPauseMusic.Checked)  // Should we play automatic pausemusic?
                        {
                            volumeSlider1.Volume = volumeSliderPause.Volume;
                            PausePlaying = true;
                            fadeInOut.BeginFadeIn((double)numericUpDownPause.Value * 1000.0);
                            waveOutDeviceSkater.Play();
                        }
                        else
                        {
                            // Change to next skaters music
                            NextSkater();
                        }
                    }
                }
                else  // We have pressed stop!
                {
                    if (PausePlaying)
                    {
                        NextSkater();
                    }
                }
            }
            StopPressed = false;  // Mark that we have handled the stop button.
        }

        void OnPlaybackStoppedWarmup(object sender, StoppedEventArgs e)
        {
            if (e.Exception != null)
            {
                MessageBox.Show(e.Exception.Message, "Playback Device Error");
            }
            if (audioFileReaderWarmup != null)
            {
                // Nollställ låt
                audioFileReaderWarmup.Position = 0;
                volumeMeter1.Amplitude = 0;
                volumeMeter2.Amplitude = 0;

                if (!StopPressed)  // Has the music ended? (That is, we haven't pressed the stop button)
                {
                    // Change to next music
                    NextWarmup();
                    waveOutDeviceWarmup.Play();
                    EnableButtons();
                }
            }
            StopPressed = false;  // Mark that we have handled the stop button.
        }

        void OnPlaybackStoppedBreak(object sender, StoppedEventArgs e)
        {
            if (e.Exception != null)
            {
                MessageBox.Show(e.Exception.Message, "Playback Device Error");
            }
            if (audioFileReaderBreak != null)
            {
                // Nollställ låt
                audioFileReaderBreak.Position = 0;
                volumeMeter1.Amplitude = 0;
                volumeMeter2.Amplitude = 0;

                if (!StopPressed)  // Has the music ended? (That is, we haven't pressed the stop button)
                {
                    // Change to next music
                    NextBreak();
                    waveOutDeviceBreak.Play();
                    EnableButtons();
                }
            }
            StopPressed = false;  // Mark that we have handled the stop button.
        }
        #endregion
        #region Buttons

        private void EnableButtons()
        {
            Player WhatsPlaying = Player.Nothing;
            PlaybackState PlayerState = PlaybackState.Stopped;

            //What is playing?
            if (waveOutDeviceWarmup != null && audioFileReaderWarmup != null)
            {
                if (waveOutDeviceWarmup.PlaybackState != PlaybackState.Stopped)
                {
                    WhatsPlaying = Player.Warmup;
                    PlayerState = waveOutDeviceWarmup.PlaybackState;
                }
            }
            if (waveOutDeviceBreak != null || audioFileReaderBreak != null)
            {
                if (waveOutDeviceBreak.PlaybackState != PlaybackState.Stopped)
                {
                    WhatsPlaying = Player.Break;
                    PlayerState = waveOutDeviceBreak.PlaybackState;
                }
            }
            if (spotifyConnected)
            {
                if (spotify.GetStatus().Playing)
                {
                    WhatsPlaying = Player.Spotify;
                    PlayerState = PlaybackState.Playing;
                }
            }
            if (waveOutDeviceSkater != null && audioFileReaderSkater != null)
            {
                if (waveOutDeviceSkater.PlaybackState != PlaybackState.Stopped)
                {
                    WhatsPlaying = Player.Skater;
                    PlayerState = waveOutDeviceSkater.PlaybackState;
                }
            }


            //Enable/Disable  group boxes if they are active or if nothing else is playing
            groupBoxWarmupMusic.Enabled = (WhatsPlaying == Player.Nothing || WhatsPlaying == Player.Warmup);
            groupBoxBreakMusic.Enabled = (WhatsPlaying == Player.Nothing || WhatsPlaying == Player.Break);
            groupBoxSpotifyMusic.Enabled = (WhatsPlaying == Player.Nothing || WhatsPlaying == Player.Spotify);


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

            // Do we have a Spotify?
            if (!spotifyConnected)
            {//No music ready
                //Disable all buttons
                buttonSpotifyPlay.Enabled = false;
                buttonSpotifyPrevious.Enabled = false;
                buttonSpotifyNext.Enabled = false;
                buttonSpotifyStop.Enabled = false;
            }
            else
            {//Yes!
                if (spotify.GetStatus().Playing)
                {
                    buttonSpotifyPlay.Enabled = false;
                    buttonSpotifyPrevious.Enabled = true;
                    buttonSpotifyNext.Enabled = true;
                    buttonSpotifyStop.Enabled = true;
                }
                else
                {
                    buttonSpotifyPlay.Enabled = true;
                    buttonSpotifyPrevious.Enabled = false;
                    buttonSpotifyNext.Enabled = false;
                    buttonSpotifyStop.Enabled = false;
                }
            }


            // Do we have a skaters music loaded?
            if (waveOutDeviceSkater == null || audioFileReaderSkater == null)
            {//No music ready
                //Disable all buttons
                buttonPlay.Enabled = false;
                buttonPause.Enabled = false;
                buttonStop.Enabled = false;
            }
            else
            {//Yes!
                if (waveOutDeviceSkater.PlaybackState == PlaybackState.Playing)
                {
                    buttonPlay.Enabled = false;
                    buttonPause.Enabled = true;
                    buttonStop.Enabled = true;
                }
                else if (waveOutDeviceSkater.PlaybackState == PlaybackState.Paused)
                {
                    buttonPlay.Enabled = true;
                    buttonPause.Enabled = false;
                    buttonStop.Enabled = true;
                }
                else if (waveOutDeviceSkater.PlaybackState == PlaybackState.Stopped)
                {
                    buttonPlay.Enabled = true;
                    buttonPause.Enabled = false;
                    buttonStop.Enabled = false;
                }

            }

        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceSkater != null && audioFileReaderSkater != null)
            {
                // Stop all other units playing!
                if (waveOutDeviceWarmup != null && waveOutDeviceWarmup.PlaybackState == PlaybackState.Playing)
                {
                    StopPressed = true;  // Mark that we manually stopped the playback
                    waveOutDeviceWarmup.Stop();
                }
                if (waveOutDeviceBreak != null && waveOutDeviceBreak.PlaybackState == PlaybackState.Playing)
                {
                    StopPressed = true;  // Mark that we manually stopped the playback
                    waveOutDeviceBreak.Stop();
                }
                if (spotifyConnected && spotify.GetStatus().Playing)
                {
                    spotify.Pause();
                }

                // Start skaters music
                waveOutDeviceSkater.Play();
                EnableButtons();
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceSkater != null)
            {
                if (waveOutDeviceSkater.PlaybackState == PlaybackState.Playing)
                {
                    waveOutDeviceSkater.Pause();
                    EnableButtons();
                }
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceSkater != null)
            {
                StopPressed = true; // markera att vi har tryckt på stopknappen så inte automatiken fortsätter...
                waveOutDeviceSkater.Stop();
                EnableButtons();
            }
        }

        private void buttonWarmupPlay_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceWarmup != null && audioFileReaderWarmup != null)
            {
                if (waveOutDeviceBreak != null && waveOutDeviceBreak.PlaybackState == PlaybackState.Playing)
                {
                    StopPressed = true;  // Mark that we manually stopped the playback
                    waveOutDeviceBreak.Stop();
                }
                waveOutDeviceWarmup.Play();
                EnableButtons();
            }
        }

        private void buttonWarmupPause_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceWarmup != null)
            {
                if (waveOutDeviceWarmup.PlaybackState == PlaybackState.Playing)
                {
                    waveOutDeviceWarmup.Pause();
                    EnableButtons();
                }
            }
        }

        private void buttonWarmupStop_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceWarmup != null)
            {
                StopPressed = true; // markera att vi har tryckt på stopknappen så inte automatiken fortsätter...
                waveOutDeviceWarmup.Stop();
                EnableButtons();
            }
        }

        private void buttonBreakPlay_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceBreak != null && audioFileReaderBreak != null)
            {
                if (waveOutDeviceWarmup != null && waveOutDeviceWarmup.PlaybackState == PlaybackState.Playing)
                {
                    StopPressed = true;  // Mark that we manually stopped the playback
                    waveOutDeviceWarmup.Stop();
                }
                waveOutDeviceBreak.Play();
                EnableButtons();
            }
        }

        private void buttonBreakPause_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceBreak != null)
            {
                if (waveOutDeviceBreak.PlaybackState == PlaybackState.Playing)
                {
                    waveOutDeviceBreak.Pause();
                    EnableButtons();
                }
            }
        }

        private void buttonBreakStop_Click(object sender, EventArgs e)
        {
            if (waveOutDeviceBreak != null)
            {
                StopPressed = true; // markera att vi har tryckt på stopknappen så inte automatiken fortsätter...
                waveOutDeviceBreak.Stop();
                EnableButtons();
            }
        }

        private void buttonSpotifyPlay_Click(object sender, EventArgs e)
        {
            spotify.Play();
            EnableButtons();
        }

        private void buttonSpotifyPrevious_Click(object sender, EventArgs e)
        {
            spotify.Previous();
            EnableButtons();
        }

        private void buttonSpotifyNext_Click(object sender, EventArgs e)
        {
            spotify.Skip();
            EnableButtons();
        }

        private void buttonSpotifyStop_Click(object sender, EventArgs e)
        {
            spotify.Pause();
            EnableButtons();
        }

        #endregion
        void OnPostVolumeMeter(object sender, StreamVolumeEventArgs e)
        {
            // we know it is stereo
            volumeMeter1.Amplitude = e.MaxSampleValues[0];
            volumeMeter2.Amplitude = e.MaxSampleValues[1];
        }

        private void volumeSlider1_VolumeChanged(object sender, EventArgs e)
        {
            if (setVolumeDelegateSkater != null)
            {
                setVolumeDelegateSkater(volumeSlider1.Volume);
            }
            if (setVolumeDelegateWarmup != null)
            {
                setVolumeDelegateWarmup(volumeSlider1.Volume);
            }
            if (setVolumeDelegateBreak != null)
            {
                setVolumeDelegateBreak(volumeSlider1.Volume);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (waveOutDeviceSkater != null && audioFileReaderSkater != null)
            {
                //TimeSpan currentTime = (waveOutDevice.PlaybackState == PlaybackState.Stopped) ? TimeSpan.Zero : audioFileReader.CurrentTime;
                TimeSpan currentTime = audioFileReaderSkater.CurrentTime;
                trackBarPosition.Value = Math.Min(trackBarPosition.Maximum, (int)(100 * currentTime.TotalSeconds / audioFileReaderSkater.TotalTime.TotalSeconds));
                labelCurrentTime.Text = String.Format("{0:00}:{1:00}", (int)currentTime.TotalMinutes, currentTime.Seconds);
            }
            else
            {
                trackBarPosition.Value = 0;
                //toolStripStatusLabel1.Text = "";
            }

            // Update statuslabel
            if (toolStripStatusLabel1.Text == "")
            {
                if (waveOutDeviceSkater != null && waveOutDeviceSkater.PlaybackState == PlaybackState.Playing)
                {// We're playing skaters music
                    if (PausePlaying)
                    {//Playing pausemusic
                        toolStripStatusLabel1.Text = "Playing pause music...";
                    }
                    else
                    {//Playing skaters music
                        toolStripStatusLabel1.Text = "Playing skaters music...";
                    }
                }
                else if (waveOutDeviceWarmup != null && waveOutDeviceWarmup.PlaybackState == PlaybackState.Playing)
                {
                    toolStripStatusLabel1.Text = "Playing warmup music...";
                }
                else if (waveOutDeviceBreak != null && waveOutDeviceBreak.PlaybackState == PlaybackState.Playing)
                {
                    toolStripStatusLabel1.Text = "Playing break music...";
                }
                else if (spotifyConnected && spotify.GetStatus().Playing)
                {
                    toolStripStatusLabel1.Text = "Playing spotify music...";
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "";
            }

        }

        private void trackBarPosition_Scroll(object sender, EventArgs e)
        {
            if (waveOutDeviceSkater != null)
            {
                audioFileReaderSkater.CurrentTime = TimeSpan.FromSeconds(audioFileReaderSkater.TotalTime.TotalSeconds * trackBarPosition.Value / 100.0);
            }
        }


        #region ListViewSelected
        private void listViewSkaters_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listViewSkaters.SelectedItems.Count == 1)
                {
                    if (listViewSkaters.Tag != null)
                    {
                        listViewSkaters.Items[(int)listViewSkaters.Tag].SubItems[0].BackColor = SystemColors.Window;
                        listViewSkaters.Items[(int)listViewSkaters.Tag].SubItems[0].ForeColor = Color.Gray;
                        listViewSkaters.Tag = null;
                    }

                    CreateWaveOutSkater();

                    //Check that there is a file in the grid. Gives a strange error message it we let AudioFileReader throw exception
                    if (listViewSkaters.SelectedItems[0].SubItems[4].Text == string.Empty)
                    {
                        throw new Exception("No file specified for skater");
                    }


                    audioFileReaderSkater = new AudioFileReader(listViewSkaters.SelectedItems[0].SubItems[4].Text);
                    var sampleChannel = new SampleChannel(audioFileReaderSkater, true);
                    this.setVolumeDelegateSkater = (vol) => sampleChannel.Volume = vol;
                    var postVolumeMeter = new MeteringSampleProvider(sampleChannel);
                    postVolumeMeter.StreamVolume += OnPostVolumeMeter;
                    fadeInOut = new FadeInOutSampleProvider(postVolumeMeter, false);

                    labelTotalTime.Text = String.Format("{0:00}:{1:00}", (int)audioFileReaderSkater.TotalTime.TotalMinutes, audioFileReaderSkater.TotalTime.Seconds);

                    //waveOutDevice.Init(postVolumeMeter);
                    waveOutDeviceSkater.Init(fadeInOut);
                    volumeSlider1.Volume = 1.0f;
                    PausePlaying = false;

                    listViewSkaters.Tag = listViewSkaters.SelectedItems[0].Index;
                    listViewSkaters.SelectedItems[0].BackColor = Color.LightGreen;
                    listViewSkaters.SelectedItems[0].EnsureVisible();
                    listViewSkaters.SelectedItems[0].Selected = false;

                    EnableButtons();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't load music\n" + listViewSkaters.SelectedItems[0].SubItems[4].Text + "\n\n" + ex.Message, "Error loading music!", MessageBoxButtons.OK);
                //MessageBox.Show("Can't load music\n" + listViewSkaters.SelectedItems[0].SubItems[4].Text + "\n\n" + ex.Message, "Error loading music!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewWarmupMusic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewWarmupMusic.SelectedItems.Count == 1)
            {
                try
                {
                    CreateWaveOutWarmup();

                    audioFileReaderWarmup = new AudioFileReader(listViewWarmupMusic.SelectedItems[0].SubItems[1].Text);
                    var sampleChannel = new SampleChannel(audioFileReaderWarmup, true);
                    this.setVolumeDelegateWarmup = (vol) => sampleChannel.Volume = vol;
                    var postVolumeMeter = new MeteringSampleProvider(sampleChannel);
                    postVolumeMeter.StreamVolume += OnPostVolumeMeter;

                    //waveOutDevice.Init(postVolumeMeter);
                    waveOutDeviceWarmup.Init(postVolumeMeter);
                    volumeSlider1.Volume = 1.0f;
                }
                catch (Exception)
                {
                    //Do nothing!
                }

                listViewWarmupMusic.SelectedItems[0].EnsureVisible();

                EnableButtons();
            }
        }

        private void listViewBreakMusic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewBreakMusic.SelectedItems.Count == 1)
            {
                try
                {
                    CreateWaveOutBreak();

                    audioFileReaderBreak = new AudioFileReader(listViewBreakMusic.SelectedItems[0].SubItems[1].Text);
                    var sampleChannel = new SampleChannel(audioFileReaderBreak, true);
                    this.setVolumeDelegateBreak = (vol) => sampleChannel.Volume = vol;
                    var postVolumeMeter = new MeteringSampleProvider(sampleChannel);
                    postVolumeMeter.StreamVolume += OnPostVolumeMeter;

                    //waveOutDevice.Init(postVolumeMeter);
                    waveOutDeviceBreak.Init(postVolumeMeter);
                    volumeSlider1.Volume = 1.0f;
                }
                catch (Exception)
                {
                    //Do nothing!
                }

                listViewBreakMusic.SelectedItems[0].EnsureVisible();

                EnableButtons();
            }
        }

        #endregion

        private void comboBoxClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSkaters(doc, comboBoxClass.SelectedItem.ToString(), listViewSkaters);
            if (listViewSkaters.Items.Count != 0)
            {// Select first skater
                listViewSkaters.Items[0].Selected = true;
            }
            buttonPlay.Focus();
        }

        #region MenuItems
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormEditCompetition EC = new FormEditCompetition(doc);
            if (EC.ShowDialog() == DialogResult.OK)
            {
                LoadXMLfile();
            }
        }

        private void editClassesMenuItem_Click(object sender, EventArgs e)
        {
            FormEditClasses EC = new FormEditClasses(doc);
            if (EC.ShowDialog() == DialogResult.OK)
            {
                LoadXMLfile();
            }
        }

        private void editSkatersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Class = string.Empty;
            if (comboBoxClass.SelectedItem != null)
            {
                Class = comboBoxClass.SelectedItem.ToString();
            }
            FormEditSkaters ES = new FormEditSkaters(this, Class);
            if (ES.ShowDialog() == DialogResult.OK)
            {
                LoadXMLfile();
                try
                {//Try to reload class
                    comboBoxClass.SelectedIndex = comboBoxClass.FindStringExact(Class);
                }
                catch (Exception)
                {
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormMusicPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to end the program?", "End program", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void importFromIndTAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogIndTA.ShowDialog() == DialogResult.OK)
            {
                LoadIndTA1(doc, openFileDialogIndTA.FileName);
                MessageBox.Show("Classes and Skaters loaded from file\n\nEdit Classes will now open to verify if class has Short", "IndTA imported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                editClassesMenuItem_Click(sender, e);
            }
        }

        private void importFromIndTA2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogIndTA.ShowDialog() == DialogResult.OK)
            {
                LoadIndTA2(doc, openFileDialogIndTA.FileName);
                MessageBox.Show("Classes and Skaters loaded from file\n\nEdit Classes will now open to verify if class has Short", "IndTA imported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                editClassesMenuItem_Click(sender, e);
            }
        }

        private void importFromClubcompOldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogCC.ShowDialog() == DialogResult.OK)
            {
                LoadClubStarComp(doc, folderBrowserDialogCC.SelectedPath);
                MessageBox.Show("Classes and Skaters loaded from Clubcomp/Starcomp\n\nEdit Classes will now open to verify if classes", "Clubcomp/Starcomp imported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                editClassesMenuItem_Click(sender, e);
            }
        }

        private void importFromClubcomp2016ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogCC.ShowDialog() == DialogResult.OK)
            {
                LoadClubStarComp2016(doc, folderBrowserDialogCC.SelectedPath);
                MessageBox.Show("Classes and Skaters loaded from Clubcomp/Starcomp\n\nEdit Classes will now open to verify if classes", "Clubcomp/Starcomp imported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                editClassesMenuItem_Click(sender, e);
            }
        }

        private void autoconnectMusicToSkatersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Class = string.Empty;
            if (comboBoxClass.SelectedItem != null)
            {
                Class = comboBoxClass.SelectedItem.ToString();
            }
            autoconnectMusic();
            LoadXMLfile();
            try
            {//Try to reload class
                comboBoxClass.SelectedIndex = comboBoxClass.FindStringExact(Class);
            }
            catch (Exception)
            {
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOptions FO = new FormOptions(this);
            if (FO.ShowDialog() == DialogResult.OK)
            {
                LoadSettings();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox.AboutBox ab = new AboutBox.AboutBox();
            ab.ShowDialog();
        }
        #endregion

    }
}
