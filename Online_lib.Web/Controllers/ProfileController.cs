using Online_lib.BusinessLogic.DBModel;
using Online_lib.Domain.Entities.User;
using System.Linq;
using System.Web.Mvc;

namespace Online_lib.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserContext db = new UserContext();

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var user = db.Users.FirstOrDefault(u => u.Username == username);

            if (user == null) return RedirectToAction("Login", "Login");

            var model = new UserProfileModel
            {
                FullName = user.Username,
                Email = user.Email,
                UserID = user.Id,
               ////////////// /// Остальные поля можно расширить/////////////
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserProfileModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = db.Users.FirstOrDefault(u => u.Id == model.UserID);
            if (user == null) return RedirectToAction("Login", "Login");

            //////////////// Проверка пароля/////////////////////
            if (user.Password != model.OldPassword)
            {
                ModelState.AddModelError("", "Неверный старый пароль.");
                return View(model);
            }

            ////////////// Обновление данных////////////////////
            user.Email = model.Email;
            user.Password = model.NewPassword ?? user.Password;
            user.Username = model.FullName;

            db.SaveChanges();

            ViewBag.Message = "Профиль обновлён!";
            return View(model);
        }
    }
}
