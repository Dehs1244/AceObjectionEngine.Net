using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AceObjectionEngine.Engine.Analyzers;
using AceObjectionEngine.Tests.TestObjects;

namespace AceObjectionEngine.Tests.Implementation
{
    public class AnalyzerTest
    {
        [Fact]
        public void SetAnalyzerTest()
        {
            AudioAnalyzer.SetAnalyzer(() => new TestAudioAnalyzer());
            Assert.IsType<TestAudioAnalyzer>(AudioAnalyzer.Analyzer);
        }

        [Fact]
        public void AnalyzeTest()
        {
            AudioAnalyzer.SetAnalyzer(() => new TestAudioAnalyzer());
            var result = AudioAnalyzer.Analyzer.Analyze(string.Empty);

            Assert.Equal(1000, result.BitRate);
        }
    }
}
