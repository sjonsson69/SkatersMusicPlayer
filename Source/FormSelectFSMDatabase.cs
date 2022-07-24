using MySqlConnector;
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
    public partial class FormSelectFSMDatabase : Form
    {
        public FormSelectFSMDatabase()
        {
            InitializeComponent();
            var conString = new MySqlConnectionStringBuilder
            {
                Server = settings.FSMServer,
                Port = settings.FSMPort,
                UserID = settings.FSMUsername,
                Password = settings.FSMPassword
            };
            using (MySqlConnection conn = new MySqlConnection(conString.ToString()))
            {
                conn.Open();
                //Find all databases
                using (MySqlCommand cmd = new MySqlCommand("show databases", conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string db = reader.GetString(0);
                            if (db != "information_schema"
                                && db != "performance_schema"
                                && db != "mysql")
                            {
                                cbDatabase.Items.Add(db);
                            }
                        }
                        reader.Close();
                    }
                }
                conn.Close();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Tag = cbDatabase.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
