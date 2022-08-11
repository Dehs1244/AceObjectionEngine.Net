using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Analyzers;
using AceObjectionEngine.Engine.Model.Analyzers;

namespace AceObjectionEngine.Tests.TestObjects
{
    public sealed class TestAudioAnalyzer : AudioAnalyzer
    {
        protected override AudioAnalysisResult AnalyzeInner(string filePath)
        {
            var result = new AudioAnalysisResult();
            result.BitRate = 1000;
            result.Codec = "Test Codec";

            return result;
        }
    }
}
