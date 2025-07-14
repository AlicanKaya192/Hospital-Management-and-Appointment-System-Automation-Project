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
    public partial class FrmDoctorPanel : Form
    {
        public FrmDoctorPanel()
        {
            InitializeComponent();
        }

        SQLConnection connection = new SQLConnection();
        private void FrmDoctorPanel_Load(object sender, EventArgs e)
        {
            // Doctors are loaded into the combo box
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT Doctorid AS ID, DoctorName AS Name, DoctorSurname As Surname, Tbl_Branches.BranchName As Branch, DoctorTC AS TC, DoctorPw AS Pw FROM Tbl_Doctors \r\nJOIN Tbl_Branches ON Tbl_Doctors.Branchid = Tbl_Branches.Branchid", connection.connect());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1; // Bind the data to the DataGridView

            SqlCommand cmd = new SqlCommand("SELECT Branchid, BranchName FROM Tbl_Branches", connection.connect());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbBranch.DisplayMember = "BranchName"; // Kullanıcıya görünen
            CmbBranch.ValueMember = "Branchid";     // Arka planda kullanılacak
            CmbBranch.DataSource = dt;

            connection.connect().Close(); // Close the database connection
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO Tbl_Doctors (DoctorName, DoctorSurname, DoctorTC, DoctorPw, Branchid) VALUES (@p1, @p2, @p3, @p4, @p5)", connection.connect());
            sqlCommand.Parameters.AddWithValue("@p1", TxtName.Text);
            sqlCommand.Parameters.AddWithValue("@p2", TxtSurname.Text);
            sqlCommand.Parameters.AddWithValue("@p3", MskTC.Text);
            sqlCommand.Parameters.AddWithValue("@p4", TxtPw.Text);
            sqlCommand.Parameters.AddWithValue("@p5", CmbBranch.SelectedValue);
            sqlCommand.ExecuteNonQuery(); // Insert the new doctor into the database

            connection.connect().Close(); // Close the database connection
            LoadDoctors();
            MessageBox.Show("Doctor added successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int choose = dataGridView1.SelectedCells[0].RowIndex; // Get the selected row index
            TxtName.Text = dataGridView1.Rows[choose].Cells[1].Value.ToString(); // Get the doctor's name
            TxtSurname.Text = dataGridView1.Rows[choose].Cells[2].Value.ToString(); // Get the doctor's surname
            CmbBranch.Text = dataGridView1.Rows[choose].Cells[3].Value.ToString(); // Get the doctor's branch
            MskTC.Text = dataGridView1.Rows[choose].Cells[4].Value.ToString(); // Get the doctor's TC number
            TxtPw.Text = dataGridView1.Rows[choose].Cells[5].Value.ToString(); // Get the doctor's password
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand commandDelete = new SqlCommand("DELETE FROM Tbl_Doctors WHERE DoctorTC=@p1", connection.connect());
            commandDelete.Parameters.AddWithValue("@p1", MskTC.Text); // Set the parameter for the TC number
            commandDelete.ExecuteNonQuery(); // Delete the doctor from the database

            connection.connect().Close(); // Close the database connection
            LoadDoctors();
            MessageBox.Show("Doctor deleted successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // This method loads the doctors from the database and binds them to the DataGridView
        private void LoadDoctors()
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(
                "SELECT Doctorid, DoctorName, DoctorSurname, Tbl_Branches.BranchName, DoctorTC, DoctorPw FROM Tbl_Doctors JOIN Tbl_Branches ON Tbl_Doctors.Branchid = Tbl_Branches.Branchid",
                connection.connect());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            connection.connect().Close();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Ensure that the connection is open before executing the command
            SqlCommand cmd = new SqlCommand(
                "UPDATE Tbl_Doctors SET DoctorName = @p1, DoctorSurname = @p2, Branchid = @p3, DoctorPw = @p4 WHERE DoctorTC = @p5",
                connection.connect());

            // Add parameters to the command to prevent SQL injection
            cmd.Parameters.AddWithValue("@p1", TxtName.Text); // Doctor's name
            cmd.Parameters.AddWithValue("@p2", TxtSurname.Text); // Doctor's surname
            cmd.Parameters.AddWithValue("@p3", CmbBranch.SelectedValue); // Doctor's password
            cmd.Parameters.AddWithValue("@p4", TxtPw.Text); // Selected branch (as Branchid)
            cmd.Parameters.AddWithValue("@p5", MskTC.Text); // Doctor's TC number (used to locate the record)

            // Execute the command to update the doctor's information
            cmd.ExecuteNonQuery();
            connection.connect().Close();

            // Show a message box to inform the user that the update was successful
            MessageBox.Show("Doctor updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reload the doctors to reflect the changes in the DataGridView
            LoadDoctors();
        }
    }
}
