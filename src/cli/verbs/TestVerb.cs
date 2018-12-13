using System.Collections.Generic;
using System.Linq;
using cli.Contract;
using CommandLine;

namespace cli.Verbs
{
    [Verb("test", HelpText = "Run tests against the model")]
    public class TestVerb
    {
        [Option('t', "target", Required = true, HelpText = "The target of test, eg CustomVision")]
        public string Target { get; set; }

        [Option('s', "dataset", Required = true, HelpText = "Dataset Id")]
        public string DatasetId { get; set; }


    }
}