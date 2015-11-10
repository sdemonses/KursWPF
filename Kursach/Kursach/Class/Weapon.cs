using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class
{
    class Weapon
    {
        public int Id { get; set; }
        public ICollection<CheckInfo> CheckInfos { get; set; }
        public ICollection<OrderInfo> OrderInfos { get; set; }
        public ICollection<DeliveryInfo> DeliveryInfos { get; set; }
        public Weapon()
        {
            CheckInfos = new List<CheckInfo>();
            OrderInfos = new List<OrderInfo>();
            DeliveryInfos = new List<DeliveryInfo>();
        }
    }
}
