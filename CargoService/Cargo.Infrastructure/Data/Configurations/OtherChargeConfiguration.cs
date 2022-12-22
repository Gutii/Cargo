using Cargo.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class OtherChargeConfiguration : IEntityTypeConfiguration<OtherCharge>
    {
        public void Configure(EntityTypeBuilder<OtherCharge> builder)
        {
            builder.ToTable("OtherCharges");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.TypeCharge).HasColumnName("TypeCharge").HasMaxLength(3);
            builder.Property(p => p.CA).HasColumnName("CA").HasMaxLength(1);
            builder.Property(p => p.Prepaid).HasColumnName("Prepaid").IsRequired();
            builder.Property(p => p.Collect).HasColumnName("Collect").IsRequired();

            builder.HasOne(sc => sc.Awb)
                .WithMany(s => s.OtherCharges)
                .HasForeignKey(k => k.AwbId);
        }
    }
}
