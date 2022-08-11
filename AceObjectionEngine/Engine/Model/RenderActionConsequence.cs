using AceObjectionEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Model
{
    public struct RenderActionConsequence
    {
        public bool IsAction { get; }
        public ISpriteSource[] Animation { get; }

        public RenderActionConsequence(ISpriteSource[] animation)
        {
            IsAction = animation.Count() > 0;
            Animation = animation;
        }

        public RenderActionConsequence(IEnumerable<ISpriteSource> animation) : this(animation.ToArray())
        {

        }

        public static RenderActionConsequence FromEmpty() => new RenderActionConsequence(Array.Empty<ISpriteSource>());
    }
}
