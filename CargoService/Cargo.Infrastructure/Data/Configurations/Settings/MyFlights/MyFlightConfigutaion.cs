using Cargo.Infrastructure.Data.Model.Settings.MyFlights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.MyFlights
{
    class MyFlightConfigutaion : IEntityTypeConfiguration<MyFlight>
    {
        public void Configure(EntityTypeBuilder<MyFlight> builder)
        {
            builder.ToTable("MyFlights");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.CarrierId).HasColumnName("CarrierId").IsRequired();
            builder.Property(p => p.Agreement).HasColumnName("Agreement").HasMaxLength(15);
            builder.Property(p => p.DateAt).HasColumnName("DateAt").IsRequired();
            builder.Property(p => p.DateTo).HasColumnName("DateTo");

            builder.HasOne(fn => fn.Carrier)
            .WithMany()
            .HasForeignKey(fn => fn.CarrierId);
        }
    }
}
