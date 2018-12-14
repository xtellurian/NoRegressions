using System;
using System.IO;
using System.Reflection;
using cli.Contract;
using cli.Handlers;
using cli.Handlers.Dataset;
using core.Contract;
using core.Dependencies;
using core.Model;
using core.Services;
using core.Storage.Azure;
using core.Target;
using core.Testers;
using core.Testers.CustomVision;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MLFlow.NET.Lib.Model;

namespace cli
{
    public static class CliDependencies
    {

        public static T Resolve<T>()
        {
            return Dependencies.Resolve<T>();
        }

        public static void RegisterServices()
        {
            BuildConfig();
            // register service provider
           // Dependencies.ServiceCollection.AddSingleton<IServiceProvider>();
            // register utilities
            Dependencies.ServiceCollection.AddTransient<ILogger, Logger>();
            Dependencies.ServiceCollection.AddSingleton<IConfiguration>(Dependencies.Configuration);
            // register CLI handlers
            Dependencies.ServiceCollection.AddSingleton<IBlobHandler, BlobHandler>();
            Dependencies.ServiceCollection.AddSingleton<IConfigureHandler, ConfigureHandler>();
            Dependencies.ServiceCollection.AddSingleton<ITestHandler, TestHandler>();
            Dependencies.ServiceCollection.AddSingleton<ICreateDatasetHandler, CreateDatasetHandler>();
            Dependencies.ServiceCollection.AddSingleton<IReadDatasetHandler, ReadDatasetHandler>();
            Dependencies.ServiceCollection.AddSingleton<IUpdateDatasetHandler, UpdateDatasetHandler>();
            Dependencies.ServiceCollection.AddSingleton<IDeleteDatasetHandler, DeleteDatasetHandler>();
            Dependencies.ServiceCollection.AddSingleton<IListDatasetHandler, ListDatasetHandler>();
            // register services
            Dependencies.ServiceCollection.AddSingleton<IDatasetService, FileSystemDatasetService>();
            Dependencies.ServiceCollection.AddTransient<IDatasetResolverService, DatasetResolverService>();
            Dependencies.ServiceCollection.AddTransient<IFileParserService, FileParserService>();
            Dependencies.ServiceCollection.AddTransient<ITestingService>( a => new TestingService(a));
            Dependencies.ServiceCollection.AddSingleton<ITestResultStore, MLFLowResultStore>();
            Dependencies.UseMLFlow();
            Dependencies.ServiceCollection.Configure<MLFlowConfiguration>
                (Dependencies.Configuration.GetSection("MLFlow"));
            // register testers
            Dependencies.ServiceCollection.AddTransient<ICustomVisionTester, CustomVisionTester>();
            // register targets
            Dependencies.ServiceCollection.AddSingleton<ITestTarget, CustomVisionTarget>();
            Dependencies.ServiceCollection.AddSingleton<IStorageService, AzureService>();
            Dependencies.ServiceCollection.AddTransient<ISingleClassImageDataset, SampleSingleClassObjectDetectionDataset>();

            Dependencies.Build();
        }

        public static string ConfigurationRoot => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private static void BuildConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(CliDependencies.ConfigurationRoot)
                .AddJsonFile("appsettings.json", // this won't load properly as CLI
                            optional: false,
                            reloadOnChange: true)
                .AddJsonFile("configsettings.json",
                            optional: true)
                .AddEnvironmentVariables();
            

            Dependencies.Configuration = builder.Build();
        }
    }
}