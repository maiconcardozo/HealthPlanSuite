using HealthPlan.Quote.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.Implementation
{
    /// <summary>
    /// Entity Framework configuration for the Company entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class CompanyMap : IEntityTypeConfiguration<Company>
    {
        /// <summary>
        /// Configures the Company entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for Company</param>
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");

            // Primary key
            builder.HasKey(x => x.Id);
            
            // Properties configuration
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

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

            // Create unique index on CNPJ for business logic enforcement
            builder.HasIndex(e => e.CNPJ)
                .IsUnique()
                .HasDatabaseName("IX_Companies_CNPJ_Unique");
        }
    }
}