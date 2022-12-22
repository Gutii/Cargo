using Cargo.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class SizeGroupConfigutaion : IEntityTypeConfiguration<SizeGroup>
    {
        public void Configure(EntityTypeBuilder<SizeGroup> builder)
        {
            builder.ToTable("SizeGroups");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Width).HasColumnName("Width").IsRequired();
            builder.Property(p => p.Height).HasColumnName("Height").IsRequired();
            builder.Property(p => p.Lenght).HasColumnName("Lenght").IsRequired();

            builder.HasOne(sc => sc.Awb).WithMany(d => d.SizeGroups).HasForeignKey(r => r.AwbId);
        }
    }
}
