using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Model.Components;
using AceObjectionEngine.Engine.Attributes;
using AceObjectionEngine.Engine.Enums;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Abstractions.Layout.Character
{
    [ParallelAnimationOptions(AnimationStateBreaker.Origin, BreakOnlyFor = new Type[] { typeof(IChatBox) } )]
    [ParallelAnimationDeception(typeof(IChatBox))]
    public interface ICharacter : IObjectionObject, IRenderInvokable
    {
        string NamePlate { get; }
        int StatePoseId { get; set; }
        BlipSexType Sex { get; }

        IReadOnlyCollection<AceComponentSpan<ICharacterPose>> Poses { get; }
        ICharacterPose ActivePose { get; }
    }
}
