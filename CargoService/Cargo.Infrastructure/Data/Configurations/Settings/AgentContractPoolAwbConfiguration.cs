using Cargo.Infrastructure.Data.Model.Settings.PoolAwbs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Settings
{
    class AgentContractPoolAwbConfiguration : IEntityTypeConfiguration<AgentContractPoolAwb>
    {
        public void Configure(EntityTypeBuilder<AgentContractPoolAwb> builder)
        {
            builder.ToTable("AgentsContractPoolAwbs");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.StartNumber).HasColumnName("StartNumber").HasMaxLength(7).IsRequired();
            builder.Property(p => p.PoolLenght).HasColumnName("PoolLenght").IsRequired();
            builder.Property(p => p.Status).HasColumnName("Status").HasMaxLength(10);

            builder.HasOne(an => an.Contract)
                .WithMany(p => p.PoolAwbs)
                .HasForeignKey(an => an.ContractId);
        }
    }
}
