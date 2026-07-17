using TestTask.Timescale.Domain.Errors;
using TestTask.Timescale.SharedKernel.Domain.BaseModels;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate
{
    public class ExecutionTime : ValueObject
    {
        public double Seconds { get; }

        private ExecutionTime(double seconds)
        {
            Seconds = seconds;
        }

        public static Result CanCreate(double seconds)
        {
            if (seconds < 0)
            {
                return Result.Fail(new ExecutionTimeErrors(seconds));
            }

            return Result.Ok();
        }

        public static Result<ExecutionTime> Create(double seconds)
        {
            Result canCreate = CanCreate(seconds);

            if (canCreate.IsFailure)
            {
                return Result.Fail<ExecutionTime>(canCreate.Errors);
            }

            return Result.Ok(new ExecutionTime(seconds));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Seconds;
        }
    }
}
