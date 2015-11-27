using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Kursach.Class;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для Salary.xaml
    /// </summary>
    public partial class Salary : Window
    {
        public int EmployeId = 1;
        public Salary()
        {
            InitializeComponent();
            
        }

        private void DatePicker_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
        public void SalarySet()
        {
            if (textBox.Text == "")
                textBox.Text = "0";
            if (textBox1.Text == "")
                textBox1.Text = "0";
            double time =0;
            double salarys = 0;
            double sum = 0;
            UserContext db = new UserContext();
            List<WorkingTime> WorkTime = db.WorkingTimes.Where(x => x.TimeStart > From.SelectedDate).Where(x => x.TimeEnd < To.SelectedDate).Where(x=>x.EmployeesId==EmployeId).ToList();
            foreach (WorkingTime fo in WorkTime)
            {
                time += fo.time;
            }
            time = time / 60.0;
            salarys += time * Convert.ToDouble(textBox.Text);
            List<Check> lstcheck = db.Checks.Where(x => x.Date > From.SelectedDate).Where(x => x.Date < To.SelectedDate).Where(x => x.EmployeesId == EmployeId).ToList();
            foreach (Check ch in lstcheck)
            {
                sum += ch.Sum;
            }
            salarys += sum * Convert.ToDouble(textBox1.Text) / 100.0;
            label_salary.Content = Convert.ToString(salarys);
            if (textBox.Text == "0")
                textBox.Text = "";
            if (textBox1.Text == "0")
                textBox1.Text = "";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SalarySet();
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox Tb1 = sender as TextBox;
            if (Char.IsDigit(e.Text, 0))
            {
                if (!Tb1.Text.Contains(","))
                {
                    e.Handled = false;
                }
                else
                {
                    if (Tb1.Text.Length - Tb1.Text.IndexOf(",") >= 3)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        e.Handled = false;
                    }
                }
            }
            else if ((e.Text == "," || e.Text == ".") && Tb1.Text != string.Empty)
            {
                if (Tb1.Text.Contains(","))
                {
                    e.Handled = true;
                }
                else
                {
                    Tb1.Text += ",";
                    e.Handled = true;
                    Tb1.Select(Tb1.Text.Length, Tb1.Text.Length);
                }
            }
            else e.Handled = true;
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb1 = sender as TextBox;
            if (tb1.Text.IndexOf(",") == tb1.Text.Length - 1)
            {
                tb1.Text += "0";
            }
            if (tb1.Text[0] == '0')
                tb1.Text = tb1.Text.TrimStart(new char[] { '0' });
        }
    }
}
