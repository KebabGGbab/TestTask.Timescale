using TestTask.Timescale.Domain.Errors;
using TestTask.Timescale.SharedKernel.Domain.BaseModels;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate
{
    public class TimeScale : AggregateRoot
    {
        // Каких либо требований нет, поэтому запишу как обычную строку.
        public string FileName { get; }

        public IReadOnlyCollection<Record> Records { get; }

        private TimeScale(ICollection<Record> records, string fileName)
        {
            Records = records.ToArray().AsReadOnly();
            FileName = fileName;
        }

        public static Result CanCreate(ICollection<Record> records)
        {
            if (records is { Count: < 1 or > 10_000})
            {
                return Result.Fail(new TimeScaleCountRecordOutOfRangeError(records.Count));
            }

            return Result.Ok();
        }

        public static Result<TimeScale> Create(ICollection<Record> records, string fileName)
        {
            Result canCreate = CanCreate(records);

            if (canCreate.IsFailure)
            {
                return Result.Fail<TimeScale>(canCreate.Errors);
            }

            return Result.Ok(new TimeScale(records, fileName));
        }
    }
}
