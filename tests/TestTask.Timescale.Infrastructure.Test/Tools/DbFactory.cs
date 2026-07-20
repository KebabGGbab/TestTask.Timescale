using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Timescale.Infrastructure.Test.Tools
{
    internal static class DbFactory
    {
        private static readonly List<DbHandlerBase<ApplicationDbContext>> _dbHandlers = [
            new ReadOnlyAppDbHandler(),
            new TransactionAppDbHandler(),
            new RefreshingAppDbHander()
        ];

        // Cоздает котекст, который ДОЛЖЕН использоваться только для чтения в тестах
        public static ApplicationDbContext GetReadOnlyAppContext()
        {
            return _dbHandlers.OfType<ReadOnlyAppDbHandler>().First().GetDbContext();
        }

        // Создает контекст, который может использоваться для записи.
        public static ApplicationDbContext GetTransactionAppContext()
        {
            return _dbHandlers.OfType<TransactionAppDbHandler>().First().GetDbContext();
        }

        // Создает контект, который может использоваться для тестирования кода, который использует транзакции
        public static async Task<ApplicationDbContext> GetRefreshingAppContextAsync()
        {
            RefreshingAppDbHander handler = _dbHandlers.OfType<RefreshingAppDbHander>().First();
            await handler.Refresh();
            return handler.GetDbContext();
        }

        public static async Task InitializeAsync()
        {
            foreach (DbHandlerBase<ApplicationDbContext> handler in _dbHandlers)
            {
                await handler.InitializeAsync();
            }
        }

        public static async Task DeinitializeAsync()
        {
            foreach (DbHandlerBase<ApplicationDbContext> handler in _dbHandlers)
            {
                await handler.DeinitializeAsync();
            }
        }

        private abstract class DbHandlerBase<T> 
            where T : DbContext
        {
            protected string ConnectionString { get; }

            public DbHandlerBase(string connectionString)
            {
                ConnectionString = connectionString;
            }

            public async Task InitializeAsync()
            {
                using T dbContext = CreateDbContext();
                await dbContext.Database.EnsureDeletedAsync();
                await dbContext.Database.EnsureCreatedAsync();
            }

            public async Task DeinitializeAsync()
            {
                using T dbContext = CreateDbContext();
                await dbContext.Database.EnsureDeletedAsync();
            }

            protected abstract T CreateDbContext();

            public abstract T GetDbContext();
        }

        private abstract class AppDbHanderBase : DbHandlerBase<ApplicationDbContext>
        {
            public AppDbHanderBase(string connectionString)
                : base(connectionString)
            { 
            }

            protected override ApplicationDbContext CreateDbContext()
            {
                return new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseNpgsql(ConnectionString)
                    .UseSeeding(TimeScaleContextSeeder.Seed)
                    .UseAsyncSeeding(TimeScaleContextSeeder.SeedAsync)
                    .LogTo(Log)
                    .EnableSensitiveDataLogging()
                    .Options);
            }

            public override ApplicationDbContext GetDbContext()
            {
                return CreateDbContext();
            }

            private void Log(string message)
            {
                Debug.WriteLine(message);
            }
        }

        // Cоздает котекст, который ДОЛЖЕН использоваться только для чтения в тестах
        private class ReadOnlyAppDbHandler : AppDbHanderBase
        {
            private const string CONNECTION_STRING = "Host=localhost:5432;Database=TimeScaleReadOnlyDb;Username=postgres;Password=1564";
            public ReadOnlyAppDbHandler() :
                base(CONNECTION_STRING)
            { 
            }
        }

        // Создает контекст, который может использоваться для записи.
        private class TransactionAppDbHandler : AppDbHanderBase
        {
            private const string CONNECTIONSTRING = "Host=localhost:5432;Database=TimeScaleTransactionDb;Username=postgres;Password=1564";

            public TransactionAppDbHandler()
                : base(CONNECTIONSTRING)
            { 
            }

            public override ApplicationDbContext GetDbContext()
            {
                ApplicationDbContext context = base.GetDbContext();
                context.Database.BeginTransaction();

                return context;
            }
        }

        // Создает контект, который может использоваться для тестирования кода, который использует транзакции
        private class RefreshingAppDbHander : AppDbHanderBase
        {
            private const string CONNECTIONSTRING = "Host=localhost:5432;Database=TimeScaleRefreshingDb;Username=postgres;Password=1564";

            public RefreshingAppDbHander()
                : base(CONNECTIONSTRING)
            { }

            public async Task Refresh()
            {
                await InitializeAsync();
            }
        }
    }
}
