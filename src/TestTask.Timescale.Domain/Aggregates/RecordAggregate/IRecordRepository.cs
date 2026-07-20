namespace TestTask.Timescale.Domain.Aggregates.RecordAggregate
{
    public interface IRecordRepository
    {
        void Add(Record record);

        void Add(IEnumerable<Record> records);

        Task<IEnumerable<Record>> GetRecordsByFileNameAsync(string fileName, CancellationToken cancellation = default);

        Task<IEnumerable<Record>> GetLastByFileNameAndOrderByDateAsync(string fileName, int count, CancellationToken cancellation = default);
    }
}
