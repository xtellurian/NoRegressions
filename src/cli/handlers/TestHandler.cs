using System;
using System.Threading.Tasks;
using cli.Contract;
using cli.Verbs;
using core;
using core.Contract;
using core.Model;
using core.Target;

namespace cli.Handlers
{
    public class TestHandler : ITestHandler
    {
        private readonly ITestingService testingService;

        //private readonly ITester<ITestTarget<ISingleClassImageDataset>, ISingleClassImageDataset> customVisionTester;
        private readonly ILogger logger;

        public TestHandler(ITestingService testingService, ILogger logger)
        {
            this.testingService = testingService;
            this.logger = logger;
        }
        
        public async Task Handle(TestVerb verb)
        {
            try
            {
                await testingService.TestTargetAsync(verb.Target, verb.DatasetId);
                logger.Log("Done Testing");
            }
            catch (Exception ex)
            {
                logger.Error("Error testing", ex);
            }            
        }
    }
}