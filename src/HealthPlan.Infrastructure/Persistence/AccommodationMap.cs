using HealthPlan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Infrastructure.Persistence
{
    /// <summary>
    /// Entity Framework configuration for the Accommodation entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class AccommodationMap : IEntityTypeConfiguration<Accommodation>
    {
        /// <summary>
        /// Configures the Accommodation entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for Accommodation</param>
        public void Configure(EntityTypeBuilder<Accommodation> builder)
        {
            builder.ToTable("Accommodations");
            
            // Primary key
            builder.HasKey(x => x.Id);
            
            // Properties configuration
            builder.Property(x => x.Id)
                .HasColumnName("IdAccommodation")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.AdditionalValue)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0.00m);

            // Create index on type for efficient filtering
            builder.HasIndex(e => e.Type)
                .HasDatabaseName("IX_Accommodations_Type");
        }
    }
}