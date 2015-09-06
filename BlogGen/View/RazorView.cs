using System;
using System.Linq;
using System.Collections.Generic;

using RazorEngine;
using System.Dynamic;
using RazorEngine.Templating;
using System.Collections.Immutable;
using System.IO;

namespace BlogGen.View
{
    public class RazorView : IView
    {
        virtual public HtmlFile GetFile(HtmlFile file, Generator gen)
        {
            var view = (GetType().GetCustomAttributes(typeof(ViewAttribute), true)[0] as ViewAttribute).View;

            var html = Engine.Razor.RunCompile(String.Format("View/{0}", view), typeof(RazorModel), new RazorModel(file, gen));

            string targetFile = file.SourceFile;
            targetFile = targetFile.Substring(0, targetFile.Length - Path.GetExtension(targetFile).Length);
            targetFile += "/";

            return file.WithHtml(html).WithTargetFile(targetFile);
        }
    }
}
