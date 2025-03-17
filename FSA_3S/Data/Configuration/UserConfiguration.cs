using FSA_3S.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSA_3S.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.PhoneNumber)
                   .HasMaxLength(13);

            builder.Property(u => u.Gender)
                   .HasMaxLength(10);

            builder.Property(u => u.BirthDate);

            builder.Property(u => u.CCCD)
                   .HasMaxLength(13);

            builder.Property(u => u.TimeOfWork)
                   .HasMaxLength(50);

            builder.Property(u => u.TypeOfStaff)
                   .HasMaxLength(50);
        }
    }
}