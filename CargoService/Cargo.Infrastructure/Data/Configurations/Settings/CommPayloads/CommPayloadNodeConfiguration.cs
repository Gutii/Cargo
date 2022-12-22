using Cargo.Infrastructure.Data.Model.Settings.CommPayloads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class CommPayloadNodeConfiguration : IEntityTypeConfiguration<CommPayloadNode>
    {
        public void Configure(EntityTypeBuilder<CommPayloadNode> builder)
        {
            builder.ToTable("CommPayloadNodes");

            builder.HasKey(a => a.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.ActionToParent).HasMaxLength(15);

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Childs)
                .HasForeignKey(x => x.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.CommercialPayload)
                .WithMany()
                .HasForeignKey(pn => pn.CommPayloadId);
        }
    }
}
