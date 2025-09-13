namespace HealthPlan.Quote.Domain.Implementation
{
    /// <summary>
    /// Base entity class for domain models, providing audit fields.
    /// </summary>
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
