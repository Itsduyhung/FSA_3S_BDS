using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FSA_3S.Models.Entities;

namespace _3SLand.Data.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<AppointmentEntity>
    {
        public void Configure(EntityTypeBuilder<AppointmentEntity> builder)
        {
            builder.ToTable("appointment");

            builder.HasKey(a => a.AppointmentId);

            builder.Property(a => a.Title)
                   .HasMaxLength(50);

            builder.Property(a => a.Status)
                   .HasMaxLength(10);

            builder.Property(a => a.Address)
                   .HasMaxLength(255);
        }
    }
}