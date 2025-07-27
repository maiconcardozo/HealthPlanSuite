namespace Foundation.Base.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}