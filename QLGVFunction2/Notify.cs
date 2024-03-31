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
    public partial class Notify : Form
    {
        public Notify(string message)
        {
            InitializeComponent();
            lbNotify.Text = message;
        }
        public void changeText(string text)
        {
            lbNotify.Text = text;
        }
    }
}
