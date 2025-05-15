using Online_lib.BusinessLogic.Core;
using Online_lib.BusinessLogic.DBModel;
using Online_lib.BusinessLogic.Interface;
using Online_lib.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_lib.BusinessLogic
{
    public class SessionBL : ISession
    {

        public UserLoginResult UserLogin(ULoginData data)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u =>
                    u.Username == data.Name && u.Password == data.Password);

                if (user != null)
                {
                    return new UserLoginResult
                    {
                        Status = true,
                        StatusMsg = $"Добро пожаловать, {user.Username}!"
                    };
                }

                return new UserLoginResult
                {
                    Status = false,
                    StatusMsg = "Неверный логин или пароль."
                };
            }
        }


    }
}
