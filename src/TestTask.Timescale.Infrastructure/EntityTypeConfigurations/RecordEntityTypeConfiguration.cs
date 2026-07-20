using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Timescale.Domain.Aggregates.RecordAggregate;
using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;

namespace TestTask.Timescale.Infrastructure.EntityTypeConfigurations
{
    internal sealed class RecordEntityTypeConfiguration : IEntityTypeConfiguration<Record>
    {
        public void Configure(EntityTypeBuilder<Record> builder)
        {
            builder.ToTable("values");

            builder.HasKey(e => e.Id);

            builder.HasOne<TimeScale>()
                .WithMany()
                .HasPrincipalKey(e => e.Id)
                .HasForeignKey(e => e.TimeScaleId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.OwnsOne(e => e.Date, n =>
            {
                n.Property(e => e.Value)
                    .HasColumnName("date");
            });

            builder.OwnsOne(e => e.Time, n =>
            {
                n.Property(e => e.Seconds)
                    .HasColumnName("execution_time");
            });

            builder.OwnsOne(e => e.Value, n =>
            {
                n.Property(e => e.Indicator)
                    .HasColumnName("value");
            });
        }
    }
}
