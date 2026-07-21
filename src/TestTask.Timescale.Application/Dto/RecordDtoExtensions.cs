using TestTask.Timescale.Domain.Aggregates.RecordAggregate;
using TestTask.Timescale.Domain.Dto;

namespace TestTask.Timescale.Application.Dto
{
    public static class RecordDtoExtensions
    {
        extension(RecordDto)
        {
            public static RecordDto From(Record record)
            {
                return new RecordDto(
                    record.Date.Value,
                    record.Time.Seconds,
                    record.Value.Indicator);
            }
        }
    }
}
