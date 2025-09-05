using Microsoft.EntityFrameworkCore;
using MyDashboard.WPF.Models;

namespace MyDashboard.WPF.Data
{
    public class AlarmDbContext : DbContext
    {
        public AlarmDbContext(DbContextOptions<AlarmDbContext> options) : base(options)
        {
        }

        public DbSet<AlarmRecord> Alarms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the AlarmRecord entity
            modelBuilder.Entity<AlarmRecord>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DateTime).IsRequired();
                entity.Property(e => e.Line).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(500);
                
                // Create index on DateTime for better query performance
                entity.HasIndex(e => e.DateTime).HasDatabaseName("IX_Alarms_DateTime");
                
                // Create index on Line for filtering
                entity.HasIndex(e => e.Line).HasDatabaseName("IX_Alarms_Line");
            });
        }
    }
}
