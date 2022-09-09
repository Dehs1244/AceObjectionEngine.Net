using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Engine.Model.Components;
using AceObjectionEngine.Settings;
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
        public void BuildTestWithLongTextTest()
        {
            ObjectionBuilder builder = new ObjectionBuilder();
            builder.CreateScene((scene) =>
            {
                scene.AddCharacter(ObjectionCharacters.PhoenixWrightDefense);
                scene.AddBackground(new BackgroundPreset(103).LoadObject());

                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "This is super mega long text with some test from Ace Objection Engine! I really hope it works because I don't want to rewrite all the code and look for an error"
                });
            });

            using var animator = builder.Build<ObjectionAnimator>();
            animator.Animate();
            var outputPath = "someObjection.mp4";
            animator.SaveAsFile(outputPath);

            Assert.True(File.Exists(outputPath));
        }

        [Fact]
        public void BuildTestWithMultipleBackgrounds()
        {
            ObjectionBuilder builder = new ObjectionBuilder();

            builder.AddGlobalAudio(ObjectionMusic.AHurtFox)
                .CreateScene((scene) =>
                {
                    scene.AddBackground(ObjectionBackgrounds.PWWitness);

                    scene.AddCharacter(ObjectionCharacters.PhoenixWrightDefense.WithPose(3));
                    scene.AddDialogue(new ChatBoxSettings()
                    {
                        Text = "Test dialogue"
                    });

                    scene.AddDialogue(new ChatBoxSettings()
                    {
                        Text = "And another dialogue"
                    });

                    scene.AddCharacter(ObjectionCharacters.MilesEdgeworthProsecution);
                    scene.AddDialogue(new ChatBoxSettings()
                    {
                        Text = "Nice test"
                    });

                    scene.AddBackground(ObjectionBackgrounds.ColiseumStage);
                    scene.AddCharacter(ObjectionCharacters.PhoenixWrightDefense.WithPose(3));
                    scene.AddDialogue(new ChatBoxSettings()
                    {
                        Text = "But now, i'm with new background"
                    });
                });

            using var animator = builder.Build<ObjectionAnimator>();
            animator.Animate();

            var outputPath = "someObjection.mp4";
            animator.SaveAsFile(outputPath);
            Assert.True(File.Exists(outputPath));
        }

        [Fact]
        public void BuildTestWithGlobalAudio()
        {
            ObjectionBuilder builder = new ObjectionBuilder();

            builder.AddGlobalAudio(ObjectionMusic.AHurtFox)
                .CreateScene((scene) =>
            {
                scene.AddBackground(ObjectionBackgrounds.PWWitness);

                scene.AddCharacter(ObjectionCharacters.PhoenixWrightDefense.WithPose(3));
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Test dialogue with desk"
                });

                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "And another dialogue"
                });

                scene.AddCharacter(ObjectionCharacters.MilesEdgeworthProsecution);
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Nice desk"
                });
            })
                .CreateScene((scene) =>
                {
                    scene.AddBackground(ObjectionBackgrounds.Studio1);

                    scene.AddCharacter(ObjectionCharacters.PhoenixWrightDefense);
                    scene.AddDialogue(new ChatBoxSettings()
                    {
                        Text = "Test next scene with global audio"
                    });

                    scene.AddCharacter(ObjectionCharacters.MilesEdgeworthProsecution);
                    scene.AddDialogue(new ChatBoxSettings()
                    {
                        Text = "Nice test"
                    });
                });

            using var animator = builder.Build<ObjectionAnimator>();
            animator.Animate();

            var outputPath = "someObjection.mp4";
            animator.SaveAsFile(outputPath);
            Assert.True(File.Exists(outputPath));
        }

        [Fact]
        public void BuildWithDeskTest()
        {
            ObjectionBuilder builder = new ObjectionBuilder();

            builder.CreateScene((scene) =>
            {
                scene.AddAudio(ObjectionMusic.Objection);
                scene.AddBackground(ObjectionBackgrounds.PWWitness);

                scene.AddCharacter(ObjectionCharacters.PhoenixWrightDefense.WithRandomPose());
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Test dialogue with desk"
                });

                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "And another dialogue"
                });

                scene.AddCharacter(ObjectionCharacters.MilesEdgeworthProsecution);
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Nice desk"
                });
            });

            using var animator = builder.Build<ObjectionAnimator>();
            animator.Animate();

            var outputPath = "someObjection.mp4";
            animator.SaveAsFile(outputPath);
            Assert.True(File.Exists(outputPath));
        }

        [Fact]
        public void BuildMyObjection()
        {
            ObjectionBuilder builder = new ObjectionBuilder();

            builder.CreateScene((scene) =>
            {
                scene.AddAudio(ObjectionMusic.Objection);
                scene.AddBackground(ObjectionBackgrounds.PWWitness);

                scene.AddCharacter(ObjectionCharacters.PhoenixWrightDefense.WithRandomPose());
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Test dialogue with deks"
                });

                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "And another dialogue"
                });

                scene.AddCharacter(ObjectionCharacters.MilesEdgeworthProsecution);
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "Nice desk"
                });

                scene.AddCharacter(ObjectionCharacters.MilesEdgeworthProsecution);
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "And then this"
                });

                scene.AddCharacter(ObjectionCharacters.MilesEdgeworthProsecution);
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "And then this and this and this and this and this and this and this and this and this and this and this"
                });

                scene.AddCharacter(ObjectionCharacters.MilesEdgeworthProsecution.WithFirstPose());
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "So this"
                });
                scene.AddCharacter(ObjectionCharacters.MilesEdgeworthProsecution);
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "And then this and this and this and this and this and this and this and this and this and this and this"
                });
                scene.AddCharacter(ObjectionCharacters.MilesEdgeworthProsecution.WithRandomPose());
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "So this"
                });
                scene.AddCharacter(ObjectionCharacters.MilesEdgeworthProsecution.WithRandomPose());
                scene.AddDialogue(new ChatBoxSettings()
                {
                    Text = "And then this and this and this and this and this and this and this and this and this and this and this"
                });
            });

            using var animator = builder.Build<ObjectionAnimator>();
            animator.Animate();

            var outputPath = "MyObjection.mp4";
            animator.SaveAsFile(outputPath);
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
