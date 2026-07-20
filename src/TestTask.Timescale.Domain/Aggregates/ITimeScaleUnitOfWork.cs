using TestTask.Timescale.Domain.Aggregates.MetricsAggregate;
using TestTask.Timescale.Domain.Aggregates.RecordAggregate;
using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.SharedKernel.Domain.Data;

namespace TestTask.Timescale.Domain.Aggregates
{
    public interface ITimeScaleUnitOfWork : IUnitOfWork
    {
        ITimeScaleRepository TimeScaleRepository { get; }

        IRecordRepository RecordRepository { get; }

        IMetricsRepository MetricsRepository { get; }
    }
}
