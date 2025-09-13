using System.Linq.Expressions;

namespace Foundation.Base.Repository.Interface
{
    /// <summary>
    /// Generic repository interface providing basic CRUD operations for entities.
    /// Compatible with Foundation.Base NuGet package interface.
    /// </summary>
    /// <typeparam name="T">Entity type that implements IEntity</typeparam>
    public interface IEntityRepository<T> where T : HealthPlan.Quote.Foundation.Entity
    {
        /// <summary>
        /// Gets an entity using another entity as a template (typically for ID lookup).
        /// </summary>
        /// <param name="entity">Entity to use as template</param>
        /// <returns>Entity if found, null otherwise</returns>
        T? Get(T entity);
        
        /// <summary>
        /// Gets an entity using another entity as a template asynchronously.
        /// </summary>
        /// <param name="entity">Entity to use as template</param>
        /// <returns>Task with entity if found, null otherwise</returns>
        Task<T?> GetAsync(T entity);
        
        /// <summary>
        /// Gets entities by a list of IDs using the LstId property.
        /// </summary>
        /// <param name="entity">Entity containing LstId property with IDs to search</param>
        /// <returns>Collection of matching entities</returns>
        IEnumerable<T> GetByLstId(T entity);
        
        /// <summary>
        /// Gets entities by a list of IDs using the LstId property asynchronously.
        /// </summary>
        /// <param name="entity">Entity containing LstId property with IDs to search</param>
        /// <returns>Task with collection of matching entities</returns>
        Task<IEnumerable<T>> GetByLstIdAsync(T entity);
        
        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>Collection of all entities</returns>
        IEnumerable<T> GetAll();
        
        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        /// <returns>Task with collection of all entities</returns>
        Task<IEnumerable<T>> GetAllAsync();
        
        /// <summary>
        /// Finds entities that match the given predicate.
        /// </summary>
        /// <param name="predicate">Expression to filter entities</param>
        /// <returns>Collection of matching entities</returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Finds entities that match the given predicate asynchronously.
        /// </summary>
        /// <param name="predicate">Expression to filter entities</param>
        /// <returns>Task with collection of matching entities</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Returns a single entity that matches the predicate, or null if no match.
        /// </summary>
        /// <param name="predicate">Expression to filter entities</param>
        /// <returns>Single entity if found, null otherwise</returns>
        T? SingleOrDefault(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Returns a single entity that matches the predicate asynchronously, or null if no match.
        /// </summary>
        /// <param name="predicate">Expression to filter entities</param>
        /// <returns>Task with single entity if found, null otherwise</returns>
        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">Entity to add</param>
        void Add(T entity);
        
        /// <summary>
        /// Adds multiple entities.
        /// </summary>
        /// <param name="lstEntity">Entities to add</param>
        void AddRange(IEnumerable<T> lstEntity);
        
        /// <summary>
        /// Removes an entity.
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        void Remove(T entity);
        
        /// <summary>
        /// Removes multiple entities.
        /// </summary>
        /// <param name="lstEntity">Entities to remove</param>
        void RemoveRange(IEnumerable<T> lstEntity);
        
        /// <summary>
        /// Retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>Entity if found, null otherwise</returns>
        T? GetById(int id);
        
        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>Task with entity if found, null otherwise</returns>
        Task<T?> GetByIdAsync(int id);
        
        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity to update</param>
        void Update(T entity);
        
        /// <summary>
        /// Retrieves all entities including deleted ones.
        /// </summary>
        /// <returns>Collection of all entities including deleted</returns>
        IEnumerable<T> GetAllIncludingDeleted();
        
        /// <summary>
        /// Permanently deletes an entity (hard delete).
        /// </summary>
        /// <param name="entity">Entity to permanently delete</param>
        void HardDelete(T entity);
    }
}