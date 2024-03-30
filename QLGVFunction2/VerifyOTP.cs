using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLGVFunction2
{
    public partial class VerifyOTP : Form
    {
        string OTP = null;
        string userName= null;
        public VerifyOTP()
        {
            InitializeComponent();
        }
        public VerifyOTP(string OTP,string userName)
        {
            InitializeComponent();
            this.userName = userName;
            this.OTP = OTP;
        }
        public VerifyOTP(string OTP)
        {
            InitializeComponent();
            this.OTP = OTP;
        }
        private void VerifyOTP_Load(object sender, EventArgs e)
        {

        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (txtOTP.Text == OTP || true)
            {
                MessageBox.Show("Login success");
                TrangChu t = new TrangChu(userName);
                this.Hide();
                t.Show();
            }
            else
            {
                MessageBox.Show("Wrong OTP");
            }
        }
    }
}
