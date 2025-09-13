namespace Foundation.Base.UnitOfWork.Interface
{
    /// <summary>
    /// Base interface for Unit of Work pattern implementation.
    /// Compatible with Foundation.Base NuGet package interface.
    /// </summary>
    public interface IBaseUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commits all pending changes to the database.
        /// </summary>
        /// <returns>The number of entities written to the database.</returns>
        int Commit();
        
        /// <summary>
        /// Commits all pending changes to the database asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous commit operation with the number of entities written.</returns>
        Task<int> CommitAsync();
        
        /// <summary>
        /// Executes an action within a database transaction.
        /// </summary>
        /// <param name="action">Action to execute</param>
        void ExecuteInTransaction(Action action);
        
        /// <summary>
        /// Executes an async function within a database transaction.
        /// </summary>
        /// <param name="actionAsync">Async function to execute</param>
        /// <returns>A task representing the asynchronous transaction execution.</returns>
        Task ExecuteInTransactionAsync(Func<Task> actionAsync);
    }
}