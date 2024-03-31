using QLGVFunction2.DAO;
using QLGVFunction2.Service;
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
    public partial class ForgotPassForm : Form
    {
        public ForgotPassForm()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Notify noti = new Notify("Verifying your mail!!!");
            noti.Show();
            noti.Refresh();
            if (AccountDAO.Instance.CheckMailUser(txbMail.Text))
            {
                //noti.CloseNotify();
                noti.Close();
                Random r = new Random();
                int pass = r.Next(1000, 9999);
                MailService.Send(txbMail.Text, "YOUR NEW PASSWORD", $"Your new password is: {pass.ToString()}");
                AccountDAO.Instance.UpdatePassword(txbMail.Text, pass.ToString());
                MessageBox.Show("Kiểm tra hòm thư email!!!");
            }
            else
            {
                //noti.CloseNotify();
                noti.Close();
                MessageBox.Show("Sai tài khoản hoặc email!!!");
            }
        }
    }
}
