using TestTask.Timescale.SharedKernel.Domain.Test.Mocks;

namespace TestTask.Timescale.SharedKernel.Domain.Test.TestClasses
{
    [TestClass]
    public sealed class ErrorTest
    {
        [TestMethod]
        public void Ctor_MessageNotEmpty_ObjectIsInitialized()
        {
            string message = "error";

            MockError error = new(message);

            Assert.AreEqual(message, error.Message);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Ctor_MessageIsEmpty_Throw(string? errorMessage)
        {
            void action() => _ = new MockError(errorMessage!);

            Assert.Throws<ArgumentException>(action);
        }
    }
}
