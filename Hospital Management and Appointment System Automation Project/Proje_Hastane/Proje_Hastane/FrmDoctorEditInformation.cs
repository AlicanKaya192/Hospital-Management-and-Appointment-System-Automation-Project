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
    public partial class FrmDoctorEditInformation : Form
    {
        public FrmDoctorEditInformation()
        {
            InitializeComponent();
        }

        SQLConnection connection = new SQLConnection();

        public string doctorTC;
        private void FrmDoctorEditInformation_Load(object sender, EventArgs e)
        {
            MskTC.Text = doctorTC;

            // Populate branches into ComboBox first
            SqlCommand branchCmd = new SqlCommand("SELECT BranchName FROM Tbl_Branches", connection.connect());
            SqlDataReader branchReader = branchCmd.ExecuteReader();
            while (branchReader.Read())
            {
                CmbBranch.Items.Add(branchReader[0].ToString());
            }
            branchReader.Close();

            // Now get doctor information with BranchName
            SqlCommand sqlCommand = new SqlCommand(@"
        SELECT D.DoctorName, D.DoctorSurname, B.BranchName, D.DoctorPw 
        FROM Tbl_Doctors D
        INNER JOIN Tbl_Branches B ON D.Branchid = B.Branchid
        WHERE D.DoctorTC = @p1", connection.connect());

            sqlCommand.Parameters.AddWithValue("@p1", MskTC.Text);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                TxtName.Text = reader["DoctorName"].ToString();
                TxtSurname.Text = reader["DoctorSurname"].ToString();
                CmbBranch.Text = reader["BranchName"].ToString();
                TxtPw.Text = reader["DoctorPw"].ToString();
            }
            reader.Close();
            connection.connect().Close();
        }

        private void BtnEditInformation_Click(object sender, EventArgs e)
        {
            SqlCommand docUpdate = new SqlCommand("UPDATE Tbl_Doctors SET DoctorName=@p1, DoctorSurname=@p2, Branchid=(SELECT Branchid FROM Tbl_Branches WHERE BranchName=@p3), DoctorPw=@p4 WHERE DoctorTC=@p5", connection.connect());
            docUpdate.Parameters.AddWithValue("@p1", TxtName.Text);
            docUpdate.Parameters.AddWithValue("@p2", TxtSurname.Text);
            docUpdate.Parameters.AddWithValue("@p3", CmbBranch.Text);
            docUpdate.Parameters.AddWithValue("@p4", TxtPw.Text);
            docUpdate.Parameters.AddWithValue("@p5", MskTC.Text);
            docUpdate.ExecuteNonQuery(); // Update the doctor information

            connection.connect().Close();
            MessageBox.Show("Information updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
