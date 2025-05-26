using Online_lib.BusinessLogic.DBModel;
using Online_lib.Domain.Entities.User;
using Online_lib.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Online_lib.BusinessLogic
{
    public class MemberBL
    {
        private readonly UserContext _db;

        public MemberBL(UserContext db)
        {
            _db = db;
        }

        public List<UDbTable> GetAllUsers()
        {
            return _db.Users.ToList();
        }

        public UDbTable GetUserById(int id)
        {
            return _db.Users.Find(id);
        }

        public void ChangeRole(int id)
        {
            var user = _db.Users.Find(id);
            if (user != null)
            {
                user.Role = user.Role == UserRole.Admin ? UserRole.User : UserRole.Admin;
                _db.SaveChanges();
            }
        }

        public void ToggleBlock(int id)
        {
            var user = _db.Users.Find(id);
            if (user != null)
            {
                user.IsBlocked = !user.IsBlocked;
                _db.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            var user = _db.Users.Find(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
        }
    }
}
