using System.Threading.Tasks;
using cli.Contract;
using cli.Verbs;
using core.Contract;
using Microsoft.Extensions.Configuration;

namespace cli.Handlers
{
    public class BlobHandler : IBlobHandler
    {
        private IConfiguration configuration;
        private readonly IStorageService service;
        public BlobHandler(IConfiguration configuration, IStorageService service) 
        {
            this.configuration = configuration;
            this.service = service;
        }
        
        public async Task Handle(BlobVerb verb)
        {
            if (verb.Upload) 
            {
                string container = verb.Destination;
                string file = verb.FolderPath;

                await service.Upload(file, container);
            } 
            else if (verb.ListContainer) 
            {
                await service.ListContainers();
            }
            else if (StringExists(verb.ListBlob)) 
            {
                await service.ListBlobs(verb.ListBlob, verb.Output, verb.GenerateToken, verb.LifeSpan);
            }
            else if (StringExists(verb.MakePublic)) 
            {
                await service.MakeContainerPublic(verb.MakePublic);
            }
        }

        private bool StringExists(string s)
        {
            return !string.IsNullOrEmpty(s);
        }
    }
}   