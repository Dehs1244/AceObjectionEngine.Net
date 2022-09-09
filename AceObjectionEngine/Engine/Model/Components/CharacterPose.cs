using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Abstractions.Layout.Character;
using AceObjectionEngine.Settings;
using AceObjectionEngine.Loader.Presets;
using AceObjectionEngine.Loader.Utils;
using AceObjectionEngine.Engine.PoseActions;
using AceObjectionEngine.Engine.Infrastructure;

namespace AceObjectionEngine.Engine.Model.Components
{
    public class CharacterPose : ICharacterPose
    {
        public string Name { get; }

        public int CharacterId { get; }
        public int Id { get; }

        public bool IsSpeedlines { get; }

        public ISpriteSource Sprite { get; }
        public IAudioSource AudioSource { get; }

        private TimeSpan _posesDuration;
        public TimeSpan DurationCounter => _posesDuration;

        public IRenderBranch ActivePoseState => PoseStates[_playPosition];

        private readonly IAudioSource[] _audioTicks;
        private bool _isPlayAudioTicks = true;
        public IAudioSource[] AudioTicks => _isPlayAudioTicks ? _audioTicks : Array.Empty<IAudioSource>();
        public readonly IRenderBranch[] PoseStates;

        private int _playPosition;

        public CharacterPose(IObjectionSettings<CharacterPose> settings)
        {
            Id = settings.Id;
            settings.Apply(this);
        }

        public CharacterPose(CharacterPoseSettings settings)
        {
            CharacterId = settings.Id;
            Name = settings.Name;
            IsSpeedlines = settings.IsSpeedLines;
            Id = settings.Id;
            var animationStates = settings.PoseStates.ToList();
            var speakSprite = MediaMaker.SpriteMaker.Make(settings.SpeakImagePath);
            var idleSprite = MediaMaker.SpriteMaker.Make(settings.IdleImagePath);

            animationStates.Add(new SinglePoseAction()
            {
                State = idleSprite
            });
            animationStates.Add(new SpeakingPoseAction()
            {
                State = speakSprite
            });
            animationStates.Add(new IdleWithChatBoxPoseAction()
            {
                State = idleSprite
            });

            PoseStates = animationStates.ToArray();
            _audioTicks = settings.AudioTicks.Select(x=> x.AudioSource).ToArray();
            _posesDuration = TimeSpan.FromTicks(PoseStates.Where(x => !(x is SpeakingPoseAction)).Sum(x => x.Duration.Ticks));
        }

        public void Dispose()
        {
            foreach(var poseState in PoseStates)
            {
                poseState.State.Dispose();
            }
        }

        public void EndAnimation()
        {
        }

        public async Task EndAnimationAsync() => await Task.Run(EndAnimation);

        public bool Play()
        {
            _isPlayAudioTicks = false;
            _playPosition++;

            if (_playPosition >= PoseStates.Count())
            {
                _playPosition = 0;
                _isPlayAudioTicks = true;
                return false;
            }
            return true;
        }

        public void StartAnimation()
        {
            _playPosition = 0;
        }

        public async Task StartAnimationAsync() => await Task.Run(StartAnimation);

        public void SetStartPoseState<T>() where T : IRenderBranch
        {
            _playPosition = Array.FindIndex(PoseStates, (x) => x is T);
            //_posesDuration -= TimeSpan.FromTicks(PoseStates.Take(_playPosition).Sum(x => x.Duration.Ticks));
            _isPlayAudioTicks = false;
            if (_playPosition < 0) throw new IndexOutOfRangeException();
        }

        public IRenderBranch[] GetAllPlayPoses() => PoseStates;
    }
}
