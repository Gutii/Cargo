using Cargo.Infrastructure.Data.Model.MessageSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.MessagesSettings
{
    public class MessagePropertyConfiguration : IEntityTypeConfiguration<MessageProperty>
    {
        public void Configure(EntityTypeBuilder<MessageProperty> builder)
        {
            builder.ToTable("MessageProperties");

            builder.HasKey(a => a.id);

            builder.Property(p => p.id).ValueGeneratedOnAdd();
            builder.Property(p => p.MessageIdentifierId).HasColumnName("MessageIdentifierId").IsRequired();
            builder.Property(p => p.CustomerId).HasColumnName("CustomerId").IsRequired();
            builder.Property(p => p.Key).HasColumnName("Key").HasMaxLength(15);
            builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(255);
            builder.Property(p => p.Type).HasColumnName("Type").HasMaxLength(255);
            builder.Property(p => p.Value).HasColumnName("Value").HasMaxLength(255);
            builder.Property(p => p.DescriptionRu).HasColumnName("DescriptionRu").HasMaxLength(255);
            builder.Property(p => p.DescriptionEn).HasColumnName("DescriptionEn").HasMaxLength(255);

            builder.HasOne(sc => sc.MessageIdentifier)
                .WithMany(s => s.MessageProperties)
                .HasForeignKey(k => k.MessageIdentifierId);
        }
    }
}
