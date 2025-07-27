using Foundation.Base.Infrastructure.Data;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.HealthPlan.Implementation
{
    public class HealthInsuranceOperatorMap : EntityMap<HealthInsuranceOperator>
    {
        public override void Configure(EntityTypeBuilder<HealthInsuranceOperator> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("HealthInsuranceOperators");
            
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
                
            builder.Property(x => x.CNPJ)
                .IsRequired()
                .HasMaxLength(18);
                
            builder.Property(x => x.Website)
                .HasMaxLength(500);
                
            builder.Property(x => x.Phone)
                .HasMaxLength(20);

            builder.HasIndex(x => x.CNPJ)
                .IsUnique();
        }
    }
}