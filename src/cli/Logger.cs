using System;
using core.Contract;

namespace cli
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}] : {message}");
        }
        public void Error(string message, Exception ex)
        {
            if(message != null) Console.Error.WriteLine($"[{DateTime.Now.ToShortTimeString()}] : {message}");
            if(ex != null) Console.Error.WriteLine($"[{DateTime.Now.ToShortTimeString()}] Exception: {ex}");
        }
    }
}