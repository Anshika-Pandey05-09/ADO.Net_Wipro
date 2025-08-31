using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Day34_LibraryManagementSystem.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}