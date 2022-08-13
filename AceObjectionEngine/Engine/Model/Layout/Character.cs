using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions.Layout.Character;
using AceObjectionEngine.Engine.Enums.SafetyEnums;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Loader.Presets;
using AceObjectionEngine.Engine.Model.Settings;
using System.Collections.ObjectModel;
using AceObjectionEngine.Loader.Utils;
using AceObjectionEngine.Engine.Attributes;
using AceObjectionEngine.Engine.Enums;
using AceObjectionEngine.Engine.PoseActions;
using AceObjectionEngine.Engine.Animator;

namespace AceObjectionEngine.Engine.Model.Layout
{
    [ParallelAnimation(nameof(IsSpeaking))]
    public class Character : ICharacter, IRerenderable
    {
        public string Name { get; }
        public int Id { get; }
        public string NamePlate { get; }
        public BlipSexType Sex { get; }

        public ICharacterPose ActivePose => Poses.Where(x=> x.Id == StatePoseId).First();

        public bool IsSpeaking => ActivePose.ActivePoseState is SpeakingPoseAction;
        public TimeSpan DurationCounter => ActivePose.DurationCounter;

        public IReadOnlyCollection<ICharacterPose> Poses { get; }

        public ISpriteSource Sprite => ActivePose.ActivePoseState.State;

        public IAudioSource AudioSource { get; }

        private int _poseId { get; set; }
        private bool _isPoseChanged;
        public int StatePoseId { get => _poseId; set
            {
                _isPoseChanged = true;
                _poseId = value;
            }
        }

        public bool IsNeedReRender { get; set; } = true;

        public CharacterLocations Side;
        public Bubble[] Bubbles = Array.Empty<Bubble>();

        public Character(IObjectionSettings<Character> settings)
        {
            Id = settings.Id;
            settings.Apply(this);
            Poses = new ReadOnlyCollection<ICharacterPose>(new List<ICharacterPose>());
        }

        public Character(CharacterSettings settings)
        {
            Id = settings.Id;
            Poses = new ReadOnlyCollection<ICharacterPose>(settings.Poses);
            Poses = settings.Poses ?? Poses;
            settings.Apply(this);
            Name = settings.Name ?? "Unknown";
            NamePlate = settings.NamePlate ?? "Unknown";
            Side = settings.Side ?? CharacterLocations.Counsel;
            Sex = settings.Sex ?? BlipSexType.Male;
            StatePoseId = settings.PoseId ?? Poses.First().Id;
        }

        public Character WithSettings(CharacterSettings settings)
        {
            settings.Name = settings.Name ?? Name;
            settings.NamePlate = settings.NamePlate ?? NamePlate;
            settings.PoseId = settings.PoseId ?? StatePoseId;
            settings.Poses.AddRange(Poses);
            settings.Sex = settings.Sex ?? Sex;
            settings.Side = settings.Side ?? Side;
            settings.Id = Id;

            return new Character(settings);
        }

        public Character WithPose(int poseId)
        {
            return WithSettings(new CharacterSettings(Id)
            {
                PoseId = poseId
            });
        }

        public Character WithFirstPose()
        {
            return WithSettings(new CharacterSettings(Id)
            {
                PoseId = Poses.First().Id
            });
        }

        public Character WithRandomPose()
        {
            var pose = Poses.ElementAt(new Random().Next(0, Poses.Count));
            return WithSettings(new CharacterSettings(Id)
            {
                PoseId = pose.Id
            });
        }

        public void Dispose()
        {
            foreach(var pose in Poses)
            {
                pose.Dispose();
            }
        }

        public void EndAnimation()
        {
            IsNeedReRender = ActivePose.Play();
            _isPoseChanged = false;
        }

        public async Task EndAnimationAsync() => await Task.Run(EndAnimation);

        public RenderActionConsequence OnRenderInvoke(AnimationRenderContext source, ICollection<IAnimationObject> parallelObjects)
        {
            return ActivePose.ActivePoseState.Action(source, parallelObjects);
        }

        public void StartAnimation()
        {
            IsNeedReRender = true;
            ActivePose.StartAnimation();
            if (!_isPoseChanged) ActivePose.SetStartPoseState<SpeakingPoseAction>();
        }

        public async Task StartAnimationAsync() => await Task.Run(StartAnimation);
    }
}
