using System.Linq.Expressions;
using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Services.Interface
{
    /// <summary>
    /// Service interface for Accommodation management operations.
    /// Provides comprehensive Accommodation CRUD operations following service layer patterns.
    /// </summary>
    public interface IAccommodationService
    {
        #region Query Operations
        
        /// <summary>
        /// Retrieves all accommodations from the system.
        /// </summary>
        /// <returns>Collection of all accommodation entities</returns>
        IEnumerable<Accommodation> GetAllAccommodations();
        
        /// <summary>
        /// Retrieves an accommodation by its unique identifier.
        /// </summary>
        /// <param name="id">Accommodation ID</param>
        /// <returns>Accommodation if found, null otherwise</returns>
        Accommodation? GetById(int id);
        
        /// <summary>
        /// Retrieves accommodations by type.
        /// </summary>
        /// <param name="type">Accommodation type to filter by</param>
        /// <returns>Collection of accommodations of the specified type</returns>
        IEnumerable<Accommodation> GetByType(string type);
        
        /// <summary>
        /// Retrieves accommodations with additional value within a range.
        /// </summary>
        /// <param name="minValue">Minimum additional value</param>
        /// <param name="maxValue">Maximum additional value</param>
        /// <returns>Collection of accommodations within the specified value range</returns>
        IEnumerable<Accommodation> GetByValueRange(decimal minValue, decimal maxValue);
        
        /// <summary>
        /// Retrieves accommodations that match the specified predicate condition.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter accommodations</param>
        /// <returns>Collection of matching accommodation entities</returns>
        IEnumerable<Accommodation> GetAccommodations(Expression<Func<Accommodation, bool>> predicate);
        
        /// <summary>
        /// Retrieves a single accommodation that matches the predicate, or null if none found.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter accommodations</param>
        /// <returns>Single matching accommodation or null</returns>
        /// <exception cref="InvalidOperationException">Thrown when multiple accommodations match the predicate</exception>
        Accommodation? GetSingleOrDefaultAccommodation(Expression<Func<Accommodation, bool>> predicate);
        
        /// <summary>
        /// Retrieves all active accommodations.
        /// </summary>
        /// <returns>Collection of active accommodations</returns>
        IEnumerable<Accommodation> GetAllActiveAccommodations();
        
        #endregion
        
        #region Modification Operations
        
        /// <summary>
        /// Creates a new accommodation in the system.
        /// Sets audit fields and validates business rules.
        /// </summary>
        /// <param name="accommodation">Accommodation to create</param>
        void AddAccommodation(Accommodation accommodation);
        
        /// <summary>
        /// Creates multiple accommodations in a single transaction.
        /// </summary>
        /// <param name="accommodations">Collection of accommodation entities to create</param>
        void AddAccommodations(IEnumerable<Accommodation> accommodations);
        
        /// <summary>
        /// Updates an existing accommodation.
        /// </summary>
        /// <param name="accommodation">Accommodation with updated information</param>
        void UpdateAccommodation(Accommodation accommodation);
        
        /// <summary>
        /// Deletes an accommodation.
        /// </summary>
        /// <param name="accommodation">Accommodation to delete</param>
        void DeleteAccommodation(Accommodation accommodation);
        
        /// <summary>
        /// Deletes an accommodation by its ID.
        /// </summary>
        /// <param name="id">Accommodation ID to delete</param>
        void DeleteAccommodation(int id);
        
        /// <summary>
        /// Deletes multiple accommodation entities.
        /// </summary>
        /// <param name="accommodations">Collection of accommodation entities to delete</param>
        void DeleteAccommodations(IEnumerable<Accommodation> accommodations);
        
        #endregion
        
        #region Business Logic
        
        /// <summary>
        /// Validates if an accommodation type is unique.
        /// </summary>
        /// <param name="type">Accommodation type to validate</param>
        /// <returns>True if type is unique, false otherwise</returns>
        bool IsTypeUnique(string type);
        
        /// <summary>
        /// Validates if an accommodation type is unique for updates (excludes current entity).
        /// </summary>
        /// <param name="type">Accommodation type to validate</param>
        /// <param name="excludeId">Accommodation ID to exclude from validation</param>
        /// <returns>True if type is unique, false otherwise</returns>
        bool IsTypeUniqueForUpdate(string type, int excludeId);
        
        #endregion
    }
}