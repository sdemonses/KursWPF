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

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для Goods.xaml
    /// </summary>
    public partial class Goods : Window
    {
        public Goods()
        {
            InitializeComponent();
        }

        private void TypeGoods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeGoods.SelectedItem.ToString() == "Оружие")
            {
                Weapon.Visibility = Visibility.Visible;
                Accessories.Visibility = Visibility.Hidden;
            }
            if (TypeGoods.SelectedItem.ToString() == "Аксесуар")
            {
                Weapon.Visibility = Visibility.Hidden;
                Accessories.Visibility = Visibility.Visible;
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedItem.ToString() == "Пользовательский")
            {
                label_percent.Visibility = Visibility.Hidden;
                Percent_Mask.Visibility = Visibility.Hidden;
            }
            if (comboBox.SelectedItem.ToString() == "%")
            {
                label_percent.Visibility = Visibility.Visible;
                Percent_Mask.Visibility = Visibility.Visible;
            }
        }
    }
}
