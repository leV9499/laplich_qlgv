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
    }
}
