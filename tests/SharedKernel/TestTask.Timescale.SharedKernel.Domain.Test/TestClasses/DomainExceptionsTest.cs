using TestTask.Timescale.SharedKernel.Domain.Exceptions;
using TestTask.Timescale.SharedKernel.Domain.Results;
using TestTask.Timescale.SharedKernel.Domain.Test.Mocks;

namespace TestTask.Timescale.SharedKernel.Domain.Test.TestClasses
{
    [TestClass]
    public sealed class DomainExceptionsTest
    {
        [TestMethod]
        public void FailureResultNotContainErrorDomainException_ThrowIfErrorCollectionEmpty_ErrorsIsNotEmpty_NotThrow()
        {
            IEnumerable<Error> errors = [new MockError("error")];

            FailureResultIsEmptyDomainException.ThrowIfErrorCollectionEmpty(errors);
        }

        [TestMethod]
        public void FailureResultNotContainErrorDomainException_ThrowIfErrorCollectionEmpty_ErrorsIsNull_Throw()
        {
            IEnumerable<Error>? errors = null;

            void action() => FailureResultIsEmptyDomainException.ThrowIfErrorCollectionEmpty(errors);

            Assert.ThrowsExactly<FailureResultIsEmptyDomainException>(action);
        }

        [TestMethod]
        public void FailureResultNotContainErrorDomainException_ThrowIfErrorCollectionEmpty_ErrorsIsEmpty_Throw()
        {
            IEnumerable<Error> errors = [];

            void action() => FailureResultIsEmptyDomainException.ThrowIfErrorCollectionEmpty(errors);

            Assert.ThrowsExactly<FailureResultIsEmptyDomainException>(action);
        }

        [TestMethod]
        public void TryGetValueFromFailureResultDomainException_ThrowIfFailure_IsSuccess_NotThrow()
        {
            bool isFailure = false;

            TryGetValueFromFailureResultDomainException.ThrowIfFailure(isFailure);
        }

        [TestMethod]
        public void TryGetValueFromFailureResultDomainException_ThrowIfFailure_IsFailure_Throw()
        {
            bool isFailure = true;

            void action() => TryGetValueFromFailureResultDomainException.ThrowIfFailure(isFailure);

            Assert.ThrowsExactly<TryGetValueFromFailureResultDomainException>(action);
        }

        [TestMethod]
        public void FailureResultContainsNullErrorDomainException_ThrowIfContainsNull_NotContainsNull_NotThrow()
        {
            IEnumerable<Error> errors = [new MockError("error"), new MockError("error2")];

            FailureResultContainsNullDomainException.ThrowIfContainsNull(errors);
        }

        [TestMethod]
        public void FailureResultContainsNullErrorDomainException_ThrowIfContainsNull_ContainsNull_Throw()
        {
            IEnumerable<Error> errors = [new MockError("error"), null!];

            void action() => FailureResultContainsNullDomainException.ThrowIfContainsNull(errors);

            Assert.ThrowsExactly<FailureResultContainsNullDomainException>(action);
        }
    }
}
