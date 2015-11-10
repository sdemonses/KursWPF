using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class
{
    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public ICollection<Check> Checks { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<DeliveryNote> DeliveryNotes { get; set; }
        public Employee()
        {
            Checks = new List<Check>();
            Orders = new List<Order>();
            DeliveryNotes = new List<DeliveryNote>();
        }
    }
}
