using System;
using System.Collections.Generic;
using core.Contract;

namespace test.Logging
{
    public class TestLogger : ILogger
    {
        private List<string> messages = new List<string>();
        public List<string> Messages => messages;
        private List<string> errors = new List<string>();
        public List<string> Errors => errors;
        private List<Exception> exceptions = new List<Exception>();
        public List<Exception> Exceptions => exceptions;

        public void Error(string message, Exception ex)
        {
            errors.Add(message);
            exceptions.Add(ex);
        }

        public void Log(string message)
        {
            messages.Add(message);
            Console.WriteLine($"Test Logging : {message}");
        }
    }
}