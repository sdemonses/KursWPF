using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class
{
    class DeliveryNote
    {
        public int Id { get; set; }
        public int Sum { get; set; }
        public string TypePayment { get; set; }

        public int? EmployeesId { get; set; }
        public CheckInfo Employees { get; set; }

        public ICollection<DeliveryInfo> DeliveryInfos { get; set; }
        public DeliveryNote()
        {
            DeliveryInfos = new List<DeliveryInfo>();
        }
    }
}
