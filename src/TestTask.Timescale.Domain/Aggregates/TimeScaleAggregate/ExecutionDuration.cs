using TestTask.Timescale.Domain.Errors;
using TestTask.Timescale.SharedKernel.Domain.BaseModels;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate
{
    public class ExecutionDuration : ValueObject
    {
        public double Seconds { get; }

        private ExecutionDuration(double seconds)
        {
            Seconds = seconds;
        }

        public static Result CanCreate(double seconds)
        {
            if (seconds < 0)
            {
                return Result.Fail(new ExecutionDurationErrors(seconds));
            }

            return Result.Ok();
        }

        public static Result<ExecutionDuration> Create(double seconds)
        {
            Result canCreate = CanCreate(seconds);

            if (canCreate.IsFailure)
            {
                return Result.Fail<ExecutionDuration>(canCreate.Errors);
            }

            return Result.Ok(new ExecutionDuration(seconds));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Seconds;
        }
    }
}
