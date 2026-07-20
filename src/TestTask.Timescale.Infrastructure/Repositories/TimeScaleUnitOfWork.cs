using TestTask.Timescale.Domain.Aggregates;
using TestTask.Timescale.Domain.Aggregates.MetricsAggregate;
using TestTask.Timescale.Domain.Aggregates.RecordAggregate;
using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.SharedKernel.Domain.Events;
using TestTask.Timescale.SharedKernel.Infrastructure;

namespace TestTask.Timescale.Infrastructure.Repositories
{
    public class TimeScaleUnitOfWork : UnitOfWork, ITimeScaleUnitOfWork
    {
        public ITimeScaleRepository TimeScaleRepository { get; }
        
        public IRecordRepository RecordRepository { get; }

        public IMetricsRepository MetricsRepository { get; }

        public TimeScaleUnitOfWork(ApplicationDbContext db, IDomainEventDispatcher eventDispatcher) 
            : base(db, eventDispatcher)
        {
            TimeScaleRepository = new TimeScaleRepository(db);
            RecordRepository = new RecordRepository(db);
            MetricsRepository = new MetricsRepository(db);
        }
    }
}
