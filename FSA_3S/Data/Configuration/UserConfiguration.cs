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
                   .HasMaxLength(50)
                   .HasColumnName("email");

            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnName("password");

            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnName("full_name");

            builder.Property(u => u.PhoneNumber)
                   .HasMaxLength(13)
                   .HasColumnName("phone_number");

            builder.Property(u => u.Gender)
                   .HasMaxLength(10)
                   .HasColumnName("gender");

            builder.Property(u => u.BirthDate)
                   .HasColumnName("birth_date");

            builder.Property(u => u.CCCD)
                   .HasMaxLength(13)
                   .HasColumnName("cccd");

            builder.Property(u => u.TimeOfWork)
                   .HasMaxLength(50)
                   .HasColumnName("time_of_work");

            builder.Property(u => u.TypeOfStaff)
                   .HasMaxLength(50)
                   .HasColumnName("type_of_staff");
        }
    }
}