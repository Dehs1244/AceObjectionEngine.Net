using AceObjectionEngine.Abstractions.Layout.Character;
using AceObjectionEngine.Engine.Attributes;
using AceObjectionEngine.Engine.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions.Layout
{
    /// <summary>
    /// Implementation interface of chatbox
    /// </summary>
    [FloatyIndexInvoker]
    public interface IChatBox : IObjectionObject
    {
        /// <summary>
        /// Chatbox binding
        /// </summary>
        ChatBoxAlign Align { get; }
        /// <summary>
        /// The character that attached to chatbox
        /// </summary>
        ICharacter ReferenceCharacter { get; }
        /// <summary>
        /// Text of chatbox
        /// </summary>
        string Text { get; }
    }
}
