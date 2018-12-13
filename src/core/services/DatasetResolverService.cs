using System.Threading.Tasks;
using core.Contract;
using core.Model;

namespace core.Services
{
    public class DatasetResolverService : IDatasetResolverService
    {
        private readonly IDatasetService datasetService;

        public DatasetResolverService(IDatasetService datasetService)
        {
            this.datasetService = datasetService;
        }

        public async Task<ISingleClassImageDataset> ResolveSingleClassImage(IDataset dataset)
        {
            if( DatasetTypes.SingleClassImage.Equals(dataset.Type) ||  DatasetTypes.Empty.Equals(dataset.Type))
            {
                return await datasetService.Get<SingleClassImageDataset>(dataset.Id);
            } 
            else
            {
                throw new System.Exception($"Dataset type {dataset.Type} for id: {dataset.Id}" +
                " cannot be cast as a SingleClassImageDataset");
            }
        }
    }
}