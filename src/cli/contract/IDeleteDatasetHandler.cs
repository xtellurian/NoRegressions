using System.Threading.Tasks;
using cli.Verbs.Dataset;

namespace cli.Contract
{
    public interface IDeleteDatasetHandler
    {
        Task Handle(DeleteDatasetVerb verb);
    }
}