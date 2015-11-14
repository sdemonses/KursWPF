using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class
{
    public class DeliveryInfo
    {
        public int Id { get; set; }
        public int? DeliveryNoteId { get; set; }
        public DeliveryNote DeliveryNote { get; set; }
        public int? GoodsId { get; set; }
        public Goods Goods { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public double Summa { get { return Count*Price; }}
    }
}
