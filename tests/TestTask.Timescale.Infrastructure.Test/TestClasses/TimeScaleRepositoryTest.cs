using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.Domain.Dto;
using TestTask.Timescale.Infrastructure.Repositories;
using TestTask.Timescale.Infrastructure.Test.Tools;

namespace TestTask.Timescale.Infrastructure.Test.TestClasses
{
    [TestClass]
    public class TimeScaleRepositoryTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Ctor_ContextIsNull_Throw()
        {
            ApplicationDbContext? context = null;

            void action() => _ = new TimeScaleRepository(context!);

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
            TimeScaleRepository repository = new(context);

            repository.Add(timeScale);
            await context.SaveChangesAsync(TestContext.CancellationToken);

            context.ChangeTracker.Clear();
            TimeScale? addedTimeScale = await repository.GetByFileNameAsync(fileName, TestContext.CancellationToken);
            Assert.IsNotNull(addedTimeScale);
        }

        [TestMethod]
        public async Task Add_Exist_Throw()
        {
            string fileName = "file3.csv";
            ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            TimeScaleRepository repository = new(context);
            TimeScale? timeScale = await repository.GetByFileNameAsync(fileName, TestContext.CancellationToken);

            repository.Add(timeScale!);
            async Task action() => await context.SaveChangesAsync(TestContext.CancellationToken);

            await Assert.ThrowsAsync<Exception>(action);
        }

        [TestMethod]
        public async Task Delete_Exist_Deleted()
        {
            string fileName = "file1.csv";
            ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            TimeScaleRepository repository = new(context);
            TimeScale? timeScale = await repository.GetByFileNameAsync(fileName, TestContext.CancellationToken);

            repository.Delete(timeScale!);
            await context.SaveChangesAsync(TestContext.CancellationToken);

            context.ChangeTracker.Clear();
            TimeScale? addedTimeScale = await repository.GetByFileNameAsync(fileName, TestContext.CancellationToken);
            Assert.IsNull(addedTimeScale);
        }

        [TestMethod]
        public async Task Delete_NotExist_Throw()
        {
            string fileName = "file999.csv";
            RecordDto[] records = [
                new RecordDto (new DateTime(2026, 05, 20, 14, 00, 00, 000, DateTimeKind.Utc), 10.150, 101.3)
            ];
            TimeScale timeScale = TimeScale.Create(records, fileName).Value;
            ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            TimeScaleRepository repository = new(context);

            async Task action() => repository.Delete(timeScale);

            await Assert.ThrowsAsync<Exception>(action);
        }

        [TestMethod]
        public async Task DeleteAync_Exist_Deleted()
        {
            int id = 1;
            ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            TimeScaleRepository repository = new(context);

            await repository.DeleteAsync(id, TestContext.CancellationToken);
            await context.SaveChangesAsync(TestContext.CancellationToken);

            context.ChangeTracker.Clear();
            TimeScale? deletedTimeScale = await repository.GetByIdAsync(id, TestContext.CancellationToken);
            Assert.IsNull(deletedTimeScale);
        }

        [TestMethod]
        public async Task DeleteAync_NotExist_Nothing()
        {
            int id = 999;
            ApplicationDbContext context = DbFactory.GetTransactionAppContext();
            TimeScaleRepository repository = new(context);

            await repository.DeleteAsync(id, TestContext.CancellationToken);
            await context.SaveChangesAsync(TestContext.CancellationToken);

            context.ChangeTracker.Clear();
            TimeScale? deletedTimeScale = await repository.GetByIdAsync(id, TestContext.CancellationToken);
            Assert.IsNull(deletedTimeScale);
        }

        [TestMethod]
        public async Task GetByIdAsync_EntityExist_Entity()
        {
            int id = 1;
            TimeScaleRepository repository = new(DbFactory.GetReadOnlyAppContext());

            TimeScale? timeScale = await repository.GetByIdAsync(id, TestContext.CancellationToken);

            Assert.IsNotNull(timeScale);
            Assert.AreEqual(id, timeScale.Id);
        }

        [TestMethod]
        public async Task GetByIdAsync_EntityNotExist_Null()
        {
            int id = 999;
            TimeScaleRepository repository = new(DbFactory.GetReadOnlyAppContext());

            TimeScale? timeScale = await repository.GetByIdAsync(id, TestContext.CancellationToken);

            Assert.IsNull(timeScale);
        }

        [TestMethod]
        public async Task GetByFileNameAsync_EntityExist_Entity()
        {
            string fileName = "file2.csv";
            TimeScaleRepository repository = new(DbFactory.GetReadOnlyAppContext());

            TimeScale? timeScale = await repository.GetByFileNameAsync(fileName, TestContext.CancellationToken);

            Assert.IsNotNull(timeScale);
            Assert.AreEqual(fileName, timeScale.FileName);
        }

        [TestMethod]
        public async Task GetByFileNameAsync_EntityNotExist_Null()
        {
            string fileName = "file-1.csv";
            TimeScaleRepository repository = new(DbFactory.GetReadOnlyAppContext());

            TimeScale? timeScale = await repository.GetByFileNameAsync(fileName, TestContext.CancellationToken);

            Assert.IsNull(timeScale);
        }
    }
}
