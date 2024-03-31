using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLGVFunction2.Service;
using System.Drawing;
using System.Data;
using QLGVFunction2.DAO;
using System.Linq;
using System.Data.Common;
using Microsoft.IdentityModel.Tokens;
namespace QLGVFunction2
{
    public partial class TrangChu : Form
    {
        private List<List<Button>> matrix;
        private List<string> dateOfWeek = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        private Dictionary<DateTime, DataTable[]> loadedData = new Dictionary<DateTime, DataTable[]>();
        private DataTable calendarCurMonth;
        private DataTable absentDays;
        private bool isLoading = false;
        string userId = null;
        public TrangChu(string userId)
        {
            this.userId = userId;
            InitializeComponent();
            LoadCalendar(DateTime.Now);
            LoadTextTime(pnStartTime);
            LoadTextTime(pnlTimeStart);
            LoadTextTime(pnEndTime);
            ShowAllJob();
        }
        public TrangChu()
        {
            InitializeComponent();
            LoadCalendar(DateTime.Now);
            LoadTextTime(pnStartTime);
            LoadTextTime(pnlTimeStart);
            LoadTextTime(pnEndTime);
            ShowAllJob();
        }
        void LoadCalendar(DateTime date)
        {
            isLoading = true;
            matrix = new List<List<Button>>();
            pnlMatrix.Controls.Clear();
            Button oldBtn = new Button() { Width = 0, Height = 0, Location = new Point(-Cons.margin, 0) };
            for (int i = 0; i < Cons.DayOfColumn; i++)
            {
                matrix.Add(new List<Button>());
                for (int j = 0; j < Cons.DayOfWeek; j++)
                {
                    Button btn = new Button() { Width = Cons.dateButtonWidth, Height = Cons.dateButtonHeight };
                    btn.Location = new Point(oldBtn.Location.X + oldBtn.Width + Cons.margin, oldBtn.Location.Y);
                    pnlMatrix.Controls.Add(btn);
                    matrix[i].Add(btn);
                    oldBtn = btn;
                }
                oldBtn = new Button() { Width = 0, Height = 0, Location = new Point(-Cons.margin, oldBtn.Location.Y + Cons.dateButtonHeight) };
            }

            AddNumberToMatrixByDate(date);
        }
        void AddNumberToMatrixByDate(DateTime date)
        {
            DateTime useDate = new DateTime(date.Year, date.Month, 1);
            int line = 0;
            for (int i = 1; i <= DateTime.DaysInMonth(date.Year, date.Month); i++)
            {
                int column = dateOfWeek.IndexOf(useDate.DayOfWeek.ToString());
                //if (column != -1)
                //{
                Button btn = matrix[line][column];
                btn.Text = i.ToString();
                if (column >= 6)
                    line++;
                //}
                useDate = useDate.AddDays(1);
            }
            Task t = new Task(async () =>
            {
                //TODO
                useDate = new DateTime(date.Year, date.Month, 1);
                line = 0;
                if (loadedData.ContainsKey(useDate))
                {
                    calendarCurMonth = loadedData[useDate][0];
                    absentDays = loadedData[useDate][1];
                }
                else
                {
                    calendarCurMonth = CourseDAO.Instance.GetCalendar(date, userId);
                    absentDays = CourseDAO.Instance.GetAbsentCalendar(date, userId);

                    loadedData.Add(useDate, new DataTable[] { calendarCurMonth, absentDays });

                    //if (CourseDAO.Instance.CheckGetCalendar(userId) && CourseDAO.Instance.CheckGetAbsentCalendar(userId))
                    //{


                    //    calendarCurMonth = CourseDAO.Instance.GetCalendar(date, userId);
                    //    absentDays = CourseDAO.Instance.GetAbsentCalendar(date, userId);

                    //    loadedData.Add(useDate, new DataTable[] { calendarCurMonth, absentDays });
                    //}
                    //else if (CourseDAO.Instance.CheckGetCalendar(userId) &&!CourseDAO.Instance.CheckGetAbsentCalendar(userId))
                    //{


                    //    calendarCurMonth = CourseDAO.Instance.GetCalendar(date, userId);

                    //    loadedData.Add(useDate, new DataTable[] { calendarCurMonth});
                    //}
                    //else if (!CourseDAO.Instance.CheckGetCalendar(userId) && CourseDAO.Instance.CheckGetAbsentCalendar(userId))
                    //{


                    //    absentDays = CourseDAO.Instance.GetAbsentCalendar(date, userId);

                    //    loadedData.Add(useDate, new DataTable[] { absentDays });
                    //}

                }
                int days = DateTime.DaysInMonth(date.Year, date.Month);

                for (int i = 1; i <= days; i++)
                {
                    int column = dateOfWeek.IndexOf(useDate.DayOfWeek.ToString());
                    Button btn = matrix[line][column];





                    if (CheckTeachingday(useDate, (column + 2).ToString()))
                    {


                        if (btn.InvokeRequired)
                        {
                            DateTime tmp = useDate;
                            btn.BeginInvoke(new Action(() =>
                            {
                                btn.Tag = tmp;
                                btn.BackColor = Color.LightCyan;
                                //btn.Tag = useDate.ToString();
                                btn.Click += ChooseDateBtnClick;
                            }));
                        }
                        else
                        {
                            btn.Tag = useDate;
                            btn.Click += ChooseDateBtnClick;
                            btn.BackColor = Color.LightCyan;
                        }

                    }
                    if (column >= 6)
                        line++;
                    useDate = useDate.AddDays(1);
                }
                isLoading = false;
            });
            t.Start();
        }

