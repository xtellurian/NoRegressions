using System.Threading.Tasks;

namespace core.Contract
{
    public interface ITestResultStore
    {
        Task LogTestResultAsync(ITestResult result);
    }
}