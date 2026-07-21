using Microsoft.EntityFrameworkCore;
using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;

namespace TestTask.Timescale.Infrastructure.Repositories
{
    public class TimeScaleRepository : ITimeScaleRepository
    {
        private readonly ApplicationDbContext _db;

        public TimeScaleRepository(ApplicationDbContext db)
        {
            ArgumentNullException.ThrowIfNull(db);

            _db = db;
        }

        public void Add(TimeScale timeScale)
        {
            _db.TimeScales.Add(timeScale);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellation = default)
        {
            TimeScale? timescale = await GetByIdAsync(id, cancellation);

            if (timescale == null)
            {
                return;
            }

            _db.Remove(timescale);
        }

        public void Delete(TimeScale timeScale)
        {
            _db.Remove(timeScale);
        }

        public async Task<TimeScale?> GetByIdAsync(int id, CancellationToken cancellation = default)
        {
            return await _db.TimeScales
                .FindAsync([id], cancellation);
        }

        public async Task<IEnumerable<TimeScale?>> GetByIdAsync(IEnumerable<int> ids, CancellationToken cancellation = default)
        {
            return await _db.TimeScales
                .Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellation);
        }

        public async Task<TimeScale?> GetByFileNameAsync(string fileName, CancellationToken cancellation = default)
        {
            return await _db.TimeScales
                .FirstOrDefaultAsync(x => x.FileName == fileName, cancellation);
        }
    }
}
