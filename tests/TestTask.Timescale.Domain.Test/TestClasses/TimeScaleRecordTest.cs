using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Test.TestClasses
{
    [TestClass]
    public class TimeScaleRecordTest
    {
        [TestMethod]
        public void CanCreate_AllPropertiesValid_Success()
        {
            DateTime date = new(2020, 01, 01);
            double seconds = 2.3d;
            double value = 2.3d;

            Result result = TimeScaleRecord.CanCreate(date, seconds, value);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        [DataRow(2020, 2.3, -1)]
        [DataRow(2020, -1, 1)]
        [DataRow(1999, 2.3, 1)]
        public void CanCreate_SomePropertiesNotValid_Fail(int year, double seconds, double value)
        {
            DateTime date = new(year, 01, 01);

            Result result = TimeScaleRecord.CanCreate(date, seconds, value);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        public void Create_AllPropertiesValid_ObjectWithCorrectValueObject()
        {
            DateTime date = new(2020, 01, 01);
            double seconds = 2.3d;
            double value = 2.3d;
            Timestamp expectedDate = Timestamp.Create(date).Value;
            ExecutionTime expectedSeconds = ExecutionTime.Create(seconds).Value;
            TimeScaleRecordValue expectedValue = TimeScaleRecordValue.Create(value).Value;

            Result<TimeScaleRecord> result = TimeScaleRecord.Create(date, seconds, value);

            Assert.AreEqual(expectedDate, result.Value.Date);
            Assert.AreEqual(expectedSeconds, result.Value.Time);
            Assert.AreEqual(expectedValue, result.Value.Value);
        }

        [TestMethod]
        [DataRow(2020, 2.3, -1)]
        [DataRow(2020, -1, 1)]
        [DataRow(1999, 2.3, 1)]
        public void Create_SomePropertiesNotValid_Fail(int year, double seconds, double value)
        {
            DateTime date = new(year, 01, 01);

            Result<TimeScaleRecord> result = TimeScaleRecord.Create(date, seconds, value);

            Assert.IsTrue(result.IsFailure);
        }
    }
}
