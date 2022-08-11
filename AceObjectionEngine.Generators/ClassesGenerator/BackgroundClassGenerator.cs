using AceObjectionEngine.Loader.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Generators.ClassesGenerator
{
    internal class BackgroundClassGenerator : BaseClassGenerator
    {
        public override string Extension => "cs";

        public BackgroundClassGenerator(string fileName) : base(fileName)
        {
        }

        public BackgroundClassGenerator(string fileName, string className) : base(fileName, className)
        {
        }

        private const string CLASS_BODY_DEFINITION = @"
public static Background {0} => new BackgroundPreset({1}).LoadObject();";

        public override void Generate()
        {
            ClassNamespace = "AceObjectionEngine";
            References.Add("AceObjectionEngine.Engine.Model.Layout");
            References.Add("AceObjectionEngine.Loader.Presets");
            var backgrounds = new BackgroundPreset().LoadAll();
            StringBuilder builder = new StringBuilder();
            foreach (var background in backgrounds)
            {
                builder.Append("\t");

                builder.AppendFormat(CLASS_BODY_DEFINITION, FormatAsPropertyName(background.Name), background.Id);
                builder.AppendLine();
            }
            WriteContentInBody(builder.ToString());
        }
    }
}
