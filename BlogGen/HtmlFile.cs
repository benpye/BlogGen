using System.Collections.Immutable;

namespace BlogGen
{
    public class HtmlFile
    {
        public HtmlFile(string sourceFile, string targetFile, string source, string content, string html, ImmutableDictionary<string, string> properties)
        {
            SourceFile = sourceFile;
            TargetFile = targetFile;
            Source = source;
            Content = content;
            Html = html;
            Properties = properties;
        }
        public string SourceFile { get; }
        public string TargetFile { get; set; }
        public string Source { get; }
        public string Content { get; }
        public string Html { get; }
        public ImmutableDictionary<string, string> Properties { get; }

        public HtmlFile WithSourceFile(string newSourceFile)
        {
            return new HtmlFile(newSourceFile, TargetFile, Source, Content, Html, Properties);
        }

        public HtmlFile WithTargetFile(string newTargetFile)
        {
            return new HtmlFile(SourceFile, newTargetFile, Source, Content, Html, Properties);
        }

        public HtmlFile WithSource(string newSource)
        {
            return new HtmlFile(SourceFile, TargetFile, newSource, Content, Html, Properties);
        }

        public HtmlFile WithContent(string newContent)
        {
            return new HtmlFile(SourceFile, TargetFile, Source, newContent, Html, Properties);
        }

        public HtmlFile WithHtml(string newHtml)
        {
            return new HtmlFile(SourceFile, TargetFile, Source, Content, newHtml, Properties);
        }

        public HtmlFile WithProperties(ImmutableDictionary<string, string> newProperties)
        {
            return new HtmlFile(SourceFile, TargetFile, Source, Content, Html, newProperties);
        }
    }
}
