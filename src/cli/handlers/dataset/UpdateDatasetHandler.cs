using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using cli.Contract;
using cli.Verbs.Dataset;
using core.Contract;
using core.Model;

namespace cli.Handlers.Dataset
{
    public class UpdateDatasetHandler : IUpdateDatasetHandler
    {
        private readonly ILogger logger;
        private readonly IDatasetService datasetService;
        private readonly IDatasetResolverService resolverService;
        private readonly IFileParserService fileParser;

        public UpdateDatasetHandler(ILogger logger,
            IDatasetService datasetService,
            IDatasetResolverService resolverService,
            IFileParserService fileParser)
        {
            this.logger = logger;
            this.datasetService = datasetService;
            this.resolverService = resolverService;
            this.fileParser = fileParser;
        }

        public async Task Handle(UpdateDatasetVerb verb)
        {
            IDataset emptySet;

            try 
            {
                emptySet = await datasetService.Get<EmptyDataset>(verb.Id);
            }
            catch (FileNotFoundException) 
            {
                logger.Error(null, new FileNotFoundException($"Dataset does not exist. Please use 'create-dataset --id {verb.Id}' before using 'update-dataset'."));
                return;
            }

            emptySet.Type = DatasetTypes.FromValue(verb.Type);

            if (verb.Replace) {
                await datasetService.Set(emptySet);
            }

            if (string.Equals(DatasetTypes.SingleClassImage.Value, verb.Type))
            {
                // will have to branch here in future based on different types of sets
                var dataset = await resolverService.ResolveSingleClassImage(emptySet);

                if (StringExists(verb.FromFile) && StringExists(verb.Label))
                {
                    // either this is an empty set, or the set exists and the user has chosen the right type
                    // depending on the type, we load the dataset
                    logger.Log($"Updating dataset {verb.Id} with file {verb.FromFile} and label {verb.Label}");
                    var urls = fileParser.LinesFromFile(verb.FromFile);
                    foreach (var url in urls)
                    {
                        logger.Log($"adding {url}");
                        // add the urls
                        dataset.AddLabelledImage(verb.Label, url);
                    }

                    await datasetService.Set(dataset);

                }
                else if (StringExists(verb.Url) && StringExists(verb.Label))
                {
                    if (!DatasetTypes.IsValid(verb.Type))
                    {
                        logger.Log($"{verb.Type} is an invalid dataset type");
                        logger.Log($"Available types are {DatasetTypes.PrintTypes()}");
                        return;
                    }
                    dataset.Type = DatasetTypes.FromValue(verb.Type);
                    logger.Log($"Adding url {verb.Url} as {verb.Label}");
                    dataset.AddLabelledImage(verb.Label, verb.Url);
                    await datasetService.Set(dataset);
                }
            }
            else if (string.Equals(verb.Type, DatasetTypes.Empty.Value))
            {
                logger.Log($"Dataset is still typed as Empty. Use --type");
            }
            else
            {
                logger.Log($"Unknown type {verb.Type}");
            }
        }

        private bool StringExists(string s)
        {
            return !string.IsNullOrEmpty(s);
        }
    }
}