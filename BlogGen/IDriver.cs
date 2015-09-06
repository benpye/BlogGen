namespace BlogGen
{
    public interface IDriver
    {
        bool PreGenerate(Generator gen);
        bool Generate(Generator gen);
        bool PostGenerate(Generator gen);
    }
}
