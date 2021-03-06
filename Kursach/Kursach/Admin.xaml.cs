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
using Kursach.Class.ViewModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public int EmployeeId;
        public Admin()
        {
            UserContext db = new UserContext();
            InitializeComponent();
            OrderGrid.Visibility = Visibility.Hidden;
            GoodsGrid.Visibility = Visibility.Visible;
            DeliveryGrid.Visibility = Visibility.Hidden;
            WorkerGrid.Visibility = Visibility.Hidden;
            SQL.Visibility = Visibility.Hidden;
            Useful.Visibility = Visibility.Hidden;
            dataGrid_goods.Visibility = Visibility.Hidden;
            dataGrid_useful.Visibility = Visibility.Hidden;
            var s = db.Goodss.Include("Weapon");
            dataGridGoods.ItemsSource = GoodsViewMode(s.Include("Accessories").ToList());
            dataGridWorker.ItemsSource = db.Emloyees.ToList();
            dataGrid_Delivery.ItemsSource = db.DeliveryNotes.ToList();
            dataGridOrder.ItemsSource = db.Orders.ToList();
        }
        private void label_Worker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OrderGrid.Visibility = Visibility.Hidden;
            GoodsGrid.Visibility = Visibility.Hidden;
            DeliveryGrid.Visibility = Visibility.Hidden;
            WorkerGrid.Visibility = Visibility.Visible;
            SQL.Visibility = Visibility.Hidden;
            Useful.Visibility = Visibility.Hidden;
        }

        private void label_Order_Copy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OrderGrid.Visibility = Visibility.Hidden;
            GoodsGrid.Visibility = Visibility.Hidden;
            DeliveryGrid.Visibility = Visibility.Hidden;
            WorkerGrid.Visibility = Visibility.Hidden;
            SQL.Visibility = Visibility.Visible;
            Useful.Visibility = Visibility.Hidden;
        }

        private void label_Goods_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OrderGrid.Visibility = Visibility.Hidden;
            GoodsGrid.Visibility = Visibility.Visible;
            DeliveryGrid.Visibility = Visibility.Hidden;
            WorkerGrid.Visibility = Visibility.Hidden;
            SQL.Visibility = Visibility.Hidden;
            Useful.Visibility = Visibility.Hidden;
        }

        private void label_Order_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OrderGrid.Visibility = Visibility.Visible;
            GoodsGrid.Visibility = Visibility.Hidden;
            DeliveryGrid.Visibility = Visibility.Hidden;
            WorkerGrid.Visibility = Visibility.Hidden;
            SQL.Visibility = Visibility.Hidden;
            Useful.Visibility = Visibility.Hidden;
        }

        private void label_Delivery_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OrderGrid.Visibility = Visibility.Hidden;
            GoodsGrid.Visibility = Visibility.Hidden;
            DeliveryGrid.Visibility = Visibility.Visible;
            WorkerGrid.Visibility = Visibility.Hidden;
            SQL.Visibility = Visibility.Hidden;
            Useful.Visibility = Visibility.Hidden;
        }
        private void label_useful_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OrderGrid.Visibility = Visibility.Hidden;
            GoodsGrid.Visibility = Visibility.Hidden;
            DeliveryGrid.Visibility = Visibility.Hidden;
            WorkerGrid.Visibility = Visibility.Hidden;
            SQL.Visibility = Visibility.Hidden;
            Useful.Visibility = Visibility.Visible;
        }

        public List<GoodsViewModel> GoodsViewMode(List<Goodss> good)
        {
            List<GoodsViewModel> lst = new List<GoodsViewModel>();
            foreach (Goodss p in good)
            {
                GoodsViewModel s = new GoodsViewModel() { SellPrice = p.SellPrice, PricePurchase = p.PricePurchase, Balance = p.Balance, Id = p.Id };
                if (p.Accessories == null)
                {
                    s.Name = p.Weapon.CodeName;
                    s.Type = p.Weapon.Type;
                }
                else
                {
                    s.Name = p.Accessories.Name;
                    s.Type = p.Accessories.Type;
                }
                lst.Add(s);
            }
            return lst;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Goods f = new Goods();
            f.Closing += addNoteWindow_Closing;
            f.ShowDialog();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            UserContext db = new UserContext();
            if (dataGridGoods.SelectedItem != null)
            {
                GoodsViewModel s = dataGridGoods.SelectedItem as GoodsViewModel;
                var bd = db.Goodss.Include("Weapon");

                Goodss fnd = bd.Include("Accessories").FirstOrDefault(x=>x.Id==s.Id);
                Goods f = new Goods();
                f.textBox.Text = Convert.ToString(fnd.Balance);
                f.textBox1.Text = Convert.ToString(fnd.PricePurchase);
                f.textBox2.Text = Convert.ToString(fnd.SellPrice);
                f.textBox_Id.Text = Convert.ToString(fnd.Id);
                if (fnd.Accessories != null)
                {
                    Accessories Accessoriess = db.Accessories.Find(s.Id);
                    f.textBox_Type.Text = Accessoriess.Type;
                    f.textBox_Name.Text = Accessoriess.Name;
                    f.textBox_Charact.Text = Accessoriess.Characteristics;
                    f.TypeGoods.SelectedItem = "Аксесуар";
                }
                else
                {
                    Weapon Weapons = db.Weapons.Find(s.Id);
                    f.textBox_TypeWeap.Text = Weapons.Type;
                    f.textBox_NameWeap.Text = Weapons.CodeName;
                    f.textBox_Avtomat.Text = Weapons.Automatic;
                    f.textBox_Calibr.Text = Convert.ToString(Weapons.Сaliber);
                    f.textBox_Ammunition.Text = Convert.ToString(Weapons.Ammunition);
                    f.textBox_KillRange.Text = Convert.ToString(Weapons.KillRange);
                    f.textBox_StartSpeed.Text = Convert.ToString(Weapons.StartBulletSpeed);
                    f.textBox_Info.Text = Weapons.Info;
                    f.checkBox.IsChecked = Weapons.Optic;
                    f.TypeGoods.SelectedItem = "Оружие";
                }
                f.Closing += addNoteWindow_Closing;
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Вы не выбрали никого", "Ошибка");
            }
        }


      

        private void button_DeleteGoods_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridGoods.SelectedItem != null)
            {
                GoodsViewModel viewmodel = dataGridGoods.SelectedItem as GoodsViewModel;
                UserContext db = new UserContext();
                var f = db.Goodss.Include("Weapon");
                var g = f.Include("Accessories");
                Goodss q = g.FirstOrDefault(x=>x.Id==viewmodel.Id);
                db.Goodss.Remove(q);
                db.SaveChanges();
                var s = db.Goodss.Include("Weapon");
                dataGridGoods.ItemsSource = GoodsViewMode(s.Include("Accessories").ToList());
            }
            else
            {
                MessageBox.Show("Воу, ну ты выбери", "Ошибка");
            }
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult res;
            res = MessageBox.Show("Наработался?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

       

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            AddWorker wind = new AddWorker();
            wind.Closing += AddWorker_Closing;
            wind.ShowDialog();
        }

        private void button_DeleteWorker_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridWorker.SelectedItem != null)
            {
                UserContext db = new UserContext();
                Employee viewmodel = dataGridWorker.SelectedItem as Employee;
                Employee q = db.Emloyees.Find(viewmodel.Id);
               
                db.Emloyees.Remove(q);
                db.SaveChanges();
                UserContext db1 = new UserContext();
                dataGridWorker.ItemsSource = db1.Emloyees.ToList();
            }
            else
            {
                MessageBox.Show("Воу, ну ты выбери", "Ошибка");
            }
        }

        private void button_EditWorker_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridWorker.SelectedItem != null)
            {
                UserContext db = new UserContext();
                Employee s = dataGridWorker.SelectedItem as Employee;
                AddWorker wind = new AddWorker();
                wind.textBox_login.Text = s.Login;
                wind.textBox_name.Text = s.Name;
                wind.textBox_surname.Text = s.Surname;
                wind.comboBox_role.SelectedItem = s.Role;
                wind.textBlock_Id.Text = Convert.ToString(s.Id);
                wind.Closing += AddWorker_Closing;
                wind.ShowDialog();
           
            }
        }
        void addNoteWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserContext db = new UserContext();
            var s = db.Goodss.Include("Weapon");
            dataGridGoods.ItemsSource = GoodsViewMode(s.Include("Accessories").ToList());
        }
        void AddWorker_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserContext db = new UserContext();
            dataGridWorker.ItemsSource = db.Emloyees.ToList();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            DeliveryEdit f = new DeliveryEdit() { IdDelivery = -1 };
            f.EmployeeId = EmployeeId;
            f.Closing += DeliveryEdit_closing;
            f.ShowDialog();
           
        }
        void DeliveryEdit_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserContext db = new UserContext();
            dataGrid_Delivery.ItemsSource = db.DeliveryNotes.ToList();
            dataGridGoods.ItemsSource = GoodsViewMode(db.Goodss.ToList());
        }

        private void button_InfoDelivery_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid_Delivery.SelectedItem != null)
            {
                DeliveryNote delnote = dataGrid_Delivery.SelectedItem as DeliveryNote;
                DeliveryEdit wind = new DeliveryEdit() {IdDelivery = delnote.Id };
                wind.EmployeeId = EmployeeId;
                wind.Closing += DeliveryEdit_closing;
                wind.ShowDialog();
            }
            else
            {
                MessageBox.Show("Воу, ну ты выбери", "Ошибка");
            }
        }

        private void button_DeleteDelivery_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid_Delivery.SelectedItem != null)
            {
                UserContext db = new UserContext();
                DeliveryNote delnote = dataGrid_Delivery.SelectedItem as DeliveryNote;
                DeliveryNote del = db.DeliveryNotes.FirstOrDefault(x => x.Id == delnote.Id);
                foreach(DeliveryInfo s in del.DeliveryInfos)
                {
                    db.Goodss.FirstOrDefault(x => x.Id == s.GoodsId).Balance -= s.Count;
                }
                db.DeliveryNotes.Remove(del);
                db.SaveChanges();
                UserContext db1 = new UserContext();
                dataGrid_Delivery.ItemsSource = db1.DeliveryNotes.ToList();
                dataGridGoods.ItemsSource = GoodsViewMode(db1.Goodss.ToList());
            }
            else
            {
                MessageBox.Show("Воу, ну ты выбери", "Ошибка");
            }
        }

        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            if (dataGridWorker.SelectedItem != null)
            {
                Employee s = dataGridWorker.SelectedItem as Employee;
                if (s.Role == "Администратор")
                {
                    MessageBox.Show("Не для Администратора");
                }
                else
                {
                    Salary f = new Salary();
                    f.EmployeId = s.Id;
                    f.ShowDialog();
                }
                
            }
          
        }

        private void button_OrderNew_Click(object sender, RoutedEventArgs e)
        {
            OrderEdit f = new OrderEdit() { IdDelivery = -1 };
            f.EmployeeId = EmployeeId;
            f.Closing += OrderEdit_closing;
            f.ShowDialog();
        }
        void OrderEdit_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserContext db = new UserContext();
            dataGridOrder.ItemsSource = null;
            dataGridOrder.ItemsSource = db.Orders.ToList();
        }

        private void button_OrderDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOrder.SelectedItem != null)
            {
                UserContext db = new UserContext();
                Order delnote = dataGridOrder.SelectedItem as Order;
                Order del = db.Orders.FirstOrDefault(x => x.Id == delnote.Id);
                db.Orders.Remove(del);
                db.SaveChanges();
                UserContext db1 = new UserContext();
                dataGridOrder.ItemsSource = db1.Orders.ToList();
            }
            else
            {
                MessageBox.Show("Воу, ну ты выбери", "Ошибка");
            }
        }

        private void button_OrderEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOrder.SelectedItem != null)
            {
                Order delnote = dataGridOrder.SelectedItem as Order;
                OrderEdit wind = new OrderEdit() { IdDelivery = delnote.Id };
                wind.EmployeeId = EmployeeId;
                wind.Closing += OrderEdit_closing;
                wind.ShowDialog();
            }
            else
            {
                MessageBox.Show("Воу, ну ты выбери", "Ошибка");
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        const string ConnectionString = @"data source=(localdb)\v11.0;Initial Catalog=userstore.mdf;Integrated Security=True;";

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection sqlconn = new SqlConnection(ConnectionString);
                sqlconn.Open();
                SqlDataAdapter oda = new SqlDataAdapter(textBoxQuery.Text, sqlconn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                dataGridCustomQuery.DataContext = dt.DefaultView;
                sqlconn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error: " + ex.Message);
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            textBoxQuery.Text = "SELECT";
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            GlobalFind f = new GlobalFind();
            f.ShowDialog();
            List<GoodsViewModel> s = dataGridGoods.ItemsSource as List<GoodsViewModel>;
            int i = s.IndexOf(s.FirstOrDefault(x => x.Id == f.Id));
            dataGridGoods.SelectedIndex = i;

        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            UserContext db = new UserContext();
            List<CheckViewModel> lst = new List<CheckViewModel>();
            List<CheckInfo> WorkTime = db.CheckInfos.ToList().Where(x => x.Check.Date > DateTime.Now.AddDays(-30)).Where(x => x.Check.Date < DateTime.Now).ToList();
            foreach (CheckInfo s in WorkTime)
            {
                CheckViewModel t = new CheckViewModel();
                t.Id = s.Id;
                string name;
                if (s.Goods.Accessories == null)
                    name = s.Goods.Weapon.CodeName;
                else
                    name = s.Goods.Accessories.Name;
                t.Name = name;
                t.Price = s.Goods.SellPrice;
                t.Count = s.Count;
                t.GoodId = s.Goods.Id;
                lst.Add(t);
            }
            dataGrid_useful.ItemsSource = lst;
            dataGrid_goods.Visibility = Visibility.Hidden;
            dataGrid_useful.Visibility = Visibility.Visible;
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            UserContext db = new UserContext();
            var s = db.Goodss.Include("Weapon");
            dataGrid_goods.ItemsSource = GoodsViewMode(s.Include("Accessories").Where(x=>x.Balance<=5).ToList());
            dataGrid_goods.Visibility = Visibility.Visible;
            dataGrid_useful.Visibility = Visibility.Hidden;
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            //UserContext db = new UserContext();
            //List<WorkerRate> s = new List<WorkerRate>();
            //List<Employee> q = db.Emloyees.Include("Checks").Where(x => x.Role == "Пользователь").ToList();
            //foreach (Employee f in q)
            //{
            //    WorkerRate wr = new WorkerRate();
            //    wr.FullName = f.Surname + " "
            //}
        }
    }
}
