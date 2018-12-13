using System;
using System.Linq;
using System.Threading.Tasks;
using cli.Contract;
using cli.Verbs.Dataset;
using core.Contract;
using core.Model;

namespace cli.Handlers.Dataset
{
    public class ListDatasetHandler : IListDatasetHandler
    {
        private readonly ILogger logger;
        private readonly IDatasetService datasetService;
        private readonly IDatasetResolverService resolverService;

        public ListDatasetHandler(ILogger logger, IDatasetService datasetService, IDatasetResolverService resolverService)
        {
            this.logger = logger;
            this.datasetService = datasetService;
            this.resolverService = resolverService;
        }

        public async Task Handle(ListDatasetVerb verb)
        {
            var datasets = await datasetService.List();
            logger.Log($"Datasets ({datasets.Count()}) ");
            foreach(var d in datasets)
            {
                logger.Log(d);
            }
        }

        private bool StringExists(string s)
        {
            return !string.IsNullOrEmpty(s);
        }
    }
}