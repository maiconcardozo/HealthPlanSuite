namespace Foundation.Base.UnitOfWork.Interface
{
    /// <summary>
    /// Base interface for Unit of Work pattern implementation.
    /// </summary>
    public interface IBaseUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves all pending changes to the database.
        /// </summary>
        /// <returns>A task representing the asynchronous save operation.</returns>
        Task<int> SaveAsync();
        
        /// <summary>
        /// Saves all pending changes to the database.
        /// </summary>
        /// <returns>The number of entities written to the database.</returns>
        int Save();
        
        /// <summary>
        /// Begins a new database transaction.
        /// </summary>
        /// <returns>A task representing the asynchronous transaction begin operation.</returns>
        Task BeginTransactionAsync();
        
        /// <summary>
        /// Commits the current database transaction.
        /// </summary>
        /// <returns>A task representing the asynchronous commit operation.</returns>
        Task CommitTransactionAsync();
        
        /// <summary>
        /// Rolls back the current database transaction.
        /// </summary>
        /// <returns>A task representing the asynchronous rollback operation.</returns>
        Task RollbackTransactionAsync();
    }
}