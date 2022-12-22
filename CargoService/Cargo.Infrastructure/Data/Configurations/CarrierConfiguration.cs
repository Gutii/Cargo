using Cargo.Infrastructure.Data.Model.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Settings
{
    class CarrierConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Carriers");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.IataCode).HasColumnName("IataCode").HasMaxLength(2).IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(150).IsRequired();
            builder.Property(p => p.Email).HasColumnName("Email").HasMaxLength(100).IsRequired();
            builder.Property(p => p.AcPrefix).HasColumnName("AcPrefix").HasMaxLength(3).IsRequired();
            builder.Property(p => p.AcMailPrefix).HasColumnName("AcMailPrefix").HasMaxLength(3).IsRequired();
        }
    }
}
