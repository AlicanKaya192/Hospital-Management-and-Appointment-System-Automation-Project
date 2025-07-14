using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmEditInformation : Form
    {
        public FrmEditInformation()
        {
            InitializeComponent();
        }

        public string TCno; // TC

        SQLConnection connection = new SQLConnection();

        private void FrmEditInformation_Load(object sender, EventArgs e)
        {
            MskTC.Text = TCno;

            SqlCommand command = new SqlCommand("SELECT * FROM Tbl_Patients WHERE PatientTC=@p1", connection.connect());
            command.Parameters.AddWithValue("@p1", TCno);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TxtName.Text = reader[1].ToString();
                TxtSurname.Text = reader[2].ToString();
                MskTC.Text = reader[3].ToString();
                MskPhone.Text = reader[4].ToString();
                TxtPw.Text = reader[5].ToString();
                CmbGender.Text = reader[6].ToString();
            }
            
            reader.Close();
            connection.connect().Close();
        }

        private void BtnEditInformation_Click(object sender, EventArgs e)
        {
            SqlCommand command2 = new SqlCommand("UPDATE Tbl_Patients SET PatientName=@p1, PatientSurname=@p2, PatientPhone=@p3, PatientPw=@p4, PatientGender=@p5 WHERE PatientTC=@p6", connection.connect());
            command2.Parameters.AddWithValue("@p1", TxtName.Text);
            command2.Parameters.AddWithValue("@p2", TxtSurname.Text);
            command2.Parameters.AddWithValue("@p3", MskPhone.Text);
            command2.Parameters.AddWithValue("@p4", TxtPw.Text);
            command2.Parameters.AddWithValue("@p5", CmbGender.Text);
            command2.Parameters.AddWithValue("@p6", MskTC.Text);
            command2.ExecuteNonQuery(); // Update the patient information

            connection.connect().Close();
            MessageBox.Show("Information updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
