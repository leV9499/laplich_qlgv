﻿using System;
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
    public partial class LichGiangDaycs : Form
    {
        public LichGiangDaycs()
        {
            InitializeComponent();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        public LichGiangDaycs(DataTable dt)
        {
            InitializeComponent();


        }
    }
}