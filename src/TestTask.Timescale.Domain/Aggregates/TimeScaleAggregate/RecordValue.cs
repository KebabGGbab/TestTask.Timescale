using TestTask.Timescale.Domain.Errors;
using TestTask.Timescale.SharedKernel.Domain.BaseModels;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate
{
    public class RecordValue : ValueObject
    {
        public double Indicator { get; }

        private RecordValue(double indicator)
        {
            Indicator = indicator;
        }

        public static Result CanCreate(double indicator)
        {
            if (indicator < 0)
            {
                return Result.Fail(new RecordValueErrors(indicator));
            }

            return Result.Ok();
        }

        public static Result<RecordValue> Create(double indicator)
        {
            Result canCreate = CanCreate(indicator);

            if (canCreate.IsFailure)
            {
                return Result.Fail<RecordValue>(canCreate.Errors);
            }

            return Result.Ok(new RecordValue(indicator));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Indicator;
        }
    }
}
