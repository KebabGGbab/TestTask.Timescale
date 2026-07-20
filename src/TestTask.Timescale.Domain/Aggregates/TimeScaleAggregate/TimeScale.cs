using TestTask.Timescale.Domain.Dto;
using TestTask.Timescale.Domain.Errors;
using TestTask.Timescale.SharedKernel.Domain.BaseModels;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate
{
    public class TimeScale : AggregateRoot
    {
        // Каких либо требований нет, поэтому запишу как обычную строку.
        public string FileName { get; }

        // Конструктор для EF
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
        private TimeScale()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
        {
        }

        private TimeScale(string fileName)
        {
            FileName = fileName;
        }

        public static Result CanCreate(ICollection<RecordDto> records)
        {
            if (records is { Count: < 1 or > 10_000})
            {
                return Result.Fail(new TimeScaleCountRecordOutOfRangeError(records.Count));
            }

            return Result.Ok();
        }

        public static Result<TimeScale> Create(ICollection<RecordDto> records, string fileName)
        {
            Result canCreate = CanCreate(records);

            if (canCreate.IsFailure)
            {
                return Result.Fail<TimeScale>(canCreate.Errors);
            }

            return Result.Ok(new TimeScale(fileName));
        }
    }
}
