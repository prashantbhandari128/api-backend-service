//+---------------------------------------------------------------------------------------------------+
//|                                        Repository.cs                                              |
//|                                    ======================                                         |
//|                                  Author : Prashant Bhandari                                       |
//+---------------------------------------------------------------------------------------------------+
//| This is a C# implementation of a generic repository class that follows the repository pattern.    |
//| The purpose of the class is to provide a set of CRUD (Create, Read, Update, Delete) operations    |
//| that can be used to interact with a database table in a consistent and reusable way.              |
//|                                                                                                   |
//| The class is parameterized with a type parameter T that is constrained to be a reference type     |
//| (class). This allows the class to work with any entity type that is mapped to the underlying      |
//| database using Entity Framework.                                                                  |
//|                                                                                                   |
//| The class implements the IRepository<T> interface, which defines the contract for the             |
//| repository operations. The interface includes methods for inserting, updating, deleting, and      |
//| querying entities, as well as methods for counting and enumerating entities.                      |
//|                                                                                                   |
//| The Repository<T> class also implements the IDisposable interface, which allows the               |
//| class to release any unmanaged resources it is holding when it is no longer needed. This is       |
//| important for ensuring that database connections are properly closed and that any other resources |
//| associated with the repository are cleaned up.                                                    |
//|                                                                                                   |
//| Overall, this class provides a simple and consistent way to interact with a database using the    |
//| repository pattern, which can help to improve code quality and maintainability.                   |
//+---------------------------------------------------------------------------------------------------+


using BackendService.DataAccess.Context;
using BackendService.DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BackendService.DataAccess.Repository.Implementation
{
    /// <summary>
    /// Represents a generic repository for CRUD operations on entities of type T using Entity Framework Core.
    /// Implements IRepository<T> for standard repository methods and IDisposable for resource cleanup.
    /// Provides methods for synchronous and asynchronous CRUD operations, entity retrieval, and enumeration.
    /// </summary>
    /// <typeparam name="T">The entity type that the repository operates on. Must be a class.</typeparam>

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Insert(T entity) => _context.Set<T>().Add(entity);

        public async Task InsertAsync(T entity) => await _context.Set<T>().AddAsync(entity);

        public void InsertRange(IEnumerable<T> entities) => _context.Set<T>().AddRange(entities);

        public async Task InsertRangeAsync(IEnumerable<T> entities) => await _context.Set<T>().AddRangeAsync(entities);

        public void Update(T entity) => _context.Set<T>().Update(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public void DeleteRange(IEnumerable<T> entities) => _context.Set<T>().RemoveRange(entities);

        public int Count() => _context.Set<T>().Count();

        public async Task<int> CountAsync() => await _context.Set<T>().CountAsync();

        public List<T> List() => _context.Set<T>().ToList();

        public List<T> List(int page, int pageSize) => _context.Set<T>().Skip((page - 1) * pageSize).Take(pageSize).ToList();

        public async Task<List<T>> ListAsync() => await _context.Set<T>().ToListAsync();

        public async Task<List<T>> ListAsync(int page, int pageSize) => await _context.Set<T>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        public T? Find(Guid id) => _context.Set<T>().Find(id);

        public async Task<T?> FindAsync(Guid id) => await _context.Set<T>().FindAsync(id);

        public IEnumerable<T> GetEnumerable() => _context.Set<T>().AsEnumerable();

        public IAsyncEnumerable<T> GetAsyncEnumerable() => _context.Set<T>().AsAsyncEnumerable();

        public IQueryable<T> GetQueryable() => _context.Set<T>().AsQueryable();

        public async Task<IQueryable<T>> GetQueryableAsync() => await Task.FromResult(_context.Set<T>().AsQueryable());

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
