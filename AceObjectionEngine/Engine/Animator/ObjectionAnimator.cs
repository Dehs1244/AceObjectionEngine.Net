using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Model;
using AceObjectionEngine.Engine.Model.Layout;
using AceObjectionEngine.Exceptions;
using AceObjectionEngine.Helpers;

namespace AceObjectionEngine.Engine.Animator
{
    public class ObjectionAnimator : IAnimator<Frame>
    {
        public ICollection<Frame> Hierarchy { get; }

        public Stream OutputStream { get; private set; }

        public int Width { get; }
        public int Height { get; }

        public int MinimalWidth => 960;
        public int MinimalHeight => 640;

        public static int FrameRate { get; set; } = 10;

        public IFrameRenderer<Frame> Renderer { get; }

        public ObjectionAnimator(IFrameRenderer<Frame> frameRender = null)
        {
            OutputStream = new MemoryStream();
            Hierarchy = new List<Frame>();
            Width = MinimalWidth;
            Height = MinimalHeight;
            Renderer = frameRender ?? new FFMpegFrameRender(this, OutputStream);
        }

        public void Animate() => AnimateAsync().GetAwaiter().GetResult();

        public async Task AnimateAsync()
        {
            if (MinimalHeight > Height) throw new ObjectionMinimalSizeException(MinimalHeight, Height);
            if (MinimalWidth > Width) throw new ObjectionMinimalSizeException(MinimalWidth, Width);
            try
            {
                foreach (var frame in Hierarchy)
                {
                    Renderer.ResetAudioMixer(frame.Duration);
                    while (frame.UpdateHierarchy())
                    {
                        IEnumerable<AnimationRenderContext> audioSources = frame.MapLayerAudioSources();
                        IEnumerable<AnimationRenderContext> frameSprites = frame.MapLayerSprites();

                        if (frameSprites.Any()) await Renderer.RenderSpriteAsync(frameSprites.ToArray());
                        if (audioSources.Any()) await Renderer.RenderAudioAsync(audioSources.ToArray());
                    }
                }

                await Renderer.RenderAllAsync();
            }finally
            {
                TempFileStream.ImmediatelyFreeTempFiles();
            }
        }

        public Stream AsStream() => OutputStream;


        public void SaveAsFile(string output)
        {
            using (var fileStream = File.Create(output))
            {
                OutputStream.Seek(0, SeekOrigin.Begin);
                OutputStream.CopyTo(fileStream);
            }
        }

        public void Dispose()
        {
            for(var i = 0; i < Hierarchy.Count; i++)
            {
                Hierarchy.ElementAt(i).Dispose();
            }
            OutputStream.Dispose();
            Renderer.Dispose();
        }

    }
}
