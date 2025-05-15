using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_lib.BusinessLogic.DBModel;
using Online_lib.Domain.Entities.User;



namespace Online_lib.Web.Controllers
{
    public class BookController : Controller
    {
        private UserContext db = new UserContext();

        public ActionResult Index()
        {
            if (Session["UserRole"] == null)
                return RedirectToAction("Index", "Login");

            string role = Session["UserRole"].ToString();

            if (role == "Admin")
            {
                var books = db.UserBooks.ToList();
                return View("~/Views/Shared/_BookInventory.cshtml", books);
            }

            if (role == "User")
            {
                int userId = (int)Session["UserID"];
                var userBooks = db.UserBooks.Where(b => b.UserId == userId).ToList();
                return View("~/Views/Shared/_AdminBookingIssuing.cshtml", userBooks);
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult AddBook(BookViewModel model, HttpPostedFileBase bookFile)
        {
            if (bookFile != null && bookFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(bookFile.FileName);
                var path = Path.Combine(Server.MapPath("~/UploadedBooks/"), fileName);
                bookFile.SaveAs(path);

                var book = new UserBook
                {
                    BookName = model.BookName,
                    Description = model.Description,
                    Edition = model.Edition,
                    Price = model.Price,
                    Pages = model.Pages,
                    Language = model.Language,
                    Genres = model.Genres,
                    PublisherId = model.PublisherId,
                    AuthorId = model.AuthorId,
                    PublisherDate = model.PublisherDate ?? DateTime.Now,
                    ActualStock = model.ActualStock,
                    CurrentStock = model.ActualStock,
                    IssuedBooks = 0,
                    FileName = fileName,
                    FilePath = "/UploadedBooks/" + fileName,
                    Status = "Available"
                };

                try
                {
                    db.UserBooks.Add(book);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Content("Ошибка при сохранении: " + ex.Message);
                }

            }

            return RedirectToAction("ChangeHomepage", "Home", new { homepage = "_BookInventory" });
        }






        public ActionResult BookInventory()
        {
            // возможно, нужно вернуть представление с моделью
            return View();
        }

        [HttpPost]
        public ActionResult BookInventory(UserBook model, HttpPostedFileBase bookFile)
        {
            // сохранить файл и модель в БД
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

    }
}
