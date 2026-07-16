using TestTask.Timescale.SharedKernel.Domain.Exceptions;

namespace TestTask.Timescale.SharedKernel.Domain.Results
{
    public class Result
    {
        private readonly static IReadOnlyCollection<Error> _errorsEmpty = [];

        public bool IsSuccess { get; }

        public IReadOnlyCollection<Error> Errors { get; }

        protected Result()
        {
            IsSuccess = true;
            Errors = _errorsEmpty;
        }

        protected Result(IEnumerable<Error> errors)
        {
            FailureResultIsEmptyDomainException.ThrowIfErrorCollectionEmpty(errors);
            FailureResultContainsNullDomainException.ThrowIfContainsNull(errors);

            IsSuccess = false;
            Errors = errors.ToList().AsReadOnly();
        }

        public static Result Ok() => new();

        public static Result<T> Ok<T>(T value) => new(value);

        public static Result Fail(IEnumerable<Error> errors) => new(errors);

        public static Result<T> Fail<T>(IEnumerable<Error> errors) => new(errors);
    }
}
