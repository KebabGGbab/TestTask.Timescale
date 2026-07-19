using TestTask.Timescale.Domain.Dto;
using TestTask.Timescale.SharedKernel.Domain.BaseModels;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Aggregates.RecordAggregate
{
    public class Record : AggregateRoot
    {
        public EntityId TimeScaleId { get; }

        public Timestamp Date { get; }

        public ExecutionDuration Time { get; }

        public RecordValue Value { get; }

        private Record(EntityId timeScaleId, Timestamp date, ExecutionDuration time, RecordValue value)
        {
            TimeScaleId = timeScaleId;
            Date = date;
            Time = time;
            Value = value;
        }

        public static Result CanCreate(RecordDto dto)
        {
            Result timestamp = Timestamp.CanCreate(dto.Date);
            Result time = ExecutionDuration.CanCreate(dto.ExecutionTime);
            Result unitValue = RecordValue.CanCreate(dto.Value);

            if (timestamp.IsFailure || time.IsFailure || unitValue.IsFailure)
            {
                return Result.Fail(timestamp.Errors
                    .Concat(time.Errors)
                    .Concat(unitValue.Errors));
            }

            return Result.Ok();
        }

        public static Result<Record> Create(EntityId timeScaleId, RecordDto dto)
        {
            Result canCreate = CanCreate(dto);

            if (canCreate.IsFailure)
            {
                return Result.Fail<Record>(canCreate.Errors);
            }

            return Result.Ok(new Record(
                timeScaleId,
                Timestamp.Create(dto.Date).Value, 
                ExecutionDuration.Create(dto.ExecutionTime).Value, 
                RecordValue.Create(dto.Value).Value));
        }
    }
}
