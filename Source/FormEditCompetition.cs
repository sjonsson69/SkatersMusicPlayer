using System;
using System.Windows.Forms;
using static SkatersMusicPlayer.formMusicPlayer;

namespace SkatersMusicPlayer
{
    public partial class FormEditEvent : Form
    {
#nullable enable
        public competitionEvent? compEvent2 = null;
#nullable disable


        public FormEditEvent(competitionEvent compEvent)
        {
            InitializeComponent();

            compEvent2 = compEvent;
            textBoxEvent.Text = compEvent.competitionName;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            compEvent2.competitionName = textBoxEvent.Text;
            serializeToFile(compEvent2, Properties.Resources.JSON_FILENAME);

            this.DialogResult = DialogResult.OK;
        }
    }
}
