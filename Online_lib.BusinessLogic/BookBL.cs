using Online_lib.BusinessLogic.DBModel;
using Online_lib.Domain.Entities.User;
using Online_lib.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Online_lib.BusinessLogic
{
    public class BookBL
    {
        private readonly UserContext _db;

        public BookBL(UserContext db)
        {
            _db = db;
        }

        public void AddBook(BookViewModel data, string filePath)
        {
            var book = new UserBook
            {
                BookName = data.BookName,
                Title = data.BookName ?? "",
                Author = data.AuthorId.ToString(),
                Genres = data.Genres,
                Description = data.Description,
                Edition = data.Edition,
                Price = data.Price,
                Pages = data.Pages,
                Language = data.Language,
                PublisherId = data.PublisherId,
                AuthorId = data.AuthorId,
                PublisherDate = data.PublisherDate,
                ActualStock = data.ActualStock,
                CurrentStock = data.CurrentStock,
                IssuedBooks = data.IssuedBooks,
                FilePath = filePath,
                UploadDate = DateTime.Now
            };

            _db.UserBooks.Add(book);
            _db.SaveChanges();
        }

        public List<BookViewModel> GetAllBooks()
        {
            return _db.UserBooks.Select(book => new BookViewModel
            {
                Id = book.Id,
                BookName = book.BookName,
                AuthorId = book.AuthorId,
                AuthorName = _db.Authors.FirstOrDefault(a => a.Id == book.AuthorId).FullName,
                PublisherName = _db.Publishers.FirstOrDefault(p => p.Id == book.PublisherId).Name,
                Genres = book.Genres,
                Description = book.Description,
                Edition = book.Edition,
                Price = book.Price,
                Pages = book.Pages,
                Language = book.Language,
                PublisherId = book.PublisherId,
                PublisherDate = book.PublisherDate,
                ActualStock = book.ActualStock,
                CurrentStock = book.CurrentStock,
                IssuedBooks = book.IssuedBooks,
                FilePath = book.FilePath,
                UploadDate = book.UploadDate
            }).ToList();
        }


        public UserBook GetBookById(int id)
        {
            return _db.UserBooks.FirstOrDefault(b => b.Id == id);
        }

        public BookViewModel GetBookViewModelById(int id)
        {
            var book = GetBookById(id);
            if (book == null) return null;

            return new BookViewModel
            {
                Id = book.Id,
                BookName = book.BookName,
                AuthorId = book.AuthorId,
                PublisherId = book.PublisherId,
                AuthorName = _db.Authors.FirstOrDefault(a => a.Id == book.AuthorId)?.FullName,
                PublisherName = _db.Publishers.FirstOrDefault(p => p.Id == book.PublisherId)?.Name,
                Genres = book.Genres,
                Description = book.Description,
                Edition = book.Edition,
                Price = book.Price,
                Pages = book.Pages,
                Language = book.Language,
                PublisherDate = book.PublisherDate,
                ActualStock = book.ActualStock,
                CurrentStock = book.CurrentStock,
                IssuedBooks = book.IssuedBooks,
                FilePath = book.FilePath,
                UploadDate = book.UploadDate
            };
        }


        public bool IsUserAdmin(string username)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == username);
            return user != null && user.Role == UserRole.Admin;
        }
        public void DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                _db.UserBooks.Remove(book);
                _db.SaveChanges();
            }
        }
        public string GetAuthorName(int authorId)
        {
            var author = _db.Authors.FirstOrDefault(a => a.Id == authorId);
            return author != null ? author.FullName : "—";
        }

        public void UpdateBookFromViewModel(BookViewModel model)
        {
            var book = GetBookById(model.Id);
            if (book == null) return;

            book.BookName = model.BookName;
            book.Title = model.BookName ?? "";
            book.Author = model.AuthorId.ToString();
            book.Genres = model.Genres;
            book.Description = model.Description;
            book.Edition = model.Edition;
            book.Price = model.Price;
            book.Pages = model.Pages;
            book.Language = model.Language;
            book.PublisherId = model.PublisherId;
            book.AuthorId = model.AuthorId;
            book.PublisherDate = model.PublisherDate;
            book.ActualStock = model.ActualStock;
            book.CurrentStock = model.CurrentStock;
            book.IssuedBooks = model.IssuedBooks;
            book.FilePath = model.FilePath;
            book.UploadDate = DateTime.Now;

            _db.SaveChanges();
        }

    }
}
