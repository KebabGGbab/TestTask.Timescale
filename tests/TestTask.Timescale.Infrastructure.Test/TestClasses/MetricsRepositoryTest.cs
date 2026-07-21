using TestTask.Timescale.Domain.Aggregates.MetricsAggregate;
using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.Domain.Dto;
using TestTask.Timescale.Infrastructure.Repositories;
using TestTask.Timescale.Infrastructure.Test.Tools;

namespace TestTask.Timescale.Infrastructure.Test.TestClasses
{
    [TestClass]
    public class MetricsRepositoryTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Ctor_ContextIsNull_Throw()
        {
            ApplicationDbContext? context = null;

            void action() => _ = new MetricsRepository(context!);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public async Task Add_NotExist_Added()
        {
            string fileName = "file.csv";
            RecordDto[] records = [
                new RecordDto (new DateTime(2026, 05, 20, 14, 00, 00, 000, DateTimeKind.Utc), 10.150, 101.3)
            ];
            TimeScale timeScale = TimeScale.Create(records, fileName).Value;
            ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            MetricsRepository repository = new(context);
            context.TimeScales.Add(timeScale);
            await context.SaveChangesAsync(TestContext.CancellationToken);
            Metrics metrics = Metrics.Create(timeScale.Id, records).Value;

            repository.Add(metrics);
            await context.SaveChangesAsync(TestContext.CancellationToken);

            context.ChangeTracker.Clear();
            Metrics? addedMetrics = await repository.GetByFileNameAsync(fileName, TestContext.CancellationToken);
            Assert.IsNotNull(addedMetrics);
        }

        [TestMethod]
        public async Task Add_Exist_Throw()
        {
            string fileName = "file3.csv";
            ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            MetricsRepository repository = new(context);
            Metrics? metrics = await repository.GetByFileNameAsync(fileName, TestContext.CancellationToken);

            repository.Add(metrics!);
            async Task action() => await context.SaveChangesAsync(TestContext.CancellationToken);

            await Assert.ThrowsAsync<Exception>(action);
        }

        [TestMethod]
        public async Task GetByFileNameAsync_MetricsExists_Metrics()
        {
            string fileName = "file2.csv";
            MetricsRepository repository = new(DbFactory.GetReadOnlyAppContext());

            Metrics? metrics = await repository.GetByFileNameAsync(fileName, TestContext.CancellationToken);

            Assert.IsNotNull(metrics);
            Assert.AreEqual(2, metrics.TimeScaleId);
        }

        [TestMethod]
        public async Task GetByFileNameAsync_MetricsNotExists_Null()
        {
            string fileName = "file999.csv";
            MetricsRepository repository = new(DbFactory.GetReadOnlyAppContext());

            Metrics? metrics = await repository.GetByFileNameAsync(fileName, TestContext.CancellationToken);

            Assert.IsNull(metrics);
        }
    }
}
