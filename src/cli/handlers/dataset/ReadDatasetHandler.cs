using System;
using System.Linq;
using System.Threading.Tasks;
using cli.Contract;
using cli.Verbs.Dataset;
using core.Contract;
using core.Model;

namespace cli.Handlers.Dataset
{
    public class ReadDatasetHandler : IReadDatasetHandler
    {
        private readonly ILogger logger;
        private readonly IDatasetService datasetService;
        private readonly IDatasetResolverService resolverService;

        public ReadDatasetHandler(ILogger logger, IDatasetService datasetService, IDatasetResolverService resolverService)
        {
            this.logger = logger;
            this.datasetService = datasetService;
            this.resolverService = resolverService;
        }

        public async Task Handle(ReadDatasetVerb verb)
        {
            if (!StringExists(verb.Id))
            {
                logger.Log("use --id <id> to choose a dataset");
                return;
            }
            var dataset = await datasetService.Get<EmptyDataset>(verb.Id);

            if (DatasetTypes.Empty.Equals(dataset.Type))
            {
                // emtpy set
                logger.Log($"{dataset.Id} is an empty dataset");
            }
            else if (DatasetTypes.SingleClassImage.Equals(dataset.Type))
            {
                var resolved = await resolverService.ResolveSingleClassImage(dataset);
                logger.Log($"{resolved.Id} is of type {resolved.Type}");
                logger.Log($"There are {resolved.LabelledImages.Count()} images in the set");
                var labelCount = resolved.LabelledImages.Select(i => i.Label).Distinct().Count();
                logger.Log($"There are {labelCount} labels");
            }
            else
            {
                logger.Log($"{dataset.Type} is an unknown type");
            }
        }

        private bool StringExists(string s)
        {
            return !string.IsNullOrEmpty(s);
        }
    }
}