using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmSecretaryLogin : Form
    {
        public FrmSecretaryLogin()
        {
            InitializeComponent();
        }

        SQLConnection connection = new SQLConnection();
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Tbl_Secretary WHERE SecretaryTC=@p1 AND SecretaryPw=@p2", connection.connect());
            command.Parameters.AddWithValue("@p1", MskTC.Text);
            command.Parameters.AddWithValue("@p2", TxtPw.Text);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                FrmSecretaryDetail panel = new FrmSecretaryDetail();
                panel.TC = MskTC.Text; // Pass the TC number to the panel
                panel.Show();
                this.Hide(); // Hide the login form
            }
            else
            {
                MessageBox.Show("Incorrect TC or Password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            reader.Close();
            connection.connect().Close();
        }

        private void TrialAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MskTC.Text = "10000000006";
            TxtPw.Text = "mary123";
        }
    }
}
