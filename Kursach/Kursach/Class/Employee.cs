﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kursach.Class
{
    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Display(Name="Login")]
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public ICollection<Check> Checks { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<DeliveryNote> DeliveryNotes { get; set; }
        public ICollection<WorkingTime> WorkingTime { get; set; }
        public Employee()
        {
            Checks = new List<Check>();
            Orders = new List<Order>();
            DeliveryNotes = new List<DeliveryNote>();
            WorkingTime = new List<WorkingTime>();
        }
    }
}
