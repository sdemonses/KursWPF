using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class
{
    class Check
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Sum { get; set; }
        public int? EmployeesId { get; set; }
        public CheckInfo Employees { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<CheckInfo> CheckInfos { get; set; }
        public Check()
        {
            CheckInfos = new List<CheckInfo>();
        }

    }
}
