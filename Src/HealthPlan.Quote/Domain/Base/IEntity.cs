namespace HealthPlan.Quote.Domain.Base
{
    /// <summary>
    /// Base interface for all entities
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Entity unique identifier
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Indicates if the entity is active
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        DateTime DtCreated { get; set; }

        /// <summary>
        /// Last update date
        /// </summary>
        DateTime? DtUpdated { get; set; }

        /// <summary>
        /// Deletion date (soft delete)
        /// </summary>
        DateTime? DtDeleted { get; set; }

        /// <summary>
        /// User who deleted the entity
        /// </summary>
        string? DeletedBy { get; set; }
    }
}