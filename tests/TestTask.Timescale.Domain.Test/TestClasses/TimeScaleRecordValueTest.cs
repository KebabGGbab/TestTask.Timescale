using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Test.TestClasses
{
    [TestClass]
    public class TimeScaleRecordValueTest
    {
        [TestMethod]
        [DataRow(0)]
        [DataRow(2.3d)]
        public void CanCreate_ValueMoreThenZero_Success(double value)
        {
            Result result = TimeScaleRecordValue.CanCreate(value);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CanCreate_ValueLessThenZero_Fail()
        {
            double value = -2.3d;

            Result result = TimeScaleRecordValue.CanCreate(value);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(2.3d)]
        public void Create_ValueMoreThenZero_ObjectWithPassedSeconds(double value)
        {
            Result<TimeScaleRecordValue> result = TimeScaleRecordValue.Create(value);

            Assert.AreEqual(value, result.Value.Indicator);
        }

        [TestMethod]
        public void Create_ValueLessThenZero_Fail()
        {
            double value = -2.3d;

            Result<TimeScaleRecordValue> result = TimeScaleRecordValue.Create(value);

            Assert.IsTrue(result.IsFailure);
        }
    }
}
