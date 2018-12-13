using System.Collections.Generic;
using System.Threading.Tasks;

namespace core.Contract
{
    public interface ITestTarget
    {
        Dictionary<string,string> Properties {get;}
        Task<string> PredictClassFromUrl(string url);
    }
}