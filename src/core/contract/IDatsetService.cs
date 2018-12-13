using System.Collections.Generic;
using System.Threading.Tasks;

namespace core.Contract
{
    public interface IDatasetService
    {
        Task<T> Get<T>(string id) where T: class, IDataset;
        Task<IEnumerable<string>> List();

        Task Set(IDataset dataset);
        Task Delete(string id);
    }
}