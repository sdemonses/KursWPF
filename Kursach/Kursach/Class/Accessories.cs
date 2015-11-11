using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Kursach.Class
{
    class Accessories
    {
        [Key]
        [ForeignKey("Goods")]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Characteristics { get; set; }
        public Goods Goods { get; set; }

    }
}
