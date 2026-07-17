using System.Globalization;
using System.Text;
using TestTask.Timescale.Domain.Resources;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Errors;

public class TimestampIsBigError : Error
{
    private static readonly CompositeFormat _messageFormat = CompositeFormat.Parse(ErrorMessages.TimestampIsBig);

    public TimestampIsBigError(DateTime actual) 
        : base(string.Format(CultureInfo.InvariantCulture, _messageFormat, DateTime.Now, actual))
    {
    }
}

public class TimestampIsLittleError : Error
{
    private static readonly CompositeFormat _messageFormat = CompositeFormat.Parse(ErrorMessages.TimestampIsLittle);

    public TimestampIsLittleError(DateTime actual) 
        : base(string.Format(CultureInfo.InvariantCulture, _messageFormat, actual))
    {
    }
}

public class TimestampIsNotUtcError : Error
{
    public TimestampIsNotUtcError()
        : base(ErrorMessages.TimestampIsNotUtc)
    {
    }
}