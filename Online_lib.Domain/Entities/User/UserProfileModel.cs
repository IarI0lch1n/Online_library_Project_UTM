using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_lib.Domain.Entities.User
{
    public class UserProfileModel
    {
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int? Pincode { get; set; }
        public string FullAddress { get; set; }

        public int UserID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public List<UserBook> Books { get; set; }

    }
}

