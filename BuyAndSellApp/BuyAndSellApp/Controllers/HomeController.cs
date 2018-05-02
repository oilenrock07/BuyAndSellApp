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
using BuyAndSellApp.Entities.Enums;
using Newtonsoft.Json;

namespace BuyAndSelApp.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index(string keyword = "")
        {
            var viewModel = new ProductListViewModel();
            if (!string.IsNullOrEmpty(keyword))
                viewModel.ProductResponse = await GetProductListings(keyword);
            
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ProductListViewModel viewModel)
        {
            if (!string.IsNullOrEmpty(viewModel.Search) && viewModel.Request == null)
            {
                var keywords = viewModel.Search.Split(',').Where(x => !string.IsNullOrEmpty(x.Trim()));
                var requests = new List<ProductRequest>();
                foreach (var keyword in keywords)
                {
                    requests.Add(new ProductRequest { Keyword = keyword});
                    requests.Add(new ProductRequest { Keyword = keyword, Source = ProductSource.Carousell});
                }
                viewModel.ProductResponse = await GetProductListings(requests);
            }
            else if (viewModel.Request != null)
            {
                var requests = JsonConvert.DeserializeObject<IEnumerable<ProductRequest>>(viewModel.Request);
                viewModel.ProductResponse = await GetProductListings(requests);
            }

            viewModel.Request = null;
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
        private async Task<IEnumerable<ProductResponse>> GetProductListings(string keyword)
        {
            var requests = new List<ProductRequest>
            {
                new ProductRequest {Keyword = keyword},
                new ProductRequest {Keyword = keyword, Source = ProductSource.Carousell}
            };

            return await GetProductListings(requests);
        }

        private async Task<IEnumerable<ProductResponse>> GetProductListings(IEnumerable<ProductRequest> requests)
        {
            var helper = new ScrapperHelper();
            var listings = await helper.GetProductList(requests);

            return listings;
        }
        #endregion
    }
}