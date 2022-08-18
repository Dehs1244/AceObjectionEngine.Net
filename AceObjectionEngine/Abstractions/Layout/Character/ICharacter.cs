using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Model.Layout;
using AceObjectionEngine.Engine.Attributes;
using AceObjectionEngine.Engine.Enums;

namespace AceObjectionEngine.Abstractions.Layout.Character
{
    [ParallelAnimationOptions(AnimationStateBreaker.Origin)]
    [ParallelAnimationDeception(typeof(IChatBox))]
    public interface ICharacter : IObjectionObject, IRenderInvokable
    {
        string Name { get; }
        string NamePlate { get; }
        int StatePoseId { get; set; }
        BlipSexType Sex { get; }

        IReadOnlyCollection<ICharacterPose> Poses { get; }
        ICharacterPose ActivePose { get; }
    }
}
