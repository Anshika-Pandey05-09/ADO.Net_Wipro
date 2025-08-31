using System.Linq.Expressions;

namespace Day34_LibraryManagementSystem.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            int? page = null, int? pageSize = null);

        Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);

        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(object id);
        void Delete(T entity);
    }
}