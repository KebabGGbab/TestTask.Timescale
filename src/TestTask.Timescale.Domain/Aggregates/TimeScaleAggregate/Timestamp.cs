using TestTask.Timescale.Domain.Errors;
using TestTask.Timescale.SharedKernel.Domain.BaseModels;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate
{
    public class Timestamp : ValueObject
    {
        private static readonly DateTime MinDate = new(2000, 01, 01);

        // С тем учётом, что формат задан как "ГГГГ-ММ-ДДTчч-мм-сс.ммммZ", предположу,
        // что часовой пояс не важен, так как формат соответствует UTC.
        public DateTime Value { get; set; }

        private Timestamp(DateTime value)
        {
            Value = value;
        }

        public static Result CanCreate(DateTime value)
        {
            if (value > DateTime.UtcNow)
            {
                return Result.Fail(new TimestampIsBigError(value));
            }

            if (value < MinDate)
            {
                return Result.Fail(new TimestampIsLittleError(value));
            }

            return Result.Ok();
        }

        public static Result<Timestamp> Create(DateTime value)
        {
            Result canCreate = CanCreate(value);

            if (canCreate.IsFailure)
            {
                return Result.Fail<Timestamp>(canCreate.Errors);
            }

            return Result.Ok(new Timestamp(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
