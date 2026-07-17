using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Test.TestClasses
{
    [TestClass]
    public class TimestampTest
    {
        [TestMethod]
        public void CanCreate_DateInRange_Success()
        {
            DateTime date = new(2020, 01, 01);

            Result result = Timestamp.CanCreate(date);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        [DataRow(1999)]
        [DataRow(9999)]
        public void CanCreate_DateOutRange_Fail(int year)
        {
            DateTime date = new(year, 01, 01);

            Result result = Timestamp.CanCreate(date);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        public void Create_DateInRange_ObjectWithPassedDateTime()
        {
            DateTime date = new(2020, 01, 01);

            Result<Timestamp> result = Timestamp.Create(date);

            Assert.AreEqual(date, result.Value.Value);
        }

        [TestMethod]
        [DataRow(1999)]
        [DataRow(9999)]
        public void Create_DateOutRange_Fail(int year)
        {
            DateTime date = new(year, 01, 01);

            Result<Timestamp> result = Timestamp.Create(date);

            Assert.IsTrue(result.IsFailure);
        }
    }
}
