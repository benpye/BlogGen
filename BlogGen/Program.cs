using System;

using RazorEngine;
using RazorEngine.Templating;
using RazorEngine.Configuration;
using System.Reflection;
using System.Linq;
using System.IO;

namespace BlogGen
{
    class Program
    {
        static int Main(string[] args)
        {
            if(args.Length != 4)
            {
                Console.WriteLine("Must have 4 command line arguments");
                Console.WriteLine("BlogGen.exe <driver> <template-dir> <in-dir> <out-dir>");
                return 1;
            }

            var cwd = Directory.GetCurrentDirectory();

            var driverAssembly = Assembly.LoadFile(Path.GetFullPath(args[0]));
            var driverKlasses = from t in driverAssembly.GetTypes()
                              let ifaces = t.GetInterfaces()
                              where ifaces.Length > 0
                              where ifaces.Contains(typeof(IDriver))
                              select t;

            if(driverKlasses.Count() != 1)
            {
                Console.WriteLine("Driver assembly may only have one {0} implementation", nameof(IDriver));
                return 1;
            }

            var config = new TemplateServiceConfiguration();
            config.DisableTempFileLocking = true;
            config.CachingProvider = new DefaultCachingProvider(t => { });
            config.TemplateManager = new ResolvePathTemplateManager(new string[] { args[1] });
            Engine.Razor = RazorEngineService.Create(config);

            var driverKlass = driverKlasses.First();
            var gen = new Generator(args[2], args[3]);
            gen.Generate((IDriver)driverKlass.GetConstructor(new Type[] { }).Invoke(new object[] { }));

            return 0;
        }
    }
}
