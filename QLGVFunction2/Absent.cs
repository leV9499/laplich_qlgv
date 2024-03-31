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
    public partial class Absent : Form
    {
        string courseId;
        public Absent()
        {
            InitializeComponent();
            LoadTextTime(pnTime);
        }

        public Absent(string CourseId, DateTime date)
        {
            InitializeComponent();
            this.courseId=CourseId;
            txbCourseId.Text = CourseId;
            dtpAbsent.Value = date;
            LoadTextTime(pnTime);
        }
        private void btnValid_Click(object sender, EventArgs e)
        {
            
            if (txbCourseId.Text == "")
            {
                MessageBox.Show("Hãy điền đủ mã khóa học");
                return;
            }
            if (!CheckUserid(courseId))
            {
                MessageBox.Show("Không tồn tại mã khóa học");
                return;
            }
  
            string userId = CourseDAO.Instance.GetUserId(courseId);
            
            string dateAbsent = ValidateService.ConvertDate(dtpAbsent.Text); 
            string dateReschedule = ValidateService.ConvertDate(dtpReschedule.Text);
            string DatetimeAbsent = dateAbsent+" "+ txbAbsentTime.Text;
            string DatetimeReschedule = dateReschedule +" "+ txbRescheduleTime.Text;
            string courseCode = txbCourseId.Text;
            if (MessageBox.Show(string.Format("Bạn có chắc xác nhận"), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                ReportCourseDAO.Instance.AddReportCourse(userId, courseCode, DatetimeAbsent, DatetimeReschedule);
                MessageBox.Show("Chỉnh sửa thành công");
            }

        }
        private bool CheckUserid(string courseId)
        {
            return CourseDAO.Instance.CheckCourseId(courseId);
        }
  
        static string ConvertDateFormat(string inputDate)
        {
            string[] parts = inputDate.Split('/');

            int month = int.Parse(parts[0]);
            int day = int.Parse(parts[1]);
            int year = int.Parse(parts[2]);
            string outputDate = $"{year}-{day:00}-{month:00}";

            return outputDate;
        }
        private void LoadTextTime(Panel pnl)
        {
            foreach (var item in pnl.Controls)
            {
                (item as TextBox).Text = "00:00";
                (item as TextBox).TextChanged += TxtChanged;
                (item as TextBox).KeyPress += TxtKeyPress;
            }
        }
        private void TxtKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txtBox.Text.Length >= 5 && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
            if (txtBox.Text.Length == 1 && e.KeyChar != ':')
            {
                e.Handled = true;
            }
            else if (txtBox.Text.Length == 2 && e.KeyChar != '\b')
            {
                e.Handled = true;
                txtBox.Text += ':';
            }
        }
        private void TxtChanged(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text.Length == 0)
            {
                txtBox.Text += "00:00";
            }
            if (txtBox.Text.Length <= 2 && !txtBox.Text.Contains(":"))
            {
                txtBox.Text += ":0";
            }
            try
            {
                string[] arr = txtBox.Text.Split(':');
                if (int.Parse(arr[0]) >= 24)
                {
                    txtBox.Text = "00:00";
                }
                if (int.Parse(arr[1]) > 59)
                {
                    txtBox.Text = arr[0] + ":00";
                }
            }
            catch { }
        }
    }
}
