namespace Day34_LibraryManagementSystem.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }
        IGenreRepository Genres { get; }
        Task<int> SaveAsync();
    }
}