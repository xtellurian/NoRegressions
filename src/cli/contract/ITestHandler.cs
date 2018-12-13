using System.Threading.Tasks;
using cli.Verbs;

namespace cli.Contract
{
    public interface ITestHandler
    {
        Task Handle(TestVerb verb);
    }
}