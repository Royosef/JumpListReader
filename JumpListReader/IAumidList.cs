using System.Collections.Generic;

namespace JumpListReader
{
    public interface IAumidList
    {
        string GetAumid(string path);
        IReadOnlyDictionary<string, string> GetList();
    }
}