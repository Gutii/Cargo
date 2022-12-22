using Cargo.Infrastructure.Data.Model.Dictionary.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Dictionary
{
    class IataLocationExtInfoConfigutaion : IEntityTypeConfiguration<IataLocationExtInfo>
    {
        public void Configure(EntityTypeBuilder<IataLocationExtInfo> builder)
        {
            builder.ToTable("IataLocationExtInfos");

            builder.HasKey(a => a.IataLocationId);

            builder.Property(p => p.CityRusName).HasColumnName("CityRusName").HasMaxLength(50);
            builder.Property(p => p.TimeZoneSummer).HasColumnName("TimeZoneSummer");
            builder.Property(p => p.TimeZoneWinter).HasColumnName("TimeZoneWinter");
            builder.Property(p => p.Remarks).HasColumnName("Remarks").HasMaxLength(200);

            builder.HasOne(sc => sc.IataLocation).WithOne(d => d.IataLocationExtInfo).HasForeignKey<IataLocationExtInfo>(r => r.IataLocationId);
        }
    }

    class TelexSettingConfigutaion : IEntityTypeConfiguration<TelexSetting>
    {
        public void Configure(EntityTypeBuilder<TelexSetting> builder)
        {
            builder.ToTable("TelexSettings");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Type).HasColumnName("Type").HasMaxLength(10).IsRequired();
            builder.Property(p => p.OffsetBase).HasColumnName("OffsetBase").HasMaxLength(20);
            builder.Property(p => p.OffsetValue).HasColumnName("OffsetValue");
            builder.Property(p => p.Emails).HasColumnName("Emails").HasMaxLength(250);

            builder.HasOne(sc => sc.IataLocationExtInfo)
                .WithMany(s => s.TelexSettings)
                .HasForeignKey(k => k.IataLocationId);
        }
    }

    class SlaProhibitionConfigutaion : IEntityTypeConfiguration<SlaProhibition>
    {
        public void Configure(EntityTypeBuilder<SlaProhibition> builder)
        {
            builder.ToTable("SlaProhibitions");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Type).HasColumnName("Type").HasMaxLength(15);
            builder.Property(p => p.Shr).HasColumnName("Shr").HasMaxLength(35);
            builder.Property(p => p.MvlVvl).HasColumnName("MvlVvl").HasMaxLength(10).IsRequired();
            builder.Property(p => p.Import).HasColumnName("Import");
            builder.Property(p => p.Export).HasColumnName("Export");
            builder.Property(p => p.Transfer).HasColumnName("Transfer");
            builder.Property(p => p.Transit).HasColumnName("Transit");

            builder.HasOne(sc => sc.IataLocationExtInfo)
                .WithMany(s => s.SlaProhibitions)
                .HasForeignKey(k => k.IataLocationId);
        }
    }

    class SlaTimeLimitationConfigutaion : IEntityTypeConfiguration<SlaTimeLimitation>
    {
        public void Configure(EntityTypeBuilder<SlaTimeLimitation> builder)
        {
            builder.ToTable("SlaTimeLimitations");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Type).HasColumnName("Type").HasMaxLength(15);
            builder.Property(p => p.Shr).HasColumnName("Shr").HasMaxLength(35);
            builder.Property(p => p.MvlVvl).HasColumnName("MvlVvl").HasMaxLength(10).IsRequired();
            builder.Property(p => p.Time).HasColumnName("Time");

            builder.HasOne(sc => sc.IataLocationExtInfo)
                .WithMany(s => s.SlaTimeLimitations)
                .HasForeignKey(k => k.IataLocationId);
        }
    }

    class AirportContactInformationConfigutaion : IEntityTypeConfiguration<AirportContactInformation>
    {
        public void Configure(EntityTypeBuilder<AirportContactInformation> builder)
        {
            builder.ToTable("AirportContactInformations");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.FullName).HasColumnName("FullName").HasMaxLength(150).IsRequired();
            builder.Property(p => p.Position).HasColumnName("Position").HasMaxLength(30);
            builder.Property(p => p.Phone1).HasColumnName("Phone1").HasMaxLength(20);
            builder.Property(p => p.Phone2).HasColumnName("Phone2").HasMaxLength(20);

            builder.Property(p => p.Email1).HasColumnName("Email1").HasMaxLength(50);
            builder.Property(p => p.Email2).HasColumnName("Email2").HasMaxLength(50);

            builder.HasOne(sc => sc.IataLocationExtInfo)
                .WithMany(s => s.AdditionalContactInfo)
                .HasForeignKey(k => k.IataLocationId);
        }
    }
}
