using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.Domain.Dto;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Test.TestClasses
{
    [TestClass]
    public class TimeScaleTest
    {
        [TestMethod]
        public void CanCreate_CountRecordsInRange_Success()
        {
            List<RecordDto> records = [
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 0, DateTimeKind.Utc), 2.3, 5),
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 0, DateTimeKind.Utc), 3, 4)
            ];

            Result result = TimeScale.CanCreate(records);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CanCreate_CountRecordsLessThanRange_Fail()
        {
            List<RecordDto> records = [];

            Result result = TimeScale.CanCreate(records);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        public void CanCreate_CountRecordsMoreThanRange_Fail()
        {
            RecordDto[] records = new RecordDto[10_001];

            Result result = TimeScale.CanCreate(records);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        public void Create_CountRecordsInRange_ObjectWithPassedFileName()
        {
            List<RecordDto> records = [
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 0, DateTimeKind.Utc), 2.3, 5),
                new RecordDto(new DateTime(2020, 01, 01, 0, 0, 0, DateTimeKind.Utc), 3, 4)
            ];
            string fileName = "test.csv";

            Result<TimeScale> result = TimeScale.Create(records, fileName);

            Assert.AreEqual(fileName, result.Value.FileName);
        }

        [TestMethod]
        public void Create_CountRecordsLessThanRange_Fail()
        {
            List<RecordDto> records = [];
            string fileName = "test.csv";

            Result<TimeScale> result = TimeScale.Create(records, fileName);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        public void Create_CountRecordsMoreThanRange_Fail()
        {
            RecordDto[] records = new RecordDto[10_001];
            string fileName = "test.csv";

            Result<TimeScale> result = TimeScale.Create(records, fileName);

            Assert.IsTrue(result.IsFailure);
        }
    }
}