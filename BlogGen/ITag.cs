using System.Collections.Generic;

namespace BlogGen
{
    public interface ITag
    {
        string GetHTML(Dictionary<string, string> kvDict);
    }
}