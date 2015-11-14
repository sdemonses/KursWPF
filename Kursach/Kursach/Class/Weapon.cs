using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kursach.Class
{
    public class Weapon
    {
        [Key]
        [ForeignKey("Goods")]
        public int Id { get; set; }
        public string Type { get; set; }
        public string CodeName { get; set; }
        public string Autimatic { get; set; }
        public double Сaliber { get; set; }
        public int KillRange { get; set; }
        public int Ammunition { get; set; }
        public int SpeedBulletSpeed { get; set; }
        public bool Optic { get; set; }

        public Goods Goods { get; set; }
    }
}
