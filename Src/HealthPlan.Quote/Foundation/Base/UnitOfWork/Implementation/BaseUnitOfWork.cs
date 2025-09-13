using Foundation.Base.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;

namespace Foundation.Base.UnitOfWork.Implementation
{
    /// <summary>
    /// Base implementation for Unit of Work pattern.
    /// Compatible with Foundation.Base NuGet package implementation.
    /// </summary>
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        protected readonly DbContext _context;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the BaseUnitOfWork.
        /// </summary>
        /// <param name="context">Database context</param>
        public BaseUnitOfWork(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Commits all pending changes to the database.
        /// </summary>
        /// <returns>The number of entities written to the database.</returns>
        public virtual int Commit()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Commits all pending changes to the database asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous commit operation with the number of entities written.</returns>
        public virtual async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Executes an action within a database transaction.
        /// </summary>
        /// <param name="action">Action to execute</param>
        public virtual void ExecuteInTransaction(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                action();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Executes an async function within a database transaction.
        /// </summary>
        /// <param name="actionAsync">Async function to execute</param>
        /// <returns>A task representing the asynchronous transaction execution.</returns>
        public virtual async Task ExecuteInTransactionAsync(Func<Task> actionAsync)
        {
            if (actionAsync == null) throw new ArgumentNullException(nameof(actionAsync));
            
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await actionAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Disposes the unit of work and underlying context.
        /// </summary>
        /// <param name="disposing">True if disposing managed resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// Disposes the unit of work.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}