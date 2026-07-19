using TestTask.Timescale.Domain.Aggregates.RecordAggregate;
using TestTask.Timescale.Domain.Dto;
using TestTask.Timescale.SharedKernel.Domain.BaseModels;
using TestTask.Timescale.SharedKernel.Domain.Results;

namespace TestTask.Timescale.Domain.Test.TestClasses
{
    [TestClass]
    public class RecordTest
    {
        [TestMethod]
        public void CanCreate_AllPropertiesValid_Success()
        {
            RecordDto dto = new(new DateTime(2020, 01, 01, 0, 0, 0, DateTimeKind.Utc), 2.3, 5);

            Result result = Record.CanCreate(dto);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        [DataRow(2020, 2.3, -1)]
        [DataRow(2020, -1, 1)]
        [DataRow(1999, 2.3, 1)]
        public void CanCreate_SomePropertiesNotValid_Fail(int year, double seconds, double value)
        {
            RecordDto dto = new(new DateTime(year, 01, 01, 0, 0, 0, DateTimeKind.Utc), seconds, value);

            Result result = Record.CanCreate(dto);

            Assert.IsTrue(result.IsFailure);
        }

        [TestMethod]
        public void Create_AllPropertiesValid_ObjectWithCorrectValueObject()
        {
            RecordDto dto = new(new DateTime(2020, 01, 01, 0, 0, 0, DateTimeKind.Utc), 2.3, 5);
            Timestamp expectedDate = Timestamp.Create(dto.Date).Value;
            ExecutionDuration expectedSeconds = ExecutionDuration.Create(dto.ExecutionTime).Value;
            RecordValue expectedValue = RecordValue.Create(dto.Value).Value;

            Result<Record> result = Record.Create(new EntityId(), dto);

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
            RecordDto dto = new(new DateTime(year, 01, 01, 0, 0, 0, DateTimeKind.Utc), seconds, value);

            Result<Record> result = Record.Create(new EntityId(), dto);

            Assert.IsTrue(result.IsFailure);
        }
    }
}
