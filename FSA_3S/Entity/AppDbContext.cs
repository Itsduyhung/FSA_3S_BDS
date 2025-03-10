
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace FSA_3S.Entity
{
    public class AppDbContext : DbContext 
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options,IConfiguration configuration) : base(options) 
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
        }
        public DbSet<User> Users { get; set; }
    }
}
