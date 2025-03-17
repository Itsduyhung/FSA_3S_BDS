using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FSA_3S.Models.Entities;

namespace FSA_3S.Data.Configuration
{
    public class MappingUserAppointmentConfiguration : IEntityTypeConfiguration<MappingUserAppointmentEntity>
    {
        public void Configure(EntityTypeBuilder<MappingUserAppointmentEntity> builder)
        {
            builder.ToTable("mappinguserappointment");

            builder.HasKey(e => e.MappingUserAppointmentId);

            builder.Property(e => e.MappingUserAppointmentId)
                   .HasColumnName("mappingUserAppointmentId")
                   .IsRequired();

            builder.HasOne(e => e.User)
                   .WithMany()
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Appointment)
                   .WithMany()
                   .HasForeignKey(e => e.AppointmentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}