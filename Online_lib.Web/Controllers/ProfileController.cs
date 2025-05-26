using System;
using System.Web.Mvc;
using Online_lib.Domain.Entities.User;
using Online_lib.BusinessLogic;
using Online_lib.BusinessLogic.DBModel;

namespace Online_lib.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ProfileBL _profileBL;

        public ProfileController()
        {
            _profileBL = new ProfileBL(new UserContext());
        }

        public ActionResult Dashboard()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Index", "Login");

            int userId = (int)Session["UserID"];
            var model = _profileBL.GetUserProfileWithBooks(userId);

            if (model == null)
                return HttpNotFound();

            return View("~/Views/Shared/_UserProfile.cshtml", model);
        }

        [HttpPost]
        public ActionResult UpdateProfile(UserProfileModel model)
        {
            bool success = _profileBL.UpdateUserProfile(model);
            TempData["SuccessMessage"] = success ? "Profile updated." : "Error updating profile.";
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public ActionResult ChangePassword(UserProfileModel model)
        {
            bool changed = _profileBL.ChangePassword(model.UserID, model.OldPassword, model.NewPassword);
            TempData["SuccessMessage"] = changed ? "Password changed successfully." : "The old password is incorrect.";
            return RedirectToAction("Dashboard");
        }
    }
}
