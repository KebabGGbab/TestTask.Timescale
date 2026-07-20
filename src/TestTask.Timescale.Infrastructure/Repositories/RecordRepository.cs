using Microsoft.EntityFrameworkCore;
using TestTask.Timescale.Domain.Aggregates.RecordAggregate;

namespace TestTask.Timescale.Infrastructure.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly ApplicationDbContext _db;

        public RecordRepository(ApplicationDbContext db)
        {
            ArgumentNullException.ThrowIfNull(db);

            _db = db;
        }

        public void Add(Record record)
        {
            _db.Records.Add(record);
        }

        public void Add(IEnumerable<Record> records)
        {
            _db.Records.AddRange(records);
        }

        public async Task<IEnumerable<Record>> GetRecordsByFileNameAsync(string fileName, CancellationToken cancellation = default)
        {
            return await _db.Records
                .Join(_db.TimeScales,
                      x => x.TimeScaleId,
                      x => x.Id,
                      (r, t) => new
                      {
                          Record = r,
                          t.FileName
                      })
                .Where(join => join.FileName == fileName)
                .Select(join => join.Record)
                .ToListAsync(cancellation);
        }

        public async Task<IEnumerable<Record>> GetLastByFileNameAndOrderByDateAsync(string fileName, int count, CancellationToken cancellation = default)
        {
            return await _db.Records
                .Join(_db.TimeScales,
                    x => x.TimeScaleId,
                    x => x.Id,
                    (r, t) => new 
                        { 
                            Record = r,
                            t.FileName 
                        })
                .Where(join => join.FileName == fileName)
                .Select(join => join.Record)
                .OrderByDescending(x => x.Date.Value)
                .Take(count)
                .ToListAsync(cancellation);
        }
    }
}
