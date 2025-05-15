using System;
using System.Linq;
using System.Web.Mvc;
using Online_lib.Domain.Entities.User;
using Online_lib.BusinessLogic.DBModel;

namespace Online_lib.Web.Controllers
{
    public class ProfileController : Controller
    {
        private UserContext db = new UserContext();

        public ActionResult Dashboard()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Index", "Login");

            int userId = (int)Session["UserID"];

            using (var db = new UserContext())
            {
                var user = db.Users.Find(userId);
                if (user == null)
                    return HttpNotFound();

                var model = new UserProfileModel
                {
                    UserID = user.Id,
                    FullName = user.Username,
                    DateOfBirth = user.DateOfBirth,
                    ContactNumber = user.ContactNumber,
                    Email = user.Email,
                    State = user.State,
                    City = user.City,
                    Pincode = user.Pincode,
                    FullAddress = user.FullAddress,

                    Books = db.UserBooks
                              .Where(b => b.UserId == user.Id)
                              .ToList()
                };

                return View("~/Views/Shared/_UserProfile.cshtml", model);
            }
        }

        [HttpPost]
        public ActionResult UpdateProfile(UserProfileModel model)
        {
            var user = db.Users.Find(model.UserID);

            if (user != null)
            {
                user.Username = model.FullName;
                user.DateOfBirth = model.DateOfBirth;
                user.ContactNumber = model.ContactNumber;
                user.Email = model.Email;
                user.State = model.State;
                user.City = model.City;
                user.Pincode = model.Pincode;
                user.FullAddress = model.FullAddress;

                db.SaveChanges();
            }

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public ActionResult ChangePassword(UserProfileModel model)
        {
            var user = db.Users.Find(model.UserID);

            if (user != null && model.OldPassword == user.Password)
            {
                user.Password = model.NewPassword;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Пароль успешно изменён.";
            }
            else
            {
                TempData["ErrorMessage"] = "Старый пароль неверен.";
            }

            return RedirectToAction("Dashboard");
        }
    }
}