        private void ChooseDateBtnClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            DateTime date = (DateTime)btn.Tag;
            DataTable data = GetInfoDay(date);
            LichGiangDaycs lichGiangDaycs = new LichGiangDaycs(data, date);
            lichGiangDaycs.ShowDialog();
            loadedData.Clear();
            LoadCalendar(DateTime.Now);
        }
        private DataTable GetInfoDay(DateTime date)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("Course Id"));
            data.Columns.Add(new DataColumn("Starting time"));
            data.Columns.Add(new DataColumn("Location"));
            string dateOfWeekStr = (dateOfWeek.IndexOf(date.DayOfWeek.ToString()) + 2).ToString();
            foreach (DataRow row in calendarCurMonth.AsEnumerable()
                .Where(r => !absentDays.AsEnumerable().Any(a => a.Field<string>("courseId") == r.Field<string>("courseId") && a.Field<DateTime>("Absentdate").Date == date.Date) && r.Field<string>("Teachingday").Split('-').Contains(dateOfWeekStr)
                )
            )
            {
                data.Rows.Add(row.Field<string>("courseId"), row.Field<string>("Startingtime"), row.Field<string>("Location"));
            }
            foreach (DataRow row in absentDays.AsEnumerable().Where(a => a.Field<DateTime>("rescheduleday").Date == date.Date))
            {
                data.Rows.Add(row.Field<string>("courseId"), row.Field<DateTime>("rescheduleday").TimeOfDay, calendarCurMonth.AsEnumerable().Where(r => r.Field<string>("courseId") == row.Field<string>("courseId")).First().Field<string>("Location"));
            }
            return data;
        }
        private bool CheckTeachingday(DateTime useDate, string dayInWeek)
        {
            return CheckScheduleday(useDate, dayInWeek) || CheckRescheduleday(useDate);
        }
        private bool CheckScheduleday(DateTime useDate, string dayInWeek)
        {
            return calendarCurMonth != null && absentDays != null &&
       calendarCurMonth.AsEnumerable()
         .Where(r => r.Field<DateTime>("calendarStart") < useDate &&
                     r.Field<DateTime>("calendarEnd") > useDate &&
                     r.Field<string>("Teachingday").Split('-').Contains((dayInWeek).ToString()) &&
                     !absentDays.AsEnumerable().Any(a => a.Field<string>("courseId") == r.Field<string>("courseId") &&
                                                          a.Field<DateTime>("Absentdate").Date == useDate.Date)).Count() != 0;


            //return calendarCurMonth.AsEnumerable()
            //    .Where(r => r.Field<DateTime>("calendarStart") < useDate && r.Field<DateTime>("calendarEnd") > useDate && r.Field<string>("Teachingday").Split('-').Contains((dayInWeek).ToString()) && !absentDays.AsEnumerable().Any(a => a.Field<string>("courseId") == r.Field<string>("courseId") && a.Field<DateTime>("Absentdate").Date == useDate.Date)).Count() != 0;
        }
        private bool CheckRescheduleday(DateTime useDate)
        {
            if (absentDays != null)
            {
                return absentDays.AsEnumerable().Any(a => a.Field<DateTime>("rescheduleday").Date == useDate.Date);
            }
            else
            {
                return false;
            }
            //return absentDays.AsEnumerable().Any(a => a.Field<DateTime>("rescheduleday").Date == useDate.Date);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string filepath = "";
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    progressChange(5);
                    filepath = openFileDialog.FileName;
                    pnLoading.Visible = true;
                    Task loadTask = new Task(async () =>
                    {
                        //Task<string> loadFile = LoadFile(filepath);
                        //loadFile.Wait();
                        string txt = await LoadFile(filepath);
                        if (txbLink.InvokeRequired)
                        {
                            txbLink.BeginInvoke(new Action(() =>
                            {
                                //txbLink.Text = loadFile.Result;
                                txbLink.Text = txt;
                            }));
                        }
                        else
                        {
                            //txbLink.Text = loadFile.Result;
                            txbLink.Text = txt;
                        }
                        ShowPanel(pnLoading, false);
                        progressChange(0);
                        openFileDialog.Dispose();
                    });
                    loadTask.Start();
                }
            }
            catch { }
        }
        private async Task<String> LoadFile(string filepath)
        {
            return await MegaCloud.UploadFile(filepath, progressChange);
        }
        private async Task DownloadFile(string filepath)
        {
            await MegaCloud.DownloadFile(filepath, progressChangeDownload);
        }
        private void progressChange(double value)
        {
            if (pbUpload.InvokeRequired)
            {
                pbUpload.BeginInvoke(new Action(() => pbUpload.Value = (int)Math.Ceiling(value)));
            }
            else
                pbUpload.Value = (int)Math.Ceiling(value);
        }
        private void progressChangeDownload(double value)
        {
            if (pgBDownload.InvokeRequired)
            {
                pgBDownload.BeginInvoke(new Action(() => pgBDownload.Value = (int)Math.Ceiling(value)));
            }
            else
                pgBDownload.Value = (int)Math.Ceiling(value);
        }
        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txbLink.Text)) return;
                Task downTask = new Task(async () =>
                {
                    ShowPanel(pnlDownload, true);
                    progressChangeDownload(5);
                    await DownloadFile(txbLink.Text);
                    ShowPanel(pnlDownload, false);
                    progressChangeDownload(0);
                });
                downTask.Start();
            }
            catch { }
        }
        private void ShowPanel(Panel pnl, bool visible)
        {
            if (pnl.InvokeRequired)
            {
                pnl.BeginInvoke(new Action(() =>
                {
                    pnl.Visible = visible;
                }));
            }
            else pnl.Visible = visible;
        }
        private void btnOpenPDF_Click(object sender, EventArgs e)
        {
            try
            {
                //OpenFileDialog openFileDialog = new OpenFileDialog();
                //openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                //if (openFileDialog.ShowDialog() == DialogResult.OK)
                //{
                //    ViewPdf viewPdf = new ViewPdf(openFileDialog.FileName);
                //    viewPdf.ShowDialog();
                //}
            }
            catch { }
        }
        private void dtpInputDate_ValueChanged(object sender, EventArgs e)
        {
            LoadCalendar(dtpInputDate.Value);
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void label11_Click(object sender, EventArgs e)
        {
        }
        private void cbMonday_CheckedChanged(object sender, EventArgs e)
        {
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
        }
        private void label13_Click(object sender, EventArgs e)
        {
        }
        private void label14_Click(object sender, EventArgs e)
        {
        }
        void updateBinding()
        {
            tbDay.DataBindings.Clear();
            tbAddress.DataBindings.Clear();
            tbTime.DataBindings.Clear();
            dtpStartCourse.DataBindings.Clear();
            dtpEndCourse.DataBindings.Clear();
            tbCourseId.DataBindings.Clear();
            tbMoneyEditDelete.DataBindings.Clear();
            LoadInforBinding();
        }
        void LoadInforBinding()
        {
            tbCourseId.DataBindings.Add(new Binding("Text", dtgvJob.DataSource, "Mã lớp", true, DataSourceUpdateMode.Never));
            tbDay.DataBindings.Add(new Binding("Text", dtgvJob.DataSource, "Thứ", true, DataSourceUpdateMode.Never));
            tbMoneyEditDelete.DataBindings.Add(new Binding("Text", dtgvJob.DataSource, "Giá tiền", true, DataSourceUpdateMode.Never));
            tbAddress.DataBindings.Add(new Binding("Text", dtgvJob.DataSource, "Địa điểm", true, DataSourceUpdateMode.Never));
            dtpStartCourse.DataBindings.Add(new Binding("Value", dtgvJob.DataSource, "Thời gian bắt đầu lớp", true, DataSourceUpdateMode.Never));
            tbTime.DataBindings.Add(new Binding("Text", dtgvJob.DataSource, "Thời gian bắt đầu dạy", true, DataSourceUpdateMode.Never));
            dtpEndCourse.DataBindings.Add(new Binding("Value", dtgvJob.DataSource, "Thời gian kết thúc lớp", true, DataSourceUpdateMode.Never));
        }
        public string checkCalender()
        {
            string t2 = "";
            string t3 = "";
            string t4 = "";
            string t5 = "";
            string t6 = "";
            string t7 = "";
            string t = "";
            if (cbMonday.Checked == true)
            {
                t2 = "2-";
            }
            if (cbTuesday.Checked == true)
            {
                t3 = "3-";
            }
            if (cbWednesday.Checked == true)
            {
                t4 = "4-";
            }
            if (cbThursday.Checked == true)
            {
                t5 = "5-";
            }
            if (cbFriday.Checked == true)
            {
                t6 = "6-";
            }
            if (cbSaturday.Checked == true)
            {
                t7 = "7";
            }
            if (t2 != null || t3 != null || t4 != null || t5 != null || t6 != null || t7 != null)
            {
                t += t2 + t3 + t4 + t5 + t6 + t7;
            }
            if (t != null)
            {
                if (t.EndsWith("-"))
                {
                    t = t.Remove(t.Length - 1);
                }
            }
            return t;
        }
        private void btnAddWork_Click(object sender, EventArgs e)
        {

            string classCode = txbClassCode.Text;

            string addressTeaching = txbAddress.Text;
            DateTime calenderStart = dtpStartDay.Value;
            DateTime calenderEnd = dtpEndDay.Value;
            string teachingDay = checkCalender();
            string timeStart = txbStartHourMonday.Text;
            float money = 0;
            if (string.IsNullOrEmpty(tbMoney.Text))
            {

                MessageBox.Show("Nhập giá tiền!!!");
            }
            else
            {
                money = (float)Convert.ChangeType(tbMoney.Text, typeof(float));
            }
            try
            {
                if (CourseDAO.Instance.CheckCourseId(classCode))
                {
                    MessageBox.Show("Hãy nhập mã lớp khác!!!");
                }

                if (calenderStart > calenderEnd)
                {
                    MessageBox.Show("Sai thời gian!!!");
                }
                else if (string.IsNullOrEmpty(teachingDay))
                {
                    MessageBox.Show("Nhập thứ!!!");
                }
                else if (string.IsNullOrEmpty(classCode))
                {
                    MessageBox.Show("Hãy điền mã lớp!!!");
                }
                else if (string.IsNullOrEmpty(timeStart))
                {
                    MessageBox.Show("Hãy nhập thời gian bắt đầu dạy!!!");
                }
                else
                {
                    //addCourse(classCode,userId,teachingDay,timeStart,addressTeaching,calenderStart,calenderEnd);
                    CourseDAO.Instance.AddTeaching(classCode, userId, teachingDay, timeStart, addressTeaching, calenderStart, calenderEnd, money);
                    MessageBox.Show("Thêm thành công!!!");
                    ShowAllJob();
                    loadedData.Clear();
                    LoadCalendar(DateTime.Now);
                }
            }
            catch (Exception ex) { }


        }
        void addCourse(string courseId, string userId, string teachingDay, string startingTime, string location, DateTime calenderStart, DateTime calenderEnd)
        {
            string calenderStartStr = calenderStart.ToString("yyyy-MM-dd HH:mm:ss");
            string calenderEndStr = calenderEnd.ToString("yyyy-MM-dd HH:mm:ss");
            CourseDAO.Instance.AddCourse(courseId, userId, teachingDay, startingTime, location, calenderStart, calenderEnd);
        }
        void ShowAllJob()
        {
            DataTable dataTable = CourseDAO.Instance.ShowJob(userId);
            dtgvJob.DataSource = dataTable;
            updateBinding();
        }

        private void btnPostMonth_Click(object sender, EventArgs e)
        {
            if (isLoading) return;
            dtpInputDate.Value = dtpInputDate.Value.AddMonths(1);
        }
        private void btnPreMonth_Click(object sender, EventArgs e)
        {
            if (isLoading) return;
            dtpInputDate.Value = dtpInputDate.Value.AddMonths(-1);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbCourseId.Text))
            {
                MessageBox.Show("Không thể xóa");
                LoadCalendar(DateTime.Now);
            }
            try
            {
                CourseDAO.Instance.DeleteJobReport(tbCourseId.Text);
                CourseDAO.Instance.DeleteJob(tbCourseId.Text);

                MessageBox.Show("Xóa thành công");
                ShowAllJob();
                loadedData.Clear();
                LoadCalendar(DateTime.Now);
            }
            catch { }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string classCode = tbCourseId.Text;
            string addressTeaching = tbAddress.Text;
            DateTime calenderStart = dtpStartCourse.Value;
            DateTime calenderEnd = dtpEndCourse.Value;
            string teachingDay = tbDay.Text;
            string timeStart = tbTime.Text;
            float money = 0;
            if (string.IsNullOrEmpty(tbMoneyEditDelete.Text))
            {

                MessageBox.Show("Nhập giá tiền!!!");
            }
            else
            {
                money = (float)Convert.ChangeType(tbMoneyEditDelete.Text, typeof(float));

            }



            if (calenderStart > calenderEnd)
            {
                MessageBox.Show("Sai thời gian!!!");
            }

            else if (string.IsNullOrEmpty(teachingDay))
            {
                MessageBox.Show("Nhập thứ!!!");
            }
            else if (string.IsNullOrEmpty(timeStart))
            {
                MessageBox.Show("Hãy nhập thời gian bắt đầu dạy!!!");
            }
            else
            {
                //addCourse(classCode,userId,teachingDay,timeStart,addressTeaching,calenderStart,calenderEnd);
                CourseDAO.Instance.EditJob(classCode, userId, teachingDay, timeStart, addressTeaching, calenderStart, calenderEnd, money);
                MessageBox.Show("Sửa thành công!!!");
                ShowAllJob();
                loadedData.Clear();
                LoadCalendar(DateTime.Now);
            }


        }

        private void tbMoney_TextChanged(object sender, EventArgs e)
        {


        }

        private void dtpInputDate2_ValueChanged(object sender, EventArgs e)
        {
            //dtpInputDate_ValueChanged(sender, e);
            //LoadCalendar(dtpInputDate.Value);
            dtpInputDate.Value = dtpInputDate2.Value;
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Mã lớp"));
            dt.Columns.Add(new DataColumn("Số buổi"));
            dt.Columns.Add(new DataColumn("tổng tiền"));
            DateTime date = dtpInputDate2.Value;
            DateTime useDate = new DateTime(date.Year, date.Month, 1);
            for (int i = 1; i <= DateTime.DaysInMonth(date.Year, date.Month); i++)
            {

                DataTable info = GetInfoDay(useDate);
                foreach (DataRow row in info.Rows)
                {
                    foreach (DataRow row2 in dt.Rows)
                    {
                        if (row["Course Id"].ToString() == row2["Mã lớp"].ToString())
                        {
                            row2["Số buổi"] = int.Parse(row2["Số buổi"].ToString())+1;
                        }
                    }
                }
                useDate = useDate.AddDays(1);
            }
            dtgvStatistic.DataSource = dt;

        }
    }
}
