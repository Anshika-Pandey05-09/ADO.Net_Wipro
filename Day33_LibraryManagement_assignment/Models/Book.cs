namespace Day33_LibraryManagement_assignment.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }

        // Foreign Key
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        // Many-to-Many
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}