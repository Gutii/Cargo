using Cargo.Infrastructure.Data.Model.Settings.PoolAwbs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Settings
{
    class AgentContractConfiguration : IEntityTypeConfiguration<AgentContract>
    {
        public void Configure(EntityTypeBuilder<AgentContract> builder)
        {
            builder.ToTable("AgentsContracts");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.SaleAgentId).HasColumnName("SaleAgentId").IsRequired();
            builder.Property(p => p.SalesChannel).HasColumnName("SalesChannel").HasMaxLength(5).IsRequired();
            builder.Property(p => p.DateAt).HasColumnName("DateAt").IsRequired();
            builder.Property(p => p.DateTo).HasColumnName("DateTo");
            builder.Property(p => p.Status).HasColumnName("Status").HasMaxLength(10);

            builder.HasOne(an => an.SaleAgent)
                .WithMany(p => p.AgentContracts)
                .HasForeignKey(an => an.SaleAgentId);
        }
    }
}
