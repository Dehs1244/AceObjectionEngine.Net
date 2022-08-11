using AceObjectionEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Attributes
{
    ///TODO INTERFACE IReRenderable
    /// <summary>
    /// Re-rendering in case of a change in the state of the inspector
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
    public class RequireReRender : BaseInspectorAttribute
    {
        private bool _state;
        /// <summary>
        /// Maximum value of re-rendering
        /// </summary>
        public int MaxReRendering { get; }
        /// <summary>
        /// Current value of re-rendering
        /// </summary>
        public int CurrentReRendering { get; private set; }

        public RequireReRender(int maxRerendering = 2, params string[] conditionProperties) : base(conditionProperties)
        {
            MaxReRendering = maxRerendering;
        }

        public override bool Inspect(IAnimationObject animationObject)
        {
            var newState = base.Inspect(animationObject);

            if (CurrentReRendering >= MaxReRendering) return false;
            var inspected = _state != newState;
            _state = newState;
            if (inspected) CurrentReRendering++;
            return inspected;
        }
    }
}
