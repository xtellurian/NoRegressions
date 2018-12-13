using System.Collections.Generic;
using core.Contract;
using core.Model;

namespace unit.Mock.Datasets
{
    public class MockSingleClassImageUrlDataset : ISingleClassImageDataset
    {
        public MockSingleClassImageUrlDataset()
        {
            LabelledImages = new List<LabelledImage> 
            {
                new LabelledImage{Label="AppleRoyalGala", ImageUrl="http://www.weikingers.de/WebRoot/Store19/Shops/62697202/4C7B/7541/986F/3AB0/4CE2/C0A8/29BA/EB5A/apfel_ml.JPG"},
                new LabelledImage{Label="Garlic", ImageUrl="https://5.imimg.com/data5/EQ/CG/MY-41917443/fresh-garlic-500x500.jpg"}
            };
            Type = DatasetTypes.SingleClassImage;
        }
        public IEnumerable<LabelledImage> LabelledImages {get;set;}

        public string Id => "Mock-SingleClassImageUrlDataset";

        public DatasetType Type {get;set;}

        public void AddLabelledImage(string label, string url)
        {
            // do nothing
        }
    }
}