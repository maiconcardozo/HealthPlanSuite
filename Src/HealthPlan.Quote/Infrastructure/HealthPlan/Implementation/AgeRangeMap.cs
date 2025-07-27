using Foundation.Base.Infrastructure.Data;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.HealthPlan.Implementation
{
    public class AgeRangeMap : EntityMap<AgeRange>
    {
        public override void Configure(EntityTypeBuilder<AgeRange> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("AgeRanges");
            
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(x => x.MinAge)
                .IsRequired();
                
            builder.Property(x => x.MaxAge)
                .IsRequired();

            // Ensure age range is valid
            builder.HasCheckConstraint("CK_AgeRange_MinAge_LTE_MaxAge", "[MinAge] <= [MaxAge]");
        }
    }
}