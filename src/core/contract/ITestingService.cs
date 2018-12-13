using System.Threading.Tasks;

namespace core.Contract
{
    public interface ITestingService
    {
        Task TestTargetAsync(string target, string datasetId);
    }
}