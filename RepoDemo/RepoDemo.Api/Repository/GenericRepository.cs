//This file will contain the implementation of the generic repository interface
//It will provide the actual data access logic for the CRUD operations defined in the interface
//It acts as a bridge between the application and the data source, allowing for a more modular and testable codebase.


using Microsoft.EntityFrameworkCore;
using RepoDemo.Api.Data;
using RepoDemo.Api.Repository.Interface;
using System.Linq.Expressions;

namespace RepoDemo.Api.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    //above class implements the IGenericRepository interface defined by us for <T>
    //in this class we are providing the implementation for all the methods defined in the interface,
    // so that these methods can be used for <T> ie the entity type being managed by the application
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

      

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}