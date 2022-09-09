using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Collections;
using AceObjectionEngine.Engine.Model;
using AceObjectionEngine.Engine.Model.Components;
using AceObjectionEngine.Exceptions;
using FFMpegCore.Pipes;

namespace AceObjectionEngine.Engine.Animator
{
    public class Frame : IAnimatorHierarchy<Frame>, IDisposable
    {
        public static TimeSpan CalculateDuration(int frameCount) => TimeSpan.FromSeconds((double)(frameCount) / ((double)ObjectionAnimator.FrameRate));
        public static double SamplingFromObjectionLolTime(double time) => time * (15d / ((double)ObjectionAnimator.FrameRate));
        public static int FrameCountFromDuration(TimeSpan duration) => (int)Math.Round(duration.TotalSeconds * ((double)ObjectionAnimator.FrameRate)); 

        public AnimatorLayersCollection<Frame> Objects { get; }
        public TimeSpan Offset { get; set; }

        public TimeSpan Duration => TimeSpan.FromTicks(Objects.AsAnimationObjectEnumerable().Where(x=> x != null)
                .Sum(x => x.DurationCounter.Ticks) + Offset.Ticks);

        public int Updater { get; private set; }
        public int ReadingLayer { get; private set; }
        public bool ResetAudioGlobalTrack { get; set; }

        public Frame()
        {
            Objects = new AnimatorLayersCollection<Frame>(this);
        }

        public void Add(int layer, IAnimationObject animateObject)
        {
            Objects[layer].Add(animateObject);
        }

        public bool IsAlreadyReaded<T>() where T : IAnimationObject
        {
            var index = Objects.FindLayerWithObjects<T>();
            if (index == -1) throw new ObjectionException("Layer with this object missing");

            return Updater > Objects[index].Count;
        }

        public bool IsAlreadyReaded(Type objectType, int indexer)
        {
            var index = Objects.FindLayerWithObjects(objectType);
            if (index == -1) throw new ObjectionException("Layer with this object missing");

            return Objects[index].MaximumReadedIterator > indexer;
        }

        private bool _CanIndexerRender(IFloatyLayerIndex indexer)
        {
            foreach(var dependency in indexer.DependencyIndexer)
            {
                if (IsAlreadyReaded(dependency, indexer.LayerIndexer)) return false;
            }
            return true;
        }

        private AnimationRenderContext? _ReadExtensiveLayer(int updater, int readingLayer, bool ignoreMinorsObjects = true)
        {
            var readUpdating = updater - 1;
            if (readingLayer < Objects.Count)
                if (readUpdating < Objects[readingLayer].Count &&
                    Objects[readingLayer][readUpdating] != null &&
                    Objects[readingLayer][readUpdating].Sprite != null)
                {
                    var context = new AnimationRenderContext(Objects[readingLayer][readUpdating]);
                    if (!context.IsMinorObject || ignoreMinorsObjects)
                    {
                        context.EndRenderFrameDuration = Duration;
                        Objects[readingLayer].MaximumReadedIterator = readUpdating;
                        if (context.IsDependentOnLayerIndex)
                        {
                            if (Updater >= context.StartLayerIndex)
                            {
                                if (_CanIndexerRender(context.LayerIndexer))
                                {
                                    return context;
                                }
                            }
                        }
                        else
                        {
                            return context;
                        }
                    }
                }
            if ((readUpdating - 1) >= 0) return _ReadExtensiveLayer(readUpdating, readingLayer, false);
            return null;
        }

        public IEnumerable<AnimationRenderContext> MapLayerSprites()
        {
            var parralelReading = ReadingLayer;
            var globalDuration = new TimeSpan();
            while (parralelReading < Objects.Count)
            {
                var context = _ReadExtensiveLayer(Updater, parralelReading);
                if (context != null)
                {
                    globalDuration = globalDuration.Add(context.Value.Sprite.Duration);
                    yield return context.Value;
                }
                parralelReading++;
            }
        }

        public IEnumerable<AnimationRenderContext> MapLayerAudioSources()
        {
            var parralelReading = ReadingLayer;
            var globalDuration = new TimeSpan();
            while (parralelReading < Objects.Count)
            {
                if (Updater <= Objects[parralelReading].Count)
                {
                    if (Objects[parralelReading][Updater - 1] != null)
                    {
                        if (Objects[parralelReading][Updater - 1].AudioSource != null && Objects[parralelReading][Updater - 1].Sprite == null)
                        {
                            var context = new AnimationRenderContext(Objects[parralelReading][Updater - 1]);
                            context.EndRenderFrameDuration = Duration;
                            if (globalDuration.Ticks > 0) context.CurrentRenderFrameDuration.Add(globalDuration);
                            yield return context;
                        }
                        if (Objects[parralelReading][Updater - 1].Sprite != null) globalDuration.Add(Objects[parralelReading][Updater - 1].Sprite.Duration);
                    }
                }
                parralelReading++;
            }
        }

        public IEnumerator<IAnimationObject> GetEnumerator() => Objects.AsAnimationObjectEnumerable().GetEnumerator();

        public IEnumerable<IAudioSample> GetAudio() => Objects.AsAnimationObjectEnumerable().Where(x => x is IAudioSample)
            .Select(x => x.AudioSource as IAudioSample);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Dispose()
        {
            for (var i = 0; i < Objects.Count; i++)
            {
                foreach (IAnimationObject animateObject in Objects[i].Where(x=> x != null))
                {
                    if (animateObject is IObjectionObject obj) obj.Dispose();
                    else animateObject.Sprite.Dispose();
                }
            }
        }

        public bool UpdateHierarchy()
        {
            if (!IsCanReadParallelObjects()) return false;
            if (!Objects.Any(x => Updater < x.Value.Count)) return false;

            Updater++;
            return true;
        }

        public bool IsCanReadParallelObjects() => ReadingLayer < Objects.Count;
    }
}
