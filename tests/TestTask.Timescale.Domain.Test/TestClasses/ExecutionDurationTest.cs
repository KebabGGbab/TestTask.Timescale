using TestTask.Timescale.Domain.Aggregates.RecordAggregate;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Test.TestClasses
{
    [TestClass]
    public class ExecutionDurationTest
    {
        [TestMethod]
        [DataRow(0)]
        [DataRow(2.3d)]
        public void CanCreate_CountSecondsMoreThenZero_Success(double seconds)
        {
            Result result = ExecutionDuration.CanCreate(seconds);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CanCreate_CountSecondsLessThenZero_Fail()
        {
            double seconds = -2.3d;

            Result result = ExecutionDuration.CanCreate(seconds);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(2.3d)]
        public void Create_CountSecondsMoreThenZero_ObjectWithPassedSeconds(double seconds)
        {
            Result<ExecutionDuration> result = ExecutionDuration.Create(seconds);

            Assert.AreEqual(seconds, result.Value.Seconds);
        }

        [TestMethod]
        public void Create_CountSecondsLessThenZero_Fail()
        {
            double seconds = -2.3d;

            Result<ExecutionDuration> result = ExecutionDuration.Create(seconds);

            Assert.IsTrue(result.IsFailure);
        }
    }
}
