﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class
{
    class OrderInfo
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        public int? WeaponId { get; set; }
        public Weapon Weapon { get; set; }
        public int Count { get; set; }
    }
}
