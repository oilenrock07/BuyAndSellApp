using System.Threading.Tasks;
using System.Collections.Generic;
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
            GetProductList();
        }

        [TestMethod]
        public void TestLoadingProductListFromFile()
        {
            LoadProductListFromFile();
        }

        [TestMethod]
        public void TestGetProductListAsynchronously()
        {
            GetProductListAsynchronously();
        }

        #region Private Methods

        private async void GetProductList()
        {
            var scrapper = new OLXScrapper();
            var request = new ProductRequest { Keyword = "ps4" };
            var products = await scrapper.GetProductList(request);
        }

        private async void LoadProductListFromFile()
        {
            var scrapper = new OLXScrapper();
            var products = await scrapper.LoadProductListFromFile("c:/olx.txt");
            Assert.IsTrue(products != null && products.AvailablePages > 0);
        }


        private async void GetProductListAsynchronously()
        {
            var scrapper = new OLXScrapper();
            var request = new ProductRequest { Keyword = "3ds" };

            var response = await scrapper.GetProductList(request);
            var productList = new List<Product>();
            object threadLock = new object();

            productList.AddRange(response.ProductList);
            
            if (response.AvailablePages > 0)
            {
                var taskList = new List<Task<ProductResponse>>();
                for (var ctr = 1; ctr <= 3; ++ctr)
                {
                    lock (threadLock)
                    {
                        request.Page += 1;
                        taskList.Add(scrapper.GetProductList(request));
                    }
                }

                var result = await Task.WhenAll(taskList);
                foreach (var productResponse in result)
                    productList.AddRange(productResponse.ProductList);
            }
        }


        private void GetMoreProducts()
        {
            
        }
        #endregion
    }
}
