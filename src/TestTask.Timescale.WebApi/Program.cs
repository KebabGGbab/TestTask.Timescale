using TestTask.Timescale.Application.Commands;
using TestTask.Timescale.Application.CsvService;
using TestTask.Timescale.Application.Queries;
using TestTask.Timescale.Infrastructure;

namespace TestTask.Timescale.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddInfrastructure("ApplicationDatabase");
            builder.Services.AddCsvService();
            builder.Services.AddCommands();
            builder.Services.AddQueries();

            WebApplication app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}
