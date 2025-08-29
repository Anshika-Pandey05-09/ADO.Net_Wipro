// Here is the generic repository interface that defines the basic CRUD operations.
using System.Linq.Expressions;

namespace RepoDemo.Api.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        // This method retrieves all entities.
        // Here we are using 
        Task<IEnumerable<T>> GetAllAsync();
        // This method retrieves an entity by its ID.
        Task<T> GetByIdAsync(int id);
            // This method adds a new entity.
        Task AddAsync(T entity);
        // This method deletes an entity by its ID.
        Task DeleteAsync(int id);
        // This method finds entities based on a predicate.
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}



//All the methods that are defined in this interface will help us in performing CRUD operations on the entities.//
//Here entities refer to the objects that are being managed by the application, such as products, orders, customers, etc.