using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Abstractions.Layout.Character;
using AceObjectionEngine.Engine.Model.Settings;
using AceObjectionEngine.Loader.Presets;
using AceObjectionEngine.Loader.Utils;
using AceObjectionEngine.Engine.PoseActions;
using AceObjectionEngine.Engine.Infrastructure;

namespace AceObjectionEngine.Engine.Model.Layout
{
    public class CharacterPose : ICharacterPose
    {
        public string Name { get; }

        public int CharacterId { get; }
        public int Id { get; }

        public bool IsSpeedlines { get; }

        public ISpriteSource Sprite { get; }
        public IAudioSource AudioSource { get; }
        
        public TimeSpan DurationCounter => TimeSpan.FromTicks(PoseStates.Where(x=> !(x is SpeakingPoseAction)).Sum(x=> x.Duration.Ticks));

        public IPoseAction ActivePoseState => PoseStates[_playPosition];

        public IAudioSource[] AudioTicks { get; private set; }
        public readonly IPoseAction[] PoseStates;

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

            animationStates.Add(new PoseActions.SimplePoseAction()
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
            AudioTicks = settings.AudioTicks.Select(x=> x.AudioSource).ToArray();
        }

        public void OnSave()
        {
            throw new NotImplementedException();
        }

        public AssetJson ToJson()
        {
            throw new NotImplementedException();
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

        public Task EndAnimationAsync()
        {
            return Task.CompletedTask;
        }

        public bool Play()
        {
            _playPosition++;
            AudioTicks = Array.Empty<IAudioSource>();

            if (_playPosition >= PoseStates.Count())
            {
                _playPosition--;
                return false;
            }
            return true;
        }
    }
}
