using AceObjectionEngine.Loader.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Generators.ClassesGenerator
{
    internal class BubbleClassGenerator : BaseClassGenerator
    {
        public override string Extension => "cs";

        private const string CLASS_BODY_DEFINITION = @"
public static Bubble {0} => new BubblePresetLoader({1}).LoadObject();";

        public BubbleClassGenerator(string fileName) : base(fileName)
        {
        }

        public BubbleClassGenerator(string fileName, string className) : base(fileName, className)
        {
        }

        public override void Generate()
        {
            ClassNamespace = "AceObjectionEngine";
            References.Add("AceObjectionEngine.Engine.Model.Components");
            References.Add("AceObjectionEngine.Loader.Presets");
            var bubbles = new BubblePresetLoader().LoadAll();
            StringBuilder builder = new StringBuilder();
            foreach (var bubble in bubbles)
            {
                builder.Append("\t");

                builder.AppendFormat(CLASS_BODY_DEFINITION, FormatAsPropertyName(bubble.Name), bubble.Id);
                builder.AppendLine();
            }
            WriteContentInBody(builder.ToString());
        }
    }
}
