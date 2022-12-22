using Cargo.Infrastructure.Data.Model.MessageSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations.MessageSettings
{
    public class MessageIdentifierConfiguration : IEntityTypeConfiguration<MessageIdentifier>
    {
        public void Configure(EntityTypeBuilder<MessageIdentifier> builder)
        {
            builder.ToTable("MessageIdentifiers");

            builder.HasKey(a => a.id);
            builder.Property(p => p.id).ValueGeneratedOnAdd();
            builder.Property(p => p.Identifier).HasColumnName("Identifier").HasMaxLength(7).IsRequired();
            builder.Property(p => p.ch).HasColumnName("ch").HasComment("Direction: CarrierHandler; Value: 0 - Не актуально; 1 - Отправляющий; 2 - Принимающий; 3 - В обе стороны;");
            builder.Property(p => p.cc).HasColumnName("cc").HasComment("Direction: CarrierCarrier;");
            builder.Property(p => p.hh).HasColumnName("hh").HasComment("Direction: HandlerHandler;");

        }
    }
}
