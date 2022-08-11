using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Animator;

namespace AceObjectionEngine.Engine.Collections
{
    /// <summary>
    /// Layer Collection of animated objects
    /// </summary>
    /// <typeparam name="T">Parent of storage animation objects</typeparam>
    public class AnimatorObjectsCollection<T> 
        : Collection<IAnimationObject>, IEnumerable<IAnimationObject>, ICloneable
        where T : class, IAnimatorHierarchy<T>
    {
        public T Parent;

        public AnimatorObjectsCollection(T parent)
        {
            Parent = parent;
        }

        public object Clone()
        {
            var cloned = new AnimatorObjectsCollection<T>(Parent);
            foreach(var item in Items)
            {
                cloned.Add(item);
            }
            return cloned;
        }
    }
}
