using Microsoft.EntityFrameworkCore;
using TestTask.Timescale.Domain.Aggregates.MetricsAggregate;
using TestTask.Timescale.Domain.Aggregates.RecordAggregate;
using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.Infrastructure.EntityTypeConfigurations;

namespace TestTask.Timescale.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TimeScale> TimeScales { get; set; }

        public DbSet<Record> Records { get; set; }

        public DbSet<Metrics> Metrics { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityAlwaysColumns();

            modelBuilder.ApplyConfiguration(new TimeScaleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RecordEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MetricsEntityTypeConfiguration());
        }
    }
}
