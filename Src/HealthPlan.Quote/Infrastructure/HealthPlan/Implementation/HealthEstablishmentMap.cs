using Foundation.Base.Infrastructure.Data;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.HealthPlan.Implementation
{
    public class HealthEstablishmentMap : EntityMap<HealthEstablishment>
    {
        public override void Configure(EntityTypeBuilder<HealthEstablishment> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("HealthEstablishments");
            
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
                
            builder.Property(x => x.Type)
                .IsRequired()
                .HasMaxLength(50);
                
            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(500);
                
            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(x => x.State)
                .IsRequired()
                .HasMaxLength(50);

            // Index for better query performance
            builder.HasIndex(x => new { x.Type, x.City, x.State });
        }
    }
}