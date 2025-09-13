using Foundation.Base.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foundation.Base.Infrastructure.Data
{
    /// <summary>
    /// Base class for Entity Framework entity configuration mappings.
    /// Provides common configuration for all entities that implement IEntity.
    /// </summary>
    /// <typeparam name="T">Entity type that implements IEntity</typeparam>
    public abstract class EntityMap<T> where T : class, IEntity
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

            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
        }
    }
}