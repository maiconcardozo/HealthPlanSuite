using Foundation.Base.Domain.Interface;

namespace HealthPlan.Quote.Foundation
{
    /// <summary>
    /// Concrete base implementation for all entities providing common audit properties.
    /// Implements Foundation.Base IEntity interface directly to ensure compatibility.
    /// </summary>
    public class Entity : IEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets whether the entity is active (soft delete flag).
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTime DtCreated { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time when the entity was deleted (soft delete).
        /// </summary>
        public DateTime? DtDeleted { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time when the entity was last updated.
        /// </summary>
        public DateTime? DtUpdated { get; set; }
        
        /// <summary>
        /// Gets or sets the user who created the entity.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the user who last updated the entity.
        /// </summary>
        public string UpdatedBy { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the user who deleted the entity (soft delete).
        /// </summary>
        public string DeletedBy { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the list of IDs for bulk operations.
        /// </summary>
        public IEnumerable<int> LstId { get; set; } = new List<int>();
        
        /// <summary>
        /// Gets or sets the start date for filtering created date range.
        /// </summary>
        public DateTime? DtCreatedStart { get; set; }
        
        /// <summary>
        /// Gets or sets the end date for filtering created date range.
        /// </summary>
        public DateTime? DtCreatedEnd { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was created (for backward compatibility).
        /// </summary>
        public DateTime CreatedAt 
        { 
            get => DtCreated; 
            set => DtCreated = value; 
        }
        
        /// <summary>
        /// Gets or sets the date and time when the entity was last updated (for backward compatibility).
        /// </summary>
        public DateTime UpdatedAt 
        { 
            get => DtUpdated ?? DateTime.UtcNow; 
            set => DtUpdated = value; 
        }
        
        /// <summary>
        /// Initializes a new instance of the Entity class.
        /// </summary>
        public Entity()
        {
            var now = DateTime.UtcNow;
            DtCreated = now;
            DtUpdated = now;
            IsActive = true;
            CreatedBy = string.Empty;
            UpdatedBy = string.Empty;
            DeletedBy = string.Empty;
            LstId = new List<int>();
        }
    }
}