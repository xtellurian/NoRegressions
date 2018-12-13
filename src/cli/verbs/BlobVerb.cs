using System.Collections.Generic; 
using CommandLine; 
using CommandLine.Text; 
 
namespace cli.Verbs 
{ 
    [Verb("blob", HelpText = "Upload data files to Azure Blob storage")] 
    public class BlobVerb 
    { 
        [Option('u', "upload", Required = false, HelpText = "Set Azure Blob connection string")] 
        public bool Upload { get; set; } 
 
        [Option('f', "filepath", Required = false, HelpText = "Direct file or directory")] 
        public string FolderPath { get; set; } 
 
        [Option('d', "destination", Required = false, HelpText = "Destination on blob storage")] 
        public string Destination { get; set; } 
 
        [Option("list-container", Required = false, HelpText = "List containers on blob")] 
        public bool ListContainer { get; set; } 
 
        [Option("list-blob", Required = false, HelpText = "List blobs within a container specific container")] 
        public string ListBlob { get; set; } 
 
        [Option('o', "output", Required = false, HelpText = "Output file.")] 
        public string Output { get; set; } 
 
        [Option("generate-token", Required = false, HelpText = "Generates SAS tokens for blobs in container. Valid for one day.")] 
        public bool GenerateToken { get; set; } 
 
        [Option("life-span", Required = false, HelpText = "Used when generating SAS token. Specifies the life stanp of the token by number of days.")] 
        public int? LifeSpan { get; set; } 
 
        [Option("make-container-public", Required = false, HelpText = "Pass container name. Makes container public so no SAS token is required to access blob.")] 
        public string MakePublic { get; set; } 
 
        [Usage(ApplicationAlias = "noreg")] 
        public static IEnumerable<Example> Examples 
        { 
            get 
            { 
                return new List<Example>() { 
                    new Example("Upload a specific file to cloud storage", new BlobVerb { Upload = true, FolderPath  = "file.png", Destination = "destination-contaniner" }), 
                    new Example("Upload a specific folder to cloud storage", new BlobVerb { Upload = true, FolderPath  = "folder/", Destination = "destination-contaniner" }), 
                    new Example("List containers in cloud storage", new BlobVerb { ListContainer = true }), 
                    new Example("List blobs inside specific container in cloud storage", new BlobVerb { ListBlob = "destination-contaniner" }),
                    new Example("Outputs list of blobs inside specific container in cloud storage", new BlobVerb { ListBlob = "destination-contaniner", Output = "list.txt" }),
                    new Example("Outputs list of blobs inside specific container in cloud storage with SAS token", new BlobVerb { ListBlob = "destination-contaniner", Output = "list.txt", GenerateToken = true }),
                    new Example("Outputs list of blobs inside specific container in cloud storage with SAS token with set expiry date", new BlobVerb { ListBlob = "destination-contaniner", Output = "list.txt", GenerateToken = true, LifeSpan = 30 }),
                    new Example("Set specific container to public.", new BlobVerb { MakePublic = "destination-contaniner" }) 
                }; 
            } 
        } 
    } 
}