using System.Diagnostics.CodeAnalysis;
using TestTask.Timescale.SharedKernel.Domain.Resources;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.SharedKernel.Domain.Exceptions
{
    public sealed class FailureResultContainsNullDomainException : DomainException
    {
        public FailureResultContainsNullDomainException()
        {
        }

        public FailureResultContainsNullDomainException(string? message) : base(message)
        {
        }

        public FailureResultContainsNullDomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public static void ThrowIfContainsNull(IEnumerable<Error> errors)
        {
            if (errors.Any(e => e == null))
            {
                Throw();
            }
        }

        [DoesNotReturn]
        private static void Throw()
        {
            throw new FailureResultContainsNullDomainException(DomainExceptionMessages.FailureResultContainsNull);
        }
    }
}