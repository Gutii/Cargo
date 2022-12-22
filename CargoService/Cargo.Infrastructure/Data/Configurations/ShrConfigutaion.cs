using Cargo.Infrastructure.Data.Model.Dictionary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    class ShrConfigutaion : IEntityTypeConfiguration<Shr>
    {
        public void Configure(EntityTypeBuilder<Shr> builder)
        {
            //SHR_CODE	SHR_CODE_NAME_EN	SHR_CODE_NAME_RU	SHR Group	FORB_SHR_GROUP_IATA	REC_SHR
            builder.ToTable("Shrs");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Code).HasColumnName("ShrCode").HasMaxLength(3).IsRequired();
            builder.Property(p => p.InternationalDescr).HasColumnName("NameEn").HasMaxLength(250).IsRequired();
            builder.Property(p => p.RussianDescr).HasColumnName("NameRu").HasMaxLength(200);
            builder.Property(p => p.ShrGroup).HasColumnName("ShrGroup").HasMaxLength(10);
            builder.Property(p => p.ForbShrGroupIata).HasColumnName("ForbShrGroupIata").HasMaxLength(50);
            builder.Property(p => p.RecomendedShr).HasColumnName("RecShr").HasMaxLength(50);
        }
    }
}
