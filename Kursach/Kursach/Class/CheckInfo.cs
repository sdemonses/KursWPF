using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class
{
    public class CheckInfo
    {
        public int Id { get; set; }
        public int? CheckId { get; set; }
        public virtual Check Check { get; set; }
        public int? GoodsId { get; set; }
        public virtual Goodss Goods { get; set; }
        public int Count { get; set; }
        public double Sum { get { return Goods.SellPrice * Count; } }
    }
}
