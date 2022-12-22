using Cargo.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Schedule
{
    public class FlightSheduleConfiguration : IEntityTypeConfiguration<FlightShedule>
    {
        public void Configure(EntityTypeBuilder<FlightShedule> builder)
        {
            builder.ToTable("FlightShedules");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Origin).HasMaxLength(255).IsRequired();
            builder.Property(p => p.Destination).HasMaxLength(255).IsRequired();
            builder.Property(p => p.Number).HasMaxLength(255).IsRequired();
            builder.Property(p => p.FlightDate).IsRequired();
            builder.Property(p => p.StOrigin).IsRequired();
            builder.Property(p => p.StDestination).IsRequired();
            builder.Property(p => p.State).IsRequired();
            builder.Property(p => p.AircraftRegistration).HasMaxLength(255).IsRequired();
            builder.Property(p => p.AircraftType).HasMaxLength(255).IsRequired();
            builder.Property(p => p.SHR).HasMaxLength(255).IsRequired();
            builder.Property(p => p.PayloadVolume);
            builder.Property(p => p.PayloadWeight);
            builder.Property(p => p.SaleState);
        }
    }
}
