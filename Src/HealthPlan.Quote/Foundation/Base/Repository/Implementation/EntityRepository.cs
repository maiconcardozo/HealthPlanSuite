using Foundation.Base.Domain.Interface;
using Foundation.Base.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Foundation.Base.Repository.Implementation
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity
    {
        protected readonly DbContext Context;

        public EntityRepository(DbContext context)
        {
            Context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public T Add(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            Context.Set<T>().Add(entity);
            return entity;
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            Context.Set<T>().Update(entity);
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                Context.Set<T>().Remove(entity);
            }
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
    }
}