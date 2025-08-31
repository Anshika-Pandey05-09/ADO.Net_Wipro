using Day34_LibraryManagementSystem.Data;
using Day34_LibraryManagementSystem.Models;
using Day34_LibraryManagementSystem.Repositories.Interfaces;

namespace Day34_LibraryManagementSystem.Repositories.Implementations
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryContext ctx) : base(ctx) { }
    }
}