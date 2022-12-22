using Cargo.Infrastructure.Data.Model.Dictionary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations;

class AircraftSizeConfiguration : IEntityTypeConfiguration<AircraftSize>
{
    public void Configure(EntityTypeBuilder<AircraftSize> builder)
    {
        builder.ToTable("AircraftSizes");

        builder.HasKey(a => a.Id);
    }
}