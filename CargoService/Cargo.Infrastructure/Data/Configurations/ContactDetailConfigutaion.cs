using Cargo.Infrastructure.Data.Model.Dictionary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class ContactDetailConfigutaion : IEntityTypeConfiguration<ContactDetail>
    {
        public void Configure(EntityTypeBuilder<ContactDetail> builder)
        {
            builder.ToTable("ContactDetails");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.ContactIdentifier).HasColumnName("ContactIdentifier").HasMaxLength(3);
            builder.Property(p => p.ContactNumber).HasColumnName("ContactNumber").HasMaxLength(25);

            builder.HasOne(sc => sc.Contragent)
                .WithMany(s => s.ContactDetails)
                .HasForeignKey(k => k.ContragentId);
        }
    }
}
