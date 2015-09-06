namespace BlogGen
{
    public class RazorModel : HtmlFile
    {
        public Generator Gen { get; private set; }
        public RazorModel(HtmlFile file, Generator gen)
                : base(file.SourceFile, file.TargetFile, file.Source, file.Content, file.Html, file.Properties)
        {
            Gen = gen;
        }
    }
}
