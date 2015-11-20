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
using Kursach.Class.ViewModel;
using Kursach.Class;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для WorkerMain.xaml
    /// </summary>
    public partial class WorkerMain : Window
    {
        public int EmployeeId {get; set; }
        int i = 1;
        double Sum = 0;
        GoodsViewModel view;
        UserContext db1 = new UserContext();
        Brush LabelCancelAdd;
        Brush LabelSibmitAdd;
        List<CheckViewModel> checks = new List<CheckViewModel>();
        public WorkerMain()
        {
            UserContext db = new UserContext();
            InitializeComponent();
            //Запоняем таблицу для нового чека
            dataGridCheck.ItemsSource = checks;
            //Запоняем таблицу для покупателей
            dataGridCustomer.ItemsSource = db.Customers.ToList();
            //Запоняем таблицу для Товаров
            var s = db.Goodss.Include("Weapon");
            dataGridGoods.ItemsSource = GoodsViewMode(s.Include("Accessories").ToList());
            AddGoodsGrid.Visibility = Visibility.Hidden;
        }

        //Конвертирование Списка товаров для отображение в таблице доступных к продаже товаров аналогия главному окну для админа
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

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var s = db1.Goodss.Include("Weapon");
            List<GoodsViewModel> list = GoodsViewMode(s.Include("Accessories").ToList());
            dataGridGoods.ItemsSource = list.Where(x => x.Name.ToUpper().Contains(textBox.Text.ToUpper()));
        }


        private void dataGridGoods_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
               if (dataGridGoods.SelectedItem != null)
                {
                    AddGoodsGrid.Visibility = Visibility.Visible;
                    view = dataGridGoods.SelectedItem as GoodsViewModel;
                    label_Price_set.Content = view.SellPrice;
                    Main.IsEnabled = false;
                }
            }
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            LabelSibmitAdd = b.Background;
            b.Background = Brushes.Gray;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            b.Background = LabelSibmitAdd;
        }

        private void CancelAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddGoodsGrid.Visibility = Visibility.Hidden;
            textBox_Count.Text = null;
            dataGridGoods.SelectedItem = null;
            Main.IsEnabled = true;
        }

        private void CancelAdd_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            LabelCancelAdd = b.Background;
            b.Background = Brushes.Gray;
        }

        private void CancelAdd_MouseLeave(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            b.Background = LabelCancelAdd;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CheckViewModel ch = new CheckViewModel();
            ch.Count = Convert.ToInt32(textBox_Count.Text);
            ch.Price = view.SellPrice;
            ch.Name = view.Name;
            ch.Id = i;
            ch.GoodId = view.Id;
            i++;
            Sum += ch.Sum;
            checks.Add(ch);
            AddGoodsGrid.Visibility = Visibility.Hidden;
            dataGridCheck.ItemsSource = checks.ToList();
            label4.Content = Convert.ToString(Sum);
            textBox_Count.Text = null;
            Main.IsEnabled = true;
        }

        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Check ch = new Check();
            ch.Date = DateTime.Now;
            ch.Sum = Sum;
            ch.Employees = db1.Emloyees.Find(EmployeeId);
            db1.Checks.Add(ch);
            CheckInfo NewChIn = new CheckInfo();
        
            foreach (CheckViewModel nwch in checks)
            {
                CheckInfo f = new CheckInfo();
                f.Count = nwch.Count;
                f.Goods = db1.Goodss.Find(nwch.GoodId);
                ch.CheckInfos.Add(f);
            }
            db1.Checks.Add(ch);
            db1.SaveChanges();
            i = 0;
            Sum = 0;
            checks = null;
            dataGridCheck.ItemsSource = checks;
        }

        private void dataGridCheck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckViewModel dataforlabel = dataGridCheck.SelectedItem as CheckViewModel;
            label_Selected.Content = dataforlabel.Name;
            label_SummaSelected.Content = Convert.ToString(dataforlabel.Price) + " X " + Convert.ToString(dataforlabel.Count) + " = " + Convert.ToString(dataforlabel.Sum);
        }
    }
}