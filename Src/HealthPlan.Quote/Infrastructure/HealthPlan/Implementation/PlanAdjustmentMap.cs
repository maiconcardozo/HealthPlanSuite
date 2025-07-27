using Foundation.Base.Infrastructure.Data;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.HealthPlan.Implementation
{
    public class PlanAdjustmentMap : EntityMap<PlanAdjustment>
    {
        public override void Configure(EntityTypeBuilder<PlanAdjustment> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("PlanAdjustments");
            
            builder.Property(x => x.Percentage)
                .IsRequired()
                .HasColumnType("decimal(5,2)");
                
            builder.Property(x => x.AdjustmentDate)
                .IsRequired();
                
            builder.Property(x => x.AdjustmentType)
                .IsRequired()
                .HasMaxLength(50);
                
            builder.Property(x => x.Observation)
                .HasMaxLength(500);

            // Foreign key relationships
            builder.HasOne(x => x.HealthPlan)
                .WithMany(x => x.PlanAdjustments)
                .HasForeignKey(x => x.HealthPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index for better query performance
            builder.HasIndex(x => new { x.HealthPlanId, x.AdjustmentDate });
        }
    }
}