using Cargo.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class AwbContragentConfiguration : IEntityTypeConfiguration<AwbContragent>
    {
        public void Configure(EntityTypeBuilder<AwbContragent> builder)
        {
            builder.ToTable("AwbContragents");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.NameRu).HasColumnName("NameRu").HasMaxLength(30);
            builder.Property(p => p.NameEn).HasColumnName("NameEn").HasMaxLength(30);
            builder.Property(p => p.NameExRu).HasColumnName("NameExRu").HasMaxLength(50);
            builder.Property(p => p.NameExEn).HasColumnName("NameExEn").HasMaxLength(50);
            builder.Property(p => p.CityRu).HasColumnName("CityRu").HasMaxLength(15);
            builder.Property(p => p.CityEn).HasColumnName("CityEn").HasMaxLength(15);
            builder.Property(p => p.CountryISO).HasColumnName("CountryISO").HasMaxLength(2);
            builder.Property(p => p.ZipCode).HasColumnName("ZipCode").HasMaxLength(9);
            builder.Property(p => p.Passport).HasColumnName("Passport").HasMaxLength(20);
            builder.Property(p => p.RegionRu).HasColumnName("RegionRu").HasMaxLength(30);
            builder.Property(p => p.RegionEn).HasColumnName("RegionEn").HasMaxLength(30);
            builder.Property(p => p.CodeEn).HasColumnName("CodeEn").HasMaxLength(9);
            builder.Property(p => p.Phone).HasColumnName("Phone").HasMaxLength(17);
            builder.Property(p => p.Fax).HasColumnName("Fax").HasMaxLength(17);
            builder.Property(p => p.AddressRu).HasColumnName("AddressRu").HasMaxLength(70);
            builder.Property(p => p.AddressEn).HasColumnName("AddressEn").HasMaxLength(70);
            builder.Property(p => p.PreviewRu).HasColumnName("PreviewRu").HasMaxLength(300);
            builder.Property(p => p.PreviewEn).HasColumnName("PreviewEn").HasMaxLength(300);
            builder.Property(p => p.AgentCass).HasColumnName("AgentCass").HasMaxLength(15);
            builder.Property(p => p.IataCode).HasColumnName("IataCode").HasMaxLength(15);
            builder.Property(p => p.Email).HasColumnName("Email").HasMaxLength(25);
        }
    }
}
