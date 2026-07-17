using TestTask.Timescale.Domain.Errors;
using TestTask.Timescale.SharedKernel.Domain.BaseModels;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate
{
    public class TimeScaleRecordValue : ValueObject
    {
        public double Indicator { get; }

        private TimeScaleRecordValue(double indicator)
        {
            Indicator = indicator;
        }

        public static Result CanCreate(double indicator)
        {
            if (indicator < 0)
            {
                return Result.Fail(new TimeScaleRecordValueErrors(indicator));
            }

            return Result.Ok();
        }

        public static Result<TimeScaleRecordValue> Create(double indicator)
        {
            Result canCreate = CanCreate(indicator);

            if (canCreate.IsFailure)
            {
                return Result.Fail<TimeScaleRecordValue>(canCreate.Errors);
            }

            return Result.Ok(new TimeScaleRecordValue(indicator));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Indicator;
        }
    }
}
