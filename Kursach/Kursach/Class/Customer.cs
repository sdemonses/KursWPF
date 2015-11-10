using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class
{
    class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PassportNumber { get; set; }
        public string PermitWeapon { get; set; }
        public ICollection<Check> Checks { get; set; }
        public Customer()
        {
            Checks = new List<Check>();
        }
    }
}
