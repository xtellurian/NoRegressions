using System;

namespace core.Contract
{
    public interface ILogger
    {
        void Log(string message);
        void Error(string message, Exception ex);
    }
}