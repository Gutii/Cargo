using Cargo.Infrastructure.Data.Model.Tariffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Rates
{
    class CarrierChargeBindingConfiguration : IEntityTypeConfiguration<CarrierChargeBinding>
    {
        public void Configure(EntityTypeBuilder<CarrierChargeBinding> builder)
        {
            builder.ToTable("CarrierChargeBindings");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Parameter).HasColumnName("Parameter");
            builder.Property(p => p.Value).HasColumnName("Value");

            builder
                .HasOne(c => c.CarrierCharge)
                .WithMany(s => s.CarrierChargeBindings)
                .HasForeignKey("CarrierChargeId");

            builder
                .HasOne(c => c.Country)
                .WithMany(s => s.CarrierChargeBindings)
                .HasForeignKey("CountryId");

            builder
                .HasOne(c => c.Currency)
                .WithMany(s => s.CarrierChargeBindings)
                .HasForeignKey("CurrencyId");

            builder
                .HasMany(c => c.Airports)
                .WithMany(s => s.CarrierChargeBindings)
                .UsingEntity(j => j.ToTable("CarrierChargeBindingAirport"));
        }
    }
}