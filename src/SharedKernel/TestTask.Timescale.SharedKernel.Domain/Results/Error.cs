using System.Collections.Immutable;

namespace TestTask.Timescale.SharedKernel.Domain.Results
{
    public class Error
    {
        private static readonly IReadOnlyDictionary<string, object> _emptyDetails = ImmutableDictionary<string, object>.Empty;

        public string Message { get; }

        public IReadOnlyDictionary<string, object> Details { get; }

        protected Error(string message)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message);

            Message = message;
            Details = _emptyDetails;
        }

        protected Error(string message, IReadOnlyDictionary<string, object> details)
            : this(message)
        {
            ArgumentNullException.ThrowIfNull(details);

            Details = details;
        }
    }
}
