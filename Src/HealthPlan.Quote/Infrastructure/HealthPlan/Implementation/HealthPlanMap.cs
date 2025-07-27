using Foundation.Base.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.HealthPlan.Implementation
{
    public class HealthPlanMap : EntityMap<Domain.HealthPlan.Implementation.HealthPlan>
    {
        public override void Configure(EntityTypeBuilder<Domain.HealthPlan.Implementation.HealthPlan> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("HealthPlans");
            
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
                
            builder.Property(x => x.Coverage)
                .IsRequired()
                .HasMaxLength(1000);
                
            builder.Property(x => x.HasCoparticipation)
                .IsRequired();

            // Foreign key relationships
            builder.HasOne(x => x.HealthInsuranceOperator)
                .WithMany(x => x.HealthPlans)
                .HasForeignKey(x => x.HealthInsuranceOperatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PlanType)
                .WithMany(x => x.HealthPlans)
                .HasForeignKey(x => x.PlanTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}