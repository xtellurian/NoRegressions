using System.Threading.Tasks;
using cli.Verbs;

namespace cli.Contract
{
    public interface IConfigureHandler
    {
        Task Handle(ConfigureVerb verb);
    }
}