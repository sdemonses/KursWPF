﻿using System;
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
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для AddWorker.xaml
    /// </summary>
    public partial class AddWorker : Window
    {
        UserContext db = new UserContext();
        public AddWorker()
        {
            InitializeComponent();
            List<string> s = new List<string>() { "Админ", "Пользователь" };
            comboBox_role.ItemsSource = s;
        }
        bool allow = false;
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res;
            res = MessageBox.Show("Сохранить изменения?", "Подтверждение", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                bool allow = true;
                Employee nw = new Employee();
                if (textBlock_Id.Text != "-1")
                {
                    nw = db.Emloyees.Find(Convert.ToInt16(textBlock_Id.Text));
                }
                nw.Name = textBox_name.Text;
                nw.Surname = textBox_surname.Text;
                nw.Role = comboBox_role.SelectedItem as string;
                if (passwordBox.Password == passwordBox_approve.Password)
                {
                    nw.Password = passwordBox_approve.Password;
                }
                else
                {
                    allow = false;
                    MessageBox.Show("Пароли не совпадают", "Ошибка");
                }
                Employee main = db.Emloyees.FirstOrDefault(x => x.Login == textBox_login.Text) as Employee;
                if (main != null)
                {
                    allow = false;
                    MessageBox.Show("Логин занят", "Ошибка");
                }
                else
                {
                    nw.Login = textBox_login.Text;
                }
                if (allow && textBlock_Id.Text == "-1" && allow)
                {
                    db.Emloyees.Add(nw);
                    db.SaveChanges();
                    this.Close();
                }
                else if (allow && textBlock_Id.Text != "-1" && allow)
                {
                    db.SaveChanges();
                    this.Close();
                }                
            }
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res;
            res = MessageBox.Show("Уверены что не хотите сохранять?", "Ошибка", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void textBox_login_TextChanged(object sender, TextChangedEventArgs e)
        {

            string pattern = @"^[a-zA-Z ]+$";
            Regex rgx = new Regex(pattern);
            string test = textBox_login.Text;
            if (!rgx.IsMatch(test))
            {
                textBox_login.BorderBrush = new SolidColorBrush(Colors.Red);
                imgErrorName.Visibility = Visibility.Visible;
                allow = true;
            }
            else
            {
                textBox_login.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFABADB3"));
                imgErrorName.Visibility = Visibility.Hidden;
                AllowDrop = false;
            }
        }
    }
}
