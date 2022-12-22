using Cargo.Infrastructure.Data.Model.Tariffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Rates
{
    class TransportProductConfiguration : IEntityTypeConfiguration<TransportProduct>
    {
        public void Configure(EntityTypeBuilder<TransportProduct> builder)
        {
            builder.ToTable("TransportProducts");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Code).HasColumnName("Code").IsRequired();
            builder.Property(p => p.Description).HasColumnName("Description");
            builder.Property(p => p.Trigger).HasColumnName("Trigger");
        }
    }
}