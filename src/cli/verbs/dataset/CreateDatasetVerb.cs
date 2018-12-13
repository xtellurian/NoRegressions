using System;
using System.Collections.Generic;
using cli.Contract;
using CommandLine;
using CommandLine.Text;

namespace cli.Verbs.Dataset
{
    [Verb("create-dataset", HelpText = "Create a datasets")]
    public class CreateDatasetVerb
    {

        [Option("id", Default = null, HelpText = "Id of the dataset")]
        public string Id { get; set; }

        [Usage(ApplicationAlias = "noreg")] 
        public static IEnumerable<Example> Examples 
        { 
            get 
            { 
                return new List<Example>() { 
                    new Example("Create new dataset", new CreateDatasetVerb { Id = "test-set" }) 
                }; 
            } 
        }
    }
}