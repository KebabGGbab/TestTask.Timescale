using TestTask.Timescale.Application.Dto;
using TestTask.Timescale.Application.Errors;
using TestTask.Timescale.Domain.Aggregates;
using TestTask.Timescale.Domain.Aggregates.MetricsAggregate;
using TestTask.Timescale.Domain.Aggregates.MetricsAggregate.Specifications;
using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.SharedKernel.Domain.Results;
using TestTask.Timescale.SharedKernel.Domain.Specifications;

namespace TestTask.Timescale.Application.Queries
{
    public class GetFiltereredMetricsQueryHandler : IQueryHandler<GetFiltereredMetricsQuery, Result<IEnumerable<MetricsDto>>>
    {
        private readonly ITimeScaleUnitOfWork _unitOfWork;

        public GetFiltereredMetricsQueryHandler(ITimeScaleUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<MetricsDto>>> HandleAsync(GetFiltereredMetricsQuery query, CancellationToken cancellation = default)
        {
            // Не вижу смысла использовать фильтры, если указано имя.
            if (query.FileName != null)
            {
                Metrics? metrics = await _unitOfWork.MetricsRepository.GetByFileNameAsync(query.FileName, cancellation);

                if (metrics == null)
                {
                    return Result.Ok<IEnumerable<MetricsDto>>([]);
                }

                return Result.Ok<IEnumerable<MetricsDto>>([MetricsDto.From(metrics, query.FileName)]);
            }

            SpecificationBuilder<Metrics> specificationBuilder = new();

            if (query.FirstDateMax.HasValue)
            {
                specificationBuilder.And(new FirstDateMaxSpecification(query.FirstDateMax.Value));
            }
            if (query.FirstDateMin.HasValue)
            {
                specificationBuilder.And(new FirstDateMinSpecification(query.FirstDateMin.Value));
            }
            if (query.AvgExecutionTimeMax.HasValue)
            {
                specificationBuilder.And(new AvgExecutionTimeMaxSpecification(query.AvgExecutionTimeMax.Value));
            }
            if (query.AvgExecutionTimeMin.HasValue)
            {
                specificationBuilder.And(new AvgExecutionTimeMinSpecification(query.AvgExecutionTimeMin.Value));
            }
            if (query.AvgValueMax.HasValue)
            {
                specificationBuilder.And(new AvgValueMaxSpecification(query.AvgValueMax.Value));
            }
            if (query.AvgValueMin.HasValue)
            {
                specificationBuilder.And(new AvgValueMinSpecification(query.AvgValueMin.Value));
            }

            ISpecification<Metrics>? specification = specificationBuilder.Build();

            if (specification == null)
            {
                return Result.Fail<IEnumerable<MetricsDto>>(new FiltersNotAppliedError());
            }

            IEnumerable<Metrics> listMetrics = (await _unitOfWork.MetricsRepository
                .GetByFilterAsync(specification, cancellation))
                .OrderBy(x => x.TimeScaleId);
            IEnumerable<TimeScale?> timeScales = (await _unitOfWork.TimeScaleRepository
                .GetByIdAsync(listMetrics.Select(x => x.TimeScaleId), cancellation))
                .OrderBy(x => x!.Id);

            List<MetricsDto> dtos = [];

            foreach (Metrics metrics in listMetrics)
            {
                TimeScale timeScale = timeScales.First(x => x!.Id == metrics.Id)!;
                dtos.Add(MetricsDto.From(metrics, timeScale.FileName));
            }

            return Result.Ok((IEnumerable<MetricsDto>)dtos);
        }
    }
}
