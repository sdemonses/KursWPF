using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class
{
    class WorkingTime
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public int? EmployeesId { get; set; }
        public Employee Employees { get; set; }
    }
}
