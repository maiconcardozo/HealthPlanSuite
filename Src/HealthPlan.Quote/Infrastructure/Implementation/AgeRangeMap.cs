using HealthPlan.Quote.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.Implementation
{
    /// <summary>
    /// Entity Framework configuration for the AgeRange entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class AgeRangeMap : IEntityTypeConfiguration<AgeRange>
    {
        /// <summary>
        /// Configures the AgeRange entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for AgeRange</param>
        public void Configure(EntityTypeBuilder<AgeRange> builder)
        {
            builder.ToTable("AgeRanges");

            // Primary key
            builder.HasKey(x => x.Id);
            
            // Properties configuration
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.MinAge)
                .IsRequired();

            builder.Property(e => e.MaxAge)
                .IsRequired();

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(100);

            // Base entity properties
            builder.Property(x => x.IsActive)
                .HasColumnName("IsActive")
                .HasDefaultValue(true);

            builder.Property(x => x.DtCreated)
                .HasColumnName("DtCreated")
                .HasDefaultValueSql("NOW()");

            builder.Property(x => x.DtUpdated)
                .HasColumnName("DtUpdated");

            builder.Property(x => x.DtDeleted)
                .HasColumnName("DtDeleted");

            builder.Property(x => x.DeletedBy)
                .HasColumnName("DeletedBy")
                .HasMaxLength(100);

            // Create index on age range for efficient lookup
            builder.HasIndex(e => new { e.MinAge, e.MaxAge })
                .HasDatabaseName("IX_AgeRanges_MinAge_MaxAge");
        }
    }
}