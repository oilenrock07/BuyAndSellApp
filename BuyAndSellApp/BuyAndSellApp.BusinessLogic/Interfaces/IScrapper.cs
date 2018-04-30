using System.Threading.Tasks;
using BuyAndSellApp.BusinessLogic.DataStructures;
using HtmlAgilityPack;

namespace BuyAndSellApp.BusinessLogic.Interfaces
{
    public interface IScrapper
    {
        ProductResponse GetProductList(ProductRequest request);
        Task<ProductResponse> GetProductListAsync(ProductRequest request);
        ProductResponse MapDocumentToProduct(HtmlDocument doc);
    }
}
