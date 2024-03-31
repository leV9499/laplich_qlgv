using Meziantou.Framework.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLGVFunction2.CONSTDATA;
using QLGVFunction2.DAO;
using QLGVFunction2.Service;
namespace QLGVFunction2
{
    public partial class LoginAccount : Form
    {
        public LoginAccount()
        {
            InitializeComponent();
            LoadCredentials();
            
        }
        public void LoadCredentials()
        {
            cbUsername.DataSource = SavePassword.LoadCredential();
            cbUsername.DisplayMember = "UserName";
            cbUsername.SelectedItem = null;
        }
        
        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            //string filePath = CONST.DOWNLOADS + "\\" + "The-Art-of-Game-Design_902853.pdf";
            //Process.Start(@"cmd.exe ", @"/c " + filePath);
            //string userNameEmail = tBoxLogin.Text;
            string userNameEmail = cbUsername.Text;
            string password = tBoxPassword.Text;
            string[] username = userNameEmail.Split('@');

            if (login(username[0], password))
            {
                try
                {
                    SavePassword.SaveCredential(userNameEmail, password, checkboxRemember.Checked);
                    //Random ran = new Random();
                    //string OTP = ran.Next(1000, 9999).ToString();
                    //MailService.sendMailOTP(userNameEmail, OTP);

                    //VerifyOTP v = new VerifyOTP(OTP);
                    //VerifyOTP demo= new VerifyOTP(OTP, username[0]);
                    TrangChu t = new TrangChu(username[0]);
                    t.Show();
                    t.FormClosing += Form_FormClosing;
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Login fail");
                }
            }
            else
            {
                MessageBox.Show("Login fail");

            }
        }
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }
        public bool login(string userName, string password)
        {
            return AccountDAO.Instance.Login(userName, password);

        }

        private void buttonSignIn_Click(object sender, EventArgs e)
        {

            signIn s = new signIn();
            s.ShowDialog();

        }

        private void LoginAccount_Load(object sender, EventArgs e)
        {

        }

        private void cbUsername_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((Credential)cbUsername.SelectedItem == null)
                {
                    tBoxPassword.Text = "";
                }
                else tBoxPassword.Text = ((Credential)cbUsername.SelectedItem).Password;
            }
            catch { }
        }

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
            ForgotPassForm forgot = new ForgotPassForm();
            forgot.Show();
        }
    }
}
