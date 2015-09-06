using System;
using System.Collections.Generic;

namespace BlogGen.Sample.Tag
{
    [Tag("Gist")]
    public class GistTag : ITag
    {
        public string GetHTML(Dictionary<string, string> kvDict)
        {
            return String.Format("<script src = \"https://gist.github.com/{0}.js\" ></script>", kvDict["Id"]);
        }
    }
}
