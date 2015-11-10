using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class
{
    class CheckInfo
    {
        public int Id { get; set; }
        public int? CheckId { get; set; }
        public Check Check { get; set; }
        public int? WeaponId { get; set; }
        public Weapon Weapon { get; set; }
        public int Count { get; set; }
    }
}
