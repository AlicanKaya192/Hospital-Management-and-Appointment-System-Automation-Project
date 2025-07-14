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
    public partial class FrmDoctorLogin : Form
    {
        public FrmDoctorLogin()
        {
            InitializeComponent();
        }

        SQLConnection connection = new SQLConnection();
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Tbl_Doctors WHERE DoctorTC=@p1 AND DoctorPw=@p2", connection.connect());
            command.Parameters.AddWithValue("@p1", MskTC.Text);
            command.Parameters.AddWithValue("@p2", TxtPw.Text);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                FrmDoctorDetail doctorDetail = new FrmDoctorDetail();
                doctorDetail.TCno = MskTC.Text; // Pass the TC number to the detail form
                doctorDetail.Show();
                this.Hide(); // Hide the login form
            }
            else
            {
                MessageBox.Show("Invalid TC or Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            reader.Close();
            connection.connect().Close();
        }

        private void TrialAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MskTC.Text = "10000000003";
            TxtPw.Text = "heart123";
        }
    }
}
