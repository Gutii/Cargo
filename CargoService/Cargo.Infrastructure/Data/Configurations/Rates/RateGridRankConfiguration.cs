using Cargo.Infrastructure.Data.Model.Tariffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Rates
{
    internal class RateGridRankConfiguration : IEntityTypeConfiguration<RateGridRank>
    {
        public void Configure(EntityTypeBuilder<RateGridRank> bld)
        {
            bld.ToTable("RateGridRanks").HasComment("Contains rank specifications of all rate grids.");


            bld.Property(p => p.Rank).HasComment("Grid's rank. Must be greater than or equal to zero.");
            bld.Property(p => p.GridId).HasColumnName("GrdId").HasComment("Reference to grid's header.");

            bld.HasOne(rnk => rnk.Grid).
                WithMany(hdr => hdr.Ranks).
                HasForeignKey(rnk => rnk.GridId).
                HasConstraintName("FK_RateGridRanks1").
                OnDelete(DeleteBehavior.Cascade);

            bld.HasKey(rnk => new { rnk.GridId, rnk.Rank }).HasName("PK_RateGridRanks");
        }
    }
}
