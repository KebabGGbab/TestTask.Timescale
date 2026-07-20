using TestTask.Timescale.Domain.Dto;
using TestTask.Timescale.Domain.Errors;
using TestTask.Timescale.SharedKernel.Domain.BaseModels;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Aggregates.MetricsAggregate
{
    public class Metrics : AggregateRoot
    {
        public int TimeScaleId { get; }

        /// <summary>
        /// Дельта времени в секундах.
        /// </summary>
        public double DeltaDate { get; }

        /// <summary>
        /// Минимальное дата и время, как момент запуска первой операции.
        /// </summary>
        public DateTime MinDate { get; }

        /// <summary>
        /// Среднее время выполнения.
        /// </summary>
        public double AvgExecutionDuration { get; }

        /// <summary>
        /// Среднее значение по показателям.
        /// </summary>
        public double AvgValue { get; }

        /// <summary>
        /// Медина по показателям.
        /// </summary>
        public double MedianValue { get; }

        /// <summary>
        /// Максимальное значение показателя.
        /// </summary>
        public double MaxValue { get; }

        /// <summary>
        /// Минимальное значение показателя.
        /// </summary>
        public double MinValue { get; }

        // Контруктор для EF
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
        private Metrics()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
        {
        }

        private Metrics(int timeScaleId, ICollection<RecordDto> records)
        {
            TimeScaleId = timeScaleId;
            RecordDto[] filteredRecords = records.OrderBy(record => record.Date).ToArray();
            // Предупреждения null подавляются, потому что TimeScaleRecord не существовал бы с null в любом из значений
            MinDate = filteredRecords.First().Date;
            DeltaDate = (filteredRecords.Last().Date - MinDate).TotalSeconds;
            AvgExecutionDuration = Math.Round(records.Average(record => record.ExecutionTime), 3, MidpointRounding.AwayFromZero);
            AvgValue = Math.Round(records.Average(record => record.Value), 2, MidpointRounding.AwayFromZero);
            MedianValue = Math.Round(CalculateMedian(filteredRecords), 2, MidpointRounding.AwayFromZero);
            MaxValue = records.Max(record => record.Value);
            MinValue = records.Min(record => record.Value);
        }

        private static double CalculateMedian(RecordDto[] records)
        {
            int count = records.Length;

            return records.Length % 2 == 0
                ? (records[count / 2 - 1].Value + records[count / 2].Value) / 2
                : records[count / 2].Value;
        }

        public static Result CanCreate(ICollection<RecordDto> records)
        {
            if (records.Count == 0)
            {
                return Result.Fail(new MetricsCountRecordIsZeroError());
            }

            return Result.Ok();
        }

        public static Result<Metrics> Create(int timeScaleId, ICollection<RecordDto> records)
        {
            Result canCreate = CanCreate(records);

            if (canCreate.IsFailure)
            {
                return Result.Fail<Metrics>(canCreate.Errors);
            }

            return Result.Ok(new Metrics(timeScaleId, records));
        }
    }
}