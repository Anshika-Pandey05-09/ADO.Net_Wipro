using System;
using System.Collections.Generic;

namespace Day33_LibraryManagement_DBFirst_assignment.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Book> BooksBooks { get; set; } = new List<Book>();
}
