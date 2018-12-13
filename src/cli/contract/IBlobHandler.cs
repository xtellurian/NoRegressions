using System.Threading.Tasks;
using cli.Verbs;

namespace cli.Contract
{
    public interface IBlobHandler
    {
        Task Handle(BlobVerb verb);
    }
}