using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Contract;
using core.Dependencies;
using core.Model;
using core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using test.Logging;

namespace unit.Services
{
    [TestClass]
    public class TestDatasetServices : TestBase
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

          
        }

        [TestMethod]
        public async Task GetAndSet_inMemory()
        {
            Dependencies.ServiceCollection.AddSingleton<IDatasetService, InMemoryDatasetService>();
            Dependencies.Build();
            await AssertGetAndSet();
        }
        [TestMethod]
        public async Task GetAndSet_fileSystem()
        {
            Dependencies.ServiceCollection.AddSingleton<IDatasetService, FileSystemDatasetService>();
            Dependencies.Build();
            await AssertGetAndSet();
        }

        private async Task AssertGetAndSet()
        {
            var id = Guid.NewGuid().ToString();
            var data = new List<LabelledImage>{
                new LabelledImage{Label="label-one",ImageUrl="imageurl-one"},
                new LabelledImage{Label="label-two",ImageUrl="imageurl-two"}
            };
            var service = Resolve<IDatasetService>();
            var dataset = new SingleClassImageDataset();
            dataset.Id = id;
            dataset.LabelledImages = data;
            await service.Set(dataset);
            var returned = await service.Get<SingleClassImageDataset>(id);

            Assert.IsNotNull(returned);
            Assert.IsInstanceOfType(returned, typeof(IDataset));
            Assert.AreEqual(id, returned.Id);
            foreach (var li in dataset.LabelledImages)
            {
                Assert.IsTrue(returned.LabelledImages.Any(
                    d => string.Equals(d.ImageUrl, li.ImageUrl) && string.Equals(d.Label, li.Label)));
            }
        }
    }
}