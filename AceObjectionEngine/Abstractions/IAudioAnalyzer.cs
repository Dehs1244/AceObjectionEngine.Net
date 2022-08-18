using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Model.Analyzers;
using AceObjectionEngine.Abstractions.Async;
using System.IO;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Data analysis implementation interface
    /// </summary>
    public interface IAudioAnalyzer : IAudioAnalyzerAsync
    {
        /// <summary>
        /// Analyzes input audio from file
        /// </summary>
        /// <param name="filePath">Path to audio</param>
        /// <returns>Audio Analyze Data</returns>
        AudioAnalysisResult Analyze(string filePath);
        /// <summary>
        /// Analyzes input audio from stream
        /// </summary>
        /// <param name="stream">Audio Stream</param>
        /// <returns>Audio Analyze Data</returns>
        AudioAnalysisResult Analyze(Stream stream);
    }
}
