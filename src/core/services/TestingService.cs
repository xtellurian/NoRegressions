using System;
using System.Threading.Tasks;
using core.Contract;
using core.Model;
using core.Target;
using Microsoft.Extensions.DependencyInjection;

namespace core.Services
{
    public class TestingService : ITestingService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IDatasetService datasetService;
        private readonly IDatasetResolverService datasetResolver;
        private readonly ILogger logger;
        private readonly ITestResultStore resultStore;

        public TestingService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.datasetService = serviceProvider.GetService<IDatasetService>();
            this.datasetResolver = serviceProvider.GetService<IDatasetResolverService>();
            this.logger = serviceProvider.GetService<ILogger>();
            this.resultStore = serviceProvider.GetService<ITestResultStore>();
        }

        public async Task TestTargetAsync(string target, string datasetId)
        {
            logger.Log($"Targetting {target} with dataset {datasetId}");
            var emptyset = await datasetService.Get<EmptyDataset>(datasetId);
            logger.Log($"Loaded dataset {emptyset.Id}");
            switch(target)
            {
                case Targets.CustomVision:
                    var tester = serviceProvider.GetService<ICustomVisionTester>();
                    var dataset = await datasetResolver.ResolveSingleClassImage(emptyset);
                    var result = await tester.Run(dataset);
                    logger.Log($"Fraction passed: {result.PercentagePassed}");
                    await resultStore.LogTestResultAsync(result);
                    break;
            }
        }
    }
}