using System.Threading.Tasks;
using cli.Verbs.Test;

namespace cli.Contract
{
    public interface ITestCustomVisionHandler
    {
        Task Handle(TestCustomVisionVerb verb);
    }
}