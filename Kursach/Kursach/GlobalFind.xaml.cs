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


namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для GlobalFind.xaml
    /// </summary>
    public partial class GlobalFind : Window
    {
        UserContext db = new UserContext();
        public GlobalFind()
        {
            InitializeComponent();
            textBox_amm_from.Text = Convert.ToString(db.Weapons.Min(x => x.Ammunition));
            textBox_amm_to.Text = Convert.ToString(db.Weapons.Max(x => x.Ammunition));
            textBox_kill_from.Text = Convert.ToString(db.Weapons.Min(x => x.KillRange));
            textBox_kill_to.Text = Convert.ToString(db.Weapons.Max(x => x.KillRange));
            textBox_speed_from.Text = Convert.ToString(db.Weapons.Min(x => x.StartBulletSpeed));
            textBox_Caliber.Text = Convert.ToString(db.Weapons.Min(x => x.Сaliber));
            textBox_speed_to.Text = Convert.ToString(db.Weapons.Max(x => x.StartBulletSpeed));
            dataGrid_Weapon.ItemsSource = db.Weapons.ToList();
            dataGrid_Accessories.ItemsSource = db.Accessories.ToList();
            List<string> s = new List<string>() { "Аксесуар", "Оружие" };
            comboBox.ItemsSource = s;
            Weapon.Visibility = Visibility.Visible;
            checkBox.Visibility = Visibility.Visible;
            dataGrid_Weapon.Visibility = Visibility.Visible;
            Accessories.Visibility = Visibility.Hidden;
            dataGrid_Accessories.Visibility = Visibility.Hidden;
            comboBox.SelectedItem = "Оружие";

        }

        private void textBox_Automatic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox_speed_to.Text != "")
            {
                SortWeapon();
            }
        }

        private void SortWeapon()
        {




            List<Weapon> lst = db.Weapons.ToList();

            if (checkBox1_name.IsChecked == true)
            {
                lst = lst = lst.Where(x => x.CodeName.ToUpper().Contains(textBox_CodeName.Text.ToUpper())).ToList();
            }
            if (checkBox2_avtom.IsChecked == true)
            {
                lst = lst.Where((x => x.Automatic.ToUpper().Contains(textBox_Automatic.Text.ToUpper()))).ToList();
            }
            if (checkBox3_calibr.IsChecked == true)
            {
                double caliber;
                if (textBox_Caliber.Text == "")
                { }
                else
                {
                    caliber = Convert.ToDouble(textBox_Caliber.Text);
                    lst = lst.Where(x => x.Сaliber == caliber).ToList();
                }
            }
            if (checkBox4_amm.IsChecked == true)
            {
                int Ammunitionfrom;
                int Ammunitionto;
                if (textBox_amm_from.Text == "")
                    Ammunitionfrom = 0;
                else
                    Ammunitionfrom = Convert.ToInt16(textBox_amm_from.Text);


                if (textBox_amm_to.Text == "")
                    Ammunitionto = db.Weapons.Max(x => x.Ammunition);
                else
                    Ammunitionto = Convert.ToInt32(textBox_amm_to.Text);

                lst = lst.Where(x => x.Ammunition >= Ammunitionfrom).ToList();
                lst = lst.Where(x => x.Ammunition <= Ammunitionto).ToList();
            }
            if (checkBox5_kill.IsChecked == true)
            {
                int KillRangefrom;
                int KillRangeto;
                if (textBox_kill_from.Text == "")
                    KillRangefrom = 0;
                else
                    KillRangefrom = Convert.ToInt32(textBox_kill_from.Text);


                if (textBox_kill_from.Text == "")
                    KillRangeto = db.Weapons.Max(x => x.KillRange);
                else
                    KillRangeto = Convert.ToInt32(textBox_kill_to.Text);

                lst = lst.Where(x => x.KillRange >= KillRangefrom).ToList();
                lst = lst.Where(x => x.KillRange <= KillRangeto).ToList();
            }
            if (checkBox6_speed.IsChecked == true)
            {
                int StartBulletSpeedfrom;
                int StartBulletSpeedto;
                if (textBox_speed_to.Text == "")
                    StartBulletSpeedfrom = 0;
                else
                    StartBulletSpeedfrom = Convert.ToInt32(textBox_speed_from.Text);


                if (textBox_speed_to.Text == "")
                    StartBulletSpeedto = db.Weapons.Max(x => x.StartBulletSpeed);
                else
                    StartBulletSpeedto = Convert.ToInt32(textBox_speed_to.Text);

                lst = lst.Where(x => x.StartBulletSpeed >= StartBulletSpeedfrom).ToList();
                lst = lst.Where(x => x.StartBulletSpeed <= StartBulletSpeedto).ToList();
            }
            if (checkBox.IsChecked == true)
            {
                lst = lst.Where(x => x.Optic == checkBox.IsChecked).ToList();
            }

            lst = lst.Where(x => x.Type.ToUpper().Contains(textBox_type.Text.ToUpper())).ToList();
            dataGrid_Weapon.ItemsSource = lst;
        }

        private void textBox_CodeName_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (textBox_speed_to.Text != "")
            {
                SortWeapon();
            }

        }

        private void textBox_Caliber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox_speed_to.Text != "")
            {
                SortWeapon();
            }

        }

        private void checkBox1_name_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBox1_name.IsChecked == true)
            {
                textBox_CodeName.IsEnabled = true;
                
            }
            else
                textBox_CodeName.IsEnabled = false;
            SortWeapon();
        }

        private void checkBox2_avtom_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBox2_avtom.IsChecked == true)
            {
                textBox_Automatic.IsEnabled = true;
              
            }
            else
                textBox_Automatic.IsEnabled = false;
            SortWeapon();
        }

        private void checkBox3_calibr_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBox3_calibr.IsChecked == true)
            {
                textBox_Caliber.IsEnabled = true;
              
            }
            else
                textBox_Caliber.IsEnabled = false;
            SortWeapon();
        }

        private void checkBox4_amm_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBox4_amm.IsChecked == true)
            {
                textBox_amm_from.IsEnabled = true;
                textBox_amm_to.IsEnabled = true;
               
            }

            else
            {
                textBox_amm_from.IsEnabled = false;
                textBox_amm_to.IsEnabled = false;
            }
            SortWeapon();
        }

        private void checkBox5_kill_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBox5_kill.IsChecked == true)
            {
                textBox_kill_from.IsEnabled = true;
                textBox_kill_to.IsEnabled = true;
              
            }

            else
            {
                textBox_kill_from.IsEnabled = false;
                textBox_kill_to.IsEnabled = false;
            }
            SortWeapon();
        }

        private void checkBox6_speed_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBox6_speed.IsChecked == true)
            {
                textBox_speed_from.IsEnabled = true;
                textBox_speed_to.IsEnabled = true;
                
            }

            else
            {
                textBox_speed_from.IsEnabled = false;
                textBox_speed_to.IsEnabled = false;
            }
            SortWeapon();
        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            SortWeapon();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBox.SelectedItem.ToString() == "Оружие")
            {
                Weapon.Visibility = Visibility.Visible;
                checkBox.Visibility = Visibility.Visible;
                dataGrid_Weapon.Visibility = Visibility.Visible;
                Accessories.Visibility = Visibility.Hidden;
                dataGrid_Accessories.Visibility = Visibility.Hidden;
            }
            else
            {
                dataGrid_Accessories.Visibility = Visibility.Visible;
                Accessories.Visibility = Visibility.Visible;
                Weapon.Visibility = Visibility.Hidden;
                dataGrid_Weapon.Visibility = Visibility.Hidden;
                checkBox.Visibility = Visibility.Hidden;
            }
        }

        private void checkBox1_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox1.IsChecked == true)
                textBox.IsEnabled = true;
            else
                textBox.IsEnabled = false;
            sortAccess();
        }

        private void checkBox2_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox2.IsChecked == true)
                textBox2.IsEnabled = true;
            else
                textBox2.IsEnabled = false;
            sortAccess();
        }

        private void sortAccess()
        {
            List<Accessories> lst = db.Accessories.ToList();

            if (checkBox1.IsChecked == true)
            {
                lst = lst = lst.Where(x => x.Name.ToUpper().Contains(textBox.Text.ToUpper())).ToList();
            }
            if (checkBox2.IsChecked == true)
            {
                lst = lst.Where((x => x.Characteristics.ToUpper().Contains(textBox2.Text.ToUpper()))).ToList();
            }

            lst = lst.Where(x => x.Type.ToUpper().Contains(textBox_type.Text.ToUpper())).ToList();
            dataGrid_Accessories.ItemsSource = lst;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sortAccess();
        }

        private void dataGrid_Accessories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Accessories s = dataGrid_Accessories.SelectedItem as Accessories;
            Goodss q = db.Goodss.FirstOrDefault(x => x.Id == s.Id);
            label_balance.Content = "На складе : " + Convert.ToString(q.Balance);
            label_Purchase.Content = "Цена закупки : " + Convert.ToString(q.PricePurchase);
            label_Sell.Content = "Цена продажи : " + Convert.ToString(q.SellPrice);
        }

        private void dataGrid_Weapon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Weapon s = dataGrid_Weapon.SelectedItem as Weapon;
            Goodss q = db.Goodss.FirstOrDefault(x => x.Id == s.Id);
            label_balance.Content = "На складе : " + Convert.ToString(q.Balance);
            label_Purchase.Content = "Цена закупки : " + Convert.ToString(q.PricePurchase);
            label_Sell.Content = "Цена продажи : " + Convert.ToString(q.SellPrice);
        }
    }
}
