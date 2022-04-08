using Microsoft.EntityFrameworkCore;
using WorkoutService.Models;

namespace WorkoutService.Contexts 
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseMySQL("server=localhost;port=3307;database=workout;user=root;password=Welkom32!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}