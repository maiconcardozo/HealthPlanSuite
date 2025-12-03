using System.Linq.Expressions;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Infrastructure.Repositories
{
    /// <summary>
    /// Generic repository implementation for entity operations
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        protected readonly IApplicationContext _context;
        protected readonly DbSet<T> _dbSet;

        public EntityRepository(IApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Remove(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                Remove(entity);
            }
        }

        public virtual T? SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}