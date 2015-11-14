using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class
{
    public class Order
    {
        public int Id { get; set; }
        public int Sum { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int? EmployeesId { get; set; }
        public Employee Employees { get; set; }

        public ICollection<OrderInfo> OrderInfos { get; set; }
        public Order()
        {
            OrderInfos = new List<OrderInfo>();
        }

    }
}
