using FSA_3S.Enum;
using FSA_3S.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace FSA_3S.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : DbContext(options)
    {
        private readonly IConfiguration _configuration = configuration;

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

            // Convert enum to string
            modelBuilder.Entity<UserEntity>()
                .Property(u => u.Status)
                .HasConversion(new EnumToStringConverter<UserStatusEnum>());

            modelBuilder.Entity<ContractEntity>()
                .Property(c => c.ContractStatus)
                .HasConversion(new EnumToStringConverter<ContractStatusEnum>());

            modelBuilder.Entity<ContractEntity>()
    .Property(c => c.StatusPayment)
    .HasConversion(new EnumToStringConverter<StatusPaymentEnum>());

            modelBuilder.Entity<ContractEntity>()
                .Property(c => c.ContractType)
                .HasConversion(new EnumToStringConverter<ContractTypeEnum>());

            modelBuilder.Entity<RealEstateEntity>()
                .Property(r => r.RealEstateStatus)
                .HasConversion(new EnumToStringConverter<RealEstateStatusEnum>());

            modelBuilder.Entity<RealEstateEntity>()
                .Property(r => r.RealEstateType)
                .HasConversion(new EnumToStringConverter<RealEstateTypeEnum>());

            // 👉 Convert ClauseTypeEnum to string
            modelBuilder.Entity<ClauseEntity>()
                .Property(c => c.ClauseType)
                .HasConversion(new EnumToStringConverter<ClauseTypeEnum>());

            // 👉 Setup many-to-many relationship using MappingContractClauseEntity
            modelBuilder.Entity<MappingContractClauseEntity>()
                .HasKey(mc => new { mc.ContractId, mc.ClauseId });

            modelBuilder.Entity<MappingContractClauseEntity>()
                .HasOne(mc => mc.Contract)
                .WithMany(c => c.ContractClauses)
                .HasForeignKey(mc => mc.ContractId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MappingContractClauseEntity>()
                .HasOne(mc => mc.Clause)
                .WithMany(c => c.ContractClauses)
                .HasForeignKey(mc => mc.ClauseId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ReportEntity> Reports { get; set; }
        public DbSet<ContractEntity> Contracts { get; set; }
        public DbSet<RealEstateEntity> RealEstates { get; set; }
        public DbSet<ClauseEntity> Clauses { get; set; }
        public DbSet<MappingContractClauseEntity> ContractClauses { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<MappingContractCustomerEntity> MappingContractCustomers { get; set; }
        public DbSet<MappingContractClauseEntity> MappingContractClauseEntities { get; set; }

    }
}