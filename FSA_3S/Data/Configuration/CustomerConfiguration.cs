using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FSA_3S.Models.Entities;

namespace FSA_3S.Data.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.ToTable("customer");

            builder.HasKey(c => c.CustomerId);

            builder.Property(c => c.FullName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(c => c.PhoneNumber)
                   .HasMaxLength(13);

            builder.Property(c => c.Address)
                   .HasMaxLength(250);

            builder.Property(c => c.CustomerType)
                   .HasMaxLength(10);

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("CURRENT_DATE");
            builder.HasMany(c => c.RealEstates)
       .WithOne(r => r.Customer)
       .HasForeignKey(r => r.Seller)
       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}