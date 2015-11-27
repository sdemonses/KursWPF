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
    /// Логика взаимодействия для DeliveryEdit.xaml
    /// </summary>
    public partial class DeliveryEdit : Window
    {
        public int IdDelivery = -1;
        public int EmployeeId;
        List<DeliveryViewModel> lst = new List<DeliveryViewModel>();
        DeliveryViewModel delinfo = null;
        public int i = 1;

        public DeliveryEdit()
        {
            InitializeComponent();
            UserContext db = new UserContext();
            
            var q = db.Goodss.Include("Weapon");
            dataGrid.ItemsSource = GoodsViewMode(q.Include("Accessories").ToList());
           
            GoodsGrid.Visibility = Visibility.Hidden;
            CountPriceGrid.Visibility = Visibility.Hidden;
            List<string> combobox = new List<string>() { "Наличная", "Безналичная", "Неоплачена" };
            comboBox_TypePayment.ItemsSource = combobox;
          
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
                DeliveryViewModel edit = dataGridGoods.SelectedItem as DeliveryViewModel;
                textBox_Count.Text = Convert.ToString(edit.Count);
                textBox_PurchasePrice.Text = Convert.ToString(edit.Price);
                textBox_SellPrice.Text = Convert.ToString(db.Goodss.Find(edit.GoodsId).SellPrice);
            }
            else
            {
                MessageBox.Show("Вы ничего не выбрали!", "Ошибка");
            }
            SetSumma();
        }
        

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            UserContext db = new UserContext();
            if (delinfo == null)
            {
   
                DeliveryViewModel edit = dataGridGoods.SelectedItem as DeliveryViewModel;
                DeliveryViewModel f = lst.FirstOrDefault(x => x == edit);
                db.Goodss.FirstOrDefault(x => x.Id == f.GoodsId).Balance -= f.Count;
                f.Price = Convert.ToDouble(textBox_PurchasePrice.Text);
                f.Count = Convert.ToInt32(textBox_Count.Text);
                db.Goodss.Find(edit.GoodsId).SellPrice = Convert.ToDouble(textBox_SellPrice.Text);
                db.Goodss.Find(f.GoodsId).PricePurchase = Convert.ToDouble(textBox_PurchasePrice.Text);
                db.Goodss.FirstOrDefault(x => x.Id == f.GoodsId).Balance += f.Count;
                dataGridGoods.ItemsSource = null;
            }
            else
            {
                delinfo.Price = Convert.ToDouble(textBox_PurchasePrice.Text);
                delinfo.Count = Convert.ToInt32(textBox_Count.Text);
                db.Goodss.Find(delinfo.GoodsId).SellPrice = Convert.ToDouble(textBox_SellPrice.Text);
                db.Goodss.Find(delinfo.GoodsId).PricePurchase = Convert.ToDouble(textBox_PurchasePrice.Text);
                lst.Add(delinfo);
                db.Goodss.FirstOrDefault(x => x.Id == delinfo.GoodsId).Balance += delinfo.Count;
                dataGridGoods.ItemsSource = null;
                delinfo = null;
            }
            db.SaveChanges();
          
            button1_Click(null, null);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            textBox_SellPrice.Text = null;
            textBox_PurchasePrice.Text = null;
            textBox_Count.Text = "1";
            MainGrid.IsEnabled = true;
            CountPriceGrid.Visibility = Visibility.Hidden;
            MainGrid.Visibility = Visibility.Visible;
            GoodsGrid.Visibility = Visibility.Hidden;
            dataGridGoods.ItemsSource = lst;
            SetSumma();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                delinfo = new DeliveryViewModel();
                UserContext db = new UserContext();
                var q = db.Goodss.Include("Weapon");
                GoodsViewModel per = dataGrid.SelectedItem as GoodsViewModel;
                Goodss f = q.Include("Accessories").FirstOrDefault(x => x.Id == per.Id);
                delinfo.GoodsId = f.Id;
                delinfo.Id = i;
                if (f.Accessories == null)
                {
                    delinfo.Name = f.Weapon.CodeName;
                }
                else
                {
                    delinfo.Name = f.Accessories.Name;
                }
                CountPriceGrid.Visibility = Visibility.Visible;
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

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Hidden;
            GoodsGrid.Visibility = Visibility.Visible;
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridGoods.SelectedItem !=null)
            {
                UserContext db = new UserContext();
                DeliveryViewModel del = dataGridGoods.SelectedItem as DeliveryViewModel;
                db.Goodss.FirstOrDefault(x => x.Id == del.GoodsId).Balance -= del.Count;
                lst.Remove(lst.FirstOrDefault(x => x == del));
                dataGridGoods.ItemsSource = null;
                dataGridGoods.ItemsSource = lst;
            }
            else
            {
                MessageBox.Show("Вы ничего не выбрали!", "Ошибка");
            }
            SetSumma();
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            UserContext db = new UserContext();
            if (IdDelivery== -1)
            {
               
                DeliveryNote delnew = new DeliveryNote();
                delnew.Employees = db.Emloyees.FirstOrDefault(x => x.Id == EmployeeId);
                delnew.TypePayment = comboBox_TypePayment.SelectedItem as string;
                delnew.Sum = 0;
                foreach (DeliveryViewModel delvm in lst)
                {
                    DeliveryInfo delinfo = new DeliveryInfo();
                    delinfo.Count = delvm.Count;
                    delinfo.Price = delvm.Price;
                    delinfo.Goods = db.Goodss.Find(delvm.GoodsId);
                    delnew.Sum += delvm.Summa;
                    delnew.DeliveryInfos.Add(delinfo);
                
                }
                db.DeliveryNotes.Add(delnew);
            }
            else
            {
                DeliveryNote delnew = db.DeliveryNotes.FirstOrDefault(x=>x.Id==IdDelivery);
                delnew.Employees = db.Emloyees.FirstOrDefault(x => x.Id == EmployeeId);
                delnew.TypePayment = comboBox_TypePayment.SelectedItem as string;
                delnew.Sum = 0;
                delnew.DeliveryInfos.Clear();
                foreach(DeliveryViewModel delvm in lst)
                {
                    DeliveryInfo delinfo = new DeliveryInfo();
                    delinfo.Count = delvm.Count;
                    delinfo.Price = delvm.Price;
                    delinfo.Goods = db.Goodss.Find(delvm.GoodsId);
                    delnew.Sum += delvm.Summa;
                    delnew.DeliveryInfos.Add(delinfo);
                }
            }
            db.SaveChanges();
            Close();
        }
        public void SetSumma()
        {
            double sum = 0;
            foreach (DeliveryViewModel s in lst)
            {
                sum += s.Summa;
            }
            textBlock.Text = Convert.ToString(sum);
        }

        private void Lol_Loaded(object sender, RoutedEventArgs e)
        {
            UserContext db = new UserContext();
            if (IdDelivery != -1)
            {
                DeliveryNote deliver = db.DeliveryNotes.FirstOrDefault(x => x.Id == IdDelivery);
                label4.Content = deliver.Employees.Surname + "  " + deliver.Employees.Name;
                foreach (DeliveryInfo s in deliver.DeliveryInfos)
                {
                    lst.Add(new DeliveryViewModel(s, s.Id));
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
            SetSumma();
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

        private void textBox_PurchasePrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBox.Text == "Поиск")
                textBox.Text = null;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserContext db = new UserContext();
            var s = db.Goodss.Include("Weapon");
            List<GoodsViewModel> list = GoodsViewMode(s.Include("Accessories").ToList());
            dataGrid.ItemsSource = list.Where(x => x.Name.ToUpper().Contains(textBox.Text.ToUpper()));
        }
    }
}
