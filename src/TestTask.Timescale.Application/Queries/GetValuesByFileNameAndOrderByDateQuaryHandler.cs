using TestTask.Timescale.Application.Dto;
using TestTask.Timescale.Application.Errors;
using TestTask.Timescale.Domain.Aggregates;
using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.Domain.Dto;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Application.Queries
{
    public class GetValuesByFileNameAndOrderByDateQuaryHandler : IQueryHandler<GetValuesByFileNameAndOrderByDateQuary, Result<IEnumerable<RecordDto>>>
    {
        private readonly ITimeScaleUnitOfWork _unitOfWork;

        public GetValuesByFileNameAndOrderByDateQuaryHandler(ITimeScaleUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<RecordDto>>> HandleAsync(GetValuesByFileNameAndOrderByDateQuary query, CancellationToken cancellation = default)
        {
            TimeScale? timeScale = await _unitOfWork.TimeScaleRepository
                .GetByFileNameAsync(query.FileName, cancellation);

            if (timeScale == null)
            {
                return Result.Fail<IEnumerable<RecordDto>>(new FileNotExistError(query.FileName));
            }

            return Result.Ok((await _unitOfWork.RecordRepository
                .GetLastByFileNameAndOrderByDateAsync(query.FileName, 10, cancellation))
                .Select(RecordDto.From));
        }
    }
}
