using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foundation.Base.Infrastructure.Data
{
    /// <summary>
    /// Base class for Entity Framework entity configuration mappings.
    /// Provides common configuration for all entities that implement IEntity.
    /// Compatible with Foundation.Base NuGet package structure.
    /// </summary>
    /// <typeparam name="T">Entity type that implements IEntity</typeparam>
    public abstract class EntityMap<T> where T : HealthPlan.Quote.Foundation.Entity
    {
        /// <summary>
        /// Configures the entity for Entity Framework.
        /// Override this method to provide specific entity configuration.
        /// </summary>
        /// <param name="builder">Entity type builder for the entity</param>
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            // Configure primary key
            builder.HasKey(e => e.Id);

            // Configure audit fields
            builder.Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.DtCreated)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DtUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
                
            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
                
            builder.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasDefaultValue("");
                
            builder.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasDefaultValue("");
                
            builder.Property(e => e.DeletedBy)
                .HasMaxLength(255)
                .HasDefaultValue("");

            // Ignore computed properties
            builder.Ignore(e => e.LstId);
            builder.Ignore(e => e.DtCreatedStart);
            builder.Ignore(e => e.DtCreatedEnd);
        }
    }
}