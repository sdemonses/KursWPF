using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class.ViewModel
{
    class OrderViewModel
    {
        public int Id { get; set; }
   
        public int GoodsId { get; set; }
 
        public string Name { get; set; }
    
        public int Count { get; set; }
       
        public double Price { get; set; }
        public OrderViewModel (OrderInfo lst, int id)
        {
            this.Count = lst.Count;
            Price = lst.Goods.PricePurchase;
            GoodsId = lst.Goods.Id;
            if (lst.Goods.Accessories == null)
            {
                Name = lst.Goods.Weapon.CodeName;
            }
            else
            {
                Name = lst.Goods.Accessories.Name;
            }
            Id = id;
        }
        public OrderViewModel()
        {

        }
    }
}
