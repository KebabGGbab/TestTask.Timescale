using TestTask.Timescale.SharedKernel.Domain.Exceptions;

namespace TestTask.Timescale.SharedKernel.Domain.Results
{
    public class Result<T> : Result
    {
        private readonly T? _value;

        public T Value
        {
            get
            {
                TryGetValueFromFailureResultDomainException.ThrowIfFailure(this.IsFailure);

                return _value!;
            }
        }

        internal Result(T value)
            : base()
        {
            ArgumentNullException.ThrowIfNull(value);

            _value = value;
        }

        internal Result(IEnumerable<Error> errors)
            : base(errors)
        {
        }
    }
}
