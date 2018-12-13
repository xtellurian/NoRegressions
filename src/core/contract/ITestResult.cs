using System.Collections.Generic;

namespace core.Contract
{
    public interface ITestResult
    {
        string Id {get;}
        int TotalTested {get;}
        double PercentagePassed { get; }
        Dictionary<string, string> Parameters {get;}
    }
}