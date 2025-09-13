namespace HealthPlan.Quote.Domain.Base
{
    /// <summary>
    /// Base class for all entities
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// Entity unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Indicates if the entity is active
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime DtCreated { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Last update date
        /// </summary>
        public DateTime? DtUpdated { get; set; }

        /// <summary>
        /// Deletion date (soft delete)
        /// </summary>
        public DateTime? DtDeleted { get; set; }

        /// <summary>
        /// User who deleted the entity
        /// </summary>
        public string? DeletedBy { get; set; }

        /// <summary>
        /// User who created the entity
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// User who last updated the entity
        /// </summary>
        public string? UpdatedBy { get; set; }
    }
}