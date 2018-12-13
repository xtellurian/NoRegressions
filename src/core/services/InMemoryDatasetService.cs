using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Contract;
using core.Exceptions;
using Newtonsoft.Json;

namespace core.Services
{
    public class InMemoryDatasetService : IDatasetService
    {
        private readonly Dictionary<string, string> _memory;

        public InMemoryDatasetService()
        {
            _memory = new Dictionary<string, string>();
        }

        Task<T> IDatasetService.Get<T>(string id)
        {
            return Task.Factory.StartNew( () => {
                if(_memory.ContainsKey(id))
                {
                    return JsonConvert.DeserializeObject<T>(_memory[id]);
                } 
                else {
                    throw new DatasetNotFoundException($"Dataset id: {id} not found in memory");
                }
            });
        }

        public Task Set(IDataset dataset)
        {
            _memory[dataset.Id] = JsonConvert.SerializeObject(dataset);
            return Task.CompletedTask;
        }

        public Task Delete(string id)
        {
            _memory.Remove(id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<string>> List()
        {
            return Task.Factory.StartNew( () => {
                return _memory.Select(kvp => kvp.Key); // get all keys
            });
        }
    }
}