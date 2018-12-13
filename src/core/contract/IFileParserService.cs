using System.Collections.Generic;

namespace core.Contract
{
    public interface IFileParserService
    {
        List<string> LinesFromFile(string path);
    }
}