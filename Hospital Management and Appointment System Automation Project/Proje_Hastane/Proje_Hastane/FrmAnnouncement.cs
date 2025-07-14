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
    public partial class FrmAnnouncement : Form
    {
        public FrmAnnouncement()
        {
            InitializeComponent();
        }

        SQLConnection connection = new SQLConnection();

        private void FrmAnnouncement_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Announcementid AS ID, Announcement FROM Tbl_Announcements", connection.connect());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
