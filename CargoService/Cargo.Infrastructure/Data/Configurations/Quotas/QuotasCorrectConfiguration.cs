using Cargo.Infrastructure.Data.Model.Quotas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Quotas
{
    public class QuotasCorrectConfiguration : IEntityTypeConfiguration<QuotasCorrect>
    {
        public void Configure(EntityTypeBuilder<QuotasCorrect> builder)
        {
            builder.ToTable("QuotasCorrect");

            builder.HasKey(a => a.QuotasOperativeId);

            builder.Property(p => p.CarrierId).HasColumnName("CarrierId").IsRequired();
            builder.Property(p => p.WeightLimit).HasColumnName("WeightLimit").IsRequired();
            builder.Property(p => p.VolumeLimit).HasColumnName("VolumeLimit").IsRequired();

            builder.HasOne(sc => sc.Agent)
                .WithMany().HasForeignKey(f => f.AgentId);
            builder.HasOne(sc => sc.FlightShedule)
                .WithMany().HasForeignKey(f => f.FlightId);
            builder.HasOne(sc => sc.QuotasOperative)
                .WithMany().HasForeignKey(f => f.QuotasOperativeId);
        }
    }
}
