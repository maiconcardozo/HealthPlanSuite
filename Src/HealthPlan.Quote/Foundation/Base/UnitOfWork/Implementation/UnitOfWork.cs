using Foundation.Base.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;

namespace Foundation.Base.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void ExecuteInTransaction(Action action)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                action();
                SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await action();
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}