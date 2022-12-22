using Cargo.Infrastructure.Data.Model.Settings.CommPayloads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class CommPayloadRule4AicraftTypeConfiguration : IEntityTypeConfiguration<CommPayloadRule4AicraftType>
    {
        public void Configure(EntityTypeBuilder<CommPayloadRule4AicraftType> builder)
        {
            builder.ToTable("CommPayloadRules4AicraftType");

            builder.HasKey(a => a.CommPayloadRuleId);

            builder.Property(p => p.AircraftTypeId).IsRequired();
            builder.Property(p => p.AircraftType).HasMaxLength(15).IsRequired();

            builder.HasOne(sc => sc.CommercialPayload)
                .WithOne(c => c.CommPayloadRule4AicraftType)
                .HasForeignKey<CommPayloadRule4AicraftType>(row => row.CommPayloadRuleId);
        }
    }
}
