using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Day34_LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required, StringLength(180)]
        public string Title { get; set; } = string.Empty;

        public DateTime? PublishedOn { get; set; }

        // FK
        [Required]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        // Many-to-many
        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}