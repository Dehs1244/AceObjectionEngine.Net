using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Model.Analyzers;

namespace AceObjectionEngine.Abstractions.Async
{
    /// <summary>
    /// Asynchronously data analysis implementation interface
    /// </summary>
    public interface IAudioAnalyzerAsync
    {
        /// <summary>
        ///  Asynchronously analyzes input audio from stream
        /// </summary>
        /// <param name="filePath">Path to audio</param>
        /// <returns>Task of Audio Analyze Data</returns>
        Task<AudioAnalysisResult> AnalyzeAsync(string filePath, CancellationToken token = default);
        /// <summary>
        ///  Asynchronously analyzes input audio from stream
        /// </summary>
        /// <param name="filePath">Audio Stream</param>
        /// <returns>Task of Audio Analyze Data</returns>
        Task<AudioAnalysisResult> AnalyzeAsync(Stream stream, CancellationToken token = default);
    }
}
