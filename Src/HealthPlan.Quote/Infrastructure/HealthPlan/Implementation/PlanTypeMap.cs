using Foundation.Base.Infrastructure.Data;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.HealthPlan.Implementation
{
    public class PlanTypeMap : EntityMap<PlanType>
    {
        public override void Configure(EntityTypeBuilder<PlanType> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("PlanTypes");
            
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(200);
                
            builder.Property(x => x.ANSRegulation)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}