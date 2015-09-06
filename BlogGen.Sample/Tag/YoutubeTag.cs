using System;
using System.Collections.Generic;

namespace BlogGen.Sample.Tag
{
    [Tag("YouTube")]
    public class YouTubeTag : ITag
    {
        public string GetHTML(Dictionary<string, string> kvDict)
        {
            return String.Format("<div class=\"youtube\"><iframe src=\"https://www.youtube.com/embed/{0}\" frameborder=\"0\" allowfullscreen></iframe></div>", kvDict["Id"]);
        }
    }
}
