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
        public int? GoodsId { get; set; }
        public Goods Goods { get; set; }
        public int Count { get; set; }
        public double Sum { get { return Goods.SellPrice * Count; } }
    }
}
