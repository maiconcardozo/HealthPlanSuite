using Foundation.Base.Domain.Interface;

namespace Foundation.Base.Domain.Implementation
{
    /// <summary>
    /// Base implementation for all entities providing common audit properties.
    /// </summary>
    public class Entity : IEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time when the entity was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time when the entity was created (legacy property).
        /// </summary>
        public DateTime DtCreated { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time when the entity was last updated (legacy property).
        /// </summary>
        public DateTime DtUpdated { get; set; }
        
        /// <summary>
        /// Gets or sets the user who created the entity.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the user who last updated the entity.
        /// </summary>
        public string UpdatedBy { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets whether the entity is active (soft delete flag).
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Gets or sets the date and time when the entity was deleted (soft delete).
        /// </summary>
        public DateTime? DtDeleted { get; set; }
        
        /// <summary>
        /// Gets or sets the user who deleted the entity (soft delete).
        /// </summary>
        public string DeletedBy { get; set; } = string.Empty;
        
        /// <summary>
        /// Initializes a new instance of the Entity class.
        /// Sets the creation and update timestamps to the current UTC time.
        /// </summary>
        public Entity()
        {
            var now = DateTime.UtcNow;
            CreatedAt = now;
            UpdatedAt = now;
            DtCreated = now;
            DtUpdated = now;
            IsActive = true;
        }
    }
}