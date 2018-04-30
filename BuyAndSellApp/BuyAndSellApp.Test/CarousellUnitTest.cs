using System;
using System.Linq;
using BuyAndSellApp.BusinessLogic.DataStructures;
using BuyAndSellApp.BusinessLogic.Scrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuyAndSellApp.Test
{
    [TestClass]
    public class CarousellUnitTest
    {
        [TestMethod]
        public void TestGettingProductList()
        {
            var scrapper = new CarousellScrapper();
            var request = new ProductRequest { Keyword = "ps4" };
            var products = scrapper.GetProductList(request);

            Assert.IsTrue(products != null && products.ProductList.Any());
        }

        [TestMethod]
        public void TestLoadingProductListFromFile()
        {
            var scrapper = new CarousellScrapper();
            var products = scrapper.LoadProductListFromFile("c:/carousell.txt");
            Assert.IsTrue(products != null && products.AvailablePages > 0);
        }
    }
}
