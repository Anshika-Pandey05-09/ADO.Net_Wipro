using Day34_LibraryManagementSystem.Models;

namespace Day34_LibraryManagementSystem.Repositories.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<(IEnumerable<Book> items, int total)> SearchAsync(
            string? search, string? sort, int page, int pageSize);
        Task<Book?> GetDetailsAsync(int id);
        Task UpdateGenresAsync(Book book, IEnumerable<int> genreIds);
    }
}