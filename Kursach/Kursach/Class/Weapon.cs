using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class
{
    public class Weapon
    {
        [Key]
        [ForeignKey("Goodss")]
        public int Id { get; set; }
        public string Type { get; set; }
        public string CodeName { get; set; }
        public string Automatic { get; set; }
        public double Сaliber { get; set; }
        public int KillRange { get; set; }
        public int Ammunition { get; set; }
        public int StartBulletSpeed { get; set; }
        public bool Optic { get; set; }
        public string Info { get; set; }
        public virtual Goodss Goodss { get; set; }
    }
}
