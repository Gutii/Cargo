using Cargo.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class BookingRcsConfiguration : IEntityTypeConfiguration<BookingRcs>
    {
        public void Configure(EntityTypeBuilder<BookingRcs> builder)
        {
            builder.ToTable("BookingsArchive");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.AwbId).HasColumnName("AwbId").IsRequired();
            builder.Property(p => p.QuanDetShipmentDescriptionCode).HasColumnName("QuanDetShipmentDescriptionCode").HasMaxLength(1).IsRequired();
            builder.Property(p => p.NumberOfPieces).HasColumnName("NumberOfPieces").IsRequired();
            builder.Property(p => p.WeightCode).HasColumnName("WeightCode").HasMaxLength(1).IsRequired();
            builder.Property(p => p.Weight).HasColumnName("Weight").IsRequired();
            builder.Property(p => p.VolumeCode).HasColumnName("VolumeCode").HasMaxLength(2).IsRequired();
            builder.Property(p => p.VolumeAmount).HasColumnName("VolumeAmount").IsRequired();
            builder.Property(p => p.SpaceAllocationCode).HasColumnName("SpaceAllocationCode").HasMaxLength(2).IsRequired();
            builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();


            builder.HasOne(sc => sc.Awb)
                .WithMany(s => s.BookingRcs)
                .HasForeignKey(k => k.AwbId);

            builder.HasOne(sc => sc.FlightSchedule)
                .WithMany(s => s.BookingRcs)
                .HasForeignKey(k => k.FlightScheduleId);


        }
    }
}
