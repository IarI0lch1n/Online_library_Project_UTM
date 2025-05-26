using System;
using System.Web.Mvc;
using Online_lib.BusinessLogic;
using Online_lib.BusinessLogic.DBModel;

namespace Online_lib.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly MemberBL _memberBL;

        public MemberController()
        {
            _memberBL = new MemberBL(new UserContext());
        }

        public ActionResult ManageUsers(int? selectedId = null)
        {
            var allUsers = _memberBL.GetAllUsers();
            var selectedUser = selectedId.HasValue ? _memberBL.GetUserById(selectedId.Value) : null;

            var model = Tuple.Create(allUsers, selectedUser);
            Session["HomepageView"] = null;

            return View("~/Views/Shared/_MemberManagement.cshtml", model);
        }

        public ActionResult ChangeRole(int id)
        {
            _memberBL.ChangeRole(id);
            return RedirectToAction("ManageUsers", new { selectedId = id });
        }

        public ActionResult ToggleBlock(int id)
        {
            _memberBL.ToggleBlock(id);
            return RedirectToAction("ManageUsers", new { selectedId = id });
        }

        public ActionResult DeleteUser(int id)
        {
            _memberBL.DeleteUser(id);
            return RedirectToAction("ManageUsers");
        }
    }
}
