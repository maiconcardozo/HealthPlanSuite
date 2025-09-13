using Foundation.Base.Domain.Implemetation;

namespace HealthPlan.Quote.Foundation
{
    /// <summary>
    /// Concrete base implementation for all entities providing common audit properties.
    /// Extends the Foundation.Base abstract Entity class and provides backward compatibility.
    /// </summary>
    public class Entity : Foundation.Base.Domain.Implemetation.Entity
    {
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
        public Entity() : base()
        {
            // Base constructor handles initialization
        }
    }
}