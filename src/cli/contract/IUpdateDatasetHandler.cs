using System.Threading.Tasks;
using cli.Verbs.Dataset;

namespace cli.Contract
{
    public interface IUpdateDatasetHandler
    {
        Task Handle(UpdateDatasetVerb verb);
    }
}