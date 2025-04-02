using FSA_3S.Enum;
using FSA_3S.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace FSA_3S.Models
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("Db");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình cho UserEntity
            modelBuilder.Entity<UserEntity>()
                .Property(u => u.Status)
                .HasConversion(new EnumToStringConverter<UserStatusEnum>());

            // Cấu hình cho ContractEntity
            modelBuilder.Entity<ContractEntity>()
                .Property(u => u.ContractStatus)
                .HasConversion(new EnumToStringConverter<ContractStatusEnum>());

            modelBuilder.Entity<ContractEntity>()
                .Property(u => u.ContractType)
                .HasConversion(new EnumToStringConverter<ContractTypeEnum>());

            // Cấu hình cho RealEstateEntity
            modelBuilder.Entity<RealEstateEntity>()
                .Property(u => u.RealEstateStatus)
                .HasConversion(new EnumToStringConverter<RealEstateStatusEnum>());

            modelBuilder.Entity<RealEstateEntity>()
                .Property(u => u.RealEstateType)
                .HasConversion(new EnumToStringConverter<RealEstateTypeEnum>());

            // Cấu hình cho AppointmentEntity
            modelBuilder.Entity<AppointmentEntity>()
                .HasMany(a => a.MappingUserAppointments)
                .WithOne(m => m.Appointment)
                .HasForeignKey(m => m.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade); // Hành vi xóa đệ quy

            modelBuilder.Entity<AppointmentEntity>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); // Hành vi xóa đệ quy

            // Cấu hình cho UserEntity
            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.MappingUserAppointments)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xung đột bằng cách sử dụng Restrict

            // Cấu hình cho MappingUserAppointmentEntity
            modelBuilder.Entity<MappingUserAppointmentEntity>()
                .HasKey(m => m.MappingUserAppointmentId);

            modelBuilder.Entity<MappingUserAppointmentEntity>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xung đột bằng cách sử dụng Restrict

            modelBuilder.Entity<MappingUserAppointmentEntity>()
                .HasOne(m => m.Creator)
                .WithMany()
                .HasForeignKey(m => m.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xung đột bằng cách sử dụng Restrict

            modelBuilder.Entity<MappingUserAppointmentEntity>()
                .HasOne(m => m.Updater)
                .WithMany()
                .HasForeignKey(m => m.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xung đột bằng cách sử dụng Restrict

            // Cấu hình cho NotificationEntity
            modelBuilder.Entity<NotificationEntity>()
                .ToTable("Notifications"); // Định nghĩa tên bảng trong DB (nếu cần thiết)

            modelBuilder.Entity<NotificationEntity>()
                .HasKey(n => n.NotificationId); // Đặt khóa chính

            modelBuilder.Entity<NotificationEntity>()
                .Property(n => n.NotificationId)
                .HasColumnName("IdNotification"); // Định nghĩa tên cột cho khóa chính

            modelBuilder.Entity<NotificationEntity>()
                .Property(n => n.Title)
                .IsRequired()  // Cột này là bắt buộc
                .HasMaxLength(255); // Giới hạn độ dài tối đa cho cột Title

            modelBuilder.Entity<NotificationEntity>()
                .Property(n => n.Message)
                .IsRequired(); // Cột Message là bắt buộc

            modelBuilder.Entity<NotificationEntity>()
                .Property(n => n.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()"); // Đặt giá trị mặc định cho CreatedAt là thời gian UTC hiện tại

            modelBuilder.Entity<NotificationEntity>()
                .Property(n => n.IsRead)
                .HasDefaultValue(false); // Đặt giá trị mặc định cho IsRead là false

            modelBuilder.Entity<MappingUserNotificationEntity>()
           .HasOne(m => m.Notification)  // Quan hệ với bảng Notification
            .WithMany()  // Một Notification có thể có nhiều MappingUserNotification
           .HasForeignKey(m => m.NotificationId)
           .OnDelete(DeleteBehavior.Cascade); // Cascade khi xóa Notification

            // Cấu hình quan hệ với bảng User (FK_User_UserId)
            modelBuilder.Entity<MappingUserNotificationEntity>()
                .HasOne(m => m.User)  // Quan hệ với bảng User
                .WithMany()  // Một User có thể có nhiều MappingUserNotification
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ReportEntity> Reports { get; set; }
        public DbSet<ContractEntity> Contracts { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<RealEstateEntity> RealEstates { get; set; }
        public DbSet<AppointmentEntity> Appointments { get; set; }
        public DbSet<MappingUserAppointmentEntity> MappingUserAppointments { get; set; }
        public DbSet<MappingContractCustomerEntity> MappingContractCustomers { get; set; }
        public DbSet<MappingContractClauseEntity> MappingContractClauseEntities { get; set; }
        public DbSet<AuditEntity> Audits { get; set; }
        public DbSet<WorkEntity> WorkEntity { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }
        public DbSet<MappingUserNotificationEntity> MappingUserNotification { get; set; }
    }
}