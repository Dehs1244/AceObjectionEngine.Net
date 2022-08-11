using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Collections;

namespace AceObjectionEngine.Abstractions
{
    public interface IAnimatorHierarchy<T> : IEnumerable<IAnimationObject>
        where T : class, IAnimatorHierarchy<T>
    {
        int Updater { get; }

        TimeSpan Duration { get; }
        AnimatorLayersCollection<T> Objects { get; }
        bool UpdateHierarchy();
        bool IsCanReadParallelObjects();
    }
}
