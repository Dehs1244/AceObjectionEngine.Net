using AceObjectionEngine.Engine.Model.Analyzers;
using FFMpegCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Analyzers
{
    public class FFMpegAudioAnalyzer : AudioAnalyzer
    {
        protected override AudioAnalysisResult AnalyzeInner(string filePath) =>
            _GetMediaAnalysis(FFProbe.Analyse(filePath));

        protected override async Task<AudioAnalysisResult> AnalyzeInnerAsync(string filePath, CancellationToken token)
            => _GetMediaAnalysis(await FFProbe.AnalyseAsync(filePath, cancellationToken: token));

        private AudioAnalysisResult _GetMediaAnalysis(IMediaAnalysis analysesData)
        {
            var result = new AudioAnalysisResult();
            result.BitRate = analysesData.PrimaryAudioStream.BitRate;
            result.Codec = analysesData.PrimaryAudioStream.CodecName;
            result.Format = analysesData.Format.FormatName;
            result.Duration = analysesData.Duration;

            return result;
        }
    }
}
