using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using Online_lib.BusinessLogic.DBModel;
using Online_lib.Domain.Entities.User;



namespace Online_lib.Web.Controllers
{
    public class BookController : Controller
    {
        private UserContext db = new UserContext();

        

        [HttpPost]
        public ActionResult AddBook(BookInventoryViewModel model, HttpPostedFileBase bookFile)
        {
            var data = model.FormData;


            if (bookFile != null && bookFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(bookFile.FileName);
                var path = Path.Combine(Server.MapPath("~/UploadedBooks/"), fileName);
                bookFile.SaveAs(path);
                var filePath = "/UploadedBooks/" + fileName;


                var book = new UserBook
                {
                    BookName = data.BookName,
                    Title = data.BookName ?? "Без названия",
                    Author = "Автор по умолчанию",
                    Description = data.Description,
                    Edition = data.Edition,
                    Price = data.Price,
                    Pages = data.Pages,
                    Language = data.Language,
                    Genres = data.Genres,
                    PublisherId = data.PublisherId,
                    AuthorId = data.AuthorId,
                    PublisherDate = data.PublisherDate ?? DateTime.Now,
                    ActualStock = data.ActualStock,
                    CurrentStock = data.ActualStock,
                    IssuedBooks = 0,
                    FileName = fileName,
                    FilePath = filePath,
                    Status = "Available",
                    UserId = null
                };


                db.UserBooks.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("ChangeHomepage", "Home", new { homepage = "_BookInventory" });
        }

        public ActionResult BookInventory()
        {
            var model = new BookInventoryViewModel
            {
                Books = db.UserBooks.ToList()
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult BookInventory(UserBook model, HttpPostedFileBase bookFile)
        {
            return RedirectToAction("BookInventory");
        }

        [HttpPost]
        public ActionResult UpdateBook(UserBook model)
        {
            if (Session["UserRole"]?.ToString() != "Admin")
                return RedirectToAction("Index", "Login");

            var existing = db.UserBooks.Find(model.Id);
            if (existing != null)
            {
                existing.Title = model.Title;
                existing.Author = model.Author;
                existing.Status = model.Status;
                existing.UserId = model.UserId;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteBook(int id)
        {
            if (Session["UserRole"]?.ToString() != "Admin")
                return RedirectToAction("Index", "Login");

            var book = db.UserBooks.Find(id);
            if (book != null)
            {
                db.UserBooks.Remove(book);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UploadBook(UserBook model, HttpPostedFileBase BookFile)
        {
            if (ModelState.IsValid)
            {
                if (BookFile != null && BookFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(BookFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/UploadedBooks/"), fileName);
                    BookFile.SaveAs(path);

                    model.FileName = fileName;
                    model.FilePath = "/UploadedBooks/" + fileName;
                }

                using (var db = new UserContext())
                {
                    db.UserBooks.Add(model);
                    db.SaveChanges();
                }

                TempData["Success"] = "Book uploaded successfully!";
                return RedirectToAction("BookInventory");
            }

            return View("_BookInventory", model);
        }

        public ActionResult Index()
        {
            if (Session["UserRole"] == null)
                return RedirectToAction("Index", "Login");

            string role = Session["UserRole"].ToString();

            if (role == "Admin")
            {
                var books = db.UserBooks.ToList();
                var viewModel = new BookInventoryViewModel
                {
                    FormData = new BookViewModel(),
                    Books = books
                };

                return View("~/Views/Shared/_BookInventory.cshtml", viewModel);
            }

            if (role == "User")
            {
                int userId = (int)Session["UserID"];
                var userBooks = db.UserBooks.Where(b => b.UserId == userId).ToList();
                return View("~/Views/Shared/_BookIssuing.cshtml", userBooks);
            }

            return RedirectToAction("Index", "Login");
        }

    }
}
