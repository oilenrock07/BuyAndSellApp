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

            var listNode = doc.DocumentNode.SelectSingleNode(@"//*[@id='listingsRow']");
            var nodes = listNode.SelectNodes("//div[@itemid='#product']");

            foreach (var node in nodes)
            {
                var product = new Product();
                product.Url = string.Format("https://www.olx.ph{0}", node.SelectSingleNode(".//a[1]").Attribute("href"));
                product.Name = node.SelectSingleNode(".//span[@class='title']").InnerText;
                product.Price = node.SelectSingleNode(".//meta[@itemprop='price']").Attribute("content");
                product.ImageUrl = node.SelectSingleNode(".//img[@itemprop='image']").Attribute("src");

                if (product.ImageUrl == "https://www.olx.ph/img/lazy-loader.gif")
                    product.ImageUrl = node.SelectSingleNode(".//img[@itemprop='image']").Attribute("data-src");

                product.DatePosted = node.SelectSingleNode(".//span[@class='posted']").InnerText;
                product.Location = node.SelectSingleNode(".//span[@class='location']").InnerText.Trim();
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
