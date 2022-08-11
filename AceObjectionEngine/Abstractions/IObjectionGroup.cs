using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Enums.SafetyEnums;

namespace AceObjectionEngine.Abstractions
{
    public interface IObjectionGroup
    {
        Groups Type { get; }
        string Name { get; }
        string CaseTag { get; }


    }
}
