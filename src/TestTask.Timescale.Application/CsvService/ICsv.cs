using TestTask.Timescale.Domain.Dto;

namespace TestTask.Timescale.Application.CsvService
{
    public interface ICsv
    {
        IEnumerable<RecordDto> Read(Stream stream);
    }
}
