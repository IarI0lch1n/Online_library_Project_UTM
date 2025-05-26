using Online_lib.BusinessLogic.DBModel;
using Online_lib.Domain.Entities.User;
using System;
using System.Linq;

namespace Online_lib.BusinessLogic
{
    public class ProfileBL
    {
        private readonly UserContext _db;

        public ProfileBL(UserContext db)
        {
            _db = db;
        }

        public UserProfileModel GetUserProfile(int userId)
        {
            var user = _db.Users.Find(userId);
            if (user == null) return null;

            return new UserProfileModel
            {
                UserID = user.Id,
                FullName = user.Username,
                DateOfBirth = user.DateOfBirth,
                ContactNumber = user.ContactNumber,
                Email = user.Email,
                State = user.State,
                City = user.City,
                Pincode = user.Pincode,
                FullAddress = user.FullAddress
            };
        }

        public UserProfileModel GetUserProfileWithBooks(int userId)
        {
            var user = _db.Users.Find(userId);
            if (user == null) return null;

            return new UserProfileModel
            {
                UserID = user.Id,
                FullName = user.Username,
                DateOfBirth = user.DateOfBirth,
                ContactNumber = user.ContactNumber,
                Email = user.Email,
                State = user.State,
                City = user.City,
                Pincode = user.Pincode,
                FullAddress = user.FullAddress,
                Books = _db.UserBooks.Where(b => b.UserId == user.Id).ToList()
            };
        }

        public bool UpdateUserProfile(UserProfileModel model)
        {
            var user = _db.Users.Find(model.UserID);
            if (user == null) return false;

            user.Username = model.FullName;
            user.DateOfBirth = model.DateOfBirth;
            user.ContactNumber = model.ContactNumber;
            user.Email = model.Email;
            user.State = model.State;
            user.City = model.City;
            user.Pincode = model.Pincode ?? 0;
            user.FullAddress = model.FullAddress;

            _db.SaveChanges();
            return true;
        }

        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = _db.Users.Find(userId);
            if (user == null) return false;

            string oldHash = HashPassword(oldPassword);
            if (user.Password != oldHash)
                return false;

            user.Password = HashPassword(newPassword);
            _db.SaveChanges();
            return true;
        }

        private string HashPassword(string input)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(input);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

    }
}
