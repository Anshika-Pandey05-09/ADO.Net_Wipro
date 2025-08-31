using Day34_LibraryManagementSystem.Data;
using Day34_LibraryManagementSystem.Repositories.Interfaces;

namespace Day34_LibraryManagementSystem.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext _ctx;
        public IBookRepository Books { get; }
        public IAuthorRepository Authors { get; }
        public IGenreRepository Genres { get; }

        public UnitOfWork(LibraryContext ctx, IBookRepository books,
                          IAuthorRepository authors, IGenreRepository genres)
        {
            _ctx = ctx;
            Books = books;
            Authors = authors;
            Genres = genres;
        }

        public Task<int> SaveAsync() => _ctx.SaveChangesAsync();
    }
}