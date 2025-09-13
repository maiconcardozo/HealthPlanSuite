using System.Linq.Expressions;

namespace HealthPlan.Quote.Repository.Base
{
    /// <summary>
    /// Generic repository interface for entity operations
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IEntityRepository<T> where T : class
    {
        /// <summary>
        /// Gets entity by ID
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns>Entity or null if not found</returns>
        T? GetById(int id);

        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <returns>List of entities</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Finds entities matching criteria
        /// </summary>
        /// <param name="predicate">Search criteria</param>
        /// <returns>Matching entities</returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds new entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        void Add(T entity);

        /// <summary>
        /// Updates existing entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        void Update(T entity);

        /// <summary>
        /// Removes entity
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        void Remove(T entity);

        /// <summary>
        /// Removes entity by ID
        /// </summary>
        /// <param name="id">Entity ID</param>
        void Remove(int id);
    }
}