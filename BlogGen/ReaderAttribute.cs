using System;

namespace BlogGen
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ReaderAttribute : Attribute
    {
        public string Extension { get; }

        public ReaderAttribute(string extension)
        {
            Extension = extension;
        }
    }
}
