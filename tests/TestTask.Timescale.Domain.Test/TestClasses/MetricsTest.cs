using TestTask.Timescale.Domain.Aggregates.MetricsAggregate;
using TestTask.Timescale.Domain.Dto;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Test.TestClasses
{
    [TestClass]
    public class MetricsTest
    {
        [TestMethod]
        public void CanCreate_CountRecordMoreThanZero_Success()
        {
            List<RecordDto> records = [
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 0, DateTimeKind.Utc), 1d, 5d),
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 1, DateTimeKind.Utc), 2d, 4d),
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 3, DateTimeKind.Utc), 1.5d, 3.7d)
            ];

            Result result = Metrics.CanCreate(records);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CanCreate_CountRecordEqualsZero_Fail()
        {
            List<RecordDto> records = [];

            Result result = Metrics.CanCreate(records);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        public void Create_CountRecordEqualsZero_Fail()
        {
            List<RecordDto> records = [];

            Result<Metrics> result = Metrics.Create(0, records);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        public void Create_OddCountRecord_MedianEqualsMiddleValue()
        {
            double middleValue = 4d;
            List<RecordDto> records = [
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 0, DateTimeKind.Utc), 1d, 5d),
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 1, DateTimeKind.Utc), 2d, middleValue),
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 3, DateTimeKind.Utc), 1.5d, 3.7d)
            ];

            Metrics metrics = Metrics.Create(0, records).Value;

            Assert.AreEqual(middleValue, metrics.MedianValue);
        }

        [TestMethod]
        public void Create_EvenCountRecord_MedianEqualsArithmeticTwoMiddleValues()
        {
            double firstMiddle = 4d;
            double secondMiddle = 3.7d;
            List<RecordDto> records = [
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 0, DateTimeKind.Utc), 1d, 5d),
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 1, DateTimeKind.Utc), 2d, firstMiddle),
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 3, DateTimeKind.Utc), 1.5d, secondMiddle),
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 4, 500, DateTimeKind.Utc), 1.5d, 7.8d)
            ];

            Metrics metrics = Metrics.Create(0, records).Value;

            Assert.AreEqual((firstMiddle + secondMiddle) / 2, metrics.MedianValue);
        }

        [TestMethod]
        public void Create_Simple_CorrectCalculations()
        {
            DateTime firstDate = new(2020, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            List<RecordDto> records = [
                new RecordDto(firstDate, 1d, 5d),
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 1, DateTimeKind.Utc), 2d, 4d),
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 3, DateTimeKind.Utc), 1.5d, 3.7d),
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 4, 500, DateTimeKind.Utc), 1.5d, 7.8d)
            ];

            Metrics metrics = Metrics.Create(0, records).Value;

            Assert.AreEqual(4.5d, metrics.DeltaDate);
            Assert.AreEqual(firstDate, metrics.MinDate);
            Assert.AreEqual(1.5d, metrics.AvgExecutionDuration);
            Assert.AreEqual(5.13d, metrics.AvgValue);
            Assert.AreEqual(3.85d, metrics.MedianValue);
            Assert.AreEqual(3.7d, metrics.MinValue);
            Assert.AreEqual(7.8d, metrics.MaxValue);
        }
    }
}
