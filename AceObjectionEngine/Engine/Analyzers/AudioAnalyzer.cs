using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Model.Analyzers;
using AceObjectionEngine.Exceptions;

namespace AceObjectionEngine.Engine.Analyzers
{
    public abstract class AudioAnalyzer : IAudioAnalyzer
    {
        protected static Lazy<IAudioAnalyzer> DefaultAnalyzer = new Lazy<IAudioAnalyzer>(() => new FFMpegAudioAnalyzer());

        /// <summary>
        /// Sets the default audio analyzer
        /// </summary>
        /// <remarks>The function needs to return a new <see cref="IAudioAnalyzer"/> object</remarks>
        /// <param name="onCreateAnalyzer">Analyzer creation function</param>
        public static void SetAnalyzer(Func<IAudioAnalyzer> onCreateAnalyzer) => DefaultAnalyzer = new Lazy<IAudioAnalyzer>(onCreateAnalyzer);

        /// <summary>
        /// Gets the installed audio analyzer
        /// </summary>
        public static IAudioAnalyzer Analyzer => DefaultAnalyzer.Value;

        public AudioAnalysisResult Analyze(string filePath)
        {
            if (TryAnalyze(() => AnalyzeInner(filePath), out AudioAnalysisResult result)) return result;

            throw new ObjectionVisorObjectException<IAudioSource>(filePath);
        }

        public async Task<AudioAnalysisResult> AnalyzeAsync(string filePath, CancellationToken token = default)
        {
            var tryingResult = await TryAnalyzeAsync(async () => await AnalyzeInnerAsync(filePath));

            if (tryingResult.IsSuccesfull) return tryingResult.result;

            throw new ObjectionVisorObjectException<IAudioSource>(filePath);
        }

        public AudioAnalysisResult Analyze(Stream stream)
        {
            if (TryAnalyze(() => AnalyzeInner(stream), out AudioAnalysisResult result)) return result;

            throw new ObjectionVisorObjectException<IAudioSource>();
        }

        public async Task<AudioAnalysisResult> AnalyzeAsync(Stream stream, CancellationToken token = default)
        {
            var tryingResult = await TryAnalyzeAsync(async () => await AnalyzeInnerAsync(stream, token));

            if (tryingResult.IsSuccesfull) return tryingResult.result;

            throw new ObjectionVisorObjectException<IAudioSource>();
        }

        /// <summary>
        /// Called when trying to analyze input audio from file
        /// </summary>
        /// <param name="filePath">Path to audio</param>
        /// <returns>Audio Analyze Result</returns>
        protected abstract AudioAnalysisResult AnalyzeInner(string filePath);
        protected virtual Task<AudioAnalysisResult> AnalyzeInnerAsync(string filePath, CancellationToken token = default) => 
            Task.Run(() => AnalyzeInner(filePath), token);
        /// <summary>
        /// Called when trying to analyze input audio from stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        protected abstract AudioAnalysisResult AnalyzeInner(Stream stream);
        protected virtual Task<AudioAnalysisResult> AnalyzeInnerAsync(Stream stream, CancellationToken token = default) =>
            Task.Run(() => AnalyzeInner(stream), token);

        /// <summary>
        /// Try Analyze input Audio without exception
        /// </summary>
        /// <param name="filePath">Path to audio</param>
        /// <param name="analyze">When true analyzed audio result or null</param>
        /// <returns>Result of an attempt to analyze audio</returns>
        public bool TryAnalyze(Func<AudioAnalysisResult> analysing, out AudioAnalysisResult analyze)
        {
            analyze = null;

            bool isAnalyzed = false;
            int attempts = 3;
            while (attempts >= 0)
            {
                try
                {
                    analyze = analysing();
                    isAnalyzed = true;
                    break;
                }
                catch
                {
                    attempts--;
                }
            }

            return isAnalyzed;
        }

        public async Task<(bool IsSuccesfull, AudioAnalysisResult result)> TryAnalyzeAsync(Func<Task<AudioAnalysisResult>> analysing)
        {
            AudioAnalysisResult analyze = null;

            bool isAnalyzed = false;
            int attempts = 3;
            while (attempts >= 0)
            {
                try
                {
                    analyze = await analysing();
                    isAnalyzed = true;
                    break;
                }
                catch
                {
                    attempts--;
                }
            }

            return (isAnalyzed, analyze);
        }
    }
}
