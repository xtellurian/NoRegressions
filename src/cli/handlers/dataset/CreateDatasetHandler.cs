using System;
using System.Threading.Tasks;
using cli.Contract;
using cli.Verbs.Dataset;
using core.Contract;
using core.Model;

namespace cli.Handlers.Dataset
{
    public class CreateDatasetHandler : ICreateDatasetHandler
    {
        private readonly ILogger logger;
        private readonly IDatasetService datasetService;

        public CreateDatasetHandler(ILogger logger, IDatasetService datasetService)
        {
            this.logger = logger;
            this.datasetService = datasetService;
        }

        public async Task Handle(CreateDatasetVerb verb)
        {
            if (string.IsNullOrEmpty(verb.Id))
            {
                verb.Id = Guid.NewGuid().ToString();
            }
            logger.Log($"Creating dataset {verb.Id}");
            var emptyDataset = new EmptyDataset(verb.Id);
            await datasetService.Set(emptyDataset);
        }
    }
}