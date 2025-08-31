using Day34_LibraryManagementSystem.Data;
using Day34_LibraryManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Day34_LibraryManagementSystem.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LibraryContext _context;
        internal DbSet<T> dbSet;

        public GenericRepository(LibraryContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(object id) => await dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            int? page = null, int? pageSize = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null) query = query.Where(filter);

            foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProp.Trim());

            if (orderBy != null) query = orderBy(query);

            if (page.HasValue && pageSize.HasValue)
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? filter = null)
        {
            return filter == null ? await dbSet.CountAsync() : await dbSet.CountAsync(filter);
        }

        public async Task AddAsync(T entity) => await dbSet.AddAsync(entity);
        public void Update(T entity) => dbSet.Update(entity);

        public async Task DeleteAsync(object id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity != null) Delete(entity);
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);
            dbSet.Remove(entity);
        }
    }
}