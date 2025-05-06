using Online_lib.BusinessLogic.Core;
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
            // Заглушка, временная логика — можешь потом заменить на настоящую проверку из БД
            if (data.Name == "admin" && data.Password == "admin")
            {
                return new UserLoginResult
                {
                    Status = true,
                    StatusMsg = "Добро пожаловать, админ!"
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
