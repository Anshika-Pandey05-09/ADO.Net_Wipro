using Day34_LibraryManagementSystem.Data;
using Day34_LibraryManagementSystem.Models;
using Day34_LibraryManagementSystem.Repositories.Interfaces;

namespace Day34_LibraryManagementSystem.Repositories.Implementations
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(LibraryContext ctx) : base(ctx) { }
    }
}