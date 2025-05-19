namespace Online_lib.Domain.Entities.User
{
    public class BookInventoryViewModel
    {
        public BookViewModel FormData { get; set; } = new BookViewModel(); // ✅ важно!
        public List<UserBook> Books { get; set; } = new List<UserBook>();
    }
}
    