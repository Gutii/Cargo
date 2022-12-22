using Cargo.Infrastructure.Data.Model.Dictionary.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class AgentConfigutaion : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.ToTable("Agents");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.CarrierId).HasColumnName("CarrierId").IsRequired();
            builder.Property(p => p.IataCargoAgentNumericCode).HasColumnName("IataCargoAgentNumericCode").HasMaxLength(7);
            builder.Property(p => p.IataCargoAgentCassAddress).HasColumnName("IataCargoAgentCassAddress").HasMaxLength(4);
            builder.Property(p => p.ParticipantIdentifier).HasColumnName("ParticipantIdentifier").HasMaxLength(3);


            builder.Property(p => p.Phone).HasColumnName("Phone").HasMaxLength(25);
            builder.Property(p => p.Fax).HasColumnName("Fax").HasMaxLength(20);
            builder.Property(p => p.Email).HasColumnName("Email").HasMaxLength(150);
            builder.Property(p => p.ImpCode).HasColumnName("ImpCode").HasMaxLength(50);
            builder.Property(p => p.KosudCode).HasColumnName("KosudCode").HasMaxLength(50);

            builder.Property(p => p.Remarks).HasColumnName("Remarks").HasMaxLength(250);

            builder.HasOne(sc => sc.Contragent).WithMany(d => d.SalesAgent).HasForeignKey(r => r.ContragentId);
            builder.HasOne(sc => sc.Carrier).WithMany().HasForeignKey(r => r.CarrierId);
        }
    }
}
