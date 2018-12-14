using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using core.Contract;
using core.Exceptions;
using core.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace core.Target
{
    public class CustomVisionTarget : ITestTarget
    {
        private readonly HttpClient httpClient;
        private string Endpoint;
        private string Key;
        private string IterationId;

        public CustomVisionTarget(ILogger logger, IConfiguration configuration)
        {
            Key = configuration["customVision:predictionKey"];
            Endpoint = configuration["customVision:endpoint"];
            IterationId = configuration["customVision:iterationId"];

            if (string.IsNullOrEmpty(Key))
            {
                logger.Log("Warning, no Custom Vision Prediction Key provided. To set the key, run:");
                logger.Log("noreg config -k cv-key -v <YOUR_KEY>"); 
            }
            if (string.IsNullOrEmpty(Endpoint))
            {
                logger.Log("WARNING. Use-> noreg config -k cv-endpoint -v <http://something/url>"); 
                throw new NullReferenceException("Custom Vision Endpoint cannot be null or empty");
            }
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Prediction-Key", Key);
            Properties = new Dictionary<string, string>();

            Properties.Add("endpoint", Endpoint);
            Properties.Add("iteration_id", IterationId);
        }

        public Dictionary<string, string> Properties {get;set;}

        public async Task<string> PredictClassFromUrl(string url)
        {
            var serialized = JsonConvert.SerializeObject(new { url = url });
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            var fullyQualifiedEndpoint = Endpoint + $"?iterationId={IterationId}";
            var httpResponse = await httpClient.PostAsync(fullyQualifiedEndpoint, content);
            if(! httpResponse.IsSuccessStatusCode) 
            {
                throw new CustomVisionException($"Error from Custom Vision Service");
            }
            var rawResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<CustomVisionResponse>(rawResponse);

            var maxProbability = response.Predictions.Max(p => p.Probability);
            var topPrediction = response.Predictions.FirstOrDefault(p => p.Probability == maxProbability);
            return topPrediction.TagName;
        }
    }
}