using System;
using System.Collections.Generic;
using cli.Contract;
using CommandLine;
using CommandLine.Text;

namespace cli.Verbs.Dataset
{
    [Verb("delete-dataset", HelpText = "Delete a dataset")]
    public class DeleteDatasetVerb
    {

        [Option("id", Required = true, HelpText = "Id of the dataset")]
        public string Id { get; set; }


        [Usage(ApplicationAlias = "noreg")] 
        public static IEnumerable<Example> Examples 
        { 
            get 
            { 
                return new List<Example>() { 
                    new Example("Delete dataset", new UpdateDatasetVerb { Id = "test-set" })
                }; 
            } 
        }
    }
}