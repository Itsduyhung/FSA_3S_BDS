using FSA_3S.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _3SLand.Data.Configuration
{
    public class ClauseConfiguration : IEntityTypeConfiguration<ClauseEntity>
    {
        public void Configure(EntityTypeBuilder<ClauseEntity> builder)
        {
            builder.ToTable("clause");

            builder.HasKey(c => c.ClauseId);

            builder.Property(c => c.ClauseNumber)
                   .IsRequired();

            builder.Property(c => c.ClauseContent);
            builder.Property(c => c.ClauseType);
        }
    }
}