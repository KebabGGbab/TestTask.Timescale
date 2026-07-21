using TestTask.Timescale.Domain.Aggregates.MetricsAggregate;

namespace TestTask.Timescale.Application.Dto
{
    public static class MetricsDtoExtensions
    {
        extension(MetricsDto)
        {
            public static MetricsDto From(Metrics metrics, string fileName)
            {
                return new MetricsDto(
                    metrics.Id,
                    fileName,
                    metrics.DeltaDate,
                    metrics.MinDate,
                    metrics.AvgExecutionDuration,
                    metrics.AvgValue,
                    metrics.MedianValue,
                    metrics.MaxValue,
                    metrics.MinValue);
            }
        }
    }
}
