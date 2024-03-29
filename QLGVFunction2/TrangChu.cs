using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLGVFunction2.Service;
using System.Drawing;

namespace QLGVFunction2
{
    public partial class TrangChu : Form
    {
        private List<List<Button>> matrix;
        private List<string> dateOfWeek = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        public TrangChu()
        {
            InitializeComponent();
            LoadMatrix();
        }
        void LoadMatrix()
        {
            matrix = new List<List<Button>>();
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
            AddNumberToMatrixByDate(dpkDate.Value);
        }
        int DayOfMonth(DateTime date)
        {
            switch (date.Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 2:
                    if ((date.Year % 4 == 0 && date.Year % 100 != 0) || date.Year % 400 == 0)
                        return 29;
                    else
                        return 28;
                    break;
                default:
                    return 30;
            }
        }
        void AddNumberToMatrixByDate(DateTime date)
        {
            DateTime useDate = new DateTime(date.Year, date.Month, 1);
            int line = 0;

            for (int i = 1; i <= DayOfMonth(date); i++)
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
        private void ShowPanel(Panel pnl,bool visible)
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

        private void label10_Click(object sender, EventArgs e)
        {

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
    }
}
