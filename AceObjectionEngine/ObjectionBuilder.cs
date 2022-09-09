using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Abstractions.Layout.Character;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Engine.Model.Components;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine
{
    public class ObjectionBuilder
    {
        private IAnimator<Frame> _animator;
        private ObjectionSettings _settings;
        private TimeSpan _currentBuildDuration;

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

        public ObjectionBuilder AddGlobalAudio(Audio audio)
        {
            _animator.GlobalAudio.Add(new AceAudioTrackContainer(audio.AudioSource)
            {
                Start = _currentBuildDuration
            });
            return this;
        }

        public ObjectionBuilder RefreshGlobalAudio()
        {
            if (!_animator.Hierarchy.Any()) return this;

            var lastFrame = _animator.Hierarchy.Last();

            foreach(var track in _animator.GlobalAudio)
            {
                track.End = lastFrame.Duration;
            }

            lastFrame.ResetAudioGlobalTrack = true;
            _currentBuildDuration = lastFrame.Duration;

            return this;
        }

        public T Build<T>() where T : IAnimator<Frame> => (T)_animator;
        public IAnimator<Frame> Build() => _animator;
    }
}
