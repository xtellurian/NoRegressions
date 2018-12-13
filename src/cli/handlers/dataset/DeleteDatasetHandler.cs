using System;
using System.Linq;
using System.Threading.Tasks;
using cli.Contract;
using cli.Verbs.Dataset;
using core.Contract;
using core.Model;

namespace cli.Handlers.Dataset
{
    public class DeleteDatasetHandler : IDeleteDatasetHandler
    {
        private readonly ILogger logger;
        private readonly IDatasetService datasetService;
        private readonly IDatasetResolverService resolverService;

        public DeleteDatasetHandler(ILogger logger, IDatasetService datasetService, IDatasetResolverService resolverService)
        {
            this.logger = logger;
            this.datasetService = datasetService;
            this.resolverService = resolverService;
        }

        public async Task Handle(DeleteDatasetVerb verb)
        {
            await datasetService.Delete(verb.Id);
            logger.Log($"Deleted {verb.Id}");
        }
    }
}