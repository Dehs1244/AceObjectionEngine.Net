using AceObjectionEngine.Loader.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Generators.ClassesGenerator
{
    internal class MusicClassGenerator : BaseClassGenerator
    {
        public override string Extension => "cs";

        private const string CLASS_BODY_DEFINITION = @"
public static Audio {0} => new SoundPreset({1}).LoadObject();";

        public MusicClassGenerator(string fileName) : base(fileName)
        {
        }

        public MusicClassGenerator(string fileName, string className) : base(fileName, className)
        {
        }

        public override void Generate()
        {
            ClassNamespace = "AceObjectionEngine";
            References.Add("AceObjectionEngine.Engine.Model.Components");
            References.Add("AceObjectionEngine.Loader.Presets");
            var sounds = new SoundPreset().LoadAll();
            StringBuilder builder = new StringBuilder();
            foreach (var sound in sounds)
            {
                builder.Append("\t");

                builder.AppendFormat(CLASS_BODY_DEFINITION, FormatAsPropertyName(sound.Name), sound.Id);
                builder.AppendLine();
            }
            WriteContentInBody(builder.ToString());
        }
    }
}
