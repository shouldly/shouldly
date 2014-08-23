using System.Diagnostics;

namespace Shouldly
{
    internal class TestEnvironment
    {
        public bool DeterminedOriginatingFrame { get; set; }
        public string ShouldMethod { get; set; }
        public string FileName { get; set; }
        public int LineNumber { get; set; }
        public StackFrame OriginatingFrame  { get; set; }
    }
}