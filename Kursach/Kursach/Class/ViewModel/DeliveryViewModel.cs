using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kursach.Class.ViewModel
{
    class DeliveryViewModel
    {
        [Display(Name = "№")]
        public int Id { get; set; }
        [Display(Name = "№ товара")]
        public int GoodsId { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Количество")]
        public int Count { get; set; }
        [Display(Name = "Цена")]
        public double Price { get; set; }
        [Display(Name = "Сумма")]
        public double Summa { get { return Count * Price; } }

        public DeliveryViewModel(DeliveryInfo lst, int id)
        {
            this.Count = lst.Count;
            Price = lst.Price;
            GoodsId = lst.Goods.Id;
            if(lst.Goods.Accessories == null)
            {
                Name = lst.Goods.Weapon.CodeName;
            }
            else
            {
               Name = lst.Goods.Accessories.Name;
            }
            Id = id;
        }
        public DeliveryViewModel()
        {

        }
    }
}
