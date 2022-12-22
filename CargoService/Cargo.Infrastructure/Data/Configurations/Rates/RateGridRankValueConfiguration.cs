using Cargo.Infrastructure.Data.Model.Tariffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Rates
{
    internal class RateGridRankValueConfiguration : IEntityTypeConfiguration<RateGridRankValue>
    {
        public void Configure(EntityTypeBuilder<RateGridRankValue> builder)
        {
            builder.ToTable("RateGridRankValues");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Rank).HasColumnName("Rank");
            builder.Property(p => p.Value).HasColumnName("Value");

            builder.HasOne(x => x.TariffSolution)
                .WithMany(x => x.RateGridRankValues)
                .HasForeignKey(x => x.TariffSolutionId)
                                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}