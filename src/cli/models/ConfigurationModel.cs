using System.Collections.Generic;
using Newtonsoft.Json;

namespace cli.Models {
    public class ConfigurationModel {
        public StorageModel storage { get; set; }

        [JsonProperty("customVision")]
        public CustomVisionConfig CustomVision {get;set;}
    }
    public class CustomVisionConfig
    {
        [JsonProperty("predictionKey")]
        public string PredictionKey {get;set;}
        
        [JsonProperty("endpoint")]
        public string Endpoint {get;set;}
        [JsonProperty("iterationId")]
        public string IterationId {get;set;}
    }
    public class StorageModel {
        public RemoteModel remote { get; set; }
    }

    public class RemoteModel {
        public AzureBlob azureBlob { get; set; }
    }

    public class AzureBlob {
        public string connectionString { get; set; }
        
        [JsonProperty("default")]
        public bool isDefault { get; set; }
        public string container { get; set; }
    }
}