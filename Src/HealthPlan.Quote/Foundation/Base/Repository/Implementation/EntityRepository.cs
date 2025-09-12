using Foundation.Base.Domain.Interface;
using Foundation.Base.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Foundation.Base.Repository.Implementation
{
    /// <summary>
    /// Generic repository implementation providing basic CRUD operations for entities.
    /// </summary>
    /// <typeparam name="T">Entity type that implements IEntity</typeparam>
    public class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of the EntityRepository.
        /// </summary>
        /// <param name="context">Database context</param>
        public EntityRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        /// <summary>
        /// Retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>Entity if found, null otherwise</returns>
        public virtual T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Retrieves entities by a collection of IDs.
        /// </summary>
        /// <param name="ids">Collection of entity IDs</param>
        /// <returns>Collection of matching entities</returns>
        public virtual IEnumerable<T> GetByIds(IEnumerable<int> ids)
        {
            return _dbSet.Where(e => ids.Contains(e.Id)).ToList();
        }

        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>Collection of all entities</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        /// <summary>
        /// Finds entities that match the given predicate.
        /// </summary>
        /// <param name="predicate">Expression to filter entities</param>
        /// <returns>Collection of matching entities</returns>
        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        /// <summary>
        /// Gets entities where the predicate condition is met.
        /// </summary>
        /// <param name="predicate">Expression to filter entities</param>
        /// <returns>Collection of matching entities</returns>
        public virtual IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        /// <summary>
        /// Returns a single entity that matches the predicate, or null if no match.
        /// </summary>
        /// <param name="predicate">Expression to filter entities</param>
        /// <returns>Single entity if found, null otherwise</returns>
        public virtual T? SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Gets a single entity that matches the predicate, or null if no match.
        /// </summary>
        /// <param name="predicate">Expression to filter entities</param>
        /// <returns>Single entity if found, null otherwise</returns>
        public virtual T? GetSingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">Entity to add</param>
        public virtual void Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Adds multiple entities.
        /// </summary>
        /// <param name="entities">Entities to add</param>
        public virtual void AddRange(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            _dbSet.AddRange(entities);
        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity to update</param>
        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Removes an entity.
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        public virtual void Remove(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Deletes an entity by ID.
        /// </summary>
        /// <param name="id">ID of the entity to delete</param>
        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                Remove(entity);
            }
        }

        /// <summary>
        /// Removes multiple entities.
        /// </summary>
        /// <param name="entities">Entities to remove</param>
        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Deletes multiple entities.
        /// </summary>
        /// <param name="entities">Entities to delete</param>
        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Checks if an entity exists by ID.
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>True if exists, false otherwise</returns>
        public virtual bool Exists(int id)
        {
            return _dbSet.Any(e => e.Id == id);
        }
    }
}