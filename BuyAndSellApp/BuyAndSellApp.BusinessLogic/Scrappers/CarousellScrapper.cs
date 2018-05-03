using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BuyAndSellApp.BusinessLogic.DataStructures;
using BuyAndSellApp.BusinessLogic.Extensions;
using BuyAndSellApp.BusinessLogic.Interfaces;
using BuyAndSellApp.Entities;
using BuyAndSellApp.Entities.Enums;
using HtmlAgilityPack;

namespace BuyAndSellApp.BusinessLogic.Scrappers
{
    public class CarousellScrapper : Scrapper, IScrapper
    {
        private readonly string _urlTemplate = "https://carousell.com/search/products/?sort_by=time_created%2Cdescending&query={0}";
        private readonly HtmlWeb _web = new HtmlWeb();

        public virtual ProductResponse GetProductList(ProductRequest request)
        {
            try
            {
                var url = string.Format(_urlTemplate, request.Keyword);
                var doc = _web.Load(url);
                var result = MapDocumentToProduct(doc);
                result.Keyword = request.Keyword;
                return result;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public virtual ProductResponse GetProductList(string url)
        {
            try
            {
                var doc = _web.Load(url);
                var result = MapDocumentToProduct(doc);
                return result;
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
                return await Task.Run(() =>
                {
                    var url = string.Format(_urlTemplate, request.Keyword, request.Page);
                    var doc = _web.Load(url);
                    var result = MapDocumentToProduct(doc);
                    result.Keyword = request.Keyword;
                    result.Source = request.Source;
                    return result;
                });
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

            var listNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'card-row')]");
            var nodes = listNode.SelectNodes("./div");

            foreach (var node in nodes)
            {
                var product = new Product();
                product.Url = string.Format("https://carousell.com{0}", node.SelectSingleNode(".//a[@id='productCardThumbnail']").Attribute("href"));
                product.Name = node.SelectSingleNode(".//h4[@id='productCardTitle']").InnerText;
                product.Price = node.SelectSingleNode(".//dd[1]").InnerText;
                product.Description = node.SelectSingleNode(".//dd[2]").InnerText;
                product.ImageUrl = node.SelectSingleNode(".//noscript/img").Attribute("src");
                product.DatePosted = node.SelectSingleNode(".//time/span").InnerText;
                product.Source = ProductSource.Carousell;
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
