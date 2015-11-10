using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class
{
    class DeliveryInfo
    {
        public int Id { get; set; }
        public int? DeliveryNoteId { get; set; }
        public DeliveryNote DeliveryNote { get; set; }
        public int? WeaponId { get; set; }
        public Weapon Weapon { get; set; }
        public int Count { get; set; }
    }
}
