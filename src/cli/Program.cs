using System;
using cli.Contract;
using cli.Verbs;
using cli.Verbs.Dataset;
using CommandLine;

namespace cli
{
    class Program
    {
        static void Main(string[] args)
        {
            CliDependencies.RegisterServices();

            CommandLine.Parser.Default.ParseArguments<
                BlobVerb, ConfigureVerb, TestVerb,
                CreateDatasetVerb, ReadDatasetVerb, UpdateDatasetVerb, DeleteDatasetVerb, ListDatasetVerb>
                (args)
              .MapResult(
                (BlobVerb opts) =>
                {
                    var service = CliDependencies.Resolve<IBlobHandler>();
                    service.Handle(opts).Wait(); ;
                    return 0;
                },
                (ConfigureVerb opts) =>
                {
                    var service = CliDependencies.Resolve<IConfigureHandler>();
                    service.Handle(opts).Wait();
                    return 0;
                },
                (TestVerb opts) =>
                {
                    var service = CliDependencies.Resolve<ITestHandler>();
                    service.Handle(opts).Wait(); ;
                    return 0;
                },
                (CreateDatasetVerb opts) =>
                {
                    var service = CliDependencies.Resolve<ICreateDatasetHandler>();
                    service.Handle(opts).Wait(); ;
                    return 0;
                },
                (ReadDatasetVerb opts) =>
                {
                    var service = CliDependencies.Resolve<IReadDatasetHandler>();
                    service.Handle(opts).Wait(); ;
                    return 0;
                },
                (UpdateDatasetVerb opts) =>
                {
                    var service = CliDependencies.Resolve<IUpdateDatasetHandler>();
                    service.Handle(opts).Wait(); ;
                    return 0;
                },
                (DeleteDatasetVerb opts) =>
                {
                    var service = CliDependencies.Resolve<IDeleteDatasetHandler>();
                    service.Handle(opts).Wait(); ;
                    return 0;
                },
                (ListDatasetVerb opts) =>
                {
                    var service = CliDependencies.Resolve<IListDatasetHandler>();
                    service.Handle(opts).Wait(); ;
                    return 0;
                },
                errs => 1);
        }
    }
}
