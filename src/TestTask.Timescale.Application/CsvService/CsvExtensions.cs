using System.Globalization;
using CsvHelper.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestTask.Timescale.Application.CsvService
{
    public static class CsvExtensions
    {
        public static IServiceCollection AddCsvService(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddSingleton<IReaderConfiguration, CsvConfiguration>(
                    (s) => new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ";",
                    })
                .AddSingleton<ICsv, Csv>();
        }
    }
}
