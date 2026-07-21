using Microsoft.EntityFrameworkCore;
using TestTask.Timescale.Domain.Aggregates.MetricsAggregate;
using TestTask.Timescale.SharedKernel.Domain.Specifications;

namespace TestTask.Timescale.Infrastructure.Repositories
{
    public class MetricsRepository : IMetricsRepository
    {
        private readonly ApplicationDbContext _db;

        public MetricsRepository(ApplicationDbContext db)
        {
            ArgumentNullException.ThrowIfNull(db);

            _db = db;
        }

        public void Add(Metrics metrics)
        {
            _db.Metrics.Add(metrics);
        }

        public async Task<Metrics?> GetByFileNameAsync(string fileName, CancellationToken cancellation = default)
        {
            return await _db.Metrics
                .Join(_db.TimeScales,
                      x => x.TimeScaleId,
                      x => x.Id,
                      (m, t) => new 
                          { 
                              Metrics = m,
                              t.FileName
                          })
                .Where(join => join.FileName == fileName)
                .Select(join => join.Metrics)
                .FirstOrDefaultAsync(cancellation);
        }

        public async Task<IEnumerable<Metrics>> GetByFilterAsync(ISpecification<Metrics> filter, CancellationToken cancellation = default)
        {
            return await _db.Metrics
                .Where(filter.ToExpression())
                .ToListAsync();
        }
    }
}
