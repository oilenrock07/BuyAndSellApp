using System.Collections.Generic;
using BuyAndSellApp.Entities;
using BuyAndSellApp.Entities.Enums;

namespace BuyAndSellApp.BusinessLogic.DataStructures
{
    public class ProductResponse
    {
        public string Keyword { get; set; }        
        public int AvailablePages { get; set; } //for olx
        public int CurrentPage { get; set; }
        public string NextPageUrl { get; set; } //carousell
        public ProductSource Source { get; set; }
        public IEnumerable<Product> ProductList { get; set; }
    }
}
