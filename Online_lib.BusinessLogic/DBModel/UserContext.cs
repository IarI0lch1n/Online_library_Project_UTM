using Online_lib.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_lib.BusinessLogic.DBModel
{
    public class UserContext : DbContext
    {
        public UserContext() : base("name=Lib_BD")
        {
        }

        public DbSet<UserBook> UserBooks { get; set; }




        public virtual DbSet<UDbTable> Users { get; set; }
    }
}
