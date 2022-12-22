using Cargo.Infrastructure.Data.Model.Dictionary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Dictionary
{
    class CurrencyConfigutaion : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currencies");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.DigitalCode).HasColumnName("DigitalCode").IsRequired();
            builder.Property(p => p.AlphabeticCode).HasColumnName("AlphabeticCode").IsRequired();
            builder.Property(p => p.NameRus).HasColumnName("NameRus");
            builder.Property(p => p.NameEng).HasColumnName("NameEng");
        }
    }
}
