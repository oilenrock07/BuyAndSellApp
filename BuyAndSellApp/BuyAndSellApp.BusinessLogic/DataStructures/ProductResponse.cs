using System.Collections.Generic;
using BuyAndSellApp.Entities;

namespace BuyAndSellApp.BusinessLogic.DataStructures
{
    public class ProductResponse
    {
        public string Keyword { get; set; }
        public int AvailablePages { get; set; }
        public string NextPageUrl { get; set; } //For carousell
        public IEnumerable<Product> ProductList { get; set; }
    }
}
