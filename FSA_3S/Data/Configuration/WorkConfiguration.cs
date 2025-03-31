using FSA_3S.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSA_3S.Data.Configuration
{
    public class WorkConfiguration : IEntityTypeConfiguration<WorkEntity>
    {
        public void Configure(EntityTypeBuilder<WorkEntity> builder)
        {
            builder.ToTable("work");

            builder.HasKey(w => w.WorkId);

            builder.Property(w => w.WorkId)
                   .HasColumnName("work_id");

            builder.Property(w => w.UserId)
                   .IsRequired()
                   .HasColumnName("userId");

            builder.Property(w => w.TimeOfWork)
                   .IsRequired()
                   .HasColumnName("time_of_work");

            builder.Property(w => w.DesWork)
                   .HasColumnName("des_work")
                   .HasMaxLength(500);

            builder.HasOne(w => w.User)
                   .WithMany(u => u.Works)
                   .HasForeignKey(w => w.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}