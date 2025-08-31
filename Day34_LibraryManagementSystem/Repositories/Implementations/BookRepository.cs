using Day34_LibraryManagementSystem.Data;
using Day34_LibraryManagementSystem.Models;
using Day34_LibraryManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Day34_LibraryManagementSystem.Repositories.Implementations
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext ctx) : base(ctx) { }

        public async Task<(IEnumerable<Book> items, int total)> SearchAsync(
            string? search, string? sort, int page, int pageSize)
        {
            var q = _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(b => b.Title.Contains(search) ||
                                 (b.Author != null && b.Author.Name.Contains(search)));

            q = sort switch
            {
                "title_desc" => q.OrderByDescending(b => b.Title),
                "date"       => q.OrderBy(b => b.PublishedOn),
                "date_desc"  => q.OrderByDescending(b => b.PublishedOn),
                _            => q.OrderBy(b => b.Title)
            };

            var total = await q.CountAsync();
            var items = await q.Skip((page - 1) * pageSize).Take(pageSize)
                               .AsNoTracking().ToListAsync();
            return (items, total);
        }

        public async Task<Book?> GetDetailsAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task UpdateGenresAsync(Book book, IEnumerable<int> genreIds)
        {
            var existing = _context.BookGenres.Where(bg => bg.BookId == book.Id);
            _context.BookGenres.RemoveRange(existing);
            await _context.BookGenres.AddRangeAsync(
                genreIds.Select(gid => new BookGenre { BookId = book.Id, GenreId = gid })
            );
        }
    }
}