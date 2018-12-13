using System.Threading.Tasks;

namespace core.Contract
{
    public interface ICustomVisionTester
    {
        Task<ITestResult> Run(ISingleClassImageDataset dataset);
    }
}