using HealthPlan.Quote.Domain.Implementation;
using Foundation.Base.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.Implementation
{
    /// <summary>
    /// Entity Framework configuration for the AgeRange entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class AgeRangeMap : EntityMap<AgeRange>, IEntityTypeConfiguration<AgeRange>
    {
        /// <summary>
        /// Configures the AgeRange entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for AgeRange</param>
        public override void Configure(EntityTypeBuilder<AgeRange> builder)
        {
            builder.ToTable("AgeRanges");
            base.Configure(builder);

            builder.Property(e => e.MinAge)
                .IsRequired();

            builder.Property(e => e.MaxAge)
                .IsRequired();

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(100);

            // Create index on age range for efficient lookup
            builder.HasIndex(e => new { e.MinAge, e.MaxAge })
                .HasDatabaseName("IX_AgeRanges_MinAge_MaxAge");
        }
    }
}