using System;
using System.Web.Mvc;
using Online_lib.BusinessLogic;
using Online_lib.BusinessLogic.DBModel;
using Online_lib.Domain.Entities.User;

namespace Online_lib.Web.Controllers
{
    public class AdminAuthorManagementController : Controller
    {
        private readonly AuthorBL _authorBL;

        public AdminAuthorManagementController()
        {
            _authorBL = new AuthorBL(new UserContext());
        }

        public ActionResult Index()
        {
            var authors = _authorBL.GetAllAuthors();
            return View("~/Views/Shared/_AdminAuthorManagement.cshtml", authors);
        }

        [HttpPost]
        public ActionResult AddAuthor(string fullName, string country, DateTime dateOfBirth, DateTime? dateOfDeath)
        {
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                var newAuthor = new Author
                {
                    FullName = fullName,
                    Country = country,
                    DateOfBirth = dateOfBirth,
                    DateOfDeath = dateOfDeath
                };
                _authorBL.AddAuthor(newAuthor);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _authorBL.DeleteAuthor(id);
            return RedirectToAction("Index");
        }
    }
}
