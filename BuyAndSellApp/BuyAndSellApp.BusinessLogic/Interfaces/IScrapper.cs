using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuyAndSellApp.BusinessLogic.DataStructures;
using BuyAndSellApp.Entities;
using HtmlAgilityPack;

namespace BuyAndSellApp.BusinessLogic.Interfaces
{
    public interface IScrapper
    {
        Task<ProductResponse> GetProductList(ProductRequest request);
        Task<ProductResponse> MapDocumentToProduct(HtmlDocument document);
    }
}
