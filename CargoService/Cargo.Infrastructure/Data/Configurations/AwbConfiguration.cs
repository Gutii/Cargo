using Cargo.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class AwbConfiguration : IEntityTypeConfiguration<Awb>
    {
        public void Configure(EntityTypeBuilder<Awb> builder)
        {
            builder.ToTable("Awbs");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.AcPrefix).HasColumnName("AcPrefix").HasMaxLength(3).IsRequired();
            builder.Property(p => p.SerialNumber).HasColumnName("SerialNumber").HasMaxLength(8).IsRequired();
            builder.Property(p => p.PoolAwbId).HasColumnName("PoolAwbId");
            builder.Property(p => p.Origin).HasColumnName("Origin").HasMaxLength(3).IsRequired();
            builder.Property(p => p.Destination).HasColumnName("Destination").HasMaxLength(3).IsRequired();
            builder.Property(p => p.ChargeWeight).HasColumnName("ChargeWeight").IsRequired();

            builder.Property(p => p.QuanDetShipmentDescriptionCode).HasColumnName("QuanDetShipmentDescriptionCode").HasMaxLength(1).IsRequired();
            builder.Property(p => p.NumberOfPieces).HasColumnName("NumberOfPieces").IsRequired();
            builder.Property(p => p.WeightCode).HasColumnName("WeightCode").HasMaxLength(1).IsRequired();
            builder.Property(p => p.Weight).HasColumnName("Weight").IsRequired();
            builder.Property(p => p.VolumeCode).HasColumnName("VolumeCode").HasMaxLength(2).IsRequired();
            builder.Property(p => p.VolumeAmount).HasColumnName("VolumeAmount").IsRequired();
            builder.Property(p => p.ManifestDescriptionOfGoods).HasColumnName("ManifestDescriptionOfGoods").IsRequired();
            builder.Property(p => p.ManifestDescriptionOfGoodsRu).HasColumnName("ManifestDescriptionOfGoodsRu").IsRequired();
            builder.Property(p => p.Product).HasColumnName("Product").HasMaxLength(16);
            builder.Property(p => p.Status).HasColumnName("Status").HasMaxLength(15);

            builder.Property(p => p.SpecialHandlingRequirements).HasColumnName("SpecialHandlingRequirements").HasMaxLength(36);// ((3+1)*9 = 36)
            builder.Property(p => p.SpecialServiceRequest).HasColumnName("SpecialServiceRequest").HasMaxLength(65);
            builder.Property(p => p.SpecialServiceRequestRu).HasColumnName("SpecialServiceRequestRu").HasMaxLength(65);
            builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(p => p.PlaceOfIssue).HasColumnName("PlaceOfIssue").HasMaxLength(60);
            builder.Property(p => p.NCV).HasColumnName("Ncv").HasMaxLength(3);
            builder.Property(p => p.NDV).HasColumnName("Ndv").HasMaxLength(3);
            builder.Property(p => p.Currency).HasColumnName("Currency").HasMaxLength(3);

            builder.Property(p => p.ConsigneeId).HasColumnName("ConsigneeId");
            builder.Property(p => p.ConsignorId).HasColumnName("ConsignorId");
            builder.Property(p => p.AgentId).HasColumnName("AgentId");
            builder.Property(p => p.CarrierId).HasColumnName("CarrierId");

            builder.Property(p => p.TariffsSolutionCode).HasColumnName("TariffsSolutionCode").HasMaxLength(16);
            builder.Property(p => p.SalesChannel).HasColumnName("SalesChannel").HasMaxLength(16);
            builder.Property(p => p.PaymentProcedure).HasColumnName("PaymentProcedure").HasMaxLength(16);
            builder.Property(p => p.WeightCharge).HasColumnName("WeightCharge").HasMaxLength(16);
            builder.Property(p => p.AllIn).HasColumnName("AllIn").IsRequired();
            builder.Property(p => p.TariffClass).HasColumnName("TariffClass").HasMaxLength(16);
            builder.Property(p => p.AddOn).HasColumnName("AddOn").HasMaxLength(16);
            builder.Property(p => p.BaseTariffRate).HasColumnName("BaseTariffRate").IsRequired();
            builder.Property(p => p.TariffRate).HasColumnName("TariffRate").IsRequired();
            builder.Property(p => p.Total).HasColumnName("Total").IsRequired();
            builder.Property(p => p.PrepaidId).HasColumnName("PrepaidId");
            builder.Property(p => p.CollectId).HasColumnName("CollectId");

            builder.HasOne(sc => sc.Consignee)
                .WithMany().HasForeignKey(f => f.ConsigneeId).IsRequired(false);

            builder.HasOne(sc => sc.Consignor)
                .WithMany().HasForeignKey(f => f.ConsignorId).IsRequired(false);

            builder.HasOne(sc => sc.Collect)
                .WithMany().HasForeignKey(f => f.CollectId).IsRequired(false);

            builder.HasOne(sc => sc.Prepaid)
                .WithMany().HasForeignKey(f => f.PrepaidId).IsRequired(false);

            builder.HasOne(sc => sc.Agent)
                .WithMany().HasForeignKey(f => f.AgentId).IsRequired(false);

            builder.HasOne(sc => sc.Carrier)
                .WithMany().HasForeignKey(f => f.CarrierId).IsRequired(false);

            builder.HasOne(t => t.PoolAwb)
                .WithMany()
                .HasForeignKey(k => k.PoolAwbId).IsRequired(false);
        }
    }
}