using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Online_lib.Domain.Entities.User;
using Online_lib.BusinessLogic;
using Online_lib.BusinessLogic.DBModel;

namespace Online_lib.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly SessionBL _sessionBL;

        public LoginController()
        {
            _sessionBL = new SessionBL(new UserContext());
        }

        public ActionResult Index()
        {
            return View("~/Views/Shared/_UserLogin.cshtml", new ULoginData());
        }
        [HttpPost]
        public ActionResult Login(ULoginData login)
        {
            var result = _sessionBL.UserLogin(login);

            if (result.Status && result.User != null)
            {
                FormsAuthentication.SetAuthCookie(login.Name, false);

                var user = result.User;

                var cookie = new HttpCookie("LoginToken", result.HmacToken)
                {
                    Expires = DateTime.Now.AddDays(7),
                    HttpOnly = true
                };
                Response.Cookies.Add(cookie);

                Session["UserID"] = user.Id;
                Session["UserFullName"] = user.Username;
                Session["UserRole"] = user.Role;

                return RedirectToAction("Dashboard", "Profile");
            }

            ViewBag.Error = result.StatusMsg;
            return View("~/Views/Shared/_UserLogin.cshtml", login);
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();

            if (Request.Cookies["LoginToken"] != null)
            {
                var cookie = new HttpCookie("LoginToken")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
