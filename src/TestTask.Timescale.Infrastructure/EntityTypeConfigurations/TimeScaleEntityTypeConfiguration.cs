using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;

namespace TestTask.Timescale.Infrastructure.EntityTypeConfigurations
{
    internal sealed class TimeScaleEntityTypeConfiguration : IEntityTypeConfiguration<TimeScale>
    {
        public void Configure(EntityTypeBuilder<TimeScale> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FileName);
        }
    }
}
