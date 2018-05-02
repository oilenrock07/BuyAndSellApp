using BuyAndSellApp.BusinessLogic.DataStructures;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BuyAndSelApp.Models
{
    public class ProductListViewModel
    {
        public string Search { get; set; }
        public IEnumerable<ProductResponse> ProductResponse { get; set; }

        public string Request { get; set; }
        public string SerializedRequest
    {
            get
            {
                if (ProductResponse != null)
                {
                    var requests = ProductResponse.Select(x => new ProductRequest
                    {
                        Keyword = x.Keyword,
                        Page = ++x.CurrentPage,
                        NextPage = x.NextPageUrl,
                        Source = x.Source
                    });

                    return JsonConvert.SerializeObject(requests);
                }

                return null;
            }
        }
    }
}