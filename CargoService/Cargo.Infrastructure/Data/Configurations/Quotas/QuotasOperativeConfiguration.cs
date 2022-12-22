using Cargo.Infrastructure.Data.Model.Quotas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Quotas
{
    public class QuotasOperativeConfiguration : IEntityTypeConfiguration<QuotasOperative>
    {
        public void Configure(EntityTypeBuilder<QuotasOperative> builder)
        {
            builder.ToTable("QuotasOperative");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.CarrierId).HasColumnName("CarrierId").IsRequired();
            builder.Property(p => p.AwbPrefix).HasColumnName("AwbPrefix").HasMaxLength(3);
            builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(25).IsRequired();
            builder.Property(p => p.StartDate).HasColumnName("StartDate").IsRequired();
            builder.Property(p => p.FinishDate).HasColumnName("FinishDate").IsRequired();
            builder.Property(p => p.Flight).HasColumnName("Flight").HasMaxLength(25).IsRequired();
            builder.Property(p => p.FlightLegFrom).HasColumnName("FlightLegFrom").HasMaxLength(3);
            builder.Property(p => p.FlightLegTo).HasColumnName("FlightLegTo").HasMaxLength(3);
            builder.Property(p => p.WeekDay).HasColumnName("WeekDay").HasMaxLength(15).IsRequired();
            builder.Property(p => p.AwbOrigin).HasColumnName("AwbOrigin").HasMaxLength(3).IsRequired();
            builder.Property(p => p.AwbDest).HasColumnName("AwbDest").HasMaxLength(3).IsRequired();
            builder.Property(p => p.SalesProduct).HasColumnName("SalesProduct").HasMaxLength(100).IsRequired();
            builder.Property(p => p.Shc).HasColumnName("Shc").HasMaxLength(255);
            builder.Property(p => p.IsHardAllotment).HasColumnName("IsHardAllotment");
            builder.Property(p => p.WeightLimit).HasColumnName("WeightLimit").IsRequired();
            builder.Property(p => p.VolumeLimit).HasColumnName("VolumeLimit").IsRequired();
            builder.Property(p => p.Currency).HasColumnName("Currency").HasMaxLength(3).IsRequired();
        }
    }
}
