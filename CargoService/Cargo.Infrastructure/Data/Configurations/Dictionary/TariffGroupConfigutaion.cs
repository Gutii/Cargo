using Cargo.Infrastructure.Data.Model.Tariffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Dictionary
{
    class TariffGroupConfigutaion : IEntityTypeConfiguration<TariffGroup>
    {
        public void Configure(EntityTypeBuilder<TariffGroup> builder)
        {
            builder.ToTable("TariffGroups");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Code).HasColumnName("Code").IsRequired();
            builder.Property(p => p.DescriptionEng).HasColumnName("DescriptionEng");
            builder.Property(p => p.DescriptionRus).HasColumnName("DescriptionRus");


            builder.HasMany(c => c.Airports)
                .WithMany(s => s.TariffGroups)
            .UsingEntity(j => j.ToTable("TariffGroupAirport"));

        }
    }
}