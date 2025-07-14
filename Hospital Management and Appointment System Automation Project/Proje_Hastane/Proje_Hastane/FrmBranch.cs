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
    public partial class FrmBranch : Form
    {
        public FrmBranch()
        {
            InitializeComponent();
        }

        SQLConnection connect = new SQLConnection();

        private void FrmBranch_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Branchid as ID, BranchName as Name FROM Tbl_Branches", connect.connect());
            da.Fill(dt);
            dataGridView1.DataSource = dt; // Bind the data to the DataGridView
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand commandAdd = new SqlCommand("INSERT INTO Tbl_Branches (BranchName) VALUES (@p1)", connect.connect());
            commandAdd.Parameters.AddWithValue("@p1", TxtBranch.Text);
            commandAdd.ExecuteNonQuery(); // Execute the insert command

            connect.connect().Close(); // Close the database connection
            MessageBox.Show("Branch added successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand commandDelete = new SqlCommand("DELETE FROM Tbl_Branches WHERE Branchid=@p1", connect.connect());
            commandDelete.Parameters.AddWithValue("@p1", TxtID.Text);
            commandDelete.ExecuteNonQuery(); // Execute the delete command

            connect.connect().Close(); // Close the database connection
            MessageBox.Show("Branch deleted successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand commandUpdate = new SqlCommand("UPDATE Tbl_Branches SET BranchName=@p1 WHERE Branchid=@p2", connect.connect());
            commandUpdate.Parameters.AddWithValue("@p1", TxtBranch.Text);
            commandUpdate.Parameters.AddWithValue("@p2", TxtID.Text);
            commandUpdate.ExecuteNonQuery(); // Execute the update command

            connect.connect().Close(); // Close the database connection
            MessageBox.Show("Branch updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int choose = dataGridView1.SelectedCells[0].RowIndex; // Get the selected row index
            TxtID.Text = dataGridView1.Rows[choose].Cells[0].Value.ToString(); // Get the ID from the selected row
            TxtBranch.Text = dataGridView1.Rows[choose].Cells[1].Value.ToString(); // Get the branch name from the selected row
        }
    }
}
