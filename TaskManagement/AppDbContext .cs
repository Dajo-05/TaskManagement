using Microsoft.EntityFrameworkCore;
using TaskManagement.Entities;

namespace TaskManagement
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Status)
                .HasDefaultValue("Pending");
        }
    }
}
