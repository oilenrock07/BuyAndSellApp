using BuyAndSellApp.BusinessLogic.DataStructures;
using System.Collections.Generic;

namespace BuyAndSelApp.Models
{
    public class ProductListViewModel
    {
        public string Search { get; set; }
        public IEnumerable<ProductResponse> ProductResponse { get; set; }
    }
}