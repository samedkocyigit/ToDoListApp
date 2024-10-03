using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

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
            // User modelinin Primary Key'ini belirleyin
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            // Task modelinin Primary Key'ini belirleyin
            modelBuilder.Entity<Tasks>()
                .HasKey(t => t.Id);

            // User ve Tasks arasındaki ilişkiyi tanımlayın
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks) // Bir kullanıcının birden fazla görevi olabilir
                .WithOne(t => t.User) // Her görevin bir kullanıcıya ait olduğu tanımlanır
                .HasForeignKey(t => t.UserId); // Görevlerin UserId ile ilişkilendirilmesi
        }
    }
}
