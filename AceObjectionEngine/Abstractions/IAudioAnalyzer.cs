using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Model.Analyzers;
using AceObjectionEngine.Abstractions.Async;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Data analysis implementation interface
    /// </summary>
    public interface IAudioAnalyzer : IAudioAnalyzerAsync
    {
        /// <summary>
        /// Analyzes input audio
        /// </summary>
        /// <param name="filePath">Path to audio</param>
        /// <returns>Audio Analyze Data</returns>
        AudioAnalysisResult Analyze(string filePath);
    }
}
