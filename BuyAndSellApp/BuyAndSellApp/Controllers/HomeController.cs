using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BuyAndSelApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            WebClient client = new WebClient();
            var content = client.DownloadString("https://www.olx.ph/all-results?q=ps4+slim&page=3");
            return View();
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
    }
}