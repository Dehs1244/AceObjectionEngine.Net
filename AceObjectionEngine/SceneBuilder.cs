using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions.Layout.Character;
using AceObjectionEngine.Engine.Model.Layout;
using AceObjectionEngine.Engine.Model.Settings;

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
            _createdCharacters.Push(character);
            _addedCharacter = true;
            return this;
        }

        public SceneBuilder AddBackground(Background background)
        {
            _frame.Objects.Add(_settings.BackgroundLayer, background);
            return this;
        }

        public SceneBuilder AddAudio(Audio audio)
        {
            _frame.Objects.Add(_settings.AudioLayer, audio);
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

            Bubble emptyBubble = null;
            _frame.Objects.Add(_settings.BubbleLayer, emptyBubble);
            _frame.Objects.Add(_settings.DialogueLayer, new ChatBox(_createdCharacters.Peek(), dialogueSettings));
            if (!_addedCharacter) AddCharacter(_createdCharacters.Peek());
            _addedCharacter = false;
            return this;
        }

        public Frame Build() => _frame;
    }
}
