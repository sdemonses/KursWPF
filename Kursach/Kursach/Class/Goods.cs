using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class
{
    public class Goodss
    {
        public int Id { get; set; }
        public int Balance { get; set; }
        public double PricePurchase { get; set; }
        public double SellPrice { get; set; }
        public virtual Weapon Weapon { get; set; }
        public virtual Accessories Accessories { get; set; }
        public virtual ICollection<CheckInfo> CheckInfos { get; set; }
        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
        public virtual ICollection<DeliveryInfo> DeliveryInfos { get; set; }
        public Goodss()
        {
            CheckInfos = new List<CheckInfo>();
            OrderInfos = new List<OrderInfo>();
            DeliveryInfos = new List<DeliveryInfo>();
        }
    }
}
