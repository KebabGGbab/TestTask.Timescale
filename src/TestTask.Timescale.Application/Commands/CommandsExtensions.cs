using Microsoft.Extensions.DependencyInjection;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Application.Commands
{
    public static class CommandsExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddScoped<ICommandHandler<UploadCsvFileCommand, Result>, UploadCsvFileCommandHandler>();
        }
    }
}
