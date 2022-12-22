using Cargo.Infrastructure.Data.Model.Settings.MyFlights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.MyFlights
{
    class MyFlightNumbersConfigutaion : IEntityTypeConfiguration<MyFlightNumbers>
    {
        public void Configure(EntityTypeBuilder<MyFlightNumbers> builder)
        {
            builder.ToTable("MyFlightNumbers");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.BeginNum).HasColumnName("BeginNum").IsRequired();
            builder.Property(p => p.EndNum).HasColumnName("EndNum");

            builder.HasOne(fn => fn.MyFlights)
                .WithMany(f => f.MyFlightsNumbers)
                .HasForeignKey(fn => fn.MyFlightsId);
        }
    }
}
