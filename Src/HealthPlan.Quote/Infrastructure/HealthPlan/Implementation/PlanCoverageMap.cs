using Foundation.Base.Infrastructure.Data;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.HealthPlan.Implementation
{
    public class PlanCoverageMap : EntityMap<PlanCoverage>
    {
        public override void Configure(EntityTypeBuilder<PlanCoverage> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("PlanCoverages");

            // Foreign key relationships
            builder.HasOne(x => x.HealthPlan)
                .WithMany(x => x.PlanCoverages)
                .HasForeignKey(x => x.HealthPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.HealthEstablishment)
                .WithMany(x => x.PlanCoverages)
                .HasForeignKey(x => x.HealthEstablishmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Unique constraint to prevent duplicate coverage
            builder.HasIndex(x => new { x.HealthPlanId, x.HealthEstablishmentId })
                .IsUnique();
        }
    }
}