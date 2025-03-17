using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Contracts;
using FSA_3S.Models.Entities;

namespace _3SLand.Data.Configuration
{
    public class ContractConfiguration : IEntityTypeConfiguration<ContractEntity>
    {
        public void Configure(EntityTypeBuilder<ContractEntity> builder)
        {
            builder.ToTable("contract");

            builder.HasKey(e => e.ContractId);

            builder.Property(e => e.ContractType)
                .HasColumnName("contracttype")
                .HasMaxLength(100);

            builder.Property(e => e.ContractStatus)
                .HasColumnName("contractstatus")
                .HasMaxLength(50);

            builder.HasOne(e => e.RealEstate)
                .WithMany(e => e.Contracts)
                .HasForeignKey(e => e.RealEstateId);

            builder.HasOne(e => e.Customer)
                .WithMany(c => c.Contracts)
                .HasForeignKey(e => e.CustomerId);
        }
    }
}