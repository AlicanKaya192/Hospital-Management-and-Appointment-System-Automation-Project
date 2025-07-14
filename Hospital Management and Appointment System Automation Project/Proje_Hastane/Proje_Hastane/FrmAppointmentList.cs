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
    public partial class FrmAppointmentList : Form
    {
        public FrmAppointmentList()
        {
            InitializeComponent();
        }

        SQLConnection connection = new SQLConnection();

        private void FrmAppointmentList_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter(@"
        SELECT 
            Appointmentid       AS [ID],
            AppointmentDate     AS [Date],
            AppointmentTime     AS [Time],
            AppointmentBranch   AS [Branch],
            AppointmentDoctor   AS [Doctor],
            AppointmentStatus   AS [Status],
            PatientTC           AS [Patient TC]
        FROM Tbl_Appointments
    ", connection.connect());

            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView1.SelectedCells[0].RowIndex;

            string id = dataGridView1.Rows[row].Cells[0].Value.ToString();
            string date = dataGridView1.Rows[row].Cells[1].Value.ToString();
            string time = dataGridView1.Rows[row].Cells[2].Value.ToString();
            string branch = dataGridView1.Rows[row].Cells[3].Value.ToString();
            string doctor = dataGridView1.Rows[row].Cells[4].Value.ToString();
            string status = dataGridView1.Rows[row].Cells[5].Value.ToString();
            string patientTC = dataGridView1.Rows[row].Cells[6].Value.ToString();

            // SecretaryDetail formu zaten açıksa bul
            foreach (Form form in Application.OpenForms)
            {
                if (form is FrmSecretaryDetail)
                {
                    FrmSecretaryDetail secForm = (FrmSecretaryDetail)form;

                    secForm.TxtIDAccess.Text = id;
                    secForm.MskDateAccess.Text = date;
                    secForm.MskTimeAccess.Text = time;
                    secForm.CmbBranchAccess.Text = branch;
                    secForm.CmbDoctorAccess.Text = doctor;
                    secForm.MskTC_Access.Text = patientTC;
                    secForm.ChkStatusAccess.Checked = (status == "True" || status == "1");

                    break;
                }
            }
        }
    }
}
