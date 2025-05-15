using Online_lib.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_lib.BusinessLogic.Interface
{
    public interface ISession
    {
        UserLoginResult UserLogin(ULoginData data);
    }
}
