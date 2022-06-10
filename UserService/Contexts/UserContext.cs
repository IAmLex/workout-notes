using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Contexts
{
    public class UserContext : DbContext
    {
        // Actions tries: 2
        
        public DbSet<User> Users { get; set; }
        private readonly IConfiguration _configuration;

        public UserContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}