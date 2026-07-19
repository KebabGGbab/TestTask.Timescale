using TestTask.Timescale.Domain.Aggregates.RecordAggregate;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Test.TestClasses
{
    [TestClass]
    public class RecordValueTest
    {
        [TestMethod]
        [DataRow(0)]
        [DataRow(2.3d)]
        public void CanCreate_ValueMoreThenZero_Success(double value)
        {
            Result result = RecordValue.CanCreate(value);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CanCreate_ValueLessThenZero_Fail()
        {
            double value = -2.3d;

            Result result = RecordValue.CanCreate(value);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(2.3d)]
        public void Create_ValueMoreThenZero_ObjectWithPassedSeconds(double value)
        {
            Result<RecordValue> result = RecordValue.Create(value);

            Assert.AreEqual(value, result.Value.Indicator);
        }

        [TestMethod]
        public void Create_ValueLessThenZero_Fail()
        {
            double value = -2.3d;

            Result<RecordValue> result = RecordValue.Create(value);

            Assert.IsTrue(result.IsFailure);
        }
    }
}
