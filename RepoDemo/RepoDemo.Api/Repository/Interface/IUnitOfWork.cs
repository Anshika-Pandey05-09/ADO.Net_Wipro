//IUnitofWork is used here to group related operations into a single transaction
//ex: saving changes to multiple entities in a single database transaction
//ex: committing all changes or rolling back in case of an error
// So, UnitOfWork is responsible for coordinating the work of multiple repositories by providing a single
// interface for saving changes.


using RepoDemo.Api.Models;
using RepoDemo.Api.Repository.Interface;
// using RepoDemo.Api.Repository.Interfaces; // this will help us in accessing the WeatherForecast model/any model
public interface IUnitOfWork : IDisposable //this interface is used to manage the lifetime of the unit of work
{
    // IGenericRepository<WeatherForecast> WeatherForecasts { get; }
    IGenericRepository<Product> Products { get; }
    // here Products is a repository for Product entities and we can define other repositories in a similar manner
    Task<int> SaveChangesAsync(); // this method will help us in saving changes to the database

}

//If we dont use UnitOfWork, we would have to manage transactions and save changes across multiple repositories manually,
// which can lead to code duplication and make it harder to maintain the application.