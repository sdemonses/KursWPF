﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class
{
    public class OrderInfo
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int? GoodsId { get; set; }
        public virtual Goodss Goods { get; set; }
        public int Count { get; set; }
    }
}
