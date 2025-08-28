// using Microsoft.EntityFrameworkCore;
// using Day33_LibraryManagement_assignment.Models;

// namespace Day33_LibraryManagement_assignment.Data
// {
//     public class LibraryContext : DbContext
//     {
//         public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

//         public DbSet<Book> Books { get; set; }
//         public DbSet<Author> Authors { get; set; }
//         public DbSet<Genre> Genres { get; set; }

//         protected override void OnModelCreating(ModelBuilder modelBuilder)
//         {
//             base.OnModelCreating(modelBuilder);

//             // Many-to-Many (Book - Genre)
//             modelBuilder.Entity<Book>()
//                 .HasMany(b => b.Genres)
//                 .WithMany(g => g.Books);
//         }
//     }
// }
using Microsoft.EntityFrameworkCore;
using Day33_LibraryManagement_assignment.Models;

namespace Day33_LibraryManagement_assignment.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-Many (Book - Genre)
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Genres)
                .WithMany(g => g.Books)
                .UsingEntity(j => j.ToTable("BookGenres"));
        }
    }
}