using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using core.Contract;
using Newtonsoft.Json;

namespace core.Services
{
    public class FileParserService : IFileParserService
    {
        public List<string> LinesFromFile (string path) 
        {
            var text = File.ReadAllText(path);
            var lines = text.Split('\n').ToList();
            foreach(var line in lines)
            {
                // clean the lines here, if needed
            }
            return lines;
        }
    }
}