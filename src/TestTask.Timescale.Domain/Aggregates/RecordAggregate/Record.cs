using TestTask.Timescale.SharedKernel.Domain.BaseModels;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Aggregates.RecordAggregate
{
    public class Record : AggregateRoot
    {
        public Timestamp Date { get; }

        public ExecutionDuration Time { get; }

        public RecordValue Value { get; }

        private Record(Timestamp date, ExecutionDuration time, RecordValue value)
        {
            Date = date;
            Time = time;
            Value = value;
        }

        public static Result CanCreate(DateTime date, double seconds, double value)
        {
            Result timestamp = Timestamp.CanCreate(date);
            Result time = ExecutionDuration.CanCreate(seconds);
            Result unitValue = RecordValue.CanCreate(value);

            if (timestamp.IsFailure || time.IsFailure || unitValue.IsFailure)
            {
                return Result.Fail(timestamp.Errors
                    .Concat(time.Errors)
                    .Concat(unitValue.Errors));
            }

            return Result.Ok();
        }

        public static Result<Record> Create(DateTime date, double seconds, double value)
        {
            Result canCreate = CanCreate(date, seconds, value);

            if (canCreate.IsFailure)
            {
                return Result.Fail<Record>(canCreate.Errors);
            }

            return Result.Ok(new Record(
                Timestamp.Create(date).Value,
                ExecutionDuration.Create(seconds).Value,
                RecordValue.Create(value).Value));
        }
    }
}
