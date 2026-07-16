using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.SharedKernel.Domain.Test.Mocks
{
    internal sealed class MockError : Error
    {
        public MockError(string message) : base(message)
        {
        }
    }
}
