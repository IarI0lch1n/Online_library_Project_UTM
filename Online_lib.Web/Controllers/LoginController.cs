using Online_lib.BusinessLogic;
using Online_lib.BusinessLogic.Interface;
using Online_lib.Domain.Entities.User;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Online_lib.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession _session;

        public LoginController()
        {
            _session = new SessionBL();
        }

        // POST: /Login/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ULoginData login)
        {
            if (ModelState.IsValid)
            {
                // Сохраняем IP и дату входа
                login.LoginIp = Request.UserHostAddress;
                login.LoginDateTime = DateTime.Now;

                // Передаём данные в бизнес-логику
                var userLogin = _session.UserLogin(login);

                if (userLogin.Status)
                {
                    // Успешный вход — создаём авторизационную куку
                    FormsAuthentication.SetAuthCookie(login.Name, false);

                    // Перенаправляем на домашнюю страницу
                    return RedirectToAction("ChangeHomepage", "Home", new { homepage = "_Homepage" });
                }
                else
                {
                    // Неверный логин/пароль — показываем ошибку
                    ViewBag.Error = userLogin.StatusMsg;
                    return View("Index", login); // предполагаем, что есть Index.cshtml
                }
            }// Если модель невалидна — возвращаем её обратно
            return View("Index", login);
        }

        // GET: /Login/Index
        [HttpGet]
        public ActionResult Index()
        {
            return View(new ULoginData());
        }

        // GET: /Login/Logout
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}