using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Online_lib.BusinessLogic;
using Online_lib.BusinessLogic.DBModel;
using Online_lib.Domain.Entities.User;

namespace Online_lib.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly BookBL _bookBL;

        public BookController()
        {
            _bookBL = new BookBL(new UserContext());
        }

        public ActionResult BookInventory(int? selectedId = null)
        {
            var model = new BookInventoryViewModel
            {
                Books = _bookBL.GetAllBooks(),
                FormData = new BookViewModel()
            };

            if (selectedId.HasValue)
            {
                var selected = _bookBL.GetBookViewModelById(selectedId.Value);
                if (selected != null)
                {
                    model.FormData = selected;
                }
                else
                {
                    ViewBag.BookError = $"Book with ID = {selectedId.Value} not found!";
                }
            }

            return View("~/Views/Shared/_BookInventory.cshtml", model);
        }


        [HttpPost]
        public ActionResult BookInventory(BookInventoryViewModel model, HttpPostedFileBase bookFile, string action)
        {
            switch (action)
            {
                case "add":
                    string filePath = "";
                    if (bookFile != null && bookFile.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(bookFile.FileName);
                        var path = Path.Combine(Server.MapPath("~/UploadedBooks/"), fileName);
                        bookFile.SaveAs(path);
                        filePath = "/UploadedBooks/" + fileName;
                    }
                    _bookBL.AddBook(model.FormData, filePath);
                    break;
            }

            return RedirectToAction("BookInventory");
        }

        public ActionResult Select(int id)
        {
            return RedirectToAction("BookInventory", new { selectedId = id });
        }

        public ActionResult Index()
        {
            return RedirectToAction("BookInventory");
        }
    }
}
