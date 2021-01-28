using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class HomeController : Controller, ILogger<HomeController>
    {
        public ActionResult Index()
        {
            this.GetLog().Debug("Home page accessed");
            return View();
        }

        public ActionResult About()
        {
            this.GetLog().Debug("About page accessed");
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            this.GetLog().Debug("Contact page accessed");
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}