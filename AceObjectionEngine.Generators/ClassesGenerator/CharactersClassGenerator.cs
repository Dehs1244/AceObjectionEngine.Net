using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Loader.Presets;

namespace AceObjectionEngine.Generators.ClassesGenerator
{
    internal class CharactersClassGenerator : BaseClassGenerator
    {
        public override string Extension => "cs";

        private const string CLASS_BODY_DEFINITION = @"
public static Character {0} => new CharacterPreset({1}).LoadObject();";

        public CharactersClassGenerator(string fileName) : base(fileName)
        {
        }

        public CharactersClassGenerator(string fileName, string className) : base(fileName, className)
        {
        }

        public override void Generate()
        {
            ClassNamespace = "AceObjectionEngine";
            References.Add("AceObjectionEngine.Engine.Model.Layout");
            References.Add("AceObjectionEngine.Loader.Presets");
            var characters = new CharacterPreset().LoadAll();
            StringBuilder builder = new StringBuilder();
            foreach(var character in characters)
            {
                builder.Append("\t");
                var characterPropertyName = characters.Any(x => x.Name == character.Name) ?
                    FormatAsPropertyName($"{character.Name} {character.Side.ToString()}") : FormatAsPropertyName(character.Name);

                builder.AppendFormat(CLASS_BODY_DEFINITION, characterPropertyName, character.Id);
                builder.AppendLine();
            }
            WriteContentInBody(builder.ToString());
        }
    }
}
