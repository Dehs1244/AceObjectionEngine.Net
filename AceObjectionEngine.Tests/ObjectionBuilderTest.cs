﻿using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Engine.Model.Layout;
using AceObjectionEngine.Engine.Model.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AceObjectionEngine.Loader.Presets;

namespace AceObjectionEngine.Tests
{
    public class ObjectionBuilderTest
    {
        [Fact]
        public void BuildCustomTestScene()
        {
            ObjectionBuilder builder = new ObjectionBuilder();
            builder.CreateScene((scene) =>
            {
                scene.AddCharacter(ObjectionCharacters.PhoenixWrightDefense.WithSettings(new CharacterSettings()
                {
                    Name = "Test Character",
                    NamePlate = "Tester",
                    Sex = AceObjectionEngine.Engine.Enums.BlipSexType.Female
                }));
                scene.AddBackground(new Background(new BackgroundSettings()
                {
                    Name = "Test Background",
                    ImagePath = TestResources.Background
                }));
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Text Dialogue!"
                });
            }).CreateScene((scene) =>
            {
                scene.AddCharacter(ObjectionCharacters.MiaFeyDefense.WithSettings(new CharacterSettings()
                {
                    Name = "Mia the tester",
                    NamePlate = "Mia Tester",
                    Sex = AceObjectionEngine.Engine.Enums.BlipSexType.Male
                }));
                scene.AddBackground(new Background(new BackgroundSettings()
                {
                    Name = "Another Background",
                    ImagePath = TestResources.Background
                }));
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Oh no, its test!"
                });
            });

            using var animator = builder.Build<ObjectionAnimator>();
            animator.Animate();
            var outputPath = "someObjection.mp4";
            animator.SaveAsFile(outputPath);

            Assert.True(File.Exists(outputPath));
        }

        [Fact]
        public void BuildTestSceneWithPresets()
        {
            ObjectionBuilder builder = new ObjectionBuilder();
            builder.CreateScene((scene) =>
            {
                scene.AddCharacter(new CharacterPreset(1).LoadObject());
                scene.AddBackground(new BackgroundPreset(103).LoadObject());
                scene.AddAudio(new SoundPreset(14).LoadObject());
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Text Dialogue!"
                });
            }).CreateScene((scene) =>
            {
                scene.AddCharacter(new CharacterPreset(2).LoadObject());
                scene.AddBackground(new BackgroundPreset(103).LoadObject());
                scene.AddAudio(new SoundPreset(13).LoadObject());

                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Oh no, its test!"
                });

                scene.AddCharacter(new CharacterPreset(5).LoadObject());
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Oh no, its test!"
                });

                scene.AddCharacter(new CharacterPreset(6).LoadObject());
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Oh no, its test!"
                });
            });

            using var animator = builder.Build<ObjectionAnimator>();
            animator.Animate();
            var outputPath = "someObjection.mp4";
            animator.SaveAsFile(outputPath);

            Assert.True(File.Exists(outputPath));
        }

        [Fact]
        public void BuildTestSceneWithAnimatedBackground()
        {
            ObjectionBuilder builder = new ObjectionBuilder();
            builder.CreateScene((scene) =>
            {
                scene.AddCharacter(new CharacterPreset(2).LoadObject());
                scene.AddBackground(new BackgroundPreset(204).LoadObject());
                scene.AddAudio(new SoundPreset(13).LoadObject());

                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Oh no, its test!"
                });

                scene.AddCharacter(new CharacterPreset(5).LoadObject());
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Oh no, its test!"
                });

                scene.AddBubble(new BubblePresetLoader(1).LoadObject());

                scene.AddCharacter(new CharacterPreset(6).LoadObject());
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Oh no, its test!"
                });
            });

            using var animator = builder.Build<ObjectionAnimator>();
            animator.Animate();
            var outputPath = "someObjection.mp4";
            animator.SaveAsFile(outputPath);
            animator.Dispose();

            Assert.True(File.Exists(outputPath));
        }

        [Fact]
        public void BuildTestSceneWithBubble()
        {
            ObjectionBuilder builder = new ObjectionBuilder();
            builder.CreateScene((scene) =>
            {
                scene.AddCharacter(ObjectionCharacters.PhoenixWrightDefense);
                scene.AddBackground(new BackgroundPreset(103).LoadObject());
                scene.AddAudio(new SoundPreset(14).LoadObject());
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Text Dialogue!"
                });
            }).CreateScene((scene) =>
            {
                scene.AddCharacter(ObjectionCharacters.MilesEdgeworthProsecution);
                scene.AddBackground(new BackgroundPreset(103).LoadObject());
                scene.AddAudio(new SoundPreset(13).LoadObject());

                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Oh no, its test!"
                });

                scene.AddCharacter(new CharacterPreset(5).LoadObject());
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Oh no, its test!"
                });

                scene.AddBubble(ObjectionBubbles.Objection);

                scene.AddCharacter(ObjectionCharacters.AprilMayWitness);
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Oh no, its test!"
                });
            });

            using var animator = builder.Build<ObjectionAnimator>();
            animator.Animate();
            var outputPath = "someObjection.mp4";
            animator.SaveAsFile(outputPath);

            Assert.True(File.Exists(outputPath));
        }

        [Fact]
        public void BuildTestWithCustomAnimatedBackground()
        {
            ObjectionBuilder builder = new ObjectionBuilder();
            //Problem with audio ticks after first person
            builder.CreateScene((scene) =>
            {
                scene.AddCharacter(ObjectionCharacters.MilesEdgeworthDefenseDefense.WithPose(701));
                scene.AddBackground(new Background(new BackgroundSettings()
                {
                    ImagePath = TestResources.AnimatedBackground,
                    Name = "Animated background"
                }));

                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Text Dialogue!"
                });

                scene.AddCharacter(ObjectionCharacters.AcroWitness.WithFirstPose());
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Hello!"
                });
            });

            using var animator = builder.Build<ObjectionAnimator>();
            animator.Animate();
            var outputPath = "someObjection.mp4";
            animator.SaveAsFile(outputPath);

            Assert.True(File.Exists(outputPath));
        }
    }
}
