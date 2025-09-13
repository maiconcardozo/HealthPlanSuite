using Foundation.Base.Domain.Interface;
using System.Linq.Expressions;

namespace Foundation.Base.Repository.Interface
{
    /// <summary>
    /// Generic repository interface providing basic CRUD operations for entities.
    /// </summary>
    /// <typeparam name="T">Entity type that implements IEntity</typeparam>
    public interface IEntityRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>Entity if found, null otherwise</returns>
        T? GetById(int id);
        
        /// <summary>
        /// Retrieves entities by a collection of IDs.
        /// </summary>
        /// <param name="ids">Collection of entity IDs</param>
        /// <returns>Collection of matching entities</returns>
        IEnumerable<T> GetByIds(IEnumerable<int> ids);
        
        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>Collection of all entities</returns>
        IEnumerable<T> GetAll();
        
        /// <summary>
        /// Finds entities that match the given predicate.
        /// </summary>
        /// <param name="predicate">Expression to filter entities</param>
        /// <returns>Collection of matching entities</returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Gets entities where the predicate condition is met.
        /// </summary>
        /// <param name="predicate">Expression to filter entities</param>
        /// <returns>Collection of matching entities</returns>
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Returns a single entity that matches the predicate, or null if no match.
        /// </summary>
        /// <param name="predicate">Expression to filter entities</param>
        /// <returns>Single entity if found, null otherwise</returns>
        T? SingleOrDefault(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Gets a single entity that matches the predicate, or null if no match.
        /// </summary>
        /// <param name="predicate">Expression to filter entities</param>
        /// <returns>Single entity if found, null otherwise</returns>
        T? GetSingleOrDefault(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">Entity to add</param>
        void Add(T entity);
        
        /// <summary>
        /// Adds multiple entities.
        /// </summary>
        /// <param name="entities">Entities to add</param>
        void AddRange(IEnumerable<T> entities);
        
        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity to update</param>
        void Update(T entity);
        
        /// <summary>
        /// Removes an entity.
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        void Remove(T entity);
        
        /// <summary>
        /// Deletes an entity by ID.
        /// </summary>
        /// <param name="id">ID of the entity to delete</param>
        void Delete(int id);
        
        /// <summary>
        /// Removes multiple entities.
        /// </summary>
        /// <param name="entities">Entities to remove</param>
        void RemoveRange(IEnumerable<T> entities);
        
        /// <summary>
        /// Deletes multiple entities.
        /// </summary>
        /// <param name="entities">Entities to delete</param>
        void DeleteRange(IEnumerable<T> entities);
        
        /// <summary>
        /// Checks if an entity exists by ID.
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>True if exists, false otherwise</returns>
        bool Exists(int id);
    }
}