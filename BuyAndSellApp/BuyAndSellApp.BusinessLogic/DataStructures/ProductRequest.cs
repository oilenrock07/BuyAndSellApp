
using BuyAndSellApp.Entities.Enums;

namespace BuyAndSellApp.BusinessLogic.DataStructures
{
    public class ProductRequest
    {
        public ProductRequest()
        {
            Page = 1;
        }

        public ProductSource Source { get; set; }
        public string Keyword { get; set; }
        public int Page { get; set; } //for olx
        public string NextPage { get; set; } //for carousell
    }
}
