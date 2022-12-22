using Cargo.Infrastructure.Data.Model.Dictionary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class CountryConfigutaion : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Alpha2).HasColumnName("Alpha2").HasMaxLength(2).IsRequired();
            builder.Property(p => p.Alpha3).HasColumnName("Alpha3").HasMaxLength(3);
            builder.Property(p => p.Numeric3).HasMaxLength(3);
            builder.Property(p => p.EnglishShortName).HasColumnName("EnglishShortName").HasMaxLength(130).IsRequired();
            builder.Property(p => p.RussianName).HasColumnName("RussianName").HasMaxLength(200);
        }
    }
}
