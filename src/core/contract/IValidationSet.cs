using core.Model;

namespace core.Contract
{
    public interface IDataset
    {
        string Id {get;}
        DatasetType Type {get; set;}
    }
}