using TestTask.Timescale.Domain.Aggregates.MetricsAggregate;
using TestTask.Timescale.Domain.Aggregates.RecordAggregate;
using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.Domain.Dto;
using TestTask.Timescale.Infrastructure.Repositories;
using TestTask.Timescale.Infrastructure.Test.Tools;

namespace TestTask.Timescale.Infrastructure.Test.TestClasses
{
    [TestClass]
    public class RecordRepositoryTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Ctor_ContextIsNull_Throw()
        {
            ApplicationDbContext? context = null;

            void action() => _ = new RecordRepository(context!);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public async Task Add_OneAndNotExist_Added()
        {
            string fileName = "file.csv";
            RecordDto recordDto = new(new DateTime(2026, 05, 20, 14, 00, 00, 000, DateTimeKind.Utc), 10.150, 101.3);
            TimeScale timeScale = TimeScale.Create([recordDto], fileName).Value;
            ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            RecordRepository repository = new(context);
            context.TimeScales.Add(timeScale);
            await context.SaveChangesAsync(TestContext.CancellationToken);
            Record record = Record.Create(timeScale.Id, recordDto).Value;

            repository.Add(record);
            await context.SaveChangesAsync(TestContext.CancellationToken);

            context.ChangeTracker.Clear();
            IEnumerable<Record> addedRecord = await repository.GetRecordsByFileNameAsync(fileName, TestContext.CancellationToken);
            Assert.IsNotEmpty(addedRecord);
        }

        [TestMethod]
        public async Task Add_OneAndExist_Throw()
        {
            string fileName = "file3.csv";
            ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            RecordRepository repository = new(context);
            Record record = (await repository.GetRecordsByFileNameAsync(fileName, TestContext.CancellationToken)).First();

            repository.Add(record);
            async Task action() => await context.SaveChangesAsync(TestContext.CancellationToken);

            await Assert.ThrowsAsync<Exception>(action);
        }

        [TestMethod]
        public async Task Add_RangeAndNotExist_Added()
        {
            string fileName = "file.csv";
            RecordDto recordDto = new(new DateTime(2026, 05, 20, 14, 00, 00, 000, DateTimeKind.Utc), 10.150, 101.3);
            TimeScale timeScale = TimeScale.Create([recordDto], fileName).Value;
            ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            RecordRepository repository = new(context);
            context.TimeScales.Add(timeScale);
            await context.SaveChangesAsync(TestContext.CancellationToken);
            IEnumerable<Record> records = [
                Record.Create(timeScale.Id, recordDto).Value
            ];

            repository.Add(records);
            await context.SaveChangesAsync(TestContext.CancellationToken);

            context.ChangeTracker.Clear();
            IEnumerable<Record> addedRecord = await repository.GetRecordsByFileNameAsync(fileName, TestContext.CancellationToken);
            Assert.IsNotEmpty(addedRecord);
        }

        [TestMethod]
        public async Task Add_RangeAndExist_Throw()
        {
            string fileName = "file3.csv";
            ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            RecordRepository repository = new(context);
            IEnumerable<Record> records = await repository.GetRecordsByFileNameAsync(fileName, TestContext.CancellationToken);

            repository.Add(records);
            async Task action() => await context.SaveChangesAsync(TestContext.CancellationToken);

            await Assert.ThrowsAsync<Exception>(action);
        }

        [TestMethod]
        public async Task GetByFileNameAsync_RecordsExists_Records()
        {
            string fileName = "file2.csv";
            RecordRepository repository = new(DbFactory.GetReadOnlyAppContext());

            IEnumerable<Record> records = await repository.GetRecordsByFileNameAsync(fileName, TestContext.CancellationToken);

            Assert.IsNotEmpty(records);
            Assert.AreEqual(2, records.First().TimeScaleId);
        }

        [TestMethod]
        public async Task GetByFileNameAsync_MetricsNotExists_Null()
        {
            string fileName = "file999.csv";
            RecordRepository repository = new(DbFactory.GetReadOnlyAppContext());

            IEnumerable<Record> records = await repository.GetRecordsByFileNameAsync(fileName, TestContext.CancellationToken);

            Assert.IsEmpty(records);
        }

        [TestMethod]
        public async Task GetLastByFileNameAndOrderByDateAsync_GetTwoRecord_TwoLatestRecordWithPassedFileName()
        {
            int count = 2;
            string fileName = "file2.csv";
            RecordRepository repository = new(DbFactory.GetReadOnlyAppContext());

            IEnumerable<Record>? records = await repository.GetLastByFileNameAndOrderByDateAsync(fileName, count, TestContext.CancellationToken);

            Assert.HasCount(count, records);
            Assert.Contains(record => record.Date.Value == new DateTime(2024, 11, 12, 06, 31, 01, 550, DateTimeKind.Utc), records);
            Assert.Contains(record => record.Date.Value == new DateTime(2024, 11, 12, 06, 31, 15, 650, DateTimeKind.Utc), records);
        }
    }
}
