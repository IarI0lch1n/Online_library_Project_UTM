using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Online_lib.Domain.Entities.User;
using Online_lib.BusinessLogic.DBModel;
using System.Security.Cryptography;
using System.Text;


namespace Online_lib.Web.Controllers
{
    public class LoginController : Controller
    {
        private UserContext db = new UserContext();

        public ActionResult Index()
        {
            return View("~/Views/Shared/_UserLogin.cshtml", new ULoginData());
        }

        [HttpPost]
        public ActionResult Login(ULoginData login)
        {
            var userLogin = CheckLoginData(login);

            if (userLogin.Status)
            {
                FormsAuthentication.SetAuthCookie(login.Name, false);

                var user = db.Users.FirstOrDefault(u =>
                    (u.Email == login.Name || u.ContactNumber == login.Name) &&
                    u.Password == login.Password);

                if (user != null)
                {
                    string guidToken = Guid.NewGuid().ToString();
                    string secretKey = "SuperStrongKey123!"; // желательно вынести в web.config

                    string hmacToken = GenerateHmac(guidToken, secretKey);

                    var cookie = new HttpCookie("LoginToken", hmacToken)
                    {
                        Expires = DateTime.Now.AddDays(7),
                        HttpOnly = true
                    };
                    Response.Cookies.Add(cookie);

                    user.LoginToken = guidToken;
                    db.SaveChanges();

                    Session["UserID"] = user.Id;
                    Session["UserFullName"] = user.Username;
                    Session["UserRole"] = user.Role;
                }

                return RedirectToAction("Dashboard", "Profile");
            }

            ViewBag.Error = "Неверный логин или пароль";
            return View("~/Views/Shared/_UserLogin.cshtml", login);
        }

        public ULoginData CheckLoginData(ULoginData data)
        {
            var user = db.Users.FirstOrDefault(u =>
                (u.Email == data.Name || u.ContactNumber == data.Name) &&
                u.Password == data.Password);

            return new ULoginData
            {
                Name = data.Name,
                Password = data.Password,
                Status = user != null
            };
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
        private string GenerateHmac(string input, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var inputBytes = Encoding.UTF8.GetBytes(input);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                var hashBytes = hmac.ComputeHash(inputBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
