using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFMpegCore;
using AceObjectionEngine.Abstractions;
using FFMpegCore.Pipes;
using System.IO;
using System.Threading;
using AceObjectionEngine.Exceptions;
using FFMpegCore.Arguments;
using AceObjectionEngine.Helpers;
using AceObjectionEngine.Engine.Analyzers;
using AceObjectionEngine.Engine.Animator;

namespace AceObjectionEngine.Engine.Model
{
    public class AudioSource : IAudioSource, IAudioSample, ICloneable
    {
        private byte[] _samples { get; }
        public TimeSpan Duration { get; }
        public string FilePath { get; }

        public TimeSpan Offset { get; }

        public string Codec { get; }
        public string Format { get; }
        public long BitRate { get; }

        public Stream Stream => _fileStream;
        private FileStream _fileStream;
        private bool _disposed = false;

        public AudioSource(FileStream stream)
        {
            _fileStream = stream;
            FilePath = stream.Name;
            stream.Close();
            var audioAnalyzer = AudioAnalyzer.Analyzer.Analyze(FilePath);
            _samples = File.ReadAllBytes(FilePath);

            Duration = audioAnalyzer.Duration;
            Codec = audioAnalyzer.Codec;
            Format = audioAnalyzer.Format;
            BitRate = audioAnalyzer.BitRate;
        }

        public AudioSource(FileStream stream, TimeSpan offset) : this(stream)
        {
            Offset = offset;// TimeSpan.FromMilliseconds((offset.TotalSeconds * ObjectionAnimator.FrameRate) * Frame.CalculateDuration(1).TotalSeconds);
        }

        ~AudioSource()
        {
            Dispose(false);
        }

        public object Clone() => new AudioSource((FileStream)Stream, Offset);

        public static AudioSource FromFile(string path, TimeSpan offset) => new AudioSource(File.OpenRead(path), offset);
        public static AudioSource FromFile(string path) => new AudioSource(File.OpenRead(path));

        public void Serialize(Stream stream)
        {
            stream.Write(_samples, 0, _samples.Length);
        }

        public async Task SerializeAsync(Stream stream, CancellationToken token)
        {
            await stream.WriteAsync(_samples, 0, _samples.Length, token);
        }

        public void Save(string path) => File.WriteAllBytes(path, _samples);

        public IAudioSource Merge(IAudioSource another)
        {
            var tempFile = new TempFileStream("OBJECTION-MERGED-AUDIO.mp3");
            tempFile.Close();
            FFMpegArguments
                .FromConcatInput(EnumerableHelper.ToEnumerable<string>(FilePath, another.FilePath))
                .OutputToFile(tempFile.FilePath, true, options =>
                {
                    options.UsingShortest(false);
                })
                .ProcessSynchronously();
            Dispose();
            another.Dispose();
            return new AudioSource(tempFile);
        }

        public IAudioSource Series(int loopTime)
        {
            var tempFile = new TempFileStream("OBJECTION-SERIES-AUDIO.mp3");
            tempFile.Close();
            FFMpegArguments
                .FromConcatInput(Enumerable.Repeat(FilePath, loopTime))
                .OutputToFile(tempFile.FilePath, true)
                .ProcessSynchronously();

            Dispose();

            return new AudioSource(tempFile);
        }

        public IAudioSource SetDuration(TimeSpan duration)
        {
            if (duration == Duration) return this;

            var tempFile = new TempFileStream("OBJECTION-DURATION-CHANGED-AUDIO.mp3");
            tempFile.Close();
            if (duration > Duration)
            {
                FFMpegArguments
                .FromFileInput(FilePath)
                .OutputToFile(tempFile.FilePath, true, options =>
                {
                    options.WithCustomArgument($"-af apad=pad_dur={(int)((duration - Duration).TotalMilliseconds)}ms");
                })
                .ProcessSynchronously();
            }
            else
            {
                FFMpegArguments
                .FromFileInput(FilePath)
                .OutputToFile(tempFile.FilePath, true, options =>
                {
                    options.WithDuration(duration);
                })
                .ProcessSynchronously();
            }
            //Dispose();

            return new AudioSource(tempFile);
        }

        public IAudioSource Mix(IEnumerable<IAudioSource> audioSources)
        {
            throw new NotImplementedException();
            //   if (!audioSources.Any()) return this;

            //   var tempFile = new TempFileStream("OBJECTION-MIXED-AUDIO.mp3");
            //   tempFile.Close();
            //   var ffmpegFactory = FFMpegArguments
            //       .FromFileInput(FilePath);
            //   StringBuilder mixerArgumentBuilder = new StringBuilder();
            //   string[] alphabet = Enumerable.Range('b', 25).Select(x=> x.ToString().ToLower()).Select(x => x).ToArray(); 
            //   for(var i = 0; i < audioSources.Count(); i++)
            //   {
            //       var audio = audioSources.ElementAt(i);
            //       ffmpegFactory.AddFileInput(audio.FilePath);
            //       mixerArgumentBuilder.AppendLine($"[{i}]adelay={audio.Offset.TotalMilliseconds}|{audio.Offset.TotalMilliseconds}[{alphabet[i]}];");
            //   }

            //   ffmpegFactory.OutputToFile(tempFile.FilePath, true, options =>
            //   {
            //       options.WithCustomArgument(@"-filter_complex
            //""[1]adelay=184000|184000[b];
            // [2]adelay=360000|360000[c];
            //   [3]adelay=962000|962000[d];
            //   [0][b][c][d]amix=4""");
            //   });
            //   Dispose();

            //   foreach(var audio in audioSources)
            //   {
            //       audio.Dispose();
            //   }
            //   return new AudioSource(tempFile, 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (_fileStream is TempFileStream tempFileStream) tempFileStream.Dispose();
            else _fileStream.Dispose();
            Stream.Close();
            _disposed = true;
        }
    }
}