using System;

namespace Online_lib.Domain.Entities.User
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public DateTime? DateOfDeath { get; set; }
    }
}
