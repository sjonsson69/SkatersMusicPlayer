using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static SkatersMusicPlayer.formMusicPlayer;

namespace SkatersMusicPlayer
{
    public partial class formEditCategories : Form
    {
        public competitionEvent compEvent2 = null;


        public static void loadCategoriesToGrid(competitionEvent compEvent, DataGridView DV)
        {
            if (compEvent != null && DV != null)
            {
                try
                {
                    DV.Rows.Clear();

                    if (compEvent.categoriesAndSegments.Count!=0)
                    {
                        // Load categories from competitionEvent
                        foreach (categorySegment cat in compEvent.categoriesAndSegments)
                        {
                            DV.Rows.Add();
                            DV[0, DV.Rows.Count - 2].Tag = cat.participants;  // Store the participants for each Category
                            DV[0, DV.Rows.Count - 2].Value = cat.discipline;
                            DV[1, DV.Rows.Count - 2].Value = cat.category;
                            DV[2, DV.Rows.Count - 2].Value = cat.segment;
                            DV[3, DV.Rows.Count - 2].Value = cat.participants.Count; // Number of participants in category
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error loading Categories\n" + e.Message, Properties.Resources.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public formEditCategories(competitionEvent compEvent)
        {
            InitializeComponent();

            compEvent2 = compEvent;
            loadCategoriesToGrid(compEvent2, dataGridView1);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Save categories to competitionEvent
            compEvent2.categoriesAndSegments.Clear();
            for (int r = 0; r < dataGridView1.Rows.Count - 1; r++)
            {
                categorySegment cat = new categorySegment();
                cat.discipline = (string)dataGridView1[0, r].Value;
                cat.category = (string)dataGridView1[1, r].Value;
                cat.segment = (string)dataGridView1[2, r].Value;
                if (dataGridView1[0, r].Tag != null)
                {
                    cat.participants = (List<participant>)dataGridView1[0, r].Tag;  // Retrieve the participants for each Category
                }
                compEvent2.categoriesAndSegments.Add(cat);
            }

            //Save the file
            serializeToFile(compEvent2, Properties.Resources.JSON_FILENAME);

            //End form
            this.DialogResult = DialogResult.OK;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridView1[3, e.RowIndex].Value = 0;
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if ((int)e.Row.Cells[3].Value != 0)
            {// Category contains participants, that will be removed
                if (MessageBox.Show("Are you sure you want to remove " + e.Row.Cells[0].Value + "?\n\nIt contains " + e.Row.Cells[3].Value + " participants that also will be deleted!", Properties.Resources.CAPTION_DELETE_CATEGORY, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }

            }
        }
    }
}
