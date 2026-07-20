namespace TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate
{
    public interface ITimeScaleRepository
    {
        void Add(TimeScale timeScale);

        Task DeleteAsync(int id, CancellationToken cancellation = default);

        void Delete(TimeScale timeScale);

        Task<TimeScale?> GetByIdAsync(int id, CancellationToken cancellation = default);

        Task<TimeScale?> GetByFileNameAsync(string fileName, CancellationToken cancellation = default);
    }
}
