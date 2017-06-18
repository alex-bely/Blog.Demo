using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPL.Controllers
{
    /// <summary>
    /// Provides methods for Errors handling
    /// </summary>
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View("Error");
        }

        public ViewResult NotFound()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }

        public ViewResult BadRequest()
        {
            Response.StatusCode = 400;
            return View("BadRequest");
        }

        public ViewResult Forbidden()
        {
            Response.StatusCode = 403;
            return View("Forbidden");
        }
    }
}