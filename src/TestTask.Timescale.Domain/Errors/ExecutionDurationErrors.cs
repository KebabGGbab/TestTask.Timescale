using System.Globalization;
using System.Text;
using TestTask.Timescale.Domain.Resources;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Errors;

public class ExecutionDurationErrors : Error
{
    private static readonly CompositeFormat _messageFormat = CompositeFormat.Parse(ErrorMessages.ExecutionDurationLessThanZero);

    public ExecutionDurationErrors(double actual)
        : base(string.Format(CultureInfo.InvariantCulture, _messageFormat, actual))
    {
    }
}