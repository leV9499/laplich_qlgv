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
    public partial class Form1 : Form
    {
        private List<List<Button>> matrix;
        private List<string> dateOfWeek = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        public Form1()
        {
            InitializeComponent();
            LoadMatrix();
        }
        void LoadMatrix()
        {
            matrix = new List<List<Button>>();
            Button oldBtn = new Button() { Width = 0, Height = 0, Location = new Point(-Cons.margin, 0) };
            for (int i = 0; i <Cons.DayOfColumn; i++)
            {
                matrix.Add(new List<Button>());
                for (int j = 0; j < Cons.DayOfWeek; j++)
                {
                    Button btn = new Button() { Width = Cons.dateButtonWidth, Height = Cons.dateButtonHeight };
                    btn.Location = new Point(oldBtn.Location.X + oldBtn.Width +Cons.margin, oldBtn.Location.Y);
                   
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
                    if((date.Year%4==0 && date.Year % 100 != 0) || date.Year % 400 == 0)
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

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
