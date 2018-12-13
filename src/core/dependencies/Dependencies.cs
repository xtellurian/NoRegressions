using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MLFlow.NET.Lib;

namespace core.Dependencies
{
    public static class Dependencies
    {
        private static IServiceProvider serviceProvider;
        private static ServiceCollection serviceCollection = new ServiceCollection();
        public static IConfiguration Configuration {get;set;}
        public static ServiceCollection ServiceCollection => serviceCollection;
        public static T Resolve<T>()
        {
            if(serviceProvider == null) {
                Build();
            }
            return serviceProvider.GetService<T>();
        }

        public static IServiceProvider Build() 
        {
            serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }

        public static void UseMLFlow()
        {
            // add ML Flow services from lib
            ServiceCollection.AddMFlowNet();
        }
    }
}