using Cargo.Infrastructure.Data.Model.Tariffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Rates
{
    class TariffSolutionConfiguration : IEntityTypeConfiguration<TariffSolution>
    {
        public void Configure(EntityTypeBuilder<TariffSolution> builder)
        {
            builder.ToTable("TariffSolutions");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Status).HasColumnName("Status");
            builder.Property(p => p.ValidationDate).HasColumnName("ValidationDate");
            builder.Property(p => p.Code).HasColumnName("Code");
            builder.Property(p => p.CoverageArea).HasColumnName("CoverageArea");
            builder.Property(p => p.Currency).HasColumnName("Currency");
            builder.Property(p => p.StartDate).HasColumnName("StartDate");
            builder.Property(p => p.EndDate).HasColumnName("EndDate");
            builder.Property(p => p.IsSpecial).HasColumnName("IsSpecial");
            builder.Property(p => p.SalesChannel).HasColumnName("SalesChannel");
            builder.Property(p => p.IataAgentCode).HasColumnName("IataAgentCode");
            builder.Property(p => p.ClientNumber).HasColumnName("ClientNumber");
            builder.Property(p => p.ClientName).HasColumnName("ClientName");
            builder.Property(p => p.Product).HasColumnName("Product");
            builder.Property(p => p.PeriodType).HasColumnName("PeriodType");

            builder.Property(p => p.Flights).HasColumnName("Flights");
            builder.Property(p => p.Routes).HasColumnName("Routes");
            builder.Property(p => p.WeekDays).HasColumnName("WeekDays");
            builder.Property(p => p.PaymentTerms).HasColumnName("PaymentTerms");
            builder.Property(p => p.WeightCharge).HasColumnName("WeightCharge");
            builder.Property(p => p.IsAllIn).HasColumnName("IsAllIn");
            builder.Property(p => p.TariffType).HasColumnName("TariffType");
            builder.Property(p => p.MinTariff).HasColumnName("MinTariff");

            builder.HasOne(c => c.AwbOriginAirport)
                .WithMany(s => s.TariffSolutionsOrigins)
                .HasForeignKey("AwbOriginAirportId");

            builder.HasOne(c => c.AwbDestinationAirport)
                .WithMany(s => s.TariffSolutionsDestinations)
                .HasForeignKey("AwbDestinationAirportId");

            builder.HasOne(c => c.AwbOriginTariffGroup)
                .WithMany(s => s.TariffSolutionsOrigins)
                .HasForeignKey("AwbOriginTariffGroupId");

            builder.HasOne(c => c.AwbDestinationTariffGroup)
                .WithMany(s => s.TariffSolutionsDesinations)
                .HasForeignKey("AwbDestinationTariffGroupId");

            builder.HasOne(c => c.TransitAirport)
                .WithMany(s => s.TariffSolutionsTransits)
                .HasForeignKey("TransitAirportId");

            builder.HasOne(c => c.RateGrid)
                .WithMany(s => s.TariffSolutions)
                .HasForeignKey("RateGridHeaderId");
        }
    }
}