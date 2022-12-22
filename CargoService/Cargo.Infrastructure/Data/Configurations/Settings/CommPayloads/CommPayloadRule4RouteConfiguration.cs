using Cargo.Infrastructure.Data.Model.Settings.CommPayloads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class CommPayloadRule4RouteConfiguration : IEntityTypeConfiguration<CommPayloadRule4Route>
    {
        public void Configure(EntityTypeBuilder<CommPayloadRule4Route> builder)
        {
            builder.ToTable("CommPayloadRules4Route");

            builder.HasKey(a => a.CommPayloadRuleId);

            builder.Property(p => p.Origin).HasMaxLength(3).IsRequired();
            builder.Property(p => p.Destination).HasMaxLength(3).IsRequired();

            builder.HasOne(sc => sc.CommercialPayload)
                .WithOne(c => c.CommPayloadRule4Route)
                .HasForeignKey<CommPayloadRule4Route>(row => row.CommPayloadRuleId);
        }
    }
}
