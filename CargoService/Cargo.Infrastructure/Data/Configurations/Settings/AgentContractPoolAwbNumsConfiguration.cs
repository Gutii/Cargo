using Cargo.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.Settings
{
    class AgentContractPoolAwbNumsConfiguration : IEntityTypeConfiguration<AgentContractPoolAwbNums>
    {
        public void Configure(EntityTypeBuilder<AgentContractPoolAwbNums> builder)
        {
            builder.ToTable("AgentsContractPoolAwbNums");

            builder.HasKey(a => new { a.AwbPoolId, a.SerialNumber });

            builder.HasOne(an => an.AwbPool)
                .WithMany(p => p.UsedAwbNumbers)
                .HasForeignKey(an => an.AwbPoolId);
        }
    }
}