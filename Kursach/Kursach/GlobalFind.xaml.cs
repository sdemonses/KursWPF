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
        }

        private void textBox_Automatic_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SortWeapon()
        {
            List<Weapon> lst = new List<Weapon>();
            lst = db.Weapons.Where(x => x.Type.ToUpper().Contains(textBox_type.Text.ToUpper())).Where(x => x.CodeName.ToUpper().Contains(textBox_CodeName.Text.ToUpper()))
                .Where((x => x.Automatic.ToUpper().Contains(textBox_Automatic.Text.ToUpper()))).ToList();
            
        }
    }
}
