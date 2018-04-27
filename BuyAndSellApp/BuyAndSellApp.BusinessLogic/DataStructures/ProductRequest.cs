
namespace BuyAndSellApp.BusinessLogic.DataStructures
{
    public class ProductRequest
    {
        public ProductRequest()
        {
            Page = 1;
        }

        public string Keyword { get; set; }
        public int Page { get; set; }

        //add more in the future
        //public string PriceRange1 { get; set; }
    }
}
