using Online_lib.BusinessLogic.Core;
using Online_lib.BusinessLogic.DBModel;
using Online_lib.BusinessLogic.Interface;
using Online_lib.Domain.Entities.User;
using Online_lib.Domain.Enums;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Online_lib.BusinessLogic
{
    public class SessionBL : ISession
    {
        private readonly UserContext _db;

        public SessionBL(UserContext db)
        {
            _db = db;
        }

        public SessionBL() : this(new UserContext()) { }

        public UserLoginResult UserLogin(ULoginData data)
        {
            string hashed = HashPassword(data.Password);

            var user = _db.Users.FirstOrDefault(u =>
                u.Email == data.Name || u.ContactNumber == data.Name || u.Username == data.Name);

            if (user != null)
            {
                if (user.IsBlocked)
                {
                    return new UserLoginResult
                    {
                        Status = false,
                        StatusMsg = "The user is blocked."
                    };
                }

                if (user.Password != hashed)
                {
                    return new UserLoginResult
                    {
                        Status = false,
                        StatusMsg = "Invalid data."
                    };
                }

                string token = GenerateHmac(Guid.NewGuid().ToString(), "SuperStrongKey123!");
                user.LoginToken = token;
                _db.SaveChanges();

                return new UserLoginResult
                {
                    Status = true,
                    StatusMsg = $"Welcome, {user.Username}!",
                    User = user,
                    HmacToken = token
                };
            }

            return new UserLoginResult
            {
                Status = false,
                StatusMsg = "Invalid data."
            };
        }



        public UserLoginResult RegisterUser(UserProfileModel model)
        {
            try
            {
                var newUser = new UDbTable
                {
                    Username = model.FullName,
                    Email = model.Email,
                    Password = HashPassword(model.NewPassword),
                    City = model.City,
                    State = model.State,
                    Pincode = model.Pincode ?? 0,
                    DateOfBirth = model.DateOfBirth,
                    ContactNumber = model.ContactNumber,
                    FullAddress = model.FullAddress ?? "N/A",
                    Role = UserRole.User,
                    LoginToken = ""
                };

                _db.Users.Add(newUser);
                _db.SaveChanges();

                return new UserLoginResult
                {
                    Status = true,
                    StatusMsg = "User registered successfully!"
                };
            }
            catch (Exception ex)
            {
                return new UserLoginResult
                {
                    Status = false,
                    StatusMsg = $"Registration error : {ex.Message}"
                };
            }
        }

        private string HashPassword(string input)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private string GenerateHmac(string input, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var inputBytes = Encoding.UTF8.GetBytes(input);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                var hashBytes = hmac.ComputeHash(inputBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
