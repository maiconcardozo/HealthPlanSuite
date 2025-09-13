using Foundation.Base.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Foundation.Base.Repository.Implementation
{
    /// <summary>
    /// Generic repository implementation providing basic CRUD operations for entities.
    /// Compatible with Foundation.Base NuGet package implementation.
    /// </summary>
    /// <typeparam name="T">Entity type that implements IEntity</typeparam>
    public class EntityRepository<T> : IEntityRepository<T> where T : class, Foundation.Base.Domain.Implemetation.Entity
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
        /// Gets an entity using another entity as a template (typically for ID lookup).
        /// </summary>
        /// <param name="entity">Entity to use as template</param>
        /// <returns>Entity if found, null otherwise</returns>
        public virtual T? Get(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetById(entity.Id);
        }

        /// <summary>
        /// Gets an entity using another entity as a template asynchronously.
        /// </summary>
        /// <param name="entity">Entity to use as template</param>
        /// <returns>Task with entity if found, null otherwise</returns>
        public virtual async Task<T?> GetAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return await GetByIdAsync(entity.Id);
        }

        /// <summary>
        /// Gets entities by a list of IDs using the LstId property.
        /// </summary>
        /// <param name="entity">Entity containing LstId property with IDs to search</param>
        /// <returns>Collection of matching entities</returns>
        public virtual IEnumerable<T> GetByLstId(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.LstId == null || !entity.LstId.Any()) return new List<T>();
            return _dbSet.Where(e => entity.LstId.Contains(e.Id)).ToList();
        }

        /// <summary>
        /// Gets entities by a list of IDs using the LstId property asynchronously.
        /// </summary>
        /// <param name="entity">Entity containing LstId property with IDs to search</param>
        /// <returns>Task with collection of matching entities</returns>
        public virtual async Task<IEnumerable<T>> GetByLstIdAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.LstId == null || !entity.LstId.Any()) return new List<T>();
            return await Task.FromResult(_dbSet.Where(e => entity.LstId.Contains(e.Id)).ToList());
        }

        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>Collection of all entities</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.Where(e => e.IsActive).ToList();
        }

        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        /// <returns>Task with collection of all entities</returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult(_dbSet.Where(e => e.IsActive).ToList());
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
        /// Finds entities that match the given predicate asynchronously.
        /// </summary>
        /// <param name="predicate">Expression to filter entities</param>
        /// <returns>Task with collection of matching entities</returns>
        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(_dbSet.Where(predicate).ToList());
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
        /// Returns a single entity that matches the predicate asynchronously, or null if no match.
        /// </summary>
        /// <param name="predicate">Expression to filter entities</param>
        /// <returns>Task with single entity if found, null otherwise</returns>
        public virtual async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(_dbSet.SingleOrDefault(predicate));
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
        /// <param name="lstEntity">Entities to add</param>
        public virtual void AddRange(IEnumerable<T> lstEntity)
        {
            if (lstEntity == null) throw new ArgumentNullException(nameof(lstEntity));
            _dbSet.AddRange(lstEntity);
        }

        /// <summary>
        /// Removes an entity.
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        public virtual void Remove(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.IsActive = false;
            entity.DtDeleted = DateTime.UtcNow;
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Removes multiple entities.
        /// </summary>
        /// <param name="lstEntity">Entities to remove</param>
        public virtual void RemoveRange(IEnumerable<T> lstEntity)
        {
            if (lstEntity == null) throw new ArgumentNullException(nameof(lstEntity));
            foreach (var entity in lstEntity)
            {
                entity.IsActive = false;
                entity.DtDeleted = DateTime.UtcNow;
            }
            _dbSet.UpdateRange(lstEntity);
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
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>Task with entity if found, null otherwise</returns>
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity to update</param>
        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.DtUpdated = DateTime.UtcNow;
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Retrieves all entities including deleted ones.
        /// </summary>
        /// <returns>Collection of all entities including deleted</returns>
        public virtual IEnumerable<T> GetAllIncludingDeleted()
        {
            return _dbSet.ToList();
        }

        /// <summary>
        /// Permanently deletes an entity (hard delete).
        /// </summary>
        /// <param name="entity">Entity to permanently delete</param>
        public virtual void HardDelete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Remove(entity);
        }
    }
}