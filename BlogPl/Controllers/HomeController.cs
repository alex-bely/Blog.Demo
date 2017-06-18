using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPL.Controllers
{
    /// <summary>
    /// Provides methods for basic views handling
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "You are really stubborn!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us with the links below";

            return View();
        }
    }
}