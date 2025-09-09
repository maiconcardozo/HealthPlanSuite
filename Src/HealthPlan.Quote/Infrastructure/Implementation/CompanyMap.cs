using HealthPlan.Quote.Domain.Implementation;
using Foundation.Base.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.Implementation
{
    /// <summary>
    /// Entity Framework configuration for the Company entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class CompanyMap : EntityMap<Company>, IEntityTypeConfiguration<Company>
    {
        /// <summary>
        /// Configures the Company entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for Company</param>
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");
            base.Configure(builder);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.TradeName)
                .HasMaxLength(200);

            builder.Property(e => e.CNPJ)
                .IsRequired()
                .HasMaxLength(18);

            builder.Property(e => e.Email)
                .HasMaxLength(100);

            builder.Property(e => e.Phone)
                .HasMaxLength(20);

            builder.Property(e => e.Address)
                .HasMaxLength(300);

            builder.Property(e => e.City)
                .HasMaxLength(100);

            builder.Property(e => e.State)
                .HasMaxLength(2);

            builder.Property(e => e.ZipCode)
                .HasMaxLength(10);

            // Create unique index on CNPJ for business logic enforcement
            builder.HasIndex(e => e.CNPJ)
                .IsUnique()
                .HasDatabaseName("IX_Companies_CNPJ_Unique");
        }
    }
}