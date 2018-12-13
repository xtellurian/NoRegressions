using System.Threading.Tasks;
using cli.Verbs.Dataset;

namespace cli.Contract
{
    public interface IListDatasetHandler
    {
        Task Handle(ListDatasetVerb verb);
    }
}