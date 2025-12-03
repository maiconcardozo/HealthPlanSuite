using HealthPlan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Infrastructure.Persistence
{
    /// <summary>
    /// Entity Framework configuration for the Quote entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class QuoteMap : IEntityTypeConfiguration<Domain.Entities.Quote>
    {
        /// <summary>
        /// Configures the Quote entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for Quote</param>
        public void Configure(EntityTypeBuilder<Domain.Entities.Quote> builder)
        {
            builder.ToTable("Quotes");
            
            // Primary key
            builder.HasKey(x => x.Id);
            
            // Properties configuration
            builder.Property(x => x.Id)
                .HasColumnName("IdQuote")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IdCompany)
                .HasColumnName("IdCompany")
                .IsRequired();

            builder.Property(e => e.IdBeneficiary)
                .HasColumnName("IdBeneficiary")
                .IsRequired();

            builder.Property(e => e.IdHealthPlan)
                .HasColumnName("IdHealthPlan")
                .IsRequired();

            builder.Property(e => e.IdAgeRange)
                .HasColumnName("IdAgeRange")
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
            builder.HasIndex(e => e.IdBeneficiary)
                .HasDatabaseName("IX_Quotes_IdBeneficiary");

            builder.HasIndex(e => e.IdCompany)
                .HasDatabaseName("IX_Quotes_IdCompany");

            builder.HasIndex(e => e.Status)
                .HasDatabaseName("IX_Quotes_Status");

            builder.HasIndex(e => e.QuoteDate)
                .HasDatabaseName("IX_Quotes_QuoteDate");

            // Foreign key relationships
            builder.HasOne<Company>()
                .WithMany()
                .HasForeignKey(e => e.IdCompany)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Beneficiary>()
                .WithMany()
                .HasForeignKey(e => e.IdBeneficiary)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Domain.Entities.HealthPlan>()
                .WithMany()
                .HasForeignKey(e => e.IdHealthPlan)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<AgeRange>()
                .WithMany()
                .HasForeignKey(e => e.IdAgeRange)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}