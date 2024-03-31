using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;

namespace QLGVFunction2
{
    public partial class LichGiangDaycs : Form
    {
        BindingSource bindingSource = new BindingSource();
        private DateTime date;
        public LichGiangDaycs()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        public LichGiangDaycs(DataTable dt, DateTime date)
        {
            InitializeComponent();
            LoadTimeTeaching(dt);
            updatedatabinding();
            this.date = date;
        }

        void LoadTimeTeaching(DataTable dt)
        {
            bindingSource.DataSource = dt; // Gán dữ liệu từ DataTable vào BindingSource
            dtgvListTeaching.DataSource = bindingSource; // Gán BindingSource vào DataGridView
        }
        void LoadBindingTimeTeaching()
        {
            txbCourseID.DataBindings.Add(new Binding("Text", bindingSource, "Course Id", true, DataSourceUpdateMode.Never));
            txbLocation.DataBindings.Add(new Binding("Text", bindingSource, "Location", true, DataSourceUpdateMode.Never));
            txbStartTime.DataBindings.Add(new Binding("Text", bindingSource, "Starting time", true, DataSourceUpdateMode.Never));
        }
        void updatedatabinding()
        {
            txbCourseID.DataBindings.Clear();
            txbLocation.DataBindings.Clear();
            txbStartTime.DataBindings.Clear();
            LoadBindingTimeTeaching();
        }

        private void btnAbsent_Click(object sender, EventArgs e)
        {

            string courseId = txbCourseID.Text;
            Absent ab = new Absent(courseId,date);
            ab.Show();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Workbook|*.xlsx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            DataTable dt = new DataTable();
                            foreach (DataGridViewColumn column in dtgvListTeaching.Columns)
                            {
                                dt.Columns.Add(column.HeaderText, column.ValueType);
                            }
                            foreach (DataGridViewRow row in dtgvListTeaching.Rows)
                            {
                                dt.Rows.Add();
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    if (cell.Value != null) 
                                    {
                                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                                    }
                                }
                            }
                            workbook.Worksheets.Add(dt, "WorksheetName");
                            workbook.SaveAs(sfd.FileName);
                            MessageBox.Show("You have successfully exported your data to an excel file.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fail: " + ex.Message);
                    }
                }
            }
        }

    }
}
