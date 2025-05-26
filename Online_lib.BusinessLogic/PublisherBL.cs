using Online_lib.BusinessLogic.DBModel;
using Online_lib.Domain.Entities.User;
using System.Collections.Generic;
using System.Linq;

namespace Online_lib.BusinessLogic
{
    public class PublisherBL
    {
        private readonly UserContext _db;

        public PublisherBL(UserContext db)
        {
            _db = db;
        }

        public List<Publisher> GetAllPublishers()
        {
            return _db.Publishers.ToList();
        }

        public void AddPublisher(Publisher publisher)
        {
            _db.Publishers.Add(publisher);
            _db.SaveChanges();
        }

        public void DeletePublisher(int id)
        {
            var publisher = _db.Publishers.Find(id);
            if (publisher != null)
            {
                _db.Publishers.Remove(publisher);
                _db.SaveChanges();
            }
        }
    }
}
