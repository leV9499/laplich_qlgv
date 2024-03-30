﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLGVFunction2.Service;
using System.Drawing;
using System.Data;
using QLGVFunction2.DAO;
using System.Linq;
using System.Data.Common;
namespace QLGVFunction2
{
    public partial class TrangChu : Form
    {
        private List<List<Button>> matrix;
        private List<string> dateOfWeek = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        private Dictionary<DateTime, DataTable[]> loadedData = new Dictionary<DateTime, DataTable[]>();
        private DataTable calendarCurMonth;
        private DataTable absentDays;
        public TrangChu()
        {
            InitializeComponent();
            LoadCalendar(DateTime.Now);
            LoadTextTime(pnStartTime);
            LoadTextTime(pnEndTime);
        }
        void LoadCalendar(DateTime date)
        {
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
                    calendarCurMonth = CourseDAO.Instance.GetCalendar(date, "minh");
                    absentDays = CourseDAO.Instance.GetAbsentCalendar(date, "minh");
                    loadedData.Add(useDate, new DataTable[] { calendarCurMonth, absentDays });
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
            });
            t.Start();
        }
        private void ChooseDateBtnClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            DateTime date = (DateTime)btn.Tag;
            DataTable data = GetInfoDay(date);
            LichGiangDaycs lichGiangDaycs = new LichGiangDaycs(data);
            lichGiangDaycs.Show();
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
            return calendarCurMonth.AsEnumerable()
                .Where(r => r.Field<DateTime>("calendarStart") < useDate && r.Field<DateTime>("calendarEnd") > useDate && r.Field<string>("Teachingday").Split('-').Contains((dayInWeek).ToString()) && !absentDays.AsEnumerable().Any(a => a.Field<string>("courseId") == r.Field<string>("courseId") && a.Field<DateTime>("Absentdate").Date == useDate.Date)).Count() != 0;
        }
        private bool CheckRescheduleday(DateTime useDate)
        {
            return absentDays.AsEnumerable().Any(a => a.Field<DateTime>("rescheduleday").Date == useDate.Date);
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
        private void btnAddWork_Click(object sender, EventArgs e)
        {
            string classCode = txbClassCode.Text;
            string subJectName = txbSubjectName.Text;
        }
        private void btnPostMonth_Click(object sender, EventArgs e)
        {
            dtpInputDate.Value = dtpInputDate.Value.AddMonths(1);
        }
        private void btnPreMonth_Click(object sender, EventArgs e)
        {
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
    }
}
