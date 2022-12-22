using Cargo.Infrastructure.Data.Model.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Settings
{
    public class MailLimitsConfiguration : IEntityTypeConfiguration<MailLimits>
    {
        public void Configure(EntityTypeBuilder<MailLimits> builder)
        {
            builder.ToTable("MailLimits");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.FlightNumber).HasColumnName("FlightNumber").HasMaxLength(30);
            builder.Property(p => p.DateStart).HasColumnName("DateStart");
            builder.Property(p => p.DateEnd).HasColumnName("DateEnd");
            builder.Property(p => p.MaxPayloadVolume).HasColumnType("decimal(20,3)").HasColumnName("MaxPayloadVolume");
            builder.Property(p => p.MaxPayloadWeight).HasColumnType("decimal(20,3)").HasColumnName("MaxPayloadWeight");
            builder.Property(p => p.IataCode).HasColumnName("IataCode").HasMaxLength(5);

            builder.HasOne(sc => sc.AircraftType).WithMany().HasForeignKey(k => k.AircraftTypeId).IsRequired();
            builder.HasOne(sc => sc.Airline).WithMany().HasForeignKey(k => k.AirlineId).IsRequired(false);
            builder.HasOne(sc => sc.FromIataLocation).WithMany().HasForeignKey(k => k.FromIataLocationId).IsRequired(false);
            builder.HasOne(sc => sc.InIataLocation).WithMany().HasForeignKey(k => k.InIataLocationId).IsRequired(false);
        }
    }
}
