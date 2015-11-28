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

            int Ammunitionfrom = Convert.ToInt16(textBox_amm_from.Text);
            int Ammunitionto = Convert.ToInt32(textBox_amm_to.Text);
            int KillRangefrom = Convert.ToInt32(textBox_kill_from.Text);
            int KillRangeto = Convert.ToInt32(textBox_kill_to.Text);
            int StartBulletSpeedfrom = Convert.ToInt32(textBox_speed_from.Text);
            int StartBulletSpeedto = Convert.ToInt32(textBox_speed_to.Text);
            double caliber = Convert.ToDouble(textBox_Caliber.Text);


            List<Weapon> lst = new List<Weapon>();
            lst = db.Weapons.Where(x => x.Type.ToUpper().Contains(textBox_type.Text.ToUpper())).Where(x => x.CodeName.ToUpper().Contains(textBox_CodeName.Text.ToUpper()))
                .Where((x => x.Automatic.ToUpper().Contains(textBox_Automatic.Text.ToUpper()))).Where(x=>x.Сaliber == caliber)
                .Where(x=>x.Ammunition <= Ammunitionfrom)
                .Where(x => x.Ammunition >= Ammunitionto)
                .Where(x => x.KillRange >= KillRangefrom)
                .Where(x => x.KillRange <= KillRangeto)
                .Where(x => x.StartBulletSpeed >= StartBulletSpeedfrom)
                .Where(x => x.StartBulletSpeed <= StartBulletSpeedto)
                .Where(x => x.Optic == checkBox.IsChecked).ToList();
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
    }
}
