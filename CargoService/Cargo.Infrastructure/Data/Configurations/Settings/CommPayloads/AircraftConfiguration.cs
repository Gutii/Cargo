using Cargo.Infrastructure.Data.Model.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Settings
{
    public class AircraftConfiguration : IEntityTypeConfiguration<Aircraft>
    {
        public void Configure(EntityTypeBuilder<Aircraft> builder)
        {
            builder.ToTable("Aircrafts");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.OnboardNumber).HasColumnName("OnboardNumber").HasMaxLength(20).IsRequired();
            builder.Property(p => p.MaxGrossPayload).HasColumnType("decimal(20,3)").HasColumnName("MaxGrossPayload");
            builder.Property(p => p.MaxTakeOffWeight).HasColumnType("decimal(20,3)").HasColumnName("MaxTakeOffWeight");
            builder.Property(p => p.AccseptedShr).HasColumnName("AccseptedShr").HasMaxLength(150);
            builder.Property(p => p.IataCode).HasColumnName("IataCode").HasMaxLength(5);

            builder.HasOne(sc => sc.AircraftType).WithMany().HasForeignKey(k => k.AircraftTypeId);
        }
    }
}
