using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BuyAndSellApp.BusinessLogic.DataStructures;
using BuyAndSellApp.BusinessLogic.Extensions;
using BuyAndSellApp.BusinessLogic.Interfaces;
using BuyAndSellApp.Entities;
using HtmlAgilityPack;

namespace BuyAndSellApp.BusinessLogic.Scrappers
{
    public class CarousellScrapper : Scrapper, IScrapper
    {
        private readonly string _urlTemplate = "https://carousell.com/search/products/?query={0}";
        private readonly HtmlWeb _web = new HtmlWeb();

        public virtual ProductResponse GetProductList(ProductRequest request)
        {
            try
            {
                var url = string.Format(_urlTemplate, request.Keyword, request.Page);
                var doc = _web.Load(url);
                return MapDocumentToProduct(doc);
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public virtual async Task<ProductResponse> GetProductListAsync(ProductRequest request)
        {
            try
            {
                var url = string.Format(_urlTemplate, request.Keyword, request.Page);
                var doc = _web.Load(url);
                return await Task.Run(() => MapDocumentToProduct(doc));
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public override ProductResponse MapDocumentToProduct(HtmlDocument doc)
        {
            var response = new ProductResponse();
            var productList = new List<Product>();

            var nodes = doc.DocumentNode.SelectNodes("//div[@class='q-X']");

            foreach (var node in nodes)
            {
                var product = new Product();
                product.Url = string.Format("https://carousell.com{0}", node.SelectSingleNode(".//a[@class='q-e']").Attribute("href"));
                product.Name = node.SelectSingleNode(".//h4[@id='productCardTitle']").InnerText;
                product.Price = node.SelectSingleNode(".//dd[1]").InnerText;
                product.ImageUrl = node.SelectSingleNode(".//noscript/img").Attribute("src");
                product.DatePosted = node.SelectSingleNode(".//time/span").InnerText;
                productList.Add(product);
            }

            var pagination = doc.DocumentNode.SelectSingleNode("//li[contains(@class, 'pagination-next')]");
            if (pagination != null)
                response.NextPageUrl = pagination.SelectSingleNode("./a").Attribute("href");

            response.ProductList = productList;
            return response;
        }
    }
}
