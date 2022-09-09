using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions.Layout.Character;
using AceObjectionEngine.Engine.Model.Components;
using AceObjectionEngine.Settings;

namespace AceObjectionEngine
{
    public class SceneBuilder
    {
        private Frame _frame = new Frame();
        private ObjectionSettings _settings;
        private Stack<ICharacter> _createdCharacters = new Stack<ICharacter>();
        private bool _addedCharacter;

        public SceneBuilder(ObjectionSettings settings)
        {
            _settings = settings;
        }

        public SceneBuilder AddCharacter(ICharacter character)
        {
            _frame.Objects.Add(_settings.CharacterLayer, character);
            var speakPosition = Array.FindIndex(character.ActivePose.GetAllPlayPoses(), (x) => x is Engine.PoseActions.SpeakingPoseAction);
            _frame.Offset = _frame.Offset.Add(TimeSpan.FromTicks(character.ActivePose.GetAllPlayPoses().Take(speakPosition).Sum(x => x.State.Duration.Ticks + x.Delay.Ticks)));
            _createdCharacters.Push(character);
            _addedCharacter = true;
            return this;
        }

        public SceneBuilder AddBackground(Background background)
        {
            _frame.Objects.Add(_settings.BackgroundLayer, background);
            if (background.Desk != null)
            {
                _frame.Objects.Add(_settings.DeskLayer, background.Desk);
            }

            return this;
        }

        public SceneBuilder AddAudio(Audio audio)
        {
            audio.AudioSource.IsFixate = true;
            _frame.Objects.Add(_settings.AudioLayer, audio);
            return this;
        }

        public SceneBuilder AddSound(Audio sound)
        {
            _frame.Objects.Add(_settings.AudioLayer, sound);
            return this;
        }

        public SceneBuilder AddCustomComponent(int layer, IObjectionObject objectionObject)
        {
            _frame.Objects.Add(layer, objectionObject);
            return this;
        }

        public SceneBuilder AddBubble(Bubble bubble)
        {
            _frame.Objects.Add(_settings.BubbleLayer, bubble);
            return this;
        }

        public SceneBuilder AddDialogue(ChatBoxSettings dialogueSettings)
        {
            dialogueSettings.Width = _settings.Width;
            dialogueSettings.Height = _settings.Height;

            if (!_addedCharacter)
            {
                var addingCharacter = _createdCharacters.Peek();
                _frame.Objects.Add(_settings.CharacterLayer, addingCharacter);
                _createdCharacters.Push(addingCharacter);
                _addedCharacter = true;
            }
            _frame.Objects.Add(_settings.DialogueLayer, new ChatBox(_createdCharacters.Peek(), dialogueSettings));
            _addedCharacter = false;
            return this;
        }

        public Frame Build() => _frame;
    }
}
