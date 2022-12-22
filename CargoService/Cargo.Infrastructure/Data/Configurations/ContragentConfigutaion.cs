using Cargo.Infrastructure.Data.Model.Dictionary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class ContragentConfigutaion : IEntityTypeConfiguration<Contragent>
    {
        public void Configure(EntityTypeBuilder<Contragent> builder)
        {
            builder.ToTable("Contragents");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.InternationalName).HasColumnName("InternationalName").HasMaxLength(200).IsRequired();
            builder.Property(p => p.NameEngAdditional).HasColumnName("NameEngAdditional").HasMaxLength(200);

            builder.Property(p => p.RussianName).HasColumnName("RussianName").HasMaxLength(230);
            builder.Property(p => p.NameRusAdditional).HasColumnName("NameRusAdditional").HasMaxLength(200);

            builder.Property(p => p.DomesticName).HasColumnName("DomesticName").HasMaxLength(250);

            builder.Property(p => p.PostCode).HasColumnName("PostCode").HasMaxLength(9);
            builder.Property(p => p.CountryId).HasColumnName("CountryId");

            builder.Property(p => p.StateCode).HasColumnName("StateCode").HasMaxLength(6);
            builder.Property(p => p.StateProvince).HasColumnName("StateProvince").HasMaxLength(9);
            builder.Property(p => p.StreetAddressName).HasColumnName("StreetAddressName").HasMaxLength(35);
            builder.Property(p => p.Place).HasColumnName("Place").HasMaxLength(17);

            builder.Property(p => p.StateProvinceRus).HasColumnName("StateProvinceRus").HasMaxLength(9);
            builder.Property(p => p.StreetAddressNameRus).HasColumnName("StreetAddressNameRus").HasMaxLength(35);
            builder.Property(p => p.PlaceRus).HasColumnName("PlaceRus").HasMaxLength(17);

            builder.Property(p => p.IsPhysic).HasColumnName("IsPhysic");
            builder.Property(p => p.OGRN).HasColumnName("OGRN").HasMaxLength(20);
            builder.Property(p => p.Inn).HasColumnName("Inn").HasMaxLength(12);
            builder.Property(p => p.Kpp).HasColumnName("Kpp").HasMaxLength(10);
            builder.Property(p => p.AccountNumber).HasColumnName("AccountNumber").HasMaxLength(14);
            builder.Property(p => p.Login).HasColumnName("Login").HasMaxLength(70).IsRequired();
            builder.Property(p => p.Pass).HasColumnName("Pass").HasMaxLength(20).IsRequired();

            builder.HasOne(p => p.Country)
                .WithMany(c => c.Contragents)
                .HasForeignKey(il => il.CountryId);
        }
    }
}
