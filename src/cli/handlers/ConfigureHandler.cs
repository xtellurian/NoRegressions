using System;
using System.IO;
using System.Threading.Tasks;
using cli.Contract;
using cli.Verbs;
using cli.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using core.Contract;
using System.Linq;

namespace cli.Handlers
{
    public class ConfigureHandler : IConfigureHandler
    {
        private readonly ILogger logger;

        public ConfigureHandler(ILogger logger)
        {
            this.logger = logger;
        }
        
        public Task Handle(ConfigureVerb verb)
        {
            return Task.Factory.StartNew(() =>
            {
                if (!string.IsNullOrEmpty(verb.AzureBlobConnectionString))
                {
                    WriteAzureBlobToConfig(new AzureBlob() { 
                        connectionString = verb.AzureBlobConnectionString, 
                        isDefault = verb.IsDefault });
                }
                if (!string.IsNullOrEmpty(verb.Key))
                {
                    switch (verb.Key)
                    {
                        case "cv-key":
                            WriteCustomVisionKeyToConfig(verb.Value);
                            break;
                        case "cv-endpoint":
                            WriteCustomVisionEndpointToConfig(verb.Value);
                            break;
                    }
                }
            });
        }

        private void WriteCustomVisionKeyToConfig(string predKey)
        {
            var model = LoadConfigurationModel();
            if (model.CustomVision == null)
            {
                model.CustomVision = new CustomVisionConfig();
            }
            model.CustomVision.PredictionKey = predKey;
            logger.Log($"Set Custom Vision Prediction key to {predKey}");
            SaveConfigurationModel(model);
        }

        private void WriteCustomVisionEndpointToConfig(string endpoint)
        {
            var model = LoadConfigurationModel();
            if (model.CustomVision == null)
            {
                model.CustomVision = new CustomVisionConfig();
            }
            model.CustomVision.Endpoint = endpoint;
            logger.Log($"Set Custom Vision Endpoint to {endpoint}");
            SaveConfigurationModel(model);
        }

        private void WriteAzureBlobToConfig(dynamic value)
        {
            var jsonObj = LoadConfigurationModel();
            jsonObj.storage.remote.azureBlob = value;
            SaveConfigurationModel(jsonObj);
        }

        private static void SaveConfigurationModel(ConfigurationModel jsonObj)
        {
            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText("configsettings.json", output);
        }

        private static ConfigurationModel LoadConfigurationModel()
        {
            string json = File.ReadAllText("configsettings.json");
            var jsonObj = JsonConvert.DeserializeObject<ConfigurationModel>(json);
            return jsonObj;
        }
    }
}