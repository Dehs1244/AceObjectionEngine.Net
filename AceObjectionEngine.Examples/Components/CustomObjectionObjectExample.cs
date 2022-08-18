using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;

namespace AceObjectionEngine.Examples.Components
{
    public class CustomObjectionObjectExample : IObjectionObject
    {
        public int Id => throw new NotImplementedException();

        public ISpriteSource Sprite => throw new NotImplementedException();

        public IAudioSource AudioSource => throw new NotImplementedException();

        public TimeSpan DurationCounter => throw new NotImplementedException();

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

        public void StartAnimation()
        {
            throw new NotImplementedException();
        }

        public Task StartAnimationAsync()
        {
            throw new NotImplementedException();
        }
    }
}
