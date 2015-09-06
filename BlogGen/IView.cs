namespace BlogGen
{
    public interface IView
    {
        HtmlFile GetFile(HtmlFile file, Generator gen);
    }
}
