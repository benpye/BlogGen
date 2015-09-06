using System;

namespace BlogGen
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewAttribute : Attribute
    {
        public string View { get; }

        public ViewAttribute(string view)
        {
            View = view;
        }
    }
}
