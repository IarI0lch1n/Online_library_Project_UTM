using Online_lib.BusinessLogic.DBModel;
using Online_lib.Domain.Entities.User;
using System.Collections.Generic;
using System.Linq;

namespace Online_lib.BusinessLogic
{
    public class AuthorBL
    {
        private readonly UserContext _db;

        public AuthorBL(UserContext db)
        {
            _db = db;
        }

        public List<Author> GetAllAuthors()
        {
            return _db.Authors.ToList();
        }

        public void AddAuthor(Author author)
        {
            _db.Authors.Add(author);
            _db.SaveChanges();
        }

        public void DeleteAuthor(int id)
        {
            var author = _db.Authors.Find(id);
            if (author != null)
            {
                _db.Authors.Remove(author);
                _db.SaveChanges();
            }
        }
    }
}
