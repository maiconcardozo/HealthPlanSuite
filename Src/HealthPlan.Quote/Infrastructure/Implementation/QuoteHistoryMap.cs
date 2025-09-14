using HealthPlan.Quote.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.Implementation
{
    /// <summary>
    /// Entity Framework configuration for the QuoteHistory entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class QuoteHistoryMap : IEntityTypeConfiguration<QuoteHistory>
    {
        /// <summary>
        /// Configures the QuoteHistory entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for QuoteHistory</param>
        public void Configure(EntityTypeBuilder<QuoteHistory> builder)
        {
            builder.ToTable("QuoteHistories");
            
            // Primary key
            builder.HasKey(x => x.Id);
            
            // Properties configuration
            builder.Property(x => x.Id)
                .HasColumnName("IdHistoricoCotacao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IdCotacao)
                .HasColumnName("IdCotacao")
                .IsRequired();

            builder.Property(e => e.PreviousStatus)
                .HasMaxLength(50);

            builder.Property(e => e.NewStatus)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Reason)
                .HasMaxLength(500);

            builder.Property(e => e.Observations)
                .HasColumnType("text");

            builder.Property(e => e.ChangeDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.ResponsibleUser)
                .IsRequired()
                .HasMaxLength(256);

            // Create indexes for efficient filtering
            builder.HasIndex(e => e.IdCotacao)
                .HasDatabaseName("IX_QuoteHistories_IdCotacao");

            builder.HasIndex(e => e.NewStatus)
                .HasDatabaseName("IX_QuoteHistories_NewStatus");

            builder.HasIndex(e => e.ChangeDate)
                .HasDatabaseName("IX_QuoteHistories_ChangeDate");

            // Foreign key relationship
            builder.HasOne<Domain.Implementation.Quote>()
                .WithMany()
                .HasForeignKey(e => e.IdCotacao)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}