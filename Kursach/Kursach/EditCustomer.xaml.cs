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
    /// Логика взаимодействия для EditCustomer.xaml
    /// </summary>
    public partial class EditCustomer : Window
    {
        public EditCustomer()
        {
            InitializeComponent();
            UserContext db = new UserContext();
            dataGrid.ItemsSource = db.Customers.ToList();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer f = new AddCustomer();
            f.Closing += AddCustomer_Closing;
            f.ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Customer cust = dataGrid.SelectedItem as Customer;
                AddCustomer f = new AddCustomer();
                f.textBox_surname.Text = cust.FullName.Split(' ')[0];
                f.textBox_name.Text = cust.FullName.Split(' ')[1];
                f.textBox_fathername.Text = cust.FullName.Split(' ')[2];
                f.textBox_Passport.Text = cust.PassportNumber;
                f.textBox_Weapon.Text = cust.PermitWeapon;
                f.textBox_validfrom.Text = cust.ValidFrom.ToString("dd:MM:yyyy").Replace(":", ".");
                f.textBox_validto.Text = cust.ValidTo.ToString("dd:MM:yyyy").Replace(":",".");
                f.Closing += AddCustomer_Closing;
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Вы не выбрали никого", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                UserContext db = new UserContext();
                Customer cust = dataGrid.SelectedItem as Customer;
                Customer custdel = db.Customers.Find(cust.Id);
                db.Customers.Remove(custdel);
                db.SaveChanges();
            }
            else
            {
                MessageBox.Show("Вы не выбрали никого", "Ошибка", MessageBoxButton.OK);
            }
        }
        void AddCustomer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserContext db = new UserContext();
            dataGrid.ItemsSource = db.Customers.ToList();
        }
    }
}
