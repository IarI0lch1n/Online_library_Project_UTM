using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_lib.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View(); 
        }
        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Terms()
        {
            return View();
        }

    }
}
