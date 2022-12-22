using Cargo.Infrastructure.Data.Model.Settings.CommPayloads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class CommPayloadRule4FlightConfiguration : IEntityTypeConfiguration<CommPayloadRule4Flight>
    {
        public void Configure(EntityTypeBuilder<CommPayloadRule4Flight> builder)
        {
            builder.ToTable("CommPayloadRules4Flight");

            builder.HasKey(a => a.CommPayloadRuleId);

            builder.Property(p => p.FlightCarrier).HasMaxLength(2).IsRequired();
            builder.Property(p => p.FlightNumber).HasMaxLength(7).IsRequired();
            builder.Property(p => p.DateAt);
            builder.Property(p => p.DateTo);

            builder.HasOne(o => o.CommercialPayload)
                .WithOne(c => c.CommPayloadRule4Flight)
                .HasForeignKey<CommPayloadRule4Flight>(fk => fk.CommPayloadRuleId);
        }
    }
}