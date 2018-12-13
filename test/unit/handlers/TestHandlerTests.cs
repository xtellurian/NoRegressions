using System.Collections.Generic;
using System.Linq;
using cli.Contract;
using cli.Handlers;
using cli.Verbs;
using core.Contract;
using core.Dependencies;
using core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using test.Logging;

namespace unit
{
    [TestClass]
    public class TestHandlerTests : TestBase
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

            Dependencies.ServiceCollection.AddSingleton<ITestHandler, TestHandler>();

            // register services
            Dependencies.ServiceCollection.AddSingleton<ITestingService, TestingService>();
            
            Dependencies.Build();
        }

        [TestMethod]
        public void CustomVision_successfulPredictions()
        {
            var verb = new TestVerb();
            verb.Target = "CustomVision";

            var handler = Resolve<ITestHandler>();
            handler.Handle(verb).Wait();

            var logger = (TestLogger) Resolve<ILogger>();

            Assert.IsTrue(logger.Messages.Any(m => m.Contains("CustomVision")));
        }
        [TestMethod]
        public void WrongType_shouldPrintErrorMessage()
        {
            var verb = new TestVerb();
            verb.Target = "somethingnotathing";

            var handler = Resolve<ITestHandler>();
            handler.Handle(verb).Wait();

            var logger = (TestLogger) Resolve<ILogger>();

            Assert.IsTrue(logger.Errors.Any());
        }
    }
}
