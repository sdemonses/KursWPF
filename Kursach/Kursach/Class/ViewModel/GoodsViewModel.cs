using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Class.ViewModel
{
    public class GoodsViewModel
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
        
    }
    

}
