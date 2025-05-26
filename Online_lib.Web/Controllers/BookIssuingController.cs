using System.Web.Mvc;
using Online_lib.BusinessLogic;
using Online_lib.BusinessLogic.DBModel;
using Online_lib.Domain.Entities.User;

namespace Online_lib.Web.Controllers
{
    public class BookIssuingController : Controller
    {
        private readonly BookBL _bookBL;

        public BookIssuingController()
        {
            _bookBL = new BookBL(new UserContext());
        }

        public ActionResult Index()
        {
            var model = new BookInventoryViewModel
            {
                Books = _bookBL.GetAllBooks()
            };

            string username = User.Identity.Name;
            ViewBag.IsAdmin = _bookBL.IsUserAdmin(username);

            return View("~/Views/Shared/_AdminBookingIssuing.cshtml", model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _bookBL.DeleteBook(id);
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var model = new BookInventoryViewModel
            {
                Books = _bookBL.GetAllBooks(),
                FormData = _bookBL.GetBookViewModelById(id)
            };

            return View("~/Views/Shared/_AdminBookingIssuing.cshtml", model);
        }

    }
}
