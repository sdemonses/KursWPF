using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Kursach.Class
{
    class UserContext : DbContext
    {
        public UserContext()
            : base("DbConnection")
        { }
        public DbSet<Check> Checks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Emloyees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DeliveryNote> DeliveryNotes { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<DeliveryInfo> DeliveryInfos { get; set; }
        public DbSet<OrderInfo> OrderInfos { get; set; }
        public DbSet<CheckInfo> CheckInfos { get; set; }
        public DbSet<WorkingTime> WorkingTimes { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Accessories> Accessories { get; set; }

    }
}
