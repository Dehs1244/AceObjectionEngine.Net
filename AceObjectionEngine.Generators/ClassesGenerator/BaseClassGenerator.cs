using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Generators.Interfaces;
using System.Globalization;

namespace AceObjectionEngine.Generators.ClassesGenerator
{
    internal abstract class BaseClassGenerator : ISourceGenerator
    {
        public abstract string Extension { get; }
        public string Name { get; }

        protected string Content;
        protected string ClassNamespace;

        public ICollection<string> References { get; }
        public string ClassName { get; }

        public BaseClassGenerator(string fileName)
        {
            Name = fileName;
            References = new List<string>();
            ClassName = Name;
            References.Add("AceObjectionEngine");
        }

        public BaseClassGenerator(string fileName, string className) : this(fileName)
        {
            ClassName = className;
        }

        protected string FormatAsPropertyName(string value) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value).Replace(" ", "")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("!", "")
            .Replace("'", "")
            .Replace("&", "")
            .Replace("[", "")
            .Replace("]", "")
            .Replace("-", "");

        protected void WriteContentInBody(string content)
        {
            Content = @$"
// Auto-generated code
using System;
{References.Select(x=> $"using {x};").Aggregate((x, y) => $"{x}\n{y}")}

namespace {ClassNamespace}
{{
    public static partial class {ClassName}
{{
        {content}
}}
}}";
        }

        public abstract void Generate();

        public virtual Task GenerateAsync() => Task.Run(() => Generate());

        public virtual Task SaveAsFileAsync() => Task.Run(() => SaveAsFile());

        public void SaveAsFile()
        {
            using FileStream stream = File.OpenWrite($"{Name}.{Extension}");
            byte[] buffer = Encoding.Default.GetBytes(Content);
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}
