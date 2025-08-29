// Here the UnitOfWork class is responsible for coordinating the work of multiple repositories
// It implements the IUnitOfWork interface and provides a single point of access to the repositories
// It also manages the database context and ensures that changes are saved in a single transaction

using RepoDemo.Api.Data;
using RepoDemo.Api.Models;
using RepoDemo.Api.Repository.Interface;
using System.Threading.Tasks;

namespace RepoDemo.Api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Products = new GenericRepository<Product>(_context);
        }

        public IGenericRepository<Product> Products { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}