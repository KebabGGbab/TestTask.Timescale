namespace TestTask.Timescale.Domain.Aggregates.RecordAggregate
{
    public interface IRecordRepository
    {
        void Add(IEnumerable<Record> records);

        Task<IEnumerable<Record>> GetLastByFileNameAndOrderByDateAsync(string fileName, int count, CancellationToken cancellation = default);
    }
}
