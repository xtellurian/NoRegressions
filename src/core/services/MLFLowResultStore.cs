using System;
using System.IO;
using System.Threading.Tasks;
using core.Contract;
using Microsoft.Extensions.Configuration;
using MLFlow.NET.Lib.Contract;
using MLFlow.NET.Lib.Model;
using MLFlow.NET.Lib.Model.Responses.Experiment;
using MLFlow.NET.Lib.Model.Responses.Run;

namespace core.Services
{
    public class MLFLowResultStore : ITestResultStore
    {
        private readonly ILogger logger;
        private readonly IMLFlowService mlflowService;
        private readonly IConfiguration configuration;

        public MLFLowResultStore(ILogger logger, IMLFlowService mlflowService, IConfiguration configuration)
        {
            this.logger = logger;
            this.mlflowService = mlflowService;
            this.configuration = configuration;
        }

        public async Task LogTestResultAsync(ITestResult result)
        {
            var expId = configuration["MLFlow:ExperimentId"] ?? "default";
           // var expId = Guid.NewGuid().ToString();
            var userId = configuration["MLFlow:UserId"] ?? "defaultuser";
            if(result.Id == null) {
                throw new NullReferenceException("ResultId cannot be null");
            }

            var exp = await mlflowService.CreateExperiment(expId) ?? new CreateResponse{ExperimentId = 1};

            logger.Log($"Using experiment id: {exp.ExperimentId}");
            
            var run = await CreateRun(exp.ExperimentId, result.Id);

            if (run == null) {
                throw new Exception("Could not create run in MLFlow. Please make sure MLFlow is running.");
            } 

            await mlflowService.LogMetric(run.Run.Info.RunUuid, "percentage_passed", (float)result.PercentagePassed);
            await mlflowService.LogMetric(run.Run.Info.RunUuid, "total_tested", (float)result.TotalTested);
            foreach(var p in result.Parameters)
            {
                await mlflowService.LogParameter(run.Run.Info.RunUuid, p.Key, p.Value);
            }
        }

        private async Task<RunResponse> CreateRun(int experimentId, string runName )
        {
            var userId = "rian finnegan";
            var sourceType = SourceType.LOCAL;
            var sourceName = "My laptop";
            var entryPointName = "cli.Program.cs";
            var startTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds(); //unix timestamp


            RunTag[] tags = { new RunTag() { Key = "test_runner", Value = "No_Regressions" } };

            var createRunRequest = new CreateRunRequest()
            {
                ExperimentId = experimentId,
                UserId = userId,
                Runname = runName,
                SourceType = sourceType,
                SourceName = sourceName,
                EntryPointName = entryPointName,
                StartTime = startTime,
                SourceVersion = "d1f3ba3c",
                Tags = tags
            };

            return await mlflowService.CreateRun(createRunRequest);
        }
    }
}