using Foundation.Base.Infrastructure.Data;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.HealthPlan.Implementation
{
    public class PriceTableMap : EntityMap<PriceTable>
    {
        public override void Configure(EntityTypeBuilder<PriceTable> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("PriceTables");
            
            builder.Property(x => x.MonthlyFee)
                .IsRequired()
                .HasColumnType("decimal(10,2)");
                
            builder.Property(x => x.CoparticipationValue)
                .HasColumnType("decimal(10,2)");
                
            builder.Property(x => x.StartDate)
                .IsRequired();
                
            builder.Property(x => x.EndDate);

            // Foreign key relationships
            builder.HasOne(x => x.HealthPlan)
                .WithMany(x => x.PriceTables)
                .HasForeignKey(x => x.HealthPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.AgeRange)
                .WithMany(x => x.PriceTables)
                .HasForeignKey(x => x.AgeRangeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Composite index for better query performance
            builder.HasIndex(x => new { x.HealthPlanId, x.AgeRangeId, x.StartDate });
        }
    }
}