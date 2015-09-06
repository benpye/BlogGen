namespace BlogGen.Sample
{
    class Driver : IDriver
    {
        public bool PreGenerate(Generator gen)
        {
            return false;
        }

        public bool Generate(Generator gen)
        {
            return false;
        }

        public bool PostGenerate(Generator gen)
        {
            gen.GenerateProgrammaticView("/blog/archive", "BlogArchive");
            gen.GenerateProgrammaticView("/blog", "BlogIndex");

            return false;
        }
    }
}
