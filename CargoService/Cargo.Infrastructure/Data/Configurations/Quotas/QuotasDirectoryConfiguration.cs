using Cargo.Infrastructure.Data.Model.Quotas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Quotas
{
    public class QuotasDirectoryConfiguration : IEntityTypeConfiguration<QuotasDirectory>
    {
        public void Configure(EntityTypeBuilder<QuotasDirectory> builder)
        {
            builder.ToTable("QuotasDirectory");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.CarrierId).HasColumnName("CarrierId").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(25).IsRequired();

            builder.HasOne(sc => sc.Agent).WithMany(c => c.QuotasDirectory).HasForeignKey(r => r.AgentId);
        }
    }
}
