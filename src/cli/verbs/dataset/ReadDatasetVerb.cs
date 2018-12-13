using System;
using System.Collections.Generic;
using cli.Contract;
using CommandLine;
using CommandLine.Text;

namespace cli.Verbs.Dataset
{
    [Verb("read-dataset", HelpText = "Read info of datasets")]
    public class ReadDatasetVerb
    {

        [Option("id", Required = true, HelpText = "Id of the dataset")]
        public string Id { get; set; }

        [Usage(ApplicationAlias = "noreg")] 
        public static IEnumerable<Example> Examples 
        { 
            get 
            { 
                return new List<Example>() { 
                    new Example("Returns list of items in a dataset", new ReadDatasetVerb { Id = "test-set" }) 
                }; 
            } 
        }
    }
}