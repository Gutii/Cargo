using Cargo.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class MessageConfigutaion : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id);
            builder.Property(p => p.MessageIdentifier).HasColumnName("MessageIdentifier").HasMaxLength(3).IsRequired();
            builder.Property(p => p.VersionNumber).HasColumnName("VersionNumber");
            builder.Property(p => p.Direction).HasColumnName("Direction").HasMaxLength(3).IsRequired();
            builder.Property(p => p.DateCreated).HasColumnName("DateCreated").IsRequired();
            builder.Property(p => p.LinkedNtId).HasColumnName("LinkedNtId");
            builder.Property(p => p.From).HasColumnName("From").HasMaxLength(250).IsRequired();
            builder.Property(p => p.To).HasColumnName("To").HasMaxLength(250).IsRequired();

            builder.Property(p => p.Carrier).HasColumnName("Carrier").HasMaxLength(2).IsRequired();
            builder.Property(p => p.LinkedAwbId).HasColumnName("LinkedAwbId");
            builder.Property(p => p.LinkedFlightId).HasColumnName("LinkedFlightId");

            builder.HasOne(sc => sc.LinkedAwb)
                .WithMany(s => s.Messages)
                .HasForeignKey(k => k.LinkedAwbId)
                .IsRequired(false);

            builder.HasOne(sc => sc.LinkedFlight)
                .WithMany(s => s.Messages)
                .HasForeignKey(k => k.LinkedFlightId)
                .IsRequired(false);

        }
    }
}
