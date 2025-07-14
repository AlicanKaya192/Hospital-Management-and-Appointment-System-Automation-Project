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
    public partial class FrmPatientSignUp : Form
    {
        public FrmPatientSignUp()
        {
            InitializeComponent();
        }

        SQLConnection connection = new SQLConnection();

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Tbl_Patients (PatientName, PatientSurname, PatientTC, PatientPhone, PatientPw, PatientGender) VALUES (@p1, @p2, @p3, @p4, @p5, @p6)", connection.connect());
            command.Parameters.AddWithValue("@p1", TxtName.Text);
            command.Parameters.AddWithValue("@p2", TxtSurname.Text);
            command.Parameters.AddWithValue("@p3", MskTC.Text);
            command.Parameters.AddWithValue("@p4", MskPhone.Text);
            command.Parameters.AddWithValue("@p5", TxtPw.Text);
            command.Parameters.AddWithValue("@p6", CmbGender.Text);

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Registration successful.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.connect().Close();
                this.Close();
            }
        }
    }
}
