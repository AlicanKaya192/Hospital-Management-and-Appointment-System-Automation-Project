using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_Hastane
{
    public partial class FrmLogins : Form
    {
        public FrmLogins()
        {
            InitializeComponent();
        }

        private void BtnPatientLogin_Click(object sender, EventArgs e)
        {
            FrmPatientLogin frmPatientLogin = new FrmPatientLogin();
            frmPatientLogin.Show();
            this.Hide();
        }

        private void BtnDoctorLogin_Click(object sender, EventArgs e)
        {
            FrmDoctorLogin frmDoctorLogin = new FrmDoctorLogin();
            frmDoctorLogin.Show();
            this.Hide();
        }

        private void BtnSecrataryLogin_Click(object sender, EventArgs e)
        {
            FrmSecretaryLogin frmSecretaryLogin = new FrmSecretaryLogin();
            frmSecretaryLogin.Show();
            this.Hide();
        }
    }
}
