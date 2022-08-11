using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Infrastructure;

namespace AceObjectionEngine.Engine.Model.Analyzers
{
    public sealed class AudioAnalysisResult
    {
        public SingleEntry<TimeSpan> Duration { get; set; }
        public SingleEntry<long> BitRate { get; set; }
        public SingleEntry<string> Format { get; set; }
        public SingleEntry<string> Codec { get; set; }
    }
}
