using TestTask.Timescale.Application.CsvService;
using TestTask.Timescale.Domain.Aggregates;
using TestTask.Timescale.Domain.Aggregates.MetricsAggregate;
using TestTask.Timescale.Domain.Aggregates.RecordAggregate;
using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.Domain.Dto;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Application.Commands
{
    public class UploadCsvFileCommandHandler : ICommandHandler<UploadCsvFileCommand, Result>
    {
        private readonly ITimeScaleUnitOfWork _unitOfWork;
        private readonly ICsv _csv;

        public UploadCsvFileCommandHandler(ITimeScaleUnitOfWork unitOfWork, ICsv csv)
        {
            _unitOfWork = unitOfWork;
            _csv = csv;
        }

        public async Task<Result> HandleAsync(UploadCsvFileCommand command, CancellationToken cancellation = default)
        {
            RecordDto[] recordDtos = _csv.Read(command.Stream).ToArray();

            Result<TimeScale> timeScaleResult = TimeScale.Create(recordDtos, command.FileName);

            if (timeScaleResult.IsFailure)
            {
                return timeScaleResult;
            }

            List<Result<Record>> recordResults = new(recordDtos.Length);

            foreach (RecordDto record in recordDtos)
            {
                recordResults.Add(Record.Create(timeScaleResult.Value.Id, record));
            }

            Result<Metrics> metricsResult = Metrics.Create(timeScaleResult.Value.Id, recordDtos);

            if (recordResults.Any(x => x.IsFailure) || metricsResult.IsFailure)
            {
                return Result.Fail(recordResults.SelectMany(x => x.Errors).Concat(metricsResult.Errors));
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellation);
                await DeleteIfExist(command.FileName, cancellation);
                _unitOfWork.TimeScaleRepository.Add(timeScaleResult.Value);
                await _unitOfWork.SaveAsync(cancellation);
                _unitOfWork.RecordRepository.Add(recordResults.Select(x => x.Value));
                _unitOfWork.MetricsRepository.Add(metricsResult.Value);
                await _unitOfWork.CommitTransactionAsync(cancellation);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellation);
                throw;
            }

            return Result.Ok();
        }

        private async Task DeleteIfExist(string fileName, CancellationToken cancellation)
        {
            TimeScale? existTimeScale = await _unitOfWork.TimeScaleRepository.GetByFileNameAsync(fileName, cancellation);

            if (existTimeScale != null)
            {
                _unitOfWork.TimeScaleRepository.Delete(existTimeScale);
            }
        }
    }
}
