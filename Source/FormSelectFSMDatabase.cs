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
        public FormSelectFSMDatabase(string server, string port, string username, string password)
        {
            InitializeComponent();
            using (MySqlConnection conn = new MySqlConnection("Server=" + server + ";Port=" + port + ";Uid=" + username + ";Pwd=" + password + ";Connection Timeout=10;"))
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
