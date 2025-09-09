using HealthPlan.Quote.Domain.Implementation;
using Foundation.Base.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.Implementation
{
    /// <summary>
    /// Entity Framework configuration for the Beneficiary entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class BeneficiaryMap : EntityMap<Beneficiary>, IEntityTypeConfiguration<Beneficiary>
    {
        /// <summary>
        /// Configures the Beneficiary entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for Beneficiary</param>
        public override void Configure(EntityTypeBuilder<Beneficiary> builder)
        {
            builder.ToTable("Beneficiaries");
            base.Configure(builder);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.CPF)
                .IsRequired()
                .HasMaxLength(14);

            builder.Property(e => e.Email)
                .HasMaxLength(100);

            builder.Property(e => e.Phone)
                .HasMaxLength(20);

            builder.Property(e => e.DateOfBirth)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(e => e.Gender)
                .HasMaxLength(10);

            builder.Property(e => e.Address)
                .HasMaxLength(300);

            builder.Property(e => e.City)
                .HasMaxLength(100);

            builder.Property(e => e.State)
                .HasMaxLength(2);

            builder.Property(e => e.ZipCode)
                .HasMaxLength(10);

            // Create unique index on CPF for business logic enforcement
            builder.HasIndex(e => e.CPF)
                .IsUnique()
                .HasDatabaseName("IX_Beneficiaries_CPF_Unique");
        }
    }
}