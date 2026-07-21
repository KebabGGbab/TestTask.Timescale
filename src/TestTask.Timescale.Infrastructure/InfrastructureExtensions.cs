using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Timescale.Domain.Aggregates;
using TestTask.Timescale.Domain.Aggregates.MetricsAggregate;
using TestTask.Timescale.Domain.Aggregates.RecordAggregate;
using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.Infrastructure.Repositories;
using TestTask.Timescale.SharedKernel.Domain.Events;
using TestTask.Timescale.SharedKernel.Infrastructure;

namespace TestTask.Timescale.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddScoped<ITimeScaleRepository, TimeScaleRepository>()
                .AddScoped<IRecordRepository, RecordRepository>()
                .AddScoped<IMetricsRepository, MetricsRepository>()
                .AddScoped<ITimeScaleUnitOfWork, TimeScaleUnitOfWork>()
                .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
                .AddDbContextPool<ApplicationDbContext>(
                    (s, b) => b.UseNpgsql(s.GetRequiredService<IConfiguration>().GetConnectionString(connectionString))
                               .UseSnakeCaseNamingConvention());
        }
    }
}
