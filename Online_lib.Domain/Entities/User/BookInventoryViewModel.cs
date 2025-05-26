namespace Online_lib.Domain.Entities.User
{
    public class BookInventoryViewModel
    {
        public BookViewModel FormData { get; set; } = new BookViewModel();
        public List<BookViewModel> Books { get; set; }
    }
}
    
