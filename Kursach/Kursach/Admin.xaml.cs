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
using System.Data.Entity;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        UserContext db = new UserContext();
        public Admin()
        {
            InitializeComponent();
            var s = db.Goods.Include("Weapon");
            dataGridGoods.ItemsSource = GoodsViewMode(s.Include("Accessories").ToList());
                
        }

        private void label_Goods_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Order.Visibility = Visibility.Hidden;
            Goods.Visibility = Visibility.Visible;
            Delivery.Visibility = Visibility.Hidden;
        }

        private void label_Order_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Order.Visibility = Visibility.Visible;
            Goods.Visibility = Visibility.Hidden;
            Delivery.Visibility = Visibility.Hidden;
        }

        private void label_Delivery_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Order.Visibility = Visibility.Hidden;
            Goods.Visibility = Visibility.Hidden;
            Delivery.Visibility = Visibility.Visible;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
        public List<GoodsViewModel> GoodsViewMode(List<Goods> good)
        {
            List<GoodsViewModel> lst = new List<GoodsViewModel>();
            foreach (Goods p in good)
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Goods f = new Goods();
            f.ShowDialog();
        }
    }
                lst.Add(s);
            }
            return lst;
        }
       

    }

}
