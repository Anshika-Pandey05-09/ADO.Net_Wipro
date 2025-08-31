using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Day34_LibraryManagementSystem.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? Bio { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}