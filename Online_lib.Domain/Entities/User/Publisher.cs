using System;

namespace Online_lib.Domain.Entities.User
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
