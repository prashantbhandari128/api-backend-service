using BackendService.DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore.Storage;

namespace BackendService.DataAccess.UnitOfWork.Interface
{
    /// <summary>
    /// Defines the contract for the Unit of Work pattern.
    /// It provides access to repositories and handles database transactions.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        // Repositories
        IBookRepository Books { get; }

        // Save changes
        int Complete();
        Task<int> CompleteAsync();

        // Transaction
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }

}
