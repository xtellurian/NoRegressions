using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cli.Contract;
using cli.Handlers;
using cli.Handlers.Dataset;
using cli.Verbs;
using cli.Verbs.Dataset;
using core.Contract;
using core.Dependencies;
using core.Exceptions;
using core.Model;
using core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using test.Logging;

namespace unit
{
    [TestClass]
    public class DatasetHandlerTests : TestBase
    {
        public static Dictionary<string, string> configs = new Dictionary<string, string>
        {
            {"array:entries:0", "value0"}
        };

        [TestInitialize]
        public void SetupDependencies()
        {
             var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(configs);

            Dependencies.Configuration = builder.Build();

            Dependencies.ServiceCollection.AddSingleton<ILogger, TestLogger>();
            Dependencies.ServiceCollection.AddSingleton<IConfiguration>(Dependencies.Configuration);
            // all the dataset handlers
            Dependencies.ServiceCollection.AddSingleton<IListDatasetHandler, ListDatasetHandler>();
            Dependencies.ServiceCollection.AddSingleton<ICreateDatasetHandler, CreateDatasetHandler>();
            Dependencies.ServiceCollection.AddSingleton<IUpdateDatasetHandler, UpdateDatasetHandler>();
            Dependencies.ServiceCollection.AddSingleton<IReadDatasetHandler, ReadDatasetHandler>();
            Dependencies.ServiceCollection.AddSingleton<IDeleteDatasetHandler, DeleteDatasetHandler>();

            // register services
            Dependencies.ServiceCollection.AddSingleton<IFileParserService, FileParserService>(); // hm, this is used even in memory
            Dependencies.ServiceCollection.AddSingleton<IDatasetService, InMemoryDatasetService>();
            Dependencies.ServiceCollection.AddSingleton<IDatasetResolverService, DatasetResolverService>();
            
            Dependencies.Build();
        }

        [TestMethod]
        public async Task Dataset_InMemoryCRUD()
        {
            var id = "ANewId";
            var logger = (TestLogger) Resolve<ILogger>();
            var datasetService = Resolve<IDatasetService>();
            var createHandler = Resolve<ICreateDatasetHandler>();
            var readHandler = Resolve<IReadDatasetHandler>();
            var updateHandler = Resolve<IUpdateDatasetHandler>();
            var deleteHandler = Resolve<IDeleteDatasetHandler>();

            // create 
            var createVerb = new CreateDatasetVerb();
            createVerb.Id = id;
            createHandler.Handle(createVerb).Wait();
            var ds = await datasetService.Get<EmptyDataset>(id);
            Assert.IsNotNull(ds);
            Assert.AreEqual(ds.Id, id);
            Assert.AreEqual(ds.Type.Value, DatasetTypes.Empty.Value);

            // update inline
            var updateVerb = new UpdateDatasetVerb();
            updateVerb.Id = id;
            updateVerb.Label = "ALabel";
            updateVerb.Type = DatasetTypes.SingleClassImage.Value;
            updateVerb.Url = "http://example.com/pcture.jpeg";
            updateHandler.Handle(updateVerb).Wait();
            var singleClassDs = await datasetService.Get<SingleClassImageDataset>(id);
            Assert.IsNotNull(singleClassDs);
            Assert.AreEqual(singleClassDs.Id, id);
            Assert.AreEqual(singleClassDs.Type.Value, DatasetTypes.SingleClassImage.Value);
            
            // read the data
            var readVerb = new ReadDatasetVerb();
            readVerb.Id = id;
            readHandler.Handle(readVerb).Wait();
            Assert.IsTrue(logger.Messages.Any(m => m.Contains($"{id} is of type {DatasetTypes.SingleClassImage.Value}")));
            Assert.IsTrue(logger.Messages.Any(m => m.Contains($"1")));

            // delete the dataset
            var deleteVerb = new DeleteDatasetVerb();
            deleteVerb.Id = createVerb.Id;

            deleteHandler.Handle(deleteVerb).Wait();

            await Assert.ThrowsExceptionAsync<DatasetNotFoundException>( () => datasetService.Get<EmptyDataset>(createVerb.Id) );

        }

    }
}
