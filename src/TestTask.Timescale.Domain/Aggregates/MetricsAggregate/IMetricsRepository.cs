using TestTask.Timescale.SharedKernel.Domain.Specifications;

namespace TestTask.Timescale.Domain.Aggregates.MetricsAggregate
{
    public interface IMetricsRepository
    {
        void Add(Metrics metrics); 

        Task<Metrics?> GetByFileNameAsync(string fileName, CancellationToken cancellation = default);

        Task<IEnumerable<Metrics>> GetByFilterAsync(ISpecification<Metrics> filter, CancellationToken cancellation = default);
    }
}
