using Foundation.Base.Domain.Interface;

namespace Foundation.Base.Domain.Implementation
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}