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

            ISpecification<Metrics>? specification = null;

            if (query.FirstDateMax.HasValue)
            {
                specification = new FirstDateMaxSpecification(query.FirstDateMax.Value);
            }
            if (query.FirstDateMin.HasValue)
            {
                FirstDateMinSpecification spec = new(query.FirstDateMin.Value);

                specification = specification == null
                    ? spec
                    : specification.And(spec);
            }
            if (query.AvgExecutionTimeMax.HasValue)
            {
                AvgExecutionTimeMaxSpecification spec = new(query.AvgExecutionTimeMax.Value);

                specification = specification == null
                    ? spec
                    : specification.And(spec);
            }
            if (query.AvgExecutionTimeMin.HasValue)
            {
                AvgExecutionTimeMinSpecification spec = new(query.AvgExecutionTimeMin.Value);

                specification = specification == null
                    ? spec
                    : specification.And(spec);
            }
            if (query.AvgValueMax.HasValue)
            {
                AvgValueMaxSpecification spec = new(query.AvgValueMax.Value);

                specification = specification == null
                    ? spec
                    : specification.And(spec);
            }
            if (query.AvgValueMin.HasValue)
            {
                AvgValueMinSpecification spec = new(query.AvgValueMin.Value);

                specification = specification == null
                    ? spec
                    : specification.And(spec);
            }

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
