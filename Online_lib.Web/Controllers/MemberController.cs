using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Online_lib.Domain.Entities.User;
using Online_lib.BusinessLogic.DBModel;
using Online_lib.Domain.Enums;

namespace Online_lib.Web.Controllers
{
    public class MemberController : Controller
    {
        private UserContext db = new UserContext();

        public ActionResult ManageUsers(int? selectedId = null)
        {
            var allUsers = db.Users.ToList();
            var selectedUser = selectedId.HasValue ? db.Users.Find(selectedId.Value) : null;

            var model = Tuple.Create(allUsers, selectedUser);
            Session["HomepageView"] = null;

            return View("~/Views/Shared/_MemberManagement.cshtml", model);
        }

        public ActionResult ChangeRole(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                user.Role = user.Role == UserRole.Admin ? UserRole.User : UserRole.Admin;
                db.SaveChanges();
            }
            return RedirectToAction("ManageUsers", new { selectedId = id });
        }

        public ActionResult ToggleBlock(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                user.IsBlocked = !user.IsBlocked;
                db.SaveChanges();
            }
            return RedirectToAction("ManageUsers", new { selectedId = id });
        }

        public ActionResult DeleteUser(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }

            return RedirectToAction("ManageUsers");
        }

    }
}
