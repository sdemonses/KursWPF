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
using Kursach.Class.ViewModel;
namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для OrderEdit.xaml
    /// </summary>
    public partial class OrderEdit : Window
    {

        public int IdDelivery = -1;
        public int EmployeeId;
        List<OrderViewModel> lst = new List<OrderViewModel>();
        OrderViewModel delinfo = null;
        public int i = 1;
        public OrderEdit()
        {
            InitializeComponent();
            UserContext db = new UserContext();
            var q = db.Goodss.Include("Weapon");
            dataGrid.ItemsSource = GoodsViewMode(q.Include("Accessories").ToList());
            GoodsGrid.Visibility = Visibility.Hidden;
            CountPriceGrid.Visibility = Visibility.Hidden;

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
            if (dataGridGoods.SelectedItem != null)
            {
                MainGrid.IsEnabled = false;
                UserContext db = new UserContext();
                CountPriceGrid.Visibility = Visibility.Visible;
                OrderViewModel edit = dataGridGoods.SelectedItem as OrderViewModel;
                textBox_Count.Text = Convert.ToString(edit.Count);
                textBLock_Price.Text = Convert.ToString(edit.Price);
            }
            else
            {
                MessageBox.Show("Вы ничего не выбрали!", "Ошибка");
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Hidden;
            GoodsGrid.Visibility = Visibility.Visible;
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridGoods.SelectedItem != null)
            {
                UserContext db = new UserContext();
                OrderViewModel del = dataGridGoods.SelectedItem as OrderViewModel;
                lst.Remove(lst.FirstOrDefault(x => x == del));
                dataGridGoods.ItemsSource = null;
                dataGridGoods.ItemsSource = lst;
            }
            else
            {
                MessageBox.Show("Вы ничего не выбрали!", "Ошибка");
            }
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserContext db = new UserContext();
                if (IdDelivery == -1)
                {
                    Order delnew = new Order();
                    delnew.Employees = db.Emloyees.FirstOrDefault(x => x.Id == EmployeeId);
                    delnew.Date = DateTime.Now;
                    if (checkBox.IsChecked == true)
                        delnew.Status = true;
                    else
                        delnew.Status = false;

                    foreach (OrderViewModel delvm in lst)
                    {
                        OrderInfo delinfo = new OrderInfo();
                        delinfo.Count = delvm.Count;
                        delinfo.Goods = db.Goodss.Find(delvm.GoodsId);
                        delnew.OrderInfos.Add(delinfo);
                    }
                    db.Orders.Add(delnew);
                    delnew.SavePDF();
                }
                else
                {
                    Order delnew = db.Orders.Include("OrderInfos").FirstOrDefault(x => x.Id == IdDelivery);
                    delnew.Employees = db.Emloyees.FirstOrDefault(x => x.Id == EmployeeId);
                    delnew.OrderInfos.Clear();
                    delnew.Date = DateTime.Now;
                    if (checkBox.IsChecked == true)
                        delnew.Status = true;
                    else
                        delnew.Status = false;
                    delnew.OrderInfos.Clear();
                    foreach (OrderViewModel delvm in lst)
                    {
                        OrderInfo delinfo = new OrderInfo();
                        delinfo.Count = delvm.Count;
                        delinfo.Goods = db.Goodss.Find(delvm.GoodsId);
                        delnew.OrderInfos.Add(delinfo);
                    }
                    delnew.SavePDF();
                }

                foreach (OrderInfo s in db.OrderInfos.ToList())
                {
                    if (s.GoodsId == null)
                        db.OrderInfos.Remove(s);
                }

                db.SaveChanges();
                Close();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                delinfo = new OrderViewModel();
                UserContext db = new UserContext();
                var q = db.Goodss.Include("Weapon");
                GoodsViewModel per = dataGrid.SelectedItem as GoodsViewModel;
                Goodss f = q.Include("Accessories").FirstOrDefault(x => x.Id == per.Id);
                delinfo.GoodsId = f.Id;
                delinfo.Id = i;
                i++;
                textBLock_Price.Text = Convert.ToString(f.PricePurchase);
                if (f.Accessories == null)
                {
                    delinfo.Name = f.Weapon.CodeName;
                }
                else
                {
                    delinfo.Name = f.Accessories.Name;
                }
                CountPriceGrid.Visibility = Visibility.Visible;
                textBox_Count.Focus();
            }
            else
            {
                MessageBox.Show("Вы ничего не выбрали!", "Ошибка");
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Visible;
            textBox.Text = "Поиск";
            GoodsGrid.Visibility = Visibility.Hidden;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            textBLock_Price.Text = null;
            textBox_Count.Text = "1";
            MainGrid.IsEnabled = true;
            CountPriceGrid.Visibility = Visibility.Hidden;
            MainGrid.Visibility = Visibility.Visible;
            GoodsGrid.Visibility = Visibility.Hidden;
            dataGridGoods.ItemsSource = lst;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            UserContext db = new UserContext();
            if (delinfo == null)
            {

                OrderViewModel edit = dataGridGoods.SelectedItem as OrderViewModel;
                OrderViewModel f = lst.FirstOrDefault(x => x == edit);
                f.Price = Convert.ToDouble(textBLock_Price.Text);
                f.Count = Convert.ToInt32(textBox_Count.Text);
                dataGridGoods.ItemsSource = null;
            }
            else
            {
                delinfo.Price = Convert.ToDouble(textBLock_Price.Text);
                delinfo.Count = Convert.ToInt32(textBox_Count.Text);
                lst.Add(delinfo);
                dataGridGoods.ItemsSource = null;
                delinfo = null;
            }
            db.SaveChanges();

            button1_Click(null, null);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserContext db = new UserContext();
            if (IdDelivery != -1)
            {
                Order deliver = db.Orders.Include("OrderInfos").FirstOrDefault(x => x.Id == IdDelivery);
                label4.Content = deliver.Employees.Surname + "  " + deliver.Employees.Name;
                foreach (OrderInfo s in deliver.OrderInfos)
                {
                    lst.Add(new OrderViewModel(s, s.Id));
                }
            }
            else
            {
                Employee emp = db.Emloyees.FirstOrDefault(x => x.Id == EmployeeId);
                label4.Content = emp.Surname + "  " + emp.Name;
            }
            if (lst.Count != 0)
            {
                i = lst[lst.Count - 1].Id + 1;
            }
            dataGridGoods.ItemsSource = lst;
        }

        private void textBox_Count_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox Tb1 = sender as TextBox;
            if (Char.IsDigit(e.Text, 0))
            {
                e.Handled = false;
            }
            else e.Handled = true;
        }

        private void textBox_SellPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb1 = sender as TextBox;
            if (tb1.Text.IndexOf(",") == tb1.Text.Length - 1)
            {
                tb1.Text += "0";
            }
            if (tb1.Text[0] == '0')
                tb1.Text = tb1.Text.TrimStart(new char[] { '0' });
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserContext db = new UserContext();
            var s = db.Goodss.Include("Weapon");
            List<GoodsViewModel> list = GoodsViewMode(s.Include("Accessories").ToList());
            dataGrid.ItemsSource = list.Where(x => x.Name.ToUpper().Contains(textBox.Text.ToUpper()));
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            GlobalFind f = new GlobalFind();
            f.ShowDialog();
            List<GoodsViewModel> s = dataGridGoods.ItemsSource as List<GoodsViewModel>;
            int i = s.IndexOf(s.FirstOrDefault(x => x.Id == f.Id));
            dataGrid.SelectedIndex = i;
        }
    }
}
