using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogGen.Sample
{
    public static class BlogExtension
    {
        public static string[] GetPreviewContent(this string content)
        {
            var split = content.Split(new string[] { "<!--more-->" }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length > 1)
                return split.Select(x => x.Trim()).ToArray();

            return new string[] { content };
        }

        public static List<HtmlFile> SortPostsByDate(this List<HtmlFile> files)
        {
            files.Sort((f1, f2) => DateTime.Parse(f2.Properties["Time"]).CompareTo(DateTime.Parse(f1.Properties["Time"])));
            return files;
        }

        public static List<HtmlFile> OfView(this List<HtmlFile> files, string view)
        {
            return files.Where(x => x.Properties["View"] == view).ToList();
        }
    }
}
