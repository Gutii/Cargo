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
    public class ParametersSettingsConfiguration : IEntityTypeConfiguration<ParametersSettings>
    {
        public void Configure(EntityTypeBuilder<ParametersSettings> builder)
        {
            builder.ToTable("ParametersSettings");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.FunctionalSection).HasColumnName("FunctionalSection").HasMaxLength(2).IsRequired();
            builder.Property(p => p.NumberGroupParameters).HasColumnName("NumberGroupParameters").IsRequired();
            builder.Property(p => p.NumberParameterOnGroup).HasColumnName("NumberParameterOnGroup").IsRequired();
            builder.Property(p => p.DescriptionRu).HasColumnName("DescriptionRu").HasMaxLength(5000);
            builder.Property(p => p.DescriptionEn).HasColumnName("DescriptionEn").HasMaxLength(5000);
            builder.Property(p => p.Value).HasColumnName("Value").HasMaxLength(100).IsRequired();
            builder.Property(p => p.ValueType).HasColumnName("ValueType").HasMaxLength(150).IsRequired();
            builder.Property(p => p.UnitMeasurement).HasColumnName("UnitMeasurement").HasMaxLength(15).IsRequired();

        }
    }
}
