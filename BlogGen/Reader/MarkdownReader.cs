using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;

using CommonMark;
using CommonMark.Syntax;

namespace BlogGen
{
    [Reader(".md")]
    public class MarkdownReader : IReader
    {
        private class CustomHtmlFormatter : CommonMark.Formatters.HtmlFormatter
        {
            private HtmlFile file;

            public CustomHtmlFormatter(System.IO.TextWriter target, CommonMarkSettings settings, HtmlFile file)
            : base(target, settings)
            {
                this.file = file;
            }

            protected override void WriteBlock(Block block, bool isOpening, bool isClosing, out bool ignoreChildNodes)
            {
                if (block.Tag == BlockTag.FencedCode)
                {
                    var blockString = block.StringContent.ToString();
                    var firstLine = blockString.Split('\n').FirstOrDefault().Trim();
                    if (firstLine.StartsWith(MarkdownReader.PrefixString))
                    {
                        ignoreChildNodes = true;

                        string kvString = block.StringContent.ToString().Substring(firstLine.Length);
                        var kvDict = MarkdownReader.ReadKV(kvString);

                        if (firstLine == MarkdownReader.PropertiesString)
                            return;
                        else
                        {
                            ITag tag = TypeManager.GetTag(firstLine.Substring(MarkdownReader.PrefixString.Length));
                            if(tag == null)
                            {
                                Console.WriteLine("File {0} has unknown tag {1}", file.SourceFile, firstLine.Substring(MarkdownReader.PrefixString.Length));
                            }
                            else
                            {
                                this.Write(tag.GetHTML(kvDict));
                                return;
                            }
                        }
                    }
                }

                base.WriteBlock(block, isOpening, isClosing, out ignoreChildNodes);
            }
        }

        internal const string PrefixString = "@Gen.";
        internal const string PropertiesString = "@Gen.Properties";

        internal static Dictionary<string, string> ReadKV(string kvString)
        {
            var kv = new Dictionary<string, string>();
            var lines = kvString.Split('\n');
            foreach(var line in lines)
            {
                var pair = line.Split('=').Select(x => x.Trim());
                if(!String.IsNullOrEmpty(pair.First()))
                    kv[pair.First()] = pair.Last();
            }

            return kv;
        }

        public HtmlFile ReadFile(HtmlFile file)
        {
            CommonMarkSettings.Default.OutputDelegate =
                (document, output, settings) =>
                new CustomHtmlFormatter(output, settings, file).WriteDocument(document);

            var doc = CommonMarkConverter.Parse(file.Source);
            var properties = new Dictionary<string, string>();
            foreach(var node in doc.AsEnumerable())
            {
                if(node.Block?.Tag == BlockTag.FencedCode)
                {
                    if(node.Block.StringContent.TakeFromStart(PropertiesString.Length) == PropertiesString)
                    {
                        string kvString = node.Block.StringContent.ToString().Substring(PropertiesString.Length);
                        var propDict = ReadKV(kvString);
                        propDict.ToList().ForEach(x => properties.Add(x.Key, x.Value));
                    }
                }
            }

            StringBuilder sb = new StringBuilder();

            CommonMarkConverter.ProcessStage3(doc, new StringWriter(sb));

            return file.WithContent(sb.ToString()).WithProperties(properties.ToImmutableDictionary());
        }
    }
}
