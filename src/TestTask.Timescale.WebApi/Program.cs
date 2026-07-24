using TestTask.Timescale.Application.Commands;
using TestTask.Timescale.Application.CsvService;
using TestTask.Timescale.Application.Queries;
using TestTask.Timescale.Infrastructure;
using TestTask.Timescale.WebApi.Resources;

namespace TestTask.Timescale.WebApi
{
    public class Program
    {
        private const string ConnectionStringKey = "ApplicationDatabase";

        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString(ConnectionStringKey)
                ?? throw new InvalidOperationException(string.Format(ExceptionMessages.UserSecretNotFound, ConnectionStringKey)));
            builder.Services.AddCsvService();
            builder.Services.AddCommands();
            builder.Services.AddQueries();

            WebApplication app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}
