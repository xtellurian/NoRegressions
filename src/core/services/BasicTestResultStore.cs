using System.Threading.Tasks;
using core.Contract;

namespace core.Services
{
    public class BasicTestResultStore : ITestResultStore
    {
        private readonly ILogger logger;

        public BasicTestResultStore(ILogger logger)
        {
            this.logger = logger;
            logger.Log("Using Basic Logger. Test results will not be persisted.");
        }
        public Task LogTestResultAsync(ITestResult result)
        {
            return Task.CompletedTask;
        }
    }
}