using Cargo.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class TaxChargeConfiguration : IEntityTypeConfiguration<TaxCharge>
    {
        public void Configure(EntityTypeBuilder<TaxCharge> builder)
        {
            builder.ToTable("TaxCharges");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Charge).HasColumnName("Charge").IsRequired();
            builder.Property(p => p.ValuationCharge).HasColumnName("ValuationCharge").IsRequired();
            builder.Property(p => p.Tax).HasColumnName("Tax").IsRequired();
            builder.Property(p => p.TotalOtherChargesDueAgent).HasColumnName("TotalOtherChargesDueAgent").IsRequired();
            builder.Property(p => p.TotalOtherChargesDueCarrier).HasColumnName("TotalOtherChargesDueCarrier").IsRequired();
            builder.Property(p => p.Total).HasColumnName("Total").IsRequired();

        }
    }
}
