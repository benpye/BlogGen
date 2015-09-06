using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

namespace BlogGen
{
    public class Generator
    {
        public List<HtmlFile> Files { get; private set; }

        public string InputDirectory { get; private set; }
        public string OutputDirectory { get; private set; }

        public Generator(string inDir, string outDir)
        {
            InputDirectory = inDir;
            OutputDirectory = outDir;
        }

        // Reads files from disk, does conversion from markdown etc to HTML
        public HtmlFile ProcessStage1(HtmlFile file)
        {
            var ext = Path.GetExtension(file.SourceFile);
            IReader reader = TypeManager.GetReader(ext);
            if (reader == null)
            {
                Console.WriteLine("File {0} has unknown extension {1}", file.SourceFile, ext);
                return null;
            }

            return reader.ReadFile(file);
        }

        // Takes HTML output in stage 1 and feeds it into the template for the views
        public HtmlFile ProcessStage2(HtmlFile file)
        {
            string viewName;
            if (!file.Properties.TryGetValue("View", out viewName))
            {
                Console.WriteLine("File {0} is missing view property", file.SourceFile);
                return null;
            }

            IView view = TypeManager.GetView(viewName);
            if (view == null)
            {
                Console.WriteLine("File {0} has unknown view {1}", file.SourceFile, viewName);
                return null;
            }

            return view.GetFile(file, this);
        }

        // Takes the final HTML and outputs it to disk
        public HtmlFile ProcessStage3(HtmlFile file)
        {
            if(String.IsNullOrWhiteSpace(file.TargetFile))
            {
                Console.WriteLine("File {0} has empty target file", file.TargetFile);
                return null;
            }

            var outPath = Path.Combine(OutputDirectory, file.TargetFile.TrimStart('/'));

            if (outPath[outPath.Length - 1] == '/')
                outPath = outPath + "index.html";

            if (File.Exists(outPath))
                File.Delete(outPath);

            Directory.CreateDirectory(Path.GetDirectoryName(outPath));

            File.WriteAllText(outPath, file.Html);
            Console.WriteLine("Writing to {0}", outPath);

            return file;
        }

        public void GenerateProgrammaticView(string path, string view, Dictionary<string, string> properties = null)
        {
            if (properties == null)
                properties = new Dictionary<string, string>();

            properties.Add("View", view);

            var file = new HtmlFile(path, "", "", "", "", properties.ToImmutableDictionary());
            if (file != null)
                file = ProcessStage2(file);
            if (file != null)
                file = ProcessStage3(file);
            if (file != null)
                Files.Add(file);
        }

        internal void Generate(IDriver driver)
        {
            Files = new List<HtmlFile>();

            if (!driver.PreGenerate(this))
            {

            }

            if (!driver.Generate(this))
            {
                var inFiles = Directory.EnumerateFiles(InputDirectory, "*.*", SearchOption.AllDirectories);

                foreach (var path in inFiles)
                {
                    // If any stage returns null this file is broken and should be ignored
                    var file = new HtmlFile(Path.GetFullPath(path).Substring(Path.GetFullPath(InputDirectory).Length).Replace('\\', '/'), "", File.ReadAllText(path), "", "", null);
                    if (file != null)
                        file = ProcessStage1(file);
                    if (file != null)
                        file = ProcessStage2(file);
                    if (file != null)
                        file = ProcessStage3(file);
                    if (file != null)
                        Files.Add(file);
                }
            }

            if(!driver.PostGenerate(this))
            {

            }
        }
    }
}
