using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using BuyAndSellApp.BusinessLogic.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuyAndSellApp.BusinessLogic.Scrappers;
using BuyAndSellApp.Entities;

namespace BuyAndSellApp.Test
{
    [TestClass]
    public class OLXScrapperUnitTest
    {
        [TestMethod]
        public void TestGettingProductList()
        {
            var scrapper = new OLXScrapper();
            var request = new ProductRequest { Keyword = "ps4" };
            var products = scrapper.GetProductList(request);

            Assert.IsTrue(products != null && products.ProductList.Any());
        }

        [TestMethod]
        public void TestGettingProductListAsync()
        {
            var response = new ProductResponse();
            GetProductListAsync(response);
            Assert.IsTrue(response != null && response.ProductList.Any());
        }

        [TestMethod]
        public void TestLoadingProductListFromFile()
        {
            var scrapper = new OLXScrapper();
            var products = scrapper.LoadProductListFromFile("c:/olx.txt");
            Assert.IsTrue(products != null && products.AvailablePages > 0);
        }

        [TestMethod]
        public void TestGetProductListAsynchronously()
        {
            var products = new List<Product>();
            GetProductListAsynchronously(products);
            Assert.IsTrue(products != null && products.Any());
        }

        #region Private Methods
        private async void GetProductListAsync(ProductResponse response)
        {
            var scrapper = new OLXScrapper();
            var request = new ProductRequest { Keyword = "ps4" };
            response = await scrapper.GetProductListAsync(request);
        }

        private async void GetProductListAsynchronously(List<Product> productList)
        {
            var scrapper = new OLXScrapper();
            var request = new ProductRequest {Keyword = "3ds"};
            object threadLock = new object();


            var taskList = new List<Task<ProductResponse>>();
            for (var ctr = 1; ctr <= 3; ++ctr)
            {
                lock (threadLock)
                {
                    request.Page += 1;
                    taskList.Add(scrapper.GetProductListAsync(request));
                }
            }

            var result = await Task.WhenAll(taskList);
            foreach (var productResponse in result)
                productList.AddRange(productResponse.ProductList);
        }

        #endregion
    }
}
