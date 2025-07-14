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
    public partial class FrmPatientLogin : Form
    {
        public FrmPatientLogin()
        {
            InitializeComponent();
        }

        private void LnkSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmPatientSignUp frmPatientSignUp = new FrmPatientSignUp();
            frmPatientSignUp.Show();
        }

        SQLConnection connection = new SQLConnection();

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Tbl_Patients WHERE PatientTC=@p1 AND PatientPw=@p2", connection.connect());
            command.Parameters.AddWithValue("@p1", MskTC.Text);
            command.Parameters.AddWithValue("@p2", TxtPw.Text);

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                FrmPatientDetail frmPatientDetail = new FrmPatientDetail();
                frmPatientDetail.tc = MskTC.Text; // Pass the TC to the detail form
                frmPatientDetail.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid TC or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            connection.connect().Close();
        }

        private void FrmPatientLogin_Load(object sender, EventArgs e)
        {

        }

        private void TrialAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MskTC.Text = "11111111111";
            TxtPw.Text = "admin";
        }
    }
}
