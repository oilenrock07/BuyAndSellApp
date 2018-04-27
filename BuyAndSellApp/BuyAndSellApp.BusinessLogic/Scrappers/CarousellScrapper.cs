using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuyAndSellApp.BusinessLogic.DataStructures;
using BuyAndSellApp.BusinessLogic.Interfaces;
using BuyAndSellApp.Entities;
using HtmlAgilityPack;

namespace BuyAndSellApp.BusinessLogic.Scrappers
{
    public class CarousellScrapper : Scrapper, IScrapper
    {
        public virtual Task<ProductResponse> GetProductList(ProductRequest request)
        {
            return null;
        }

        public Task<ProductResponse> MapDocumentToProduct(HtmlDocument document)
        {
            return null;
        }
    }
}
