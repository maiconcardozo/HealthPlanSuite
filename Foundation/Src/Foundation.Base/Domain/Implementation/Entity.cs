using System.ComponentModel.DataAnnotations;

namespace Foundation.Base.Domain.Implementation
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        protected Entity()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public virtual void MarkAsUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}