using FSA_3S.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSA_3S.Data.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<PaymentEntity>
    {
        public void Configure(EntityTypeBuilder<PaymentEntity> builder)
        {
            builder.ToTable("payment");

            builder.HasKey(p => p.PaymentId);

            builder.Property(p => p.Amount)
                   .IsRequired();

            builder.Property(p => p.PaymentStatus)
                   .HasMaxLength(20);
        }
    }
}