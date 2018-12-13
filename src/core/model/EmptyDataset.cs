using core.Contract;

namespace core.Model
{
    public class EmptyDataset : IDataset
    {
        public EmptyDataset()
        {
            
        }
        public EmptyDataset(string id)
        {
            Id = id;
            Type = DatasetTypes.Empty; // when creating the first time, we set the value to empty
        }
        public string Id {get;set;}

        public DatasetType Type {get;set;}
    }
}