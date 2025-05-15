namespace Online_lib.Domain.Entities.User
{
    public class UserBook
    {
        public int Id { get; set; }  // ID книги (обязателен для EF)
        public string Title { get; set; }  // Название книги
        public string Author { get; set; }  // Автор книги
        public string Status { get; set; }  // Статус (Available, Issued и т.д.)
        public int? UserId { get; set; }  // Владелец (пользователь)
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
        public string FilePath { get; set; } // путь к pdf-файлу
        public string FileName { get; set; } // оригинальное имя файла
    }
}
