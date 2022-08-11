using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Abstractions.Layout.Character;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Engine.Model.Layout;

namespace AceObjectionEngine
{
    public class ObjectionBuilder
    {
        private IAnimator<Frame> _animator;
        private ObjectionSettings _settings;

        public ObjectionBuilder(ObjectionSettings settings = null)
        {
            _settings = settings ?? ObjectionSettings.Default;
            _animator = new ObjectionAnimator();
        }

        public ObjectionBuilder CreateScene(Action<SceneBuilder> creation)
        {
            SceneBuilder scene = new SceneBuilder(_settings);
            creation(scene);
            _animator.Hierarchy.Add(scene.Build());
            return this;
        }

        public T Build<T>() where T : IAnimator<Frame> => (T)_animator;
        public IAnimator<Frame> Build() => _animator;
    }
}
