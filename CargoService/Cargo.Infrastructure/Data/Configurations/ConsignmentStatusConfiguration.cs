using Cargo.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class ConsignmentStatusConfiguration : IEntityTypeConfiguration<ConsignmentStatus>
    {
        public void Configure(EntityTypeBuilder<ConsignmentStatus> builder)
        {
            builder.ToTable("ConsignmentStatuses");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.AwbId).HasColumnName("AwbId").IsRequired();
            builder.Property(p => p.DateChange).HasColumnName("DateChange").IsRequired();
            builder.Property(p => p.StatusCode).HasColumnName("StatusCode").HasMaxLength(3).IsRequired();
            builder.Property(p => p.AirportCode).HasColumnName("AirportCode").HasMaxLength(3);
            builder.Property(p => p.NumberOfPiece).HasColumnName("NumberOfPiece");
            builder.Property(p => p.Weight).HasColumnName("Weight");
            builder.Property(p => p.VolumeAmount).HasColumnName("VolumeAmount");
            builder.Property(p => p.FlightNumber).HasColumnName("FlightNumber").HasMaxLength(7);
            builder.Property(p => p.FlightDate);
            builder.Property(p => p.Source).HasColumnName("Source").HasMaxLength(4).IsRequired();
            builder.Property(p => p.TitleRu).HasColumnName("TitleRu").HasMaxLength(200);
            builder.Property(p => p.TitleEn).HasColumnName("TitleEn").HasMaxLength(200);

            builder.HasOne(sc => sc.Awb)
                    .WithMany(s => s.Tracking)
                    .HasForeignKey(k => k.AwbId);
        }
    }
}
