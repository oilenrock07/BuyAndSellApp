using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuyAndSellApp.Entities;

namespace BuyAndSellApp.BusinessLogic.DataStructures
{
    public class ProductResponse
    {
        public int AvailablePages { get; set; }
        public IEnumerable<Product> ProductList { get; set; }
    }
}
