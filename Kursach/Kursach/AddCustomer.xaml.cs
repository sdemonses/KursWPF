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
    /// Логика взаимодействия для AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        Brush brsh;
        public AddCustomer()
        {
            InitializeComponent();
        }

        private void SubmitBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            Border brd = sender as Border;
            brsh = brd.Background;
            brd.Background = Brushes.Gray;
        }

        private void SubmitBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            Border brd = sender as Border;
            brd.Background = brsh;
        }

        private void CancelBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void SubmitBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Customer cust = new Customer();
            cust.FullName = textBox_surname.Text +" " + textBox_name.Text + " " + textBox_fathername.Text;
            cust.PassportNumber = textBox_Passport.Text;
            cust.PermitWeapon = textBox_Weapon.Text;
            cust.ValidFrom = Convert.ToDateTime(textBox_validfrom.Text);
            cust.ValidTo = Convert.ToDateTime(textBox_validto.Text);
            UserContext db = new UserContext();
            db.Customers.Add(cust);
            db.SaveChanges();
            this.Close();
        }
    }
}
