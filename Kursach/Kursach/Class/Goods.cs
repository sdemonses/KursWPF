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
        public Weapon Weapon { get; set; }
        public Accessories Accessories { get; set; }
        public ICollection<CheckInfo> CheckInfos { get; set; }
        public ICollection<OrderInfo> OrderInfos { get; set; }
        public ICollection<DeliveryInfo> DeliveryInfos { get; set; }
        public Goodss()
        {
            CheckInfos = new List<CheckInfo>();
            OrderInfos = new List<OrderInfo>();
            DeliveryInfos = new List<DeliveryInfo>();
        }
    }
}
