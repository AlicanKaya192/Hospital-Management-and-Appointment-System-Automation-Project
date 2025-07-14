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
    public partial class FrmPatientDetail : Form
    {
        public FrmPatientDetail()
        {
            InitializeComponent();
        }

        public string tc; // TC

        SQLConnection connection = new SQLConnection();

        private void FrmPatientDetail_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;

            // Name - Surname
            SqlCommand command = new SqlCommand("SELECT PatientName, PatientSurname FROM Tbl_Patients WHERE PatientTC=@p1", connection.connect());
            command.Parameters.AddWithValue("@p1", tc);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                LblName.Text = reader[0] + " " + reader[1];
            }

            reader.Close();
            connection.connect().Close();

            LoadAppointments(); // Load appointments for the patient

            // Branch 
            SqlCommand branchCommand = new SqlCommand("SELECT BranchName FROM Tbl_Branches", connection.connect());
            SqlDataReader branchReader = branchCommand.ExecuteReader();
            while (branchReader.Read())
            {
                CmbBranch.Items.Add(branchReader[0]);
            }

            branchReader.Close();
            connection.connect().Close();
        }

        private void CmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoctor.Items.Clear(); // Clear previous items
            CmbDoctor.Text = ""; // Reset the doctor combo box

            SqlCommand command3 = new SqlCommand("SELECT DoctorName, DoctorSurname FROM Tbl_Doctors JOIN Tbl_Branches ON Tbl_Doctors.Branchid = Tbl_Branches.Branchid WHERE Tbl_Branches.BranchName=@p1", connection.connect());
            command3.Parameters.AddWithValue("@p1", CmbBranch.Text);
            SqlDataReader reader3 = command3.ExecuteReader();
            CmbDoctor.Items.Clear(); // Clear previous items
            while (reader3.Read())
            {
                CmbDoctor.Items.Add(reader3[0] + " " + reader3[1]);
            }

            reader3.Close();
            connection.connect().Close();
        }

        private void CmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_Appointments WHERE AppointmentBranch='" + CmbBranch.Text + "'" + "and AppointmentDoctor='" + CmbDoctor.Text + "'and AppointmentStatus=0", connection.connect());
            da.Fill(dataTable);
            dataGridView2.DataSource = dataTable;
        }

        private void LnkEditInformation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmEditInformation editForm = new FrmEditInformation();
            editForm.TCno = LblTC.Text; // Pass the TC number to the edit form
            editForm.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int choose = dataGridView2.SelectedCells[0].RowIndex; // Get the selected row index
            Txtid.Text = dataGridView2.Rows[choose].Cells[0].Value.ToString(); // Get the ID from the selected row
        }

        private void BtnAppointment_Click(object sender, EventArgs e)
        {
            SqlCommand makeAppointment = new SqlCommand("Update Tbl_Appointments set AppointmentStatus=1, PatientTC=@p1, PatientComplaint=@p2 where Appointmentid=@p3", connection.connect());
            makeAppointment.Parameters.AddWithValue("@p1", LblTC.Text); // Set the TC number
            makeAppointment.Parameters.AddWithValue("@p2", RchComplaint.Text); // Set the patient's complaint
            makeAppointment.Parameters.AddWithValue("@p3", Txtid.Text); // Set the appointment ID
            makeAppointment.ExecuteNonQuery(); // Execute the command to update the appointment status

            connection.connect().Close(); // Close the database connection
            MessageBox.Show("Appointment successfully made.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadAppointments(); // Reload appointments after making an appointment
        }

        private void LoadAppointments()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT Appointmentid AS ID, AppointmentDate AS Date, AppointmentTime AS Time, AppointmentBranch AS Branch, AppointmentDoctor AS Doctor, AppointmentStatus AS Status, PatientTC AS TC, PatientComplaint AS Complaint FROM Tbl_Appointments WHERE PatientTC = @tc", connection.connect());
            da.SelectCommand.Parameters.AddWithValue("@tc", LblTC.Text); // giriş yapan kişinin TC'si
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.connect().Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
