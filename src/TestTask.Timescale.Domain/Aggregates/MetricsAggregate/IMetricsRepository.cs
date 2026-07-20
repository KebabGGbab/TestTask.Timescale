namespace TestTask.Timescale.Domain.Aggregates.MetricsAggregate
{
    public interface IMetricsRepository
    {
        void Add(Metrics metrics);

        Task<Metrics?> GetByFileNameAsync(string fileName, CancellationToken cancellation = default);

        Task<IEnumerable<Metrics>> GetByTimestampFirstRecordAsync(DateTime timestamp, CancellationToken cancellation = default);

        Task<IEnumerable<Metrics>> GetByAvgValueAsync(double avgValue, CancellationToken cancellation = default);

        Task<IEnumerable<Metrics>> GetByAvgExecutionDurationAsync(double avgExecutionDuration, CancellationToken cancellation = default);
    }
}
