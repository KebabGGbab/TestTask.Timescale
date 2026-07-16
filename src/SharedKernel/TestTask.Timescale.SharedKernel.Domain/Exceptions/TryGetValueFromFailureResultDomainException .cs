using System.Diagnostics.CodeAnalysis;
using TestTask.Timescale.SharedKernel.Domain.Resources;

namespace TestTask.Timescale.SharedKernel.Domain.Exceptions
{
    public sealed class TryGetValueFromFailureResultDomainException : DomainException
    {
        public TryGetValueFromFailureResultDomainException()
        {
        }

        public TryGetValueFromFailureResultDomainException(string? message) : base(message)
        {
        }

        public TryGetValueFromFailureResultDomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public static void ThrowIfFailure([DoesNotReturnIf(true)] bool isFailure)
        {
            if (isFailure)
            {
                Throw();
            }
        }

        [DoesNotReturn]
        private static void Throw()
        {
            throw new TryGetValueFromFailureResultDomainException(DomainExceptionMessages.TryGetValueFromFailureResult);
        }
    }
}
