using System.Collections.Generic;
using System.Threading.Tasks;

namespace core.Contract
{
    public interface IStorageService
    {
        Task Upload(string file, string container);
        Task ListContainers();
        Task ListBlobs(string container, string outputFile, bool generateToken, int? lifespan);
        Task MakeContainerPublic(string container);
    }
}