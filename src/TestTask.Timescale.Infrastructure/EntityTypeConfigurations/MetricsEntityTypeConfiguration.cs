using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Timescale.Domain.Aggregates.MetricsAggregate;
using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;

namespace TestTask.Timescale.Infrastructure.EntityTypeConfigurations
{
    internal sealed class MetricsEntityTypeConfiguration : IEntityTypeConfiguration<Metrics>
    {
        public void Configure(EntityTypeBuilder<Metrics> builder)
        {
            builder.ToTable("result");

            builder.HasKey(e => e.Id);

            builder.HasOne<TimeScale>()
                .WithOne()
                .HasPrincipalKey<TimeScale>(e => e.Id)
                .HasForeignKey<Metrics>(e => e.TimeScaleId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(e => e.DeltaDate);
            builder.Property(e => e.MinDate);
            builder.Property(e => e.AvgExecutionDuration);
            builder.Property(e => e.AvgValue);
            builder.Property(e => e.MedianValue);
            builder.Property(e => e.MaxValue);
            builder.Property(e => e.MinValue);
        }
    }
}
