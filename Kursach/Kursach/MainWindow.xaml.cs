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
            //using (UserContext db = new UserContext())
            //{
            //    Employee emp1 = new Employee() { Name = "Dmitry", Login = "sdemonses", Password = "Lol", Role = "Admin", Surname = "Bibliy"};
            //    Employee emp2 = new Employee() { Name = "D", Login = "sdemonses", Password = "Lol", Role = "Admin", Surname = "Bibliy" };
            //    db.Emloyees.Add(emp1);
            //    db.Emloyees.Add(emp2);
            //    db.SaveChanges();

            //}
          
            //dataGrid.ItemsSource = db.Emloyees;
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Autorization.Visibility = Visibility.Visible;
        }

        private void Autorization_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Autorization.IsVisible == true)
            {
                grid
            }
        }
    }
}
