using System.Collections.Generic;
using core.Model;

namespace core.Contract
{
    public interface ISingleClassImageDataset : IDataset
    {
        void AddLabelledImage(string label, string url);
        IEnumerable<LabelledImage> LabelledImages {get; }
    }
}   