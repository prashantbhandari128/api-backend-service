//+---------------------------------------------------------------------------+
//|                            IRepository.cs                                 |
//|                        =======================                            |
//|                       Author : Prashant Bhandari                          |
//+---------------------------------------------------------------------------+
//| This code defines an interface IRepository<T> which provides a set        |
//| of methods to interact with a database. The interface is generic, meaning |
//| that it can work with any type T that is a class.                         |
//+---------------------------------------------------------------------------+


namespace BackendService.DataAccess.Repository.Interface
{
    /// <summary>
    /// Represents a generic repository interface for interacting with entities of type T in a database.
    /// </summary>
    /// <typeparam name="T">The entity type that the repository operates on. Must be a class.</typeparam>

    public interface IRepository<T> : IDisposable where T : class
    {
        // Inserts a new entity of type T into the database.
        void Insert(T entity);

        // Asynchronously inserts a new entity of type T into the database.
        Task InsertAsync(T entity);

        // Inserts a range of entities of type T into the database.
        void InsertRange(IEnumerable<T> entities);

        // Asynchronously inserts a range of entities of type T into the database.
        Task InsertRangeAsync(IEnumerable<T> entities);

        // Updates an existing entity of type T in the database.
        void Update(T entity);

        // Deletes an existing entity of type T from the database.
        void Delete(T entity);

        // Deletes a range of entities of type T from the database.
        void DeleteRange(IEnumerable<T> entities);

        // Returns the total number of entities of type T in the database.
        int Count();

        // Asynchronously returns the total number of entities of type T in the database.
        Task<int> CountAsync();

        // Returns a list of all entities of type T in the database.
        List<T> List();

        // Returns a list of entities of type T in the database, with optional paging parameters.
        List<T> List(int page, int pageSize);

        // Returns a list of all entities of type T in the database asynchronously.
        Task<List<T>> ListAsync();

        // Returns a list of entities of type T in the database asynchronously, with optional paging parameters.
        Task<List<T>> ListAsync(int page, int pageSize);

        // Returns an entity of type T by its primary key from the database.
        T? Find(Guid id);

        // Returns an entity of type T by its primary key from the database asynchronously.
        Task<T?> FindAsync(Guid id);

        // Returns an IEnumerable of entities of type T from the database.
        IEnumerable<T> GetEnumerable();

        // Returns an IAsyncEnumerable of entities of type T from the database.
        IAsyncEnumerable<T> GetAsyncEnumerable();

        // Returns an IQueryable of entities of type T from the database.
        IQueryable<T> GetQueryable();

        // Returns an IQueryable of entities of type T from the database asynchronously.
        Task<IQueryable<T>> GetQueryableAsync();
    }

}
