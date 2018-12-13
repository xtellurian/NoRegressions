using System;
using System.Linq;
using System.Threading.Tasks;
using core.Contract;
using core.Exceptions;
using core.Results;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace core.Testers.CustomVision
{
    public class CustomVisionTester : ICustomVisionTester
    {
        private readonly ILogger logger;
        private readonly ITestTarget target;

        public CustomVisionTester(ILogger logger,
        ITestTarget target)
        {
            this.logger = logger;
            this.target = target;
        }

        public IDataset Dataset {get;set;}

        public async Task<ITestResult> Run(ISingleClassImageDataset dataset)
        {
            var results = new CustomVisionTestResult();
            results.Id = DateTime.Now.ToShortDateString() + "|" + DateTime.Now.ToShortTimeString();
            results.SetParam("dataset_id", dataset.Id);
            foreach(var kvp in target.Properties)
            {
                results.SetParam($"target_{kvp.Key}", kvp.Value);
            }
            // is there a better way to load this??
            logger.Log($"There are {dataset.LabelledImages.Count()} images in the set");

            foreach (var line in dataset.LabelledImages)
            {
                logger.Log($"Looking at {line.ImageUrl}");
                try
                {
                    var prediction = await target.PredictClassFromUrl(line.ImageUrl);
                    if (string.Equals(prediction, line.Label))
                    {
                        logger.Log("Prediction passed");
                        results.AddPassed();
                    }
                    else
                    {
                        logger.Log($"Failed prediction. Expected {line.Label}, got {prediction}");
                        results.AddFailed();
                    }
                }
                catch (CustomVisionException ex)
                {
                    logger.Log($"Error: {ex.Message}");
                }
            }
            return results;
        }
    }
}