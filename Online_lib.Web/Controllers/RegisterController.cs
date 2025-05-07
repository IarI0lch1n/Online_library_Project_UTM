using Online_lib.BusinessLogic.DBModel;
using Online_lib.Domain.Entities.User;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Online_lib.Web.Controllers
{
    public class RegisterController : Controller
    {
        private UserContext db = new UserContext();

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserProfileModel model)
        {
            //if (ModelState.IsValid)
            {
                var newUser = new UDbTable
                {
                    Username = model.FullName,
                    Email = model.Email,
                    Password = model.NewPassword,
                    City = model.City,
                    State = model.State,
                    Pincode = model.Pincode ?? 0,
                    DateOfBirth = model.DateOfBirth,
                    ContactNumber = model.ContactNumber,
                    //FullAddress = model.FullAddress ?? "N/A",
                    // и т.д.
                };

                db.Users.Add(newUser);   // <== создаст запись
                db.SaveChanges();        // <== создаст таблицу, если её нет

                ViewBag.Message = "Пользователь успешно зарегистрирован!";
                return RedirectToAction("Login", "Login");
            }

            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }

        
    }
}