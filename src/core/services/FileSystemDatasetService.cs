using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using core.Contract;
using core.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace core.Services
{
    public class FileSystemDatasetService : IDatasetService
    {
        private readonly IConfiguration configuration;
        private string dataDir;
        private string fileExtension = ".set";

        public FileSystemDatasetService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.dataDir = configuration["datasets:dataDir"];
            if (string.IsNullOrEmpty(dataDir))
            {
                dataDir = Directory.GetCurrentDirectory();
            }
        }

        public Task Delete(string id)
        {
            return Task.Factory.StartNew(() =>
            {
                var path = Path.Combine(dataDir, id + fileExtension);
                File.Delete(path);
            });
        }

        public Task<T> Get<T>(string id) where T : class, IDataset
        {
            return Task.Factory.StartNew(() =>
            {
                var path = Path.Combine(dataDir, id + fileExtension);
                var data = File.ReadAllText(path);
                var dataset = JsonConvert.DeserializeObject<T>(data);
                return dataset;
            });
        }

        public Task<IEnumerable<string>> List()
        {
            return Task.Factory.StartNew(() =>
            {
                var d = new DirectoryInfo(dataDir);
                var files = d.GetFiles($"*{fileExtension}");
                return files.Select(f => f.Name.Replace(fileExtension,""));
            });
        }

        public Task Set(IDataset dataset)
        {
            return Task.Factory.StartNew(() =>
            {
                var data = JsonConvert.SerializeObject(dataset);
                var path = Path.Combine(dataDir, dataset.Id + fileExtension);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                File.WriteAllText(path, data);
            });
        }
    }
}