using System.Collections.Generic;
using core.Contract;

namespace core.Results
{
    public class CustomVisionTestResult : ITestResult
    {
        private int totalPassed = 0;
        private int totalFailed = 0;
        public CustomVisionTestResult()
        {
            Parameters = new Dictionary<string, string>();
        }

        public void SetParam(string key, string value)
        {
            Parameters[key] = value;
        }
        public void AddFailed()
        {
            totalFailed++;
        }

        public void AddPassed() 
        {
            totalPassed++;
        }
        public string Id {get;set;}
        public int TotalTested => totalFailed + totalPassed;
        public double PercentagePassed => (double)totalPassed / (double)TotalTested;


        public Dictionary<string, string> Parameters {get; private set;}
    }
}