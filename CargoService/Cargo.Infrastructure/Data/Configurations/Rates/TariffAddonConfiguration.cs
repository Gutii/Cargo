using Cargo.Infrastructure.Data.Model.Tariffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Rates
{
    class TariffAddonConfiguration : IEntityTypeConfiguration<TariffAddon>
    {
        public void Configure(EntityTypeBuilder<TariffAddon> builder)
        {
            builder.ToTable("TariffAddons");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.MinimumAddon).HasColumnName("MinimumAddon");
            builder.Property(p => p.WeightAddon).HasColumnName("WeightAddon");

            builder.HasOne(x => x.TariffSolution)
                .WithMany(x => x.Addons)
                .HasForeignKey("TariffSolutionId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}