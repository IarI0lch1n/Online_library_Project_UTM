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

        [HttpGet]
        public ActionResult ChangeHomepage(string homepage)
        {
            if (!string.IsNullOrEmpty(homepage))
            {
                Session["HomepageView"] = homepage; 
            }

            return RedirectToAction("Index", "Home"); 
        }
    }
}