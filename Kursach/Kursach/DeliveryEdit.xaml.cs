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

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для DeliveryEdit.xaml
    /// </summary>
    public partial class DeliveryEdit : Window
    {
        public int IdDelivery = -1;
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
                f.Price = Convert.ToDouble(textBox_PurchasePrice.Text);
                f.Count = Convert.ToInt32(textBox_Count.Text);
                db.Goodss.Find(edit.GoodsId).SellPrice = Convert.ToDouble(textBox_SellPrice.Text);
                db.Goodss.Find(f.GoodsId).PricePurchase = Convert.ToDouble(textBox_PurchasePrice.Text);
                dataGridGoods.ItemsSource = null;
            }
            else
            {
                delinfo.Price = Convert.ToDouble(textBox_PurchasePrice.Text);
                delinfo.Count = Convert.ToInt32(textBox_Count.Text);
                db.Goodss.Find(delinfo.GoodsId).SellPrice = Convert.ToDouble(textBox_SellPrice.Text);
                db.Goodss.Find(delinfo.GoodsId).PricePurchase = Convert.ToDouble(textBox_PurchasePrice.Text);
                lst.Add(delinfo);
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
                DeliveryViewModel del = dataGridGoods.SelectedItem as DeliveryViewModel;
                lst.Remove(lst.FirstOrDefault(x => x == del));
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
                DeliveryNote delnew = db.DeliveryNotes.FirstOrDefault(x => x.Id == IdDelivery);
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
                foreach (DeliveryInfo s in deliver.DeliveryInfos)
                {
                    lst.Add(new DeliveryViewModel(s, s.Id));
                }
            }
            if (lst.Count != 0)
            {
                i = lst[lst.Count - 1].Id + 1;
            }
            dataGridGoods.ItemsSource = lst;
            SetSumma();
        }
    }
}
