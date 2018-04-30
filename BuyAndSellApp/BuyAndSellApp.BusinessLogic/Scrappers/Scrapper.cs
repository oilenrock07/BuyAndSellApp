using System.Collections.Generic;
using BuyAndSellApp.BusinessLogic.DataStructures;
using BuyAndSellApp.Entities;
using HtmlAgilityPack;
using System.IO;

namespace BuyAndSellApp.BusinessLogic.Scrappers
{
    public abstract class Scrapper
    {
        public virtual ProductResponse LoadProductListFromFile(string fileName)
        {
            var doc = new HtmlDocument();
            var html = File.ReadAllText(fileName);
            doc.LoadHtml(html);

            var productList = MapDocumentToProduct(doc);
            return productList;
        }

        public abstract ProductResponse MapDocumentToProduct(HtmlDocument doc);
    }
}
