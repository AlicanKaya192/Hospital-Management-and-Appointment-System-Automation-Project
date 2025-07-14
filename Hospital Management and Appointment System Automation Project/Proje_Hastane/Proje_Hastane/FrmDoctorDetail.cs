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
    public partial class FrmDoctorDetail : Form
    {
        public FrmDoctorDetail()
        {
            InitializeComponent();
        }

        SQLConnection connection = new SQLConnection();

        public string TCno; // TC

        private void FrmDoctorDetail_Load(object sender, EventArgs e)
        {
            LblTC.Text = TCno;

            // Doctor Name and Surname Retrieval
            SqlCommand name = new SqlCommand("SELECT DoctorName, DoctorSurname FROM Tbl_Doctors WHERE DoctorTC=@p1", connection.connect());
            name.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader reader = name.ExecuteReader();
            while (reader.Read())
            {
                LblNameSurname.Text = reader[0] + " " + reader[1];
            }
            reader.Close();
            connection.connect().Close();

            // Appointment List Retrieval
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Appointmentid AS ID, AppointmentDate AS Date, AppointmentTime AS Time, AppointmentBranch AS Branch, AppointmentDoctor AS Doctor, AppointmentStatus AS Status, PatientTC AS TC, PatientComplaint AS Complaint FROM Tbl_Appointments WHERE AppointmentDoctor=@p1", connection.connect());
            da.SelectCommand.Parameters.AddWithValue("@p1", LblNameSurname.Text);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            FrmDoctorEditInformation frmDoctorEditInformation = new FrmDoctorEditInformation();
            frmDoctorEditInformation.doctorTC = LblTC.Text; // Pass the TC number to the edit form
            frmDoctorEditInformation.Show();
        }

        private void BtnAnnouncements_Click(object sender, EventArgs e)
        {
            FrmAnnouncement frmAnnouncement = new FrmAnnouncement();
            frmAnnouncement.Show();
        }

        private void BtnLogOut_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Close the application
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int choose = dataGridView1.SelectedCells[0].RowIndex;
            RchComplaint.Text = dataGridView1.Rows[choose].Cells[7].Value.ToString();
        }

        private void BtnGoogle_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com.tr/search?q=" + RchComplaint.Text);
        }
    }
}
