using Kursach.Class;
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
        UserContext db = new UserContext();
        public Goods()
        {
            InitializeComponent();
            List<string> s = new List<string>() { "Оружие", "Аксесуар" };
            TypeGoods.ItemsSource = s;
        }

        private void TypeGoods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var s = TypeGoods.SelectedItem as string;
           
            if (s == "Оружие")
            {

                WeaponGrid.Visibility = Visibility.Visible;
                AccessoriesGrid.Visibility = Visibility.Hidden;
            }
            if (s == "Аксесуар")
            {
                WeaponGrid.Visibility = Visibility.Hidden;
                AccessoriesGrid.Visibility = Visibility.Visible;
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

        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res;
            res = MessageBox.Show("Вы уверены, что хотите сохранить товар?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                if (textBox_Id.Text == "-1")
                {
                    if (TypeGoods.SelectedItem == null)
                    {
                        MessageBox.Show("Вы кароч не сделали ниче для того чтобы всё работало", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Question);
                    }
                    else if (TypeGoods.SelectedItem.ToString() == "Оружие")
                    {
                        Weapon f = new Weapon();
                        f.Type = textBox_TypeWeap.Text;
                        f.CodeName = textBox_NameWeap.Text;
                        f.Automatic = textBox_Avtomat.Text;
                        f.Сaliber = Convert.ToDouble(textBox_Calibr.Text);
                        f.KillRange = Convert.ToInt32(textBox_KillRange.Text);
                        f.Ammunition = Convert.ToInt32(textBox_Ammunition.Text);
                        f.StartBulletSpeed = Convert.ToInt32(textBox_StartSpeed.Text);
                        f.Optic = checkBox.IsChecked.Value;
                        f.Info = textBox_Info.Text;
                        Goodss god = new Goodss()
                        {
                            Balance = Convert.ToInt32(textBox.Text),
                            PricePurchase = Convert.ToInt32(textBox1.Text),
                            SellPrice = Convert.ToInt32(textBox2.Text),
                            Weapon = f
                        };
                        db.Goodss.Add(god);
                        db.SaveChanges();
                    }
                    else if (TypeGoods.SelectedItem.ToString() == "Аксесуар")
                    {
                        Accessories access = new Accessories();
                        access.Type = textBox_Type.Text;
                        access.Name = textBox_Name.Text;
                        access.Characteristics = textBox_Charact.Text;
                        Goodss god = new Goodss()
                        {
                            Accessories = access
                        };
                        god.Balance = Convert.ToInt32(textBox.Text);
                        god.PricePurchase = Convert.ToInt32(textBox1.Text);
                        god.SellPrice = Convert.ToInt32(textBox2.Text);
                        db.Goodss.Add(god);
                        db.SaveChanges();
                    }
                    
                }
                else
                {
                    Goodss f = db.Goodss.Find(Convert.ToInt32(textBox_Id.Text));
                    if (TypeGoods.SelectedItem.ToString() == "Оружие")
                    {
                        Weapon Weapons = db.Weapons.Find(Convert.ToInt32(textBox_Id.Text));
                        Weapons.Type = textBox_TypeWeap.Text;
                        Weapons.CodeName = textBox_NameWeap.Text;
                        Weapons.Automatic = textBox_Avtomat.Text;
                        Weapons.Сaliber = Convert.ToDouble(textBox_Calibr.Text);
                        Weapons.KillRange = Convert.ToInt32(textBox_KillRange.Text);
                        Weapons.Ammunition = Convert.ToInt32(textBox_Ammunition.Text);
                        Weapons.StartBulletSpeed = Convert.ToInt32(textBox_StartSpeed.Text);
                        Weapons.Optic = checkBox.IsChecked.Value;
                        Weapons.Info = textBox_Info.Text;
                        f.Balance = Convert.ToInt32(textBox.Text);
                        f.PricePurchase = Convert.ToInt32(textBox1.Text);
                        f.SellPrice = Convert.ToInt32(textBox2.Text);
                            
                        db.SaveChanges();
                    }
                    else if (TypeGoods.SelectedItem.ToString() == "Аксесуар")
                    {
                        Accessories Accessoriess = db.Accessories.Find(Convert.ToInt32(textBox_Id.Text));
                        Accessoriess.Type = textBox_Type.Text;
                        Accessoriess.Name = textBox_Name.Text;
                        Accessoriess.Characteristics = textBox_Charact.Text;
                        f.Balance = Convert.ToInt32(textBox.Text);
                        f.PricePurchase = Convert.ToInt32(textBox1.Text);
                        f.SellPrice = Convert.ToInt32(textBox2.Text);

                        db.SaveChanges();
                    }
                }
            }
            this.Close();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //if ((e. >= '0') && (e.KeyChar <= '9'))
            //{
            //    return;
            //}
            ////Точку заменим запятой
            //if (e.KeyChar == '.')
            //{
            //    e.KeyChar = ',';
            //}

            //if (e.KeyChar == ',')
            //{
            //    if (textBox1.Text.IndexOf(',') != -1)
            //    {
            //        //Уже есть одна запятая в textBox1
            //        e.Handled = true;
            //    }
            //    return;
            //}

            ////Управляющие клавиши <Backspace>, <Enter> и т.д.
            //if (Char.IsControl(e.KeyChar))
            //{
            //    return;
            //}

            ////Остальное запрещено
            //e.Handled = true;
        }
    }
}
