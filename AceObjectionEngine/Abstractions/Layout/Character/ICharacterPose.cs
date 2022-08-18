using AceObjectionEngine.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions.Layout.Character
{
    public interface ICharacterPose : IObjectionObject
    {
        string Name { get; }
        int CharacterId { get; }
        bool IsSpeedlines { get; }
        IRenderBranch ActivePoseState { get; }
        IAudioSource[] AudioTicks { get; }

        bool Play();
        void SetStartPoseState<T>() where T : IRenderBranch;
    }
}
