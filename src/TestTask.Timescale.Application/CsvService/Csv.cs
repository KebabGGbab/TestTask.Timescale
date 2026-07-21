using CsvHelper;
using CsvHelper.Configuration;
using TestTask.Timescale.Domain.Dto;

namespace TestTask.Timescale.Application.CsvService
{
    public class Csv : ICsv
    {
        private readonly IReaderConfiguration _configuration;

        public Csv(IReaderConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            _configuration = configuration;
        }

        public IEnumerable<RecordDto> Read(Stream stream)
        {
            using StreamReader reader = new(stream);
            using CsvReader csv = new(reader, _configuration);

            return csv.GetRecords<RecordDto>();
        }
    }
}
