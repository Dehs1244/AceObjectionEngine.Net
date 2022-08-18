using AceObjectionEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Attributes
{
    /// <summary>
    /// Allows to run parallel animation of object
    /// </summary>
    public class ParallelAnimationAttribute : BaseInspectorAttribute
    {
        public ParallelAnimationAttribute()
        {

        }

        public ParallelAnimationAttribute(params string[] conditionProperties) : base(conditionProperties)
        {
        }
    }
}
