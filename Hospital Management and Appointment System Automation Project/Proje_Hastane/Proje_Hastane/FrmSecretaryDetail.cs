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
    public partial class FrmSecretaryDetail : Form
    {
        public FrmSecretaryDetail()
        {
            InitializeComponent();
        }

        public string TC; // TC number

        public TextBox TxtIDAccess { get { return TxtID; } }
        public MaskedTextBox MskDateAccess { get { return MskDate; } }
        public MaskedTextBox MskTimeAccess { get { return MskTime; } }
        public ComboBox CmbBranchAccess { get { return CmbBranch; } }
        public ComboBox CmbDoctorAccess { get { return CmbDoctor; } }
        public MaskedTextBox MskTC_Access { get { return MskTC; } }
        public CheckBox ChkStatusAccess { get { return ChkStatus; } }

        SQLConnection connection = new SQLConnection(); // Database connection instance
        private void FrmSecretaryDetail_Load(object sender, EventArgs e)
        {
            LblTC.Text = TC; // Display the TC number on the label

            SqlCommand command = new SqlCommand("SELECT SecretaryNameSurname FROM Tbl_Secretary WHERE SecretaryTC=@p1", connection.connect());
            command.Parameters.AddWithValue("@p1", TC); // Set the parameter for the TC number
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                LblName.Text = reader[0].ToString(); // Display the secretary's name
            }

            reader.Close(); // Close the data reader
            connection.connect().Close(); // Close the database connection

            // Branches are loaded into the combo box
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT Branchid AS ID, BranchName AS Name FROM Tbl_Branches", connection.connect());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1; // Bind the data to the DataGridView

            // Doctors are loaded into the combo box
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT Doctorid AS ID, (DoctorName + ' ' + DoctorSurname) AS Doctors, Tbl_Branches.BranchName AS Branch, DoctorTC AS TC FROM Tbl_Doctors \r\nJOIN Tbl_Branches ON Tbl_Doctors.Branchid = Tbl_Branches.Branchid", connection.connect());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2; // Bind the data to the DataGridView

            // Branches are loaded into the combo box for appointment selection
            SqlCommand command2 = new SqlCommand("SELECT BranchName FROM Tbl_Branches", connection.connect());
            SqlDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                CmbBranch.Items.Add(reader2[0]); // Add each branch name to the combo box
            }
            reader2.Close(); // Close the data reader for branches
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SqlCommand commmandSave = new SqlCommand(@"
        INSERT INTO Tbl_Appointments 
        (AppointmentDate, AppointmentTime, AppointmentBranch, AppointmentDoctor) 
        VALUES (@r1, @r2, @r3, @r4)", connection.connect());

            commmandSave.Parameters.AddWithValue("@r1", MskDate.Text);         // Appointment date
            commmandSave.Parameters.AddWithValue("@r2", MskTime.Text);         // Appointment time
            commmandSave.Parameters.AddWithValue("@r3", CmbBranch.Text);       // Branch name
            commmandSave.Parameters.AddWithValue("@r4", CmbDoctor.Text);       // Doctor name

            commmandSave.ExecuteNonQuery(); // Save to database
            connection.connect().Close();   // Close connection

            MessageBox.Show("Appointment saved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoctor.Items.Clear(); // Clear existing doctors
            CmbDoctor.Text = "";     // Clear selected text
            CmbDoctor.SelectedIndex = -1; // Reset selection

            SqlCommand command = new SqlCommand(@"
        SELECT DoctorName + ' ' + DoctorSurname 
        FROM Tbl_Doctors 
        WHERE Branchid = (
            SELECT Branchid FROM Tbl_Branches WHERE BranchName = @p1
        )", connection.connect());

            command.Parameters.AddWithValue("@p1", CmbBranch.Text);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                CmbDoctor.Items.Add(reader[0].ToString()); // Add each doctor
            }

            reader.Close();
            connection.connect().Close();
        }

        private void BtnAnnCreate_Click(object sender, EventArgs e)
        {
            SqlCommand commandAnn = new SqlCommand("INSERT INTO Tbl_Announcements (Announcement) VALUES (@a1)", connection.connect());
            commandAnn.Parameters.AddWithValue("@a1", RchAnnouncement.Text); // Set the announcement text
            commandAnn.ExecuteNonQuery(); // Execute the command to save the announcement

            connection.connect().Close(); // Close the database connection
            MessageBox.Show("Announcement created successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDoctorPanel_Click(object sender, EventArgs e)
        {
            FrmDoctorPanel frmDoctorPanel = new FrmDoctorPanel();
            frmDoctorPanel.Show(); // Open the doctor panel form
        }

        private void BtnBranchesPanel_Click(object sender, EventArgs e)
        {
            FrmBranch frmBranch = new FrmBranch();
            frmBranch.Show(); // Open the branches panel form
        }

        private void BtnAppoList_Click(object sender, EventArgs e)
        {
            FrmAppointmentList frmAppointmentList = new FrmAppointmentList();
            frmAppointmentList.Show(); // Open the appointment list form
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand commandUpdate = new SqlCommand("UPDATE Tbl_Appointments SET AppointmentDate=@r1, AppointmentTime=@r2, AppointmentBranch=@r3, AppointmentDoctor=@r4, AppointmentStatus=@r5 WHERE Appointmentid=@r6", connection.connect());
            commandUpdate.Parameters.AddWithValue("@r1", MskDate.Text);         // Appointment date
            commandUpdate.Parameters.AddWithValue("@r2", MskTime.Text);         // Appointment time
            commandUpdate.Parameters.AddWithValue("@r3", CmbBranch.Text);       // Branch name
            commandUpdate.Parameters.AddWithValue("@r4", CmbDoctor.Text);       // Doctor name
            commandUpdate.Parameters.AddWithValue("@r5", ChkStatus.Checked);    // Status (bit)
            commandUpdate.Parameters.AddWithValue("@r6", TxtID.Text);           // Appointment ID
            commandUpdate.ExecuteNonQuery(); // Update the appointment in the database

            connection.connect().Close();   // Close connection
            MessageBox.Show("Appointment updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmAnnouncement frmAnnouncement = new FrmAnnouncement();
            frmAnnouncement.Show(); // Open the announcement form
        }
    }
}