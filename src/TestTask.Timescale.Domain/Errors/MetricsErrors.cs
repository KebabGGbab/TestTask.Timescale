using TestTask.Timescale.Domain.Resources;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Errors;

public class MetricsCountRecordIsZeroError : Error
{
    public MetricsCountRecordIsZeroError()
        : base(ErrorMessages.MetricsCountRecordIsZero)
    {
    }
}
