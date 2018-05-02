using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuyAndSellApp.BusinessLogic.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuyAndSellApp.Test
{
    [TestClass]
    public class ScrappingUnitTest
    {
        [TestMethod]
        public void TestGetProductList()
        {
            LoadProducts();
        }

        private async void LoadProducts()
        {
            var helper = new ScrapperHelper();
            var keywords = new[] {"ps4", "ps3"};
            //var products = await helper.GetProductList(keywords);
        }
    }
}
