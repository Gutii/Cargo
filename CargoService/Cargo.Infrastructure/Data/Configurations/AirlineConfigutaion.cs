using Cargo.Infrastructure.Data.Model.Dictionary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class AirlineConfigutaion : IEntityTypeConfiguration<Airline>
    {
        public void Configure(EntityTypeBuilder<Airline> builder)
        {
            builder.ToTable("Airlines");

            builder.HasKey(p => p.ContragentId);

            builder.Property(p => p.IataCode).HasColumnName("Iata").HasMaxLength(2).IsRequired();
            builder.Property(p => p.IcaoCode).HasColumnName("Icao").HasMaxLength(3).IsRequired();
            builder.Property(p => p.PrefixAwb).HasColumnName("PrefixAwb").HasMaxLength(3);

            builder.HasOne(sc => sc.Contragent).WithOne(d => d.Carrier).HasForeignKey<Airline>(r => r.ContragentId);
        }
    }
}
