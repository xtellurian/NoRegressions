using System.Threading.Tasks;

namespace core.Contract
{
    public interface IDatasetResolverService
    {
        Task<ISingleClassImageDataset> ResolveSingleClassImage(IDataset dataset);
    }
}