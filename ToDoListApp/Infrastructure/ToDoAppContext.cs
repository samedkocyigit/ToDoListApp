using Microsoft.EntityFrameworkCore;
using ToDoListApp.Domain.Models.Models;

namespace ToDoListApp.Data
{
    public class ToDoAppContext : DbContext
    {
        public ToDoAppContext(DbContextOptions<ToDoAppContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Tasks>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks) 
                .WithOne(t => t.User) 
                .HasForeignKey(t => t.UserId); 
        }
    }
}
