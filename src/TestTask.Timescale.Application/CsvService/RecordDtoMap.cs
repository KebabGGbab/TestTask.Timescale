using CsvHelper.Configuration;
using TestTask.Timescale.Domain.Dto;

namespace TestTask.Timescale.Application.CsvService
{
    public class RecordDtoMap : ClassMap<RecordDto>
    {
        public RecordDtoMap()
        {
            Map(e => e.Date)
                .Name(nameof(RecordDto.Date))
                .Index(0);

            Map(e => e.ExecutionTime)
                .Name(nameof(RecordDto.ExecutionTime))
                .Index(1);

            Map(e => e.Value)
                .Name(nameof(RecordDto.Value))
                .Index(2);
        }
    }
}
