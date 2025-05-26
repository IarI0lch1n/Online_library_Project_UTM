using Online_lib.BusinessLogic;
using Online_lib.BusinessLogic.DBModel;
using Online_lib.Domain.Entities.User;
using System.Web.Mvc;

namespace Online_lib.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly SessionBL _sessionBL;

        public RegisterController()
        {
            _sessionBL = new SessionBL(new UserContext());
        }

        public ActionResult Index()
        {
            return View("_UserSignIn");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserProfileModel model)
        {
            var result = _sessionBL.RegisterUser(model);

            if (result.Status)
            {
                ViewBag.Message = result.StatusMsg;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = result.StatusMsg;
            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
