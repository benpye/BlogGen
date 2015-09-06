using System;
using System.Linq;

namespace BlogGen
{
    public class TypeManager
    {
        public static IReader GetReader(string extension)
        {
            var klass =
                (from a in AppDomain.CurrentDomain.GetAssemblies()
                from t in a.GetTypes()
                let attributes = t.GetCustomAttributes(typeof(ReaderAttribute), true)
                where attributes != null && attributes.Length > 0
                where (attributes[0] as ReaderAttribute).Extension == extension
                let interfaces = t.GetInterfaces()
                where interfaces != null && interfaces.Contains(typeof(IReader))
                select t).FirstOrDefault();

            return (IReader)klass?.GetConstructor(new Type[0]).Invoke(null);
        }

        public static ITag GetTag(string tag)
        {
            var klass =
                (from a in AppDomain.CurrentDomain.GetAssemblies()
                 from t in a.GetTypes()
                 let attributes = t.GetCustomAttributes(typeof(TagAttribute), true)
                 where attributes != null && attributes.Length > 0
                 where (attributes[0] as TagAttribute).Tag == tag
                 let interfaces = t.GetInterfaces()
                 where interfaces != null && interfaces.Contains(typeof(ITag))
                 select t).FirstOrDefault();

            return (ITag)klass?.GetConstructor(new Type[0]).Invoke(null);
        }

        public static IView GetView(string type)
        {
            var klass =
                (from a in AppDomain.CurrentDomain.GetAssemblies()
                 from t in a.GetTypes()
                 let attributes = t.GetCustomAttributes(typeof(ViewAttribute), true)
                 where attributes != null && attributes.Length > 0
                 where (attributes[0] as ViewAttribute).View == type
                 let interfaces = t.GetInterfaces()
                 where interfaces != null && interfaces.Contains(typeof(IView))
                 select t).FirstOrDefault();

            return (IView)klass?.GetConstructor(new Type[0]).Invoke(null);
        }
    }
}
