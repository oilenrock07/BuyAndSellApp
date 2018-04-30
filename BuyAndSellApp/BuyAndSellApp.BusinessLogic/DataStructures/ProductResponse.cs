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
        public string NextPageUrl { get; set; } //For carousell
        public IEnumerable<Product> ProductList { get; set; }
    }
}
