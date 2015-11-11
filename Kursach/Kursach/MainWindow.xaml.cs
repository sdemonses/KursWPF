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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kursach.Class;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserContext db = new UserContext();
        public MainWindow()
        {
            InitializeComponent();
            //using (UserContext db = new UserContext())
            //{
            //    Employee emp1 = new Employee() { Name = "Dmitry", Login = "sdemonses", Password = "Lol", Role = "Admin", Surname = "Bibliy" };
            //    Employee emp2 = new Employee() { Name = "D", Login = "sdemonses", Password = "Lol", Role = "Admin", Surname = "Bibliy" };
            //    db.Emloyees.Add(emp1);
            //    db.Emloyees.Add(emp2);
            //    db.SaveChanges();

            //}

            dataGridWorker.ItemsSource = db.Emloyees.ToList().OrderBy(x=>x.Login);
            if (db.Emloyees.Where(p=>p.Role == "Admin") != null)
                {
                button_Copy.Visibility = Visibility.Hidden;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridWorker.SelectedItem != null)
            {
                Autorization.Visibility = Visibility.Visible;
                Employee forlog = dataGridWorker.SelectedItem as Employee; //вытаскиваем логин
                textBox_log.Text = forlog.Login;
            }
            else
            {
                MessageBox.Show("Вы не выбрали никого", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void Autorization_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Autorization.IsVisible == true)
            {
                button_Copy.IsEnabled = false;
                button.IsEnabled = false;
                dataGridWorker.IsEnabled = false;
            }
            else
            {
                button_Copy.IsEnabled = true;
                button.IsEnabled = true;
                dataGridWorker.IsEnabled = true;
                dataGridWorker.SelectedItem = null;
            }
        }

        private void button1_back_Click(object sender, RoutedEventArgs e)
        {
            Autorization.Visibility = Visibility.Hidden;
        }

        private void button1_enter_Click(object sender, RoutedEventArgs e)
        {
            Employee forpass = dataGridWorker.SelectedItem as Employee;
            if (passwordBox.Password == forpass.Password)
            {
                if (forpass.Role == "Admin")
                {
                    Admin f = new Admin();
                    f.Show();
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Неправильный пароль, попробуйте ещё раз", "Ошибка", MessageBoxButton.OK);
                passwordBox.Password = null;
            }
            
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Create.Visibility = Visibility.Hidden;
            textBox.Text = null;
            textBox1.Text = null;
            textBox2.Text = null;
            passwordBox1.Password = null;
            passwordBox1_Copy.Password = null;
        }

        private void button_Create_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox1.Password == passwordBox1_Copy.Password)
            {
                Employee emp = new Employee() { Name = textBox1.Text, Surname = textBox2.Text, Password = passwordBox1_Copy.Password, Login = textBox.Text, Role = "Admin" };
                db.Emloyees.Add(emp);
                db.SaveChanges();
            }
            textBox.Text = null;
            textBox1.Text = null;
            textBox2.Text = null;
            passwordBox1.Password = null;
            passwordBox1_Copy.Password = null;
            Create.Visibility = Visibility.Hidden;
            dataGridWorker.ItemsSource = db.Emloyees.ToList().OrderBy(x => x.Login);
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Create.Visibility = Visibility.Visible;
        }

        private void Create_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Create.IsVisible == true)
            {
                button_Copy.IsEnabled = false;
                button.IsEnabled = false;
                dataGridWorker.IsEnabled = false;
            }
            else
            {
                button_Copy.IsEnabled = true;
                button.IsEnabled = true;
                dataGridWorker.IsEnabled = true;
                dataGridWorker.SelectedItem = null;
            }
        }
    }
}
