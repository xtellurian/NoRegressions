using System.Collections.Generic;
using System.Linq;
using cli.Contract;
using CommandLine;

namespace cli.Verbs.Test
{
    [Verb("test-cv", HelpText = "Run tests against a custom vision endpoint")]
    public class TestCustomVisionVerb
    {
        [Option('e', "endpoint", Required = true, HelpText = "The type of test, eg CustomVision")]
        public string Endpoint { get; set; }

        [Option('k', "key", Required = true, HelpText = "Prediction Key used in HTTP requrest")]
        public string Key { get; set; }

    }
}