using System;
using System.Collections.Generic;

namespace Day33_LibraryManagement_DBFirst_assignment.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public int AuthorId { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<Genre> GenresGenres { get; set; } = new List<Genre>();
}
