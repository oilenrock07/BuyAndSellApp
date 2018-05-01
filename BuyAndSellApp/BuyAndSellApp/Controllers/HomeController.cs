using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BuyAndSelApp.Models;
using BuyAndSellApp.BusinessLogic.DataStructures;
using BuyAndSellApp.BusinessLogic.Helpers;

namespace BuyAndSelApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string keyword = "")
        {
            var viewModel = new ProductListViewModel();
            if (!string.IsNullOrEmpty(keyword))
                viewModel.ProductResponse = GetProductListings(new[] {keyword});

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(ProductListViewModel viewModel)
        {
            if (!string.IsNullOrEmpty(viewModel.Search))
                viewModel.ProductResponse = GetProductListings(viewModel.Search.Split(','));
         
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region Private Methods

        private IEnumerable<ProductResponse> GetProductListings(IEnumerable<string> search)
        {
            return GetProductListings2(search).Result;
        }

        private async Task<IEnumerable<ProductResponse>> GetProductListings2(IEnumerable<string> search)
        {
            var helper = new ScrapperHelper();
            var listings = await helper.GetProductList(search);

            return listings;
        }
        #endregion
    }
}