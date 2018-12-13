using System;
using System.Collections.Generic;
using cli.Contract;
using CommandLine;
using CommandLine.Text;

namespace cli.Verbs.Dataset
{
    [Verb("update-dataset", HelpText = "Work with validation datasets")]
    public class UpdateDatasetVerb
    {
        [Option("id", Required = true, HelpText = "Id of the dataset")]
        public string Id { get; set; }

        [Option('l', "label", Required = true, HelpText = "Label to apply to images that are added")]
        public string Label { get; set; }

        [Option("from-file", Default = null, HelpText = "Path to file. File should be a list of images. Requires --label")]
        public string FromFile { get; set; }

        [Option("url", Default = null, HelpText = "Image URL to add. Requires --label")]
        public string Url { get; set; }

        [Option("type", Default = "Empty", HelpText = "Type of the dataset - currently only 'SingleClassImage'")]
        public string Type { get; set; }

        [Option('r', "replace-all", Default = false, HelpText = "Replace all data in dataset with new data")]
        public bool Replace { get; set; }

        [Usage(ApplicationAlias = "noreg")] 
        public static IEnumerable<Example> Examples 
        { 
            get 
            { 
                return new List<Example>() { 
                    new Example("Update the label of dataset and set data type", new UpdateDatasetVerb { Id = "test-set", Label = "apples", Type = "SingleClassImage" }),
                    new Example("Update dataset with image urls from a text file", new UpdateDatasetVerb { Id = "test-set", Label = "apples", Type = "SingleClassImage", FromFile = "apples.txt" }),
                    new Example("Update dataset with images with url to image", new UpdateDatasetVerb { Id = "test-set", Label = "apples", Type = "SingleClassImage", Url = "https://microsoft.com" }),
                    new Example("Replace existing dataset with image urls from a text file", new UpdateDatasetVerb { Id = "test-set", Label = "apples", Type = "SingleClassImage", FromFile = "apples.txt", Replace = true }),
                    new Example("Replace existing dataset with images with url to image", new UpdateDatasetVerb { Id = "test-set", Label = "apples", Type = "SingleClassImage", Url = "https://microsoft.com", Replace = true })
                }; 
            } 
        }
    }
}