using AceObjectionEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFMpegCore;
using FFMpegCore.Pipes;
using System.IO;
using AceObjectionEngine.Helpers;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Engine.AudioMixers
{
    public class FFMpegAudioMixer : AudioMixer
    {
        private StreamPipeSink _pipe;
        private StreamPipeSource _pipeSource;

        public FFMpegAudioMixer(TimeSpan timeLine) : base(timeLine)
        {
        }

        public FFMpegAudioMixer(Stream stream, TimeSpan timeLine) : base(stream, timeLine)
        {

        }

        protected override void Init()
        {
            _pipe = new StreamPipeSink(Stream);
            _pipeSource = new StreamPipeSource(Stream);
        }

        public override IAudioSource Create()
        {
            var tempFile = new TempFileStream("OBJECTION-MIXER-AUDIO-RENDERED.mp3");
            tempFile.Close();
            Save(tempFile.FilePath);
            return new AudioSource(tempFile);
        }

        public override void CreateTimeLine()
        {
            FFMpegHelper.FromSilenceInput((options) =>
            {
                options.ForceFormat("lavfi");
            }).OutputToPipe(_pipe, (options) =>
            {
                options.ForceFormat("mp3");
                options.WithDuration(Duration);
            }).ProcessSynchronously();
        }

        public override void Merge(ref IAudioSource audioSource)
        {
            FFMpegArguments.FromFileInput(audioSource.FilePath)
            .OutputToPipe(new StreamPipeSink(Stream), (options) =>
            {
                options.UsingShortest(false);
                options.ForceFormat("mp3");
            }).ProcessSynchronously();
        }

        public void Save(string file)
        {
            using (var fileStream = File.Create(file))
            {
                Stream.Seek(0, SeekOrigin.Begin);
                Stream.CopyTo(fileStream);
            }
        }

        public override void Mix(ref IAudioSource audioSource)
        {
            var tempForTimeStamp = new TempFileStream("OBJECTION-MIXER-MIXING-BRIDGE.mp3");
            tempForTimeStamp.Close();
            Save(tempForTimeStamp.FilePath);
            Stream.Position = 0;
            Stream.SetLength(0);

            audioSource = audioSource.SetDuration(Duration);
            FFMpegArguments.FromFileInput(audioSource.FilePath)
                .AddFileInput(tempForTimeStamp.Name)
            .OutputToPipe(new StreamPipeSink(Stream), (options) =>
            {
                options.WithCustomArgument("-filter_complex amerge=inputs=2");
                options.ForceFormat("mp3");
            })
            .ProcessSynchronously();

            tempForTimeStamp.Dispose();
        }

        public override object Clone()
        {
            var stream = new MemoryStream();
            Stream.CopyTo(stream);
            return new FFMpegAudioMixer(stream, Duration);
        }
    }
}
