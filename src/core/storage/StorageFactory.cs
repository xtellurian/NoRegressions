using System;
using core.Contract;
using core.Storage.Azure;
using Microsoft.Extensions.Configuration;

namespace core.Storage
{
    public class StorageFactory : IStorageFactory
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        public StorageFactory(IConfiguration configuration, ILogger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }
        public T CreateCloudProvider<T>() where T : class
        {
            if(typeof(T) == typeof(AzureService))
            {
                return new AzureService(logger, configuration) as T;
            }
            else
            {
                throw new ArgumentException($"No services found for {typeof(T)}");
            }
        }
    }
}