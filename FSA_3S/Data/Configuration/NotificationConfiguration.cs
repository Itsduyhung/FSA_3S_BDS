using FSA_3S.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSA_3S.Data.Configuration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<NotificationEntity>
    {
        public void Configure(EntityTypeBuilder<NotificationEntity> builder)
        {
            builder.ToTable("notification");

            builder.HasKey(n => n.NotificationId);

            builder.Property(n => n.NotificationType)
                   .HasMaxLength(50);

            builder.Property(n => n.RelatedId)
                   .HasMaxLength(50);
        }
    }
}