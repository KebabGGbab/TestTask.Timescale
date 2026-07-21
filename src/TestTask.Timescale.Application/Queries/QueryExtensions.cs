using Microsoft.Extensions.DependencyInjection;
using TestTask.Timescale.Application.Dto;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Application.Queries
{
    public static class QueryExtensions
    {
        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddScoped<IQueryHandler<GetFiltereredMetricsQuery, Result<IEnumerable<MetricsDto>>>, GetFiltereredMetricsQueryHandler>();
        }
    }
}
