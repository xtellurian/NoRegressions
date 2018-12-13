using System.Collections.Generic;
using System.Linq;
using core.Contract;

namespace core.Model
{
    public class SingleClassImageDataset : EmptyDataset, ISingleClassImageDataset
    {

        public IEnumerable<LabelledImage> LabelledImages {get; set;}


        public void AddLabelledImage(string label, string url)
        {
            if(LabelledImages == null) LabelledImages = new List<LabelledImage>();

            var list = LabelledImages.ToList();
            list.Add(new LabelledImage{Label=label, ImageUrl = url});
            LabelledImages = list;
        }
    }
}