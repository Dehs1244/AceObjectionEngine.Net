using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Engine.Attributes;
using AceObjectionEngine.Loader.Utils;

namespace AceObjectionEngine.Engine.Model.Layout
{
    [ParallelAnimation]
    public class Desk : IObjectionObject
    {
        public int Id { get; }

        public ISpriteSource Sprite { get; }

        public IAudioSource AudioSource => null;

        public TimeSpan DurationCounter => Frame.CalculateDuration(1);

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void EndAnimation()
        {
            throw new NotImplementedException();
        }

        public Task EndAnimationAsync()
        {
            throw new NotImplementedException();
        }
    }
}
