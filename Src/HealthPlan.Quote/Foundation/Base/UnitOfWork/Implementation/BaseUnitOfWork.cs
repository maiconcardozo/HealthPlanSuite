using Foundation.Base.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Foundation.Base.UnitOfWork.Implementation
{
    /// <summary>
    /// Base implementation of Unit of Work pattern for Entity Framework Core.
    /// </summary>
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        private readonly DbContext _context;
        private IDbContextTransaction? _transaction;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the BaseUnitOfWork class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public BaseUnitOfWork(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Saves all pending changes to the database.
        /// </summary>
        /// <returns>A task representing the asynchronous save operation.</returns>
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Saves all pending changes to the database.
        /// </summary>
        /// <returns>The number of entities written to the database.</returns>
        public int Save()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Begins a new database transaction.
        /// </summary>
        /// <returns>A task representing the asynchronous transaction begin operation.</returns>
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// Commits the current database transaction.
        /// </summary>
        /// <returns>A task representing the asynchronous commit operation.</returns>
        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        /// <summary>
        /// Rolls back the current database transaction.
        /// </summary>
        /// <returns>A task representing the asynchronous rollback operation.</returns>
        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        /// <summary>
        /// Disposes the Unit of Work and releases resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected dispose method for inheritance support.
        /// </summary>
        /// <param name="disposing">True if disposing managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _transaction?.Dispose();
                _disposed = true;
            }
        }
    }
}