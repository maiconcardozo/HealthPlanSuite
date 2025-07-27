namespace Foundation.Base.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();
        void ExecuteInTransaction(Action action);
        Task ExecuteInTransactionAsync(Func<Task> action);
    }
}