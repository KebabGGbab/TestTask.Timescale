using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Test.TestClasses
{
    [TestClass]
    public class ExecutionTimeTest
    {
        [TestMethod]
        [DataRow(0)]
        [DataRow(2.3d)]
        public void CanCreate_CountSecondsMoreThenZero_Success(double seconds)
        {
            Result result = ExecutionTime.CanCreate(seconds);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CanCreate_CountSecondsLessThenZero_Fail()
        {
            double seconds = -2.3d;

            Result result = ExecutionTime.CanCreate(seconds);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(2.3d)]
        public void Create_CountSecondsMoreThenZero_ObjectWithPassedSeconds(double seconds)
        {
            Result<ExecutionTime> result = ExecutionTime.Create(seconds);

            Assert.AreEqual(seconds, result.Value.Seconds);
        }

        [TestMethod]
        public void Create_CountSecondsLessThenZero_Fail()
        {
            double seconds = -2.3d;

            Result<ExecutionTime> result = ExecutionTime.Create(seconds);

            Assert.IsTrue(result.IsFailure);
        }
    }
}
