using Cargo.Infrastructure.Data.Model.Dictionary.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class ContactInformationConfigutaion : IEntityTypeConfiguration<ContactInformation>
    {
        public void Configure(EntityTypeBuilder<ContactInformation> builder)
        {
            builder.ToTable("ContactInformations");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.FullName).HasColumnName("FullName").HasMaxLength(150).IsRequired();
            builder.Property(p => p.Position).HasColumnName("Position").HasMaxLength(30);
            builder.Property(p => p.Phone1).HasColumnName("Phone1").HasMaxLength(20);
            builder.Property(p => p.Phone2).HasColumnName("Phone2").HasMaxLength(20);

            builder.Property(p => p.Email1).HasColumnName("Email1").HasMaxLength(50);
            builder.Property(p => p.Email2).HasColumnName("Email2").HasMaxLength(50);


            builder.HasOne(sc => sc.Agent)
            .WithMany(s => s.AdditionalContactInfo)
            .HasForeignKey(k => k.AgentId);
        }
    }
}
