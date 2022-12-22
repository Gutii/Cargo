using Directories.Infrastructure.Data.Model.Settings.CarrierBookingProps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Infrastructure.Data.Configurations.Settings.BookingRules
{
    public class CarrierSettingsConfiguration : IEntityTypeConfiguration<CarrierSettings>
    {
        public void Configure(EntityTypeBuilder<CarrierSettings> builder)
        {
            builder.ToTable("CarrierSettings");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Value).HasColumnName("Value").HasMaxLength(100).IsRequired();
            builder.Property(p => p.CarrierId).HasColumnName("CarrierId").IsRequired();

            builder.HasOne(p => p.ParametersSettings)
                .WithMany(d => d.CarrierSettings)
                .HasForeignKey(il => il.ParametersSettingsId);
        }
    }
}
