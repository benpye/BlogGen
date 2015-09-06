using System;

namespace BlogGen
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TagAttribute : Attribute
    {
        public string Tag { get; }

        public TagAttribute(string tag)
        {
            Tag = tag;
        }
    }
}
