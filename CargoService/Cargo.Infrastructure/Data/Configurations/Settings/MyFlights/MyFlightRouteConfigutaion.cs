using Cargo.Infrastructure.Data.Model.Settings.MyFlights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.MyFlights
{
    class MyFlightRouteConfigutaion : IEntityTypeConfiguration<MyFlightRoute>
    {
        public void Configure(EntityTypeBuilder<MyFlightRoute> builder)
        {
            builder.ToTable("MyFlightRoutes");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Origin).HasColumnName("Origin").HasMaxLength(3).IsRequired();
            builder.Property(p => p.Destination).HasColumnName("Destination").HasMaxLength(3).IsRequired();

            builder.HasOne(fn => fn.MyFlights)
                .WithMany(f => f.MyFlightsRoutes)
                .HasForeignKey(fn => fn.MyFlightsId);
        }
    }
}
