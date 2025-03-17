using FSA_3S.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSA_3S.Data.Configuration
{
    public class MappingRealEstateCustomerConfiguration : IEntityTypeConfiguration<MappingRealEstateCustomerEntity>
    {
        public void Configure(EntityTypeBuilder<MappingRealEstateCustomerEntity> builder)
        {
            builder.ToTable("mappingrealestatecustomer");

            builder.HasKey(m => m.MappingRealEstateCustomerId);

            builder.Property(m => m.Likes)
                   .HasMaxLength(5);

            builder.Property(m => m.Comment);
        }
    }
}