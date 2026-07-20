using TestTask.Timescale.Infrastructure.Test.Tools;

namespace TestTask.Timescale.Infrastructure.Test.Initialization
{
    [TestClass]
    public static class DbInitializer
    {
        [AssemblyInitialize]
        public static async Task SetupAsync(TestContext context)
        {
            await DbFactory.InitializeAsync();
        }

        [AssemblyCleanup]
        public static async Task CleanUp()
        {
            await DbFactory.DeinitializeAsync();
        }
    }
}
