using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class.ViewModel
{
    class GoodsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double SellPrice { get; set; }
        public double PricePurchase { get; set; }
        public int Balance { get; set; }
        public string Type { get; set; }
        
        public GoodsViewModel()
        {

        }
        public GoodsViewModel (List<Goods> good)
        {
            List<GoodsViewModel> lst = new List<GoodsViewModel>();
            foreach (Goods p in good)
            {
                GoodsViewModel s = new GoodsViewModel() {SellPrice = p.SellPrice, PricePurchase = p.PricePurchase, Balance = p.Balance};
                if (p.Accessories == null)
                {
                    s.Name = p.Weapon.CodeName;
                    s.Type = "Weapon";
                }
                else
                {
                    s.Name = p.Accessories.Name;
                    s.Type = p.Accessories.Type;
                }
            }
        }
    }

}
