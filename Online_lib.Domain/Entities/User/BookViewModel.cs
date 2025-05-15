namespace Online_lib.Domain.Entities.User
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Description { get; set; }
        public string Edition { get; set; }
        public decimal Price { get; set; }
        public int Pages { get; set; }
        public string Language { get; set; }
        public string Genres { get; set; }
        public int PublisherId { get; set; }
        public int AuthorId { get; set; }
        public DateTime? PublisherDate { get; set; }
        public int ActualStock { get; set; }
        public int CurrentStock { get; set; }
        public int IssuedBooks { get; set; }

        // файл лучше оставить в Web модели
    }
}
