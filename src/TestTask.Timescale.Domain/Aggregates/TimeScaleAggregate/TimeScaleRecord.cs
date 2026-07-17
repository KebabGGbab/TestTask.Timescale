using TestTask.Timescale.SharedKernel.Domain.BaseModels;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate
{
    public class TimeScaleRecord : Entity
    {
        public Timestamp Date { get; }

        public ExecutionTime Time { get; }

        public TimeScaleRecordValue Value { get; }

        private TimeScaleRecord(Timestamp date, ExecutionTime time, TimeScaleRecordValue value)
        {
            Date = date;
            Time = time;
            Value = value;
        }

        public static Result CanCreate(DateTime date, double seconds, double value)
        {
            Result timestamp = Timestamp.CanCreate(date);
            Result time = ExecutionTime.CanCreate(seconds);
            Result unitValue = TimeScaleRecordValue.CanCreate(value);

            if (timestamp.IsFailure || time.IsFailure || unitValue.IsFailure)
            {
                return Result.Fail(timestamp.Errors
                    .Concat(time.Errors)
                    .Concat(unitValue.Errors));
            }

            return Result.Ok();
        }

        public static Result<TimeScaleRecord> Create(DateTime date, double seconds, double value)
        {
            Result canCreate = CanCreate(date, seconds, value);

            if (canCreate.IsFailure)
            {
                return Result.Fail<TimeScaleRecord>(canCreate.Errors);
            }

            return Result.Ok(new TimeScaleRecord(
                Timestamp.Create(date).Value, 
                ExecutionTime.Create(seconds).Value, 
                TimeScaleRecordValue.Create(value).Value));
        }
    }
}
