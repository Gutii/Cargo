using Cargo.Infrastructure.Data.Model.Tariffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Rates
{
    class CarrierChargeConfiguration : IEntityTypeConfiguration<CarrierCharge>
    {
        public void Configure(EntityTypeBuilder<CarrierCharge> builder)
        {
            builder.ToTable("CarrierCharges");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Code).HasColumnName("Code");
            builder.Property(p => p.Category).HasColumnName("Category");
            builder.Property(p => p.DescriptionEng).HasColumnName("DescriptionEng");
            builder.Property(p => p.DescriptionRus).HasColumnName("DescriptionRus");

            builder.Property(p => p.ApplicationType).HasColumnName("ApplicationType");
            builder.Property(p => p.Recepient).HasColumnName("Recepient");
            builder.Property(p => p.IsAllIn).HasColumnName("IsAllIn");

            builder.Property(p => p.SalesChannels)
                .HasColumnName("SalesChannels")
                .HasConversion(
                    x => string.Join(',', x),
                    x => x.Split(',', System.StringSplitOptions.RemoveEmptyEntries));

            builder
                .HasMany(c => c.IncludedShrCodes)
                .WithMany(s => s.IncludingCarrierCharges)
                .UsingEntity(j => j.ToTable("CarrierChargeShrIncluded"));

            builder
                .HasMany(c => c.ExcludedShrCodes)
                .WithMany(s => s.ExcludingCarrierCharges)
                .UsingEntity(j => j.ToTable("CarrierChargeShrExcluded"));

            builder
                .HasMany(c => c.IncludedProducts)
                .WithMany(s => s.IncludingCarrierCharges)
                .UsingEntity(j => j.ToTable("CarrierChargeProductIncluded"));

            builder
                .HasMany(c => c.ExcludedProducts)
                .WithMany(s => s.ExcludingCarrierCharges)
                .UsingEntity(j => j.ToTable("CarrierChargeProductExcluded"));
        }
    }
}