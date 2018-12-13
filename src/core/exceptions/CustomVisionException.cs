namespace core.Exceptions
{
    public class CustomVisionException : System.Exception
    {
        public CustomVisionException(string message): base(message){}
        public CustomVisionException(string message, System.Exception ex): base(message, ex){}
    }
}