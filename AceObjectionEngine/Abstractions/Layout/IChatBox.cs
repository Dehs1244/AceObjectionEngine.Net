using AceObjectionEngine.Abstractions.Layout.Character;
using AceObjectionEngine.Engine.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions.Layout
{
    public interface IChatBox : IObjectionObject
    {
        ChatBoxAlign Align { get; }
        ICharacter ReferenceCharacter { get; }
        string Text { get; }
    }
}
