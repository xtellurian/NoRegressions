namespace core.Model
{
    public static class DatasetTypes
    {
        public static string PrintTypes()
        {
            return $"{SingleClassImage}, {Empty}";
        }
        public static bool IsValid(string type)
        {
            return string.Equals(type, Empty.Value) || string.Equals(type, SingleClassImage.Value);
        }
        
        public static DatasetType FromValue(string value)
        {
            if(string.Equals(value, SingleClassImage.Value))
            {
                return SingleClassImage;
            }
            if(string.Equals(value, Empty.Value))
            {
                return Empty;
            }
            return Empty;
        }
        public static DatasetType Empty => new DatasetType("Empty");
        public static DatasetType SingleClassImage => new DatasetType("SingleClassImage");
    }

    public class DatasetType
    {
        public DatasetType(){} // emtpy ctor for deserialisaton if needed
        public DatasetType(string value)
        {
            Value = value;
        }
        public string Value {get;set;}

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var other = (DatasetType) obj;
            return string.Equals(this.Value, other.Value);
        }
        
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}