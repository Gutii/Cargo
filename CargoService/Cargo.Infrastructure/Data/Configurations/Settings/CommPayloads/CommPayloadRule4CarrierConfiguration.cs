using Cargo.Infrastructure.Data.Model.Settings.CommPayloads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class CommPayloadRule4CarrierConfiguration : IEntityTypeConfiguration<CommPayloadRule4Carrier>
    {
        public void Configure(EntityTypeBuilder<CommPayloadRule4Carrier> builder)
        {
            builder.ToTable("CommPayloadRules4Carrier");

            builder.HasKey(a => a.CommPayloadRuleId);

            builder.Property(p => p.Carrier).HasMaxLength(2).IsRequired();

            builder.HasOne(sc => sc.CommercialPayload)
                .WithOne(c => c.CommPayloadRule4Carrier)
                .HasForeignKey<CommPayloadRule4Carrier>(row => row.CommPayloadRuleId);
        }
    }
}
