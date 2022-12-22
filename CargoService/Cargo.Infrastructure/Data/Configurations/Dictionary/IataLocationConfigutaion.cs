using Cargo.Infrastructure.Data.Model.Dictionary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Dictionary
{
    class IataLocationConfigutaion : IEntityTypeConfiguration<IataLocation>
    {
        public void Configure(EntityTypeBuilder<IataLocation> builder)
        {
            builder.ToTable("IataLocations");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Code).HasColumnName("Code").HasMaxLength(3).IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(150).IsRequired();
            builder.Property(p => p.CityName).HasColumnName("CityName").HasMaxLength(150).IsRequired();
            builder.Property(p => p.RussianName).HasColumnName("RussianName").HasMaxLength(200);
            builder.Property(p => p.TimeZone).HasColumnName("TimeZone");

            builder.HasOne(p => p.MetropolitanArea)
                .WithMany(p => p.Airports);

            builder.HasOne(sc => sc.Country)
                .WithMany(s => s.Locations)
                .HasForeignKey(k => k.CountryId);
        }
    }
}
