using Microsoft.EntityFrameworkCore;
using TestTask.Timescale.Domain.Aggregates.MetricsAggregate;
using TestTask.Timescale.Domain.Aggregates.RecordAggregate;
using TestTask.Timescale.Domain.Aggregates.TimeScaleAggregate;
using TestTask.Timescale.Infrastructure.Test.Tools;

namespace TestTask.Timescale.Infrastructure.Test.TestClasses
{
    [TestClass]
    public class ReadTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public async Task Read_ForeignKeysSetCorrectly()
        {
            ApplicationDbContext db = DbFactory.GetReadOnlyAppContext();

            TimeScale timeScale = await db.TimeScales.FirstAsync(TestContext.CancellationToken);
            List<Record> records = await db.Records.Where(x => x.TimeScaleId == timeScale.Id).ToListAsync(TestContext.CancellationToken);
            Metrics? metrics = await db.Metrics.FirstOrDefaultAsync(x => x.TimeScaleId == timeScale.Id, TestContext.CancellationToken);

            Assert.IsNotEmpty(records);
            Assert.IsNotNull(metrics);
        }
    }
}
