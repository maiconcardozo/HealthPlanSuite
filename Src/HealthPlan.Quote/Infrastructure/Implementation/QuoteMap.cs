using HealthPlan.Quote.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.Implementation
{
    /// <summary>
    /// Entity Framework configuration for the Quote entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class QuoteMap : IEntityTypeConfiguration<Domain.Implementation.Quote>
    {
        /// <summary>
        /// Configures the Quote entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for Quote</param>
        public void Configure(EntityTypeBuilder<Domain.Implementation.Quote> builder)
        {
            builder.ToTable("Quotes");
            

            builder.Property(e => e.CompanyId)
                .IsRequired();

            builder.Property(e => e.BeneficiaryId)
                .IsRequired();

            builder.Property(e => e.HealthPlanId)
                .IsRequired();

            builder.Property(e => e.QuoteNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.QuoteDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.ValidUntil)
                .IsRequired();

            builder.Property(e => e.MonthlyPremium)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(e => e.AgeRangeId)
                .IsRequired();

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            builder.Property(e => e.Notes)
                .HasMaxLength(1000);

            // Create unique index on QuoteNumber for business logic enforcement
            builder.HasIndex(e => e.QuoteNumber)
                .IsUnique()
                .HasDatabaseName("IX_Quotes_QuoteNumber_Unique");

            // Create indexes for efficient filtering
            builder.HasIndex(e => e.BeneficiaryId)
                .HasDatabaseName("IX_Quotes_BeneficiaryId");

            builder.HasIndex(e => e.CompanyId)
                .HasDatabaseName("IX_Quotes_CompanyId");

            builder.HasIndex(e => e.Status)
                .HasDatabaseName("IX_Quotes_Status");

            builder.HasIndex(e => e.QuoteDate)
                .HasDatabaseName("IX_Quotes_QuoteDate");

            // Foreign key relationships
            builder.HasOne<Company>()
                .WithMany()
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Beneficiary>()
                .WithMany()
                .HasForeignKey(e => e.BeneficiaryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Domain.Implementation.HealthPlan>()
                .WithMany()
                .HasForeignKey(e => e.HealthPlanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<AgeRange>()
                .WithMany()
                .HasForeignKey(e => e.AgeRangeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}