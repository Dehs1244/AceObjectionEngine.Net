using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Enums.SafetyEnums
{
    public class CharacterLocations : SafetyEnum<CharacterLocations>
    {
        public static CharacterLocations Defense => RegisterValue("defense");
        public static CharacterLocations Prosecution => RegisterValue("prosecution");
        public static CharacterLocations Counsel => RegisterValue("counsel");
        public static CharacterLocations Witness => RegisterValue("witness");
        public static CharacterLocations Judge => RegisterValue("judge");
        public static CharacterLocations Gallery => RegisterValue("gallery");
    }
}
