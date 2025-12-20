using Microsoft.EntityFrameworkCore;
using SmartTasks.Domain.Entities;

namespace SmartTasks.Infrastructure.Persistence
{
    public class AppDbContext  : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example: configure columns, constraints, etc.
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Title).IsRequired().HasMaxLength(100);
            });
        }
    }
}
