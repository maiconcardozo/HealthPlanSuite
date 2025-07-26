using Foundation.Base.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Foundation.Base.Repository.Implementation
{
    public abstract class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        protected readonly DbContext Context;

        protected EntityRepository(DbContext context)
        {
            Context = context;
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var entry = await Context.Set<T>().AddAsync(entity);
            return entry.Entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            Context.Set<T>().Update(entity);
            return entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id) != null;
        }
    }
}