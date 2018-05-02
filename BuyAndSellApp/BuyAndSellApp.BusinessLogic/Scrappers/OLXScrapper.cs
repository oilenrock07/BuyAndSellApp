using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuyAndSellApp.BusinessLogic.DataStructures;
using BuyAndSellApp.BusinessLogic.Interfaces;
using BuyAndSellApp.Entities;
using HtmlAgilityPack;
using System.IO;
using BuyAndSellApp.BusinessLogic.Extensions;
using BuyAndSellApp.Entities.Enums;

namespace BuyAndSellApp.BusinessLogic.Scrappers
{
    public class OLXScrapper : Scrapper, IScrapper
    {
        private readonly string _urlTemplate = "https://www.olx.ph/all-results?q={0}&page={1}";
        private readonly HtmlWeb _web = new HtmlWeb();

        public virtual ProductResponse GetProductList(ProductRequest request)
        {
            try
            {
                var url = string.Format(_urlTemplate, request.Keyword, request.Page);
                var doc = _web.Load(url);
                var result = MapDocumentToProduct(doc);
                result.CurrentPage = request.Page;
                result.Keyword = request.Keyword;
                result.Source = request.Source;
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

            var listNode = doc.DocumentNode.SelectSingleNode(@"//*[@id='listingsRow']");
            var nodes = listNode.SelectNodes("//div[@itemid='#product']");

            foreach (var node in nodes)
            {
                var product = new Product();
                product.Url = string.Format("https://www.olx.ph{0}", node.SelectSingleNode(".//a[1]").Attribute("href"));
                product.Name = node.SelectSingleNode(".//span[@class='title']").InnerText;
                product.Price = node.SelectSingleNode(".//meta[@itemprop='price']").Attribute("content").CleanPrice();
                product.ImageUrl = node.SelectSingleNode(".//img[@itemprop='image']").Attribute("src");

                if (product.ImageUrl == "https://www.olx.ph/img/lazy-loader.gif")
                    product.ImageUrl = node.SelectSingleNode(".//img[@itemprop='image']").Attribute("data-src");

                product.DatePosted = node.SelectSingleNode(".//span[@class='posted']").InnerText;
                product.Location = node.SelectSingleNode(".//span[@class='location']").InnerText.Trim();
                product.Source = ProductSource.Olx;
                productList.Add(product);
            }

            var availablePages = 0;
            var pagination = doc.DocumentNode.SelectSingleNode("//ul[contains(@class, 'pagination')]");
            if (pagination != null)
            {
                var lastPage = pagination.SelectSingleNode("./li[last()-1]/a").InnerText;
                availablePages = Convert.ToInt32(lastPage);
            }

            response.AvailablePages = availablePages;
            response.ProductList = productList;
            return response;
        }
    }
}
