using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FSA_3S.Models.Entities;

namespace FSA_3S.Data.Configuration
{
    public class MappingUserNotificationConfiguration : IEntityTypeConfiguration<MappingUserNotificationEntity>
    {
        public void Configure(EntityTypeBuilder<MappingUserNotificationEntity> builder)
        {
            builder.ToTable("mappingusernotification");

            builder.HasKey(e => e.MappingUserNotificationId);

            builder.Property(e => e.MappingUserNotificationId)
                   .HasColumnName("mappingUserNotificationId")
                   .IsRequired();

            builder.HasOne(e => e.User)
                   .WithMany()
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Notification)
                   .WithMany()
                   .HasForeignKey(e => e.NotificationId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}