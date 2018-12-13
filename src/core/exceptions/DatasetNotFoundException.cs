namespace core.Exceptions
{
    public class DatasetNotFoundException : System.Exception
    {
        public DatasetNotFoundException(string message): base(message){}
        public DatasetNotFoundException(string message, System.Exception ex): base(message, ex){}
    }
}