using CommandLine;

namespace cli.Verbs
{
    [Verb("config", HelpText = "Configure the CLI")]
    public class ConfigureVerb
    {
        [Option("azure-blob-string", Required = false, HelpText = "Set Azure Blob connection string")]
        public string AzureBlobConnectionString { get; set; }

        [Option('d', "default", Default = false, Required = false, HelpText = "Specify if value is default")]
        public bool IsDefault { get; set; }

        [Option('k', "key", Required = false, HelpText = "They key of the setting")]
        public string Key { get; set; }

        [Option('v', "value", Required = false, HelpText = "The value of the setting")]
        public string Value { get; set; }
    }
}