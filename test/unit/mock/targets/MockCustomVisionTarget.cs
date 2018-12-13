using System.Collections.Generic;
using System.Threading.Tasks;
using core.Contract;

namespace unit.Mock.Target
{
    public class MockCustomVisionTarget : ITestTarget
    {
        public string Endpoint => "http://fake";

        public Dictionary<string, string> Properties => new Dictionary<string, string>();

        public Task<string> PredictClassFromUrl(string url)
        {
            return Task.Factory.StartNew(() =>
            {
                return "cheese";
            });
        }
    }
}