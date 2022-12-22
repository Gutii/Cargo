using Cargo.Infrastructure.Data.Model.Tariffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Rates
{
    class IataChargeConfiguration : IEntityTypeConfiguration<IataCharge>
    {
        public void Configure(EntityTypeBuilder<IataCharge> builder)
        {
            builder.ToTable("IataCharges");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Code).HasColumnName("Code");
            builder.Property(p => p.Category).HasColumnName("Category");
            builder.Property(p => p.DescriptionEng).HasColumnName("DescriptionEng");
            builder.Property(p => p.DescriptionRus).HasColumnName("DescriptionRus");
        }
    }
}